using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable, IHealable
{
    [Header("Максимальное количество жизней")] [SerializeField]
    private int maxLives;

    [Header("Количество жизней на старте")] [SerializeField]
    private int _livesOnStart;

    [Header("Продолжительность неуязвимости после получения урона")] [SerializeField]
    private float _invulnerabilityDuration;


    [Header("FOR TEST")] [SerializeField] private int currentLives;

    private IHealthDisplayer healthDisplayer;

    private bool _canTakeDamage = true;

    public bool IsOutOfLifes => currentLives <= 0;

    public Action onTakeDamage;
    public Action onOutOfLifes;

    void Start()
    {
        currentLives = _livesOnStart;
        if (!TryGetComponent(out healthDisplayer))
        {
            healthDisplayer = GetComponentInChildren<IHealthDisplayer>();
        }

        ChangeHealthDisplayer();
    }

    public void TakeDamage(int damage)
    {
        if (_canTakeDamage)
        {
            _canTakeDamage = false;
            currentLives -= damage;
            ChangeHealthDisplayer();
            if (currentLives <= 0)
            {
                Die();
                return;
            }

            StartCoroutine(RestoreCanTakeDamage());

            onTakeDamage?.Invoke();
        }
    }

    private IEnumerator RestoreCanTakeDamage()
    {
        yield return new WaitForSeconds(_invulnerabilityDuration);
        _canTakeDamage = true;
    }

    private void Die()
    {
        if (TryGetComponent<ICanDie>(out ICanDie canDieObject))
        {
            canDieObject.Die();
            return;
        }

        Debug.Log($"GameObject {gameObject.name} is died. Didn't you see?");
        onOutOfLifes?.Invoke();
    }

    public bool RestoreHealth(int healthAmountToRestore)
    {
        if (currentLives >= maxLives)
        {
            return false;
        }

        currentLives += healthAmountToRestore;
        
        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }
        
        ChangeHealthDisplayer();
        return true;
    }

    private void ChangeHealthDisplayer()
    {
        if (healthDisplayer != null) healthDisplayer.ShowActualHealth(currentLives, maxLives);
    }
}
using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable, IHealable
{
    [Header("Максимальное количество жизней")]
    [SerializeField] private int maxLives;

    [SerializeField] private int currentLives; // TODO: hide from inspector after tests

    private IHealthDisplayer healthDisplayer;

    public bool IsOutOfLifes => currentLives <= 0;

    public Action onTakeDamage;
    public Action onOutOfLifes;

    void Start()
    {
        currentLives = maxLives;
        if (!TryGetComponent(out healthDisplayer))
        {
            healthDisplayer = GetComponentInChildren<IHealthDisplayer>();
        }
        ChangeHealthDisplayer();
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        ChangeHealthDisplayer();
        if (currentLives <= 0)
        {
            Die();
            return;
        }
        onTakeDamage?.Invoke();
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
        if (currentLives + healthAmountToRestore > maxLives)
        {
            currentLives = maxLives;
            ChangeHealthDisplayer();
            return false;
        }
        else
        {
            currentLives += healthAmountToRestore;
            ChangeHealthDisplayer();
            return true;
        }
    }

    private void ChangeHealthDisplayer()
    {
        if (healthDisplayer != null) healthDisplayer.ShowActualHealth(currentLives, maxLives);
    }
}

using UnityEngine;

public class Health : MonoBehaviour, IDamagable, IHealable
{
    [SerializeField] private int maxLives;

    [SerializeField] private int currentLives; // TODO: hide from inspector after tests

    private IHealthDisplayer healthDisplayer;

    public bool IsOutOfLifes => currentLives <= 0;

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
        if (currentLives <= 0) Die();
    }

    private void Die()
    {
        if (TryGetComponent<ICanDie>(out ICanDie canDieObject))
        {
            canDieObject.Die();
            return;
        }
        Debug.Log($"GameObject {gameObject.name} doesn't have specified Die behaviour and just deactivated");
        //gameObject.SetActive(false);
    }

    public bool RestoreHealth(int healthAmountToRestore)
    {
        if (currentLives + healthAmountToRestore  > maxLives)
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

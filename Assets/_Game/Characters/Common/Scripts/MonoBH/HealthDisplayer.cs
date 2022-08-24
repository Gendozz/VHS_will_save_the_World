using UnityEngine;
using TMPro;

public class HealthDisplayer : MonoBehaviour, IHealthDisplayer
{
    [SerializeField] private TMP_Text _health;

    public void ShowActualHealth(int currentHealth, int maxHealth)
    {
        _health.text = currentHealth.ToString();
    }
}

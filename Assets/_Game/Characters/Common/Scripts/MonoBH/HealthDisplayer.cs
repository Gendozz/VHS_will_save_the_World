using UnityEngine;
using TMPro;

public class HealthDisplayer : MonoBehaviour, IHealthDisplayer
{
    [SerializeField] private TMP_Text _health;

    [SerializeField] private string _textBeforeNumbs;

    public void ShowActualHealth(int currentHealth, int maxHealth)
    {
        // TODO: delete if/else after all levels are ready
        if (_health != null)
        {
            _health.text = _textBeforeNumbs + " " + currentHealth.ToString();
        }
        else
        {
            Debug.Log("Current health " + currentHealth.ToString());
        }
    }
}

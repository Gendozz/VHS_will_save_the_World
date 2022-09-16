using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour, IHealthDisplayer
{
   // [SerializeField] private TMP_Text _health;

    //[SerializeField] private string _textBeforeNumbs;

    [SerializeField] private Image _healthBar;

    public void ShowActualHealth(int currentHealth, int maxHealth)
    {
        _healthBar.fillAmount = (float) currentHealth / (float) maxHealth;



        //// TODO: delete if/else after all levels are ready
        //if (_health != null)
        //{
        //    _health.text = _textBeforeNumbs + " " + currentHealth.ToString();
        //}
        //else
        //{
        //    Debug.Log("Current health " + currentHealth.ToString());
        //}
    }
}

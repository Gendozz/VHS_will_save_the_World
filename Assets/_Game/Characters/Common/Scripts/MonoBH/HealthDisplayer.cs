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
        if (_healthBar != null)
        {
            _healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        }
        else
        {
            //Debug.Log($"{gameObject.name} got damage. It's current health is {currentHealth}");
        }



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

using System.Collections;
using UnityEngine;

public class WalkOnSpikes : MonoBehaviour
{
    [Header("Все шипы, которые должны стать землёй")]
    [SerializeField] private SpikesGroundChanger[] _spikesGroundChangers;

    [SerializeField] private float _abilytyDuration;

    [SerializeField] private Health _playerHealth;

    private void OnEnable()
    {
        _playerHealth.onTakeDamage += TurnAbilityOff;
        SwitchAbilty(true);
        StartCoroutine(TurnOffAbilityInSeconds(_abilytyDuration));
        Debug.Log("Ability is enabled");
    }

    private void OnDisable()
    {
        _playerHealth.onTakeDamage -= TurnAbilityOff;
    }

    private void TurnAbilityOff()
    {
        StopAllCoroutines();
        SwitchAbilty(false);
        this.enabled = false;
    }

    private void SwitchAbilty(bool isAbilityOn)
    {
        foreach (var spike in _spikesGroundChangers)
        {
            spike.ChangeSpikesWithGroundOrBackwards(isAbilityOn);
        }
    }

    private IEnumerator TurnOffAbilityInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SwitchAbilty(false);
        this.enabled = false;
    }
}

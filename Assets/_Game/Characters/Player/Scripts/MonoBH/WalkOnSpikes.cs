using System.Collections;
using UnityEngine;

public class WalkOnSpikes : MonoBehaviour
{
    [Header("Все шипы, которые должны стать землёй")]
    [SerializeField] private SpikesGroundChanger[] _spikesGroundChangers;

    [SerializeField] private float _abilytyDuration;

    private void OnEnable()
    {
        SwitchAbilty(true);
        StartCoroutine(TurnOffAbilityInSeconds(_abilytyDuration));
        Debug.Log("Ability is enabled");
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

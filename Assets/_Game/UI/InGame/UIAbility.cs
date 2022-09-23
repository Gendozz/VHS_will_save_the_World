using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAbility : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private GameObject[] _objects;

    private Coroutine _stolenAbilities;
    private Coroutine _walkingSpikes;

    public void TimerDisplayBreakingDoors(float time)
    {
        _objects[1].SetActive(true);
        _images[2].sprite = _sprites[1];
        _images[3].fillAmount = 1;
        if (_stolenAbilities != null)
        {
            StopCoroutine(_stolenAbilities);
            _stolenAbilities = StartCoroutine(ITimerDisplayStolenAbilities(1 / time, 3, 1));
        }
        else
        {
            _stolenAbilities = StartCoroutine(ITimerDisplayStolenAbilities(1 / time, 3, 1));
        }
    }

    public void TimerDisplayDoubleJump(float time)
    {
        _objects[1].SetActive(true);
        _images[2].sprite = _sprites[0];
        _images[3].fillAmount = 1;
        if (_stolenAbilities != null)
        {
            StopCoroutine(_stolenAbilities);
            _stolenAbilities = StartCoroutine(ITimerDisplayStolenAbilities(1 / time, 3, 1));
        }
        else
        {
            _stolenAbilities = StartCoroutine(ITimerDisplayStolenAbilities(1 / time, 3, 1));
        }
    }

    public void TimerDisplayWalkingOnSpikes(float time)
    {
        _objects[0].SetActive(true);
        _images[1].fillAmount = 1;
        if (_walkingSpikes != null)
        {
            StopCoroutine(_walkingSpikes);
            _walkingSpikes = StartCoroutine(ITimerDisplayStolenAbilities(1 / time, 1, 0));
        }
        else
        {
            _walkingSpikes = StartCoroutine(ITimerDisplayStolenAbilities(1 / time, 1, 0));
        }
    }

    private IEnumerator ITimerDisplayStolenAbilities(float speed, int indexImage, int indexObj)
    {
        while(_images[indexImage].fillAmount > 0 && _images[indexImage].fillAmount > speed * Time.deltaTime)
        {
            yield return _images[indexImage].fillAmount -= speed * Time.deltaTime;
        }

        _objects[indexObj].SetActive(false);
    }
}
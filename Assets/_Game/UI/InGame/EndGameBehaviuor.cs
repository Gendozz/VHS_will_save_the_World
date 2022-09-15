using JSAM;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameBehaviuor : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalResultsText;

    [SerializeField] private Music[] _resultMusics;

    [SerializeField] private GameObject _thanksText;

    private float _delayBeforShowThanks;

    private int _totalTapes;

    void Start()
    {
        _thanksText.SetActive(false);
        _totalTapes = PlayerPrefs.GetInt(StringConsts.TOTAL_TAPES_AMOUNT);
        _delayBeforShowThanks = _totalTapes * 10;
        ShowTotalResult();
        PlayResultMusic();
    }

    private void ShowTotalResult()
    {
        if (PlayerPrefs.HasKey(StringConsts.TOTAL_TAPES_AMOUNT))
        {
            _totalResultsText.text = "Всего плёнок собрано " + _totalTapes + " из 12";
        }
    }
    
    private void PlayResultMusic()
    {
        AudioManager.PlayMusic(_resultMusics[_totalTapes - 1]);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator ShowThanks()
    {
        yield return new WaitForSeconds(_delayBeforShowThanks);
        _thanksText.SetActive(true);
    }

}

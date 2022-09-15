using JSAM;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameBehaviuor : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalResultsText;

    [SerializeField] private Music[] _resultMusics;

    private int _totalTapes;

    void Start()
    {
        _totalTapes = PlayerPrefs.GetInt(StringConsts.TOTAL_TAPES_AMOUNT);
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
}

using JSAM;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameBehaviuor : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalResultsText;
    
    [SerializeField] private GameObject _thanksTextObject;

    [SerializeField] private TMP_Text _thanksText;

    [SerializeField] private Image _trackPlayingVisualizationFilling;

    private Music[] _resultMusics = new Music[12];
    
    private float _currentTrackDuration;

    private int _totalTapes;

    private AudioSource _finalClipSource;

    private float _fullTrackDuration = 119f;

    private WaitForSeconds _oneSecondDelay = new(1);

    private void Start()
    {
        _thanksTextObject.SetActive(false);
        _trackPlayingVisualizationFilling.fillAmount = 0;

        FillResultMusicList();

        _totalTapes = GameFlowController.TotalTapesAmount;
        SetThanksText();
        ShowTotalResult();
        PlayResultMusic();

        if (_finalClipSource != null)
        {
            _currentTrackDuration = _finalClipSource.clip.length;
            //Debug.Log($"Current track duration is {_currentTrackDuration}");

        }
        else
        {
            _currentTrackDuration = 0;
            Debug.Log("Delay before show thanks hardcodded to 0");
        }

        StartCoroutine(ShowThanks());
        StartCoroutine(FillTrackPlayingVisualization());
    }

    private void SetThanksText()
    {
        //Debug.Log("_totalTapes" + _totalTapes);
        if (_totalTapes == 0)
        {
            _thanksText.text = "Ни одна плёнка не собрана(((";
            return;
        }
        
        if (_totalTapes < 12)
        {
            _thanksText.text = "Собери все плёнки для полного трека";
            return;
        }
        
        _thanksText.text = "Спасибо! Музыка спасена!";
        
        //Debug.Log("Thanks text set to " + _thanksText.text + ", _totalTapes is " + _totalTapes);
        
    } 

    private void FillResultMusicList()
    {
        _resultMusics[0] = Music.FinalTrack_01;
        _resultMusics[1] = Music.FinalTrack_02;
        _resultMusics[2] = Music.FinalTrack_03;
        _resultMusics[3] = Music.FinalTrack_04;
        _resultMusics[4] = Music.FinalTrack_05;
        _resultMusics[5] = Music.FinalTrack_06;
        _resultMusics[6] = Music.FinalTrack_07;
        _resultMusics[7] = Music.FinalTrack_08;
        _resultMusics[8] = Music.FinalTrack_09;
        _resultMusics[9] = Music.FinalTrack_10;
        _resultMusics[10] = Music.FinalTrack_11;
        _resultMusics[11] = Music.FinalTrack_12;
    }

    private void ShowTotalResult()
    {
        _totalResultsText.text = "Всего плёнок собрано " + _totalTapes + " из 12";
    }

    private void PlayResultMusic()
    {
        if (_totalTapes > 0)
        {
            _finalClipSource = AudioManager.PlayMusic(_resultMusics[_totalTapes - 1]);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    private IEnumerator ShowThanks()
    {
        yield return new WaitForSeconds(_currentTrackDuration);
        _thanksTextObject.SetActive(true);
    }

    private IEnumerator FillTrackPlayingVisualization()
    {
        float timeElapsed = 0;
        while (timeElapsed < _currentTrackDuration)
        {
            timeElapsed += Time.deltaTime + 1;
            yield return _oneSecondDelay;
            _trackPlayingVisualizationFilling.fillAmount = timeElapsed / _fullTrackDuration;
            //Debug.Log($"timeElapsed = {timeElapsed}. _trackPlayingVisualizationFilling.fillAmount is {_trackPlayingVisualizationFilling.fillAmount}");
        }
    }
}
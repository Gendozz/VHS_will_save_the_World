using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _settingsCanvasGroup;
    [SerializeField] private CanvasGroup _authorsCanvasGroup;
    [SerializeField] private GameObject _continueButton;

    private void Awake()
    {
        _settingsCanvasGroup.gameObject.SetActive(true);
        _authorsCanvasGroup.gameObject.SetActive(true);
        SetUpCanvasGroup(_settingsCanvasGroup, 0, false, false);
        SetUpCanvasGroup(_authorsCanvasGroup, 0, false, false);

        if (PlayerPrefs.HasKey(StringConsts.LEVELS_COMPLETE))
        {
            if(PlayerPrefs.GetInt(StringConsts.LEVELS_COMPLETE) > 0)
            {
                _continueButton.SetActive(true);
            }
        }
    }

    public void SetUpCanvasGroup(CanvasGroup canvasGroup, float alpha, bool blocksRaycasts, bool interactable)
    {
        canvasGroup.alpha = alpha;
        canvasGroup.blocksRaycasts = blocksRaycasts;
        canvasGroup.interactable = interactable;
    }

    public void ShowSettingsCanvas()
    {
        SetUpCanvasGroup(_settingsCanvasGroup, 1, true, true);
    }

    public void CloseSettingsCanvas()
    {
        SetUpCanvasGroup(_settingsCanvasGroup, 0, false, false);
    }

    public void ShowAuthorsCanvas()
    {
        SetUpCanvasGroup(_authorsCanvasGroup, 1, true, true);
    }

    public void CloseAuthorsCanvas()
    {
        SetUpCanvasGroup(_authorsCanvasGroup, 0, false, false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartNextUnfinishedLevel()
    {
        Debug.Log("PlayerPrefs.GetInt(StringConsts.LEVELS_COMPLETE) =" + PlayerPrefs.GetInt(StringConsts.LEVELS_COMPLETE));
        int tempInt = PlayerPrefs.GetInt(StringConsts.LEVELS_COMPLETE + 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt(StringConsts.LEVELS_COMPLETE) + 1);
    }

    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

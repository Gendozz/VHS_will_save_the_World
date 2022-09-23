using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _settingsCanvasGroup;
    [SerializeField] private CanvasGroup _aboutCanvasGroup;
    [SerializeField] private CanvasGroup _controllsCanvasGroup;
    [SerializeField] private CanvasGroup _materialsCanvasGroup;
    [SerializeField] private Button _continueButton;
    [SerializeField] private TMP_Text _continueButtonText;

    private void OnEnable()
    {
        GameFlowController.onProgessLoaded += EnableContinueButton;
        Debug.Log("UIMainMenu subscribed on GameFlowController.onProgessLoaded");
    }
    private void OnDisable()
    {
        GameFlowController.onProgessLoaded -= EnableContinueButton;
    }

    private void Start()
    {
        _settingsCanvasGroup.gameObject.SetActive(true);
        _aboutCanvasGroup.gameObject.SetActive(true);
        _controllsCanvasGroup.gameObject.SetActive(true);
        _materialsCanvasGroup.gameObject.SetActive(true);
        SetUpCanvasGroup(_settingsCanvasGroup, 0, false, false);
        SetUpCanvasGroup(_aboutCanvasGroup, 0, false, false);
        SetUpCanvasGroup(_controllsCanvasGroup, 0, false, false);
        SetUpCanvasGroup(_materialsCanvasGroup, 0, false, false);
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

    public void ShowAboutCanvas()
    {
        SetUpCanvasGroup(_aboutCanvasGroup, 1, true, true);
    }

    public void CloseAboutCanvas()
    {
        SetUpCanvasGroup(_aboutCanvasGroup, 0, false, false);
    }    
    
    public void ShowMaterialsCanvas()
    {
        SetUpCanvasGroup(_materialsCanvasGroup, 1, true, true);
    }

    public void CloseMaterialsCanvas()
    {
        SetUpCanvasGroup(_materialsCanvasGroup, 0, false, false);
    }
    
    public void ShowControllsCanvas()
    {
        SetUpCanvasGroup(_controllsCanvasGroup, 1, true, true);
    }

    public void CloseControllsCanvas()
    {
        SetUpCanvasGroup(_controllsCanvasGroup, 0, false, false);
    }


    public void EnableContinueButton()
    {
        if(GameFlowController.LevelsComplete > 1)
        {
            _continueButton.interactable = true;

            Color currentColor = _continueButton.image.color;

            _continueButton.image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
            _continueButtonText.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartNextUnfinishedLevel()
    {
        SceneManager.LoadScene(GameFlowController.LevelsComplete);
    }

    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

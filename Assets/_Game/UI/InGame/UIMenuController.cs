using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup failCanvasGroup;
    [SerializeField] private CanvasGroup winCanvasGroup;
    [SerializeField] private CanvasGroup pauseCanvasGroup;

    private void Awake()
    {
        winCanvasGroup.gameObject.SetActive(true);
        failCanvasGroup.gameObject.SetActive(true);
        pauseCanvasGroup.gameObject.SetActive(true);
        SetUpCanvasGroup(winCanvasGroup, 0, false, false);
        SetUpCanvasGroup(failCanvasGroup, 0, false, false);
        SetUpCanvasGroup(pauseCanvasGroup, 0, false, false);
    }

    private void Update()
    {
        if (Input.GetButtonDown(StringConsts.ESC))
        {
            ShowPauseCanvas(true);
        }
    }

    public void ShowFailCanvas()
    {
        SetUpCanvasGroup(failCanvasGroup, 1, true, true);
    }

    public void ShowWinCanvas()
    {
        SetUpCanvasGroup(winCanvasGroup, 1, true, true);
    }

    private void ShowPauseCanvas(bool toShow)
    {
        float canvasAlpha = toShow ? 1 : 0;
        SetUpCanvasGroup(pauseCanvasGroup, canvasAlpha, toShow, toShow);
    }

    public void SetUpCanvasGroup(CanvasGroup canvasGroup, float alpha, bool blocksRaycasts, bool interactable)
    {
        canvasGroup.alpha = alpha;
        canvasGroup.blocksRaycasts = blocksRaycasts;
        canvasGroup.interactable = interactable;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // TODO: What if the last level
    }
}

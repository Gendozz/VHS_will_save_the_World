using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionHandeler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Toggle _resolutionToggle;
    private Resolution[] _systemResolutions;
    private Resolution _currentResolution;

    // 1 for fullscreen, 0 for windowed
    private int _isFullScreen;

    private void Start()
    {
        _systemResolutions = Screen.resolutions;

        List<string> _systemResolutionsStrings = new List<string>();
        foreach (var res in _systemResolutions)
        {
            _systemResolutionsStrings.Add(res.ToString());
            // Print the resolutions
            //Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
        }

        _resolutionDropdown.AddOptions(_systemResolutionsStrings);

        LoadResolution();
    }

    private void LoadResolution()
    {
        if (PlayerPrefs.HasKey(StringConsts.RESOLUTION_WIDTH))
        {
            _currentResolution.width = PlayerPrefs.GetInt(StringConsts.RESOLUTION_WIDTH);
            _currentResolution.height = PlayerPrefs.GetInt(StringConsts.RESOLUTION_HEIGHT);
            _isFullScreen = PlayerPrefs.GetInt(StringConsts.RESOLUTION_FULLSCREEN);
            _resolutionDropdown.value = PlayerPrefs.GetInt(StringConsts.RESOLUTION_DROPDOWN_INDEX);
            _resolutionToggle.isOn = _isFullScreen == 1 ? true : false;
        }
        else
        {
            _currentResolution.width = _systemResolutions[_systemResolutions.Length - 1].width;
            _currentResolution.height = _systemResolutions[_systemResolutions.Length - 1].height;
            _resolutionDropdown.value = _systemResolutions.Length - 1;
            _resolutionToggle.isOn = true;
            _isFullScreen = 1;
        }

        //Debug.Log($" Loaded _currentResolution.width is {_currentResolution.width}, _currentResolution.height is {_currentResolution.height}");


        SetResolution(_currentResolution.width, _currentResolution.height, _isFullScreen == 1 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
    }

    private void SetResolution(int width, int height, FullScreenMode fullScreenMode)
    {
        Screen.SetResolution(width, height, fullScreenMode);

        PlayerPrefs.SetInt(StringConsts.RESOLUTION_WIDTH, _currentResolution.width);
        PlayerPrefs.SetInt(StringConsts.RESOLUTION_HEIGHT, _currentResolution.height);
        PlayerPrefs.SetInt(StringConsts.RESOLUTION_FULLSCREEN, fullScreenMode == FullScreenMode.FullScreenWindow ? 1 : 3);
        PlayerPrefs.SetInt(StringConsts.RESOLUTION_DROPDOWN_INDEX, _resolutionDropdown.value);

        //Debug.Log($"Resolution changed to {_resolutionDropdown.value}, Screen.currentResolution is {Screen.currentResolution}");
    }

    public void ChangeResolution()
    {
        _currentResolution = _systemResolutions[_resolutionDropdown.value];
        SetResolution(_currentResolution.width, _currentResolution.height, _isFullScreen == 1 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        PlayerPrefs.SetInt(StringConsts.RESOLUTION_FULLSCREEN, isFullScreen ? 1 : 3);

        //Debug.Log($"Selected windowed type is {(isFullScreen ? 1 : 3)}");
    }
}

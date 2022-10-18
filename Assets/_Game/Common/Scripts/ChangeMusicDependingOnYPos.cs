using System.Collections;
using JSAM;
using UnityEngine;

public class ChangeMusicDependingOnYPos : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    [SerializeField] private float _yPositionOfMusicChanging;
    
    [SerializeField] private float _checkYPositionPeriod;

    [SerializeField] private Music _upLevelMusic;

    [SerializeField] private Music _downLevelMusic;
    
    private WaitForSeconds _checkYPositionPeriodDelay;
    
    private void Start()
    {
        _checkYPositionPeriodDelay = new WaitForSeconds(_checkYPositionPeriod);
        StartCoroutine(PlayTrackDependingOnPosition());
    }

    private IEnumerator PlayTrackDependingOnPosition()
    {
        while (true)
        {
            float yPosition = _targetTransform.position.y;

            if (yPosition > _yPositionOfMusicChanging)
            {
                if (AudioManager.IsMusicPlaying(_downLevelMusic))
                {
                    AudioManager.StopMusic(_downLevelMusic);
                }

                if (!AudioManager.IsMusicPlaying(_upLevelMusic))
                {
                    AudioManager.PlayMusic(_upLevelMusic); 
                    //Debug.Log("up music started");
                }
            }
            else
            {
                if (AudioManager.IsMusicPlaying(_upLevelMusic))
                {
                    AudioManager.StopMusic(_upLevelMusic);
                }

                if (!AudioManager.IsMusicPlaying(_downLevelMusic))
                {
                    AudioManager.PlayMusic(_downLevelMusic);
                    //Debug.Log("down music started");
                }

            }        
            yield return _checkYPositionPeriodDelay;
        }

    }
}

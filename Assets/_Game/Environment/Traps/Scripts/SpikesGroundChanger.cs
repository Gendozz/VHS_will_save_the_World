using UnityEngine;

public class SpikesGroundChanger : MonoBehaviour
{
    [Header("Часть с шипами")]
    [SerializeField] private GameObject _spikes;

    [Header("Часть с землёй")]
    [SerializeField] private GameObject _ground;

    private bool _isLocalGroundTime;

    private void Start()
    {
        _spikes.SetActive(true);
        _ground.SetActive(false);
    }

    public void ChangeSpikesWithGroundOrBackwards(bool isGroundTime)
    {
        _spikes.SetActive(!isGroundTime);
        _ground.SetActive(isGroundTime);
    }
}

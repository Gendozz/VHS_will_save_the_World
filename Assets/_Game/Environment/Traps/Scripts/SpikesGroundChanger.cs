using UnityEngine;

public class SpikesGroundChanger : MonoBehaviour
{
    [Header("„асть с шипами")]
    [SerializeField] private GameObject _spikes;

    [Header("„асть с землЄй")]
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

using UnityEngine;

public class SelfDestoyedPartSys : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private float _duration;

    private void Start()
    {
        _duration = _particleSystem.startLifetime;
        _particleSystem.Play();
        Destroy(this.gameObject, _duration);
    }
}

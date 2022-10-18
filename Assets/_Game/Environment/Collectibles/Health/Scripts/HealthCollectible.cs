using JSAM;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int _healthAmount;

    [SerializeField] private GameObject _particleSystemOnCollectPrefab;
    
    private bool _wasTaken;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IHealable>(out IHealable healable))
        {
            _wasTaken = healable.RestoreHealth(_healthAmount);
            if (_wasTaken)
            {
                AudioManager.PlaySound(Sounds.HealthCollectible);
                Instantiate(_particleSystemOnCollectPrefab, transform.position, _particleSystemOnCollectPrefab.transform.rotation);
            }

            gameObject.SetActive(!_wasTaken);
        }

    }
}

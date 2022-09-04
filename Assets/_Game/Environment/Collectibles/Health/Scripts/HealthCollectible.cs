using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int _healthAmount;

    private bool _wasTaken;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IHealable>(out IHealable healable))
        {
            _wasTaken = !healable.RestoreHealth(_healthAmount);
            gameObject.SetActive(_wasTaken);
            
        }

    }
}

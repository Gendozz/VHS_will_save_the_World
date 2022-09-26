using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColliderResizing : MonoBehaviour
{
    [SerializeField] private AnimationClip _death;
    [SerializeField] private BoxCollider _colliderEnemy3;
    
    private float _centerColliderReductionPercent = 75;
    private float _sizeColliderReductionPercent = 65;
    private float _timeAnimDeath;
    private float _startCenterCollider;
    private float _endCenterCollider;

    private float _speedCenter;
    private float _speedSize;

    private void Start()
    {
        _timeAnimDeath = _death.length;

        _centerColliderReductionPercent = _centerColliderReductionPercent / 100;
        _sizeColliderReductionPercent = _sizeColliderReductionPercent / 100;

        _startCenterCollider = _colliderEnemy3.center.y;
        _endCenterCollider = _startCenterCollider * _centerColliderReductionPercent;

        _speedCenter = _colliderEnemy3.center.y * (1 - _centerColliderReductionPercent) / _timeAnimDeath * Time.deltaTime;
        _speedSize = _colliderEnemy3.size.y * (1 - _sizeColliderReductionPercent) / _timeAnimDeath * Time.deltaTime;
    }

    public void IncreaseSizeCollider()
    {
        StartCoroutine(IToChangeSizeCollider(-_speedCenter, -_speedSize));
    }

    public void ReductionSizeCollider()
    {
        StartCoroutine(IToChangeSizeCollider(_speedCenter, _speedSize));
    }

    private IEnumerator IToChangeSizeCollider(float speedCenter, float speedSize)
    {
        yield return _colliderEnemy3.center = new Vector3(_colliderEnemy3.center.x, _colliderEnemy3.center.y - speedCenter, _colliderEnemy3.center.z);
        yield return _colliderEnemy3.size = new Vector3(_colliderEnemy3.size.x, _colliderEnemy3.size.y - speedSize, _colliderEnemy3.size.z);

        if (speedCenter > 0 && _colliderEnemy3.center.y > _endCenterCollider ||
            speedCenter < 0 && _colliderEnemy3.center.y < _startCenterCollider)
        {
            yield return StartCoroutine(IToChangeSizeCollider(speedCenter, speedSize));
        }
    }
}

using UnityEngine;

public class ResurrectionEnemy3 : MonoBehaviour
{
    [Header("-----      ���������      -----")]
    [SerializeField] private float _timeUntilRes;
    [Space]
    [Header("-----      ���������� � ���������      -----")]
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _Enemy3Objs;

    private float _timerRes = 0;

    private void Update()
    {
        if (transform.childCount == 0 && _door != null)
        {
            if (_timerRes >= _timeUntilRes)
            {
                var enemy3 = Instantiate(_Enemy3Objs, transform.position, transform.rotation, transform);

                _timerRes = 0;
            }

            _timerRes += Time.deltaTime;
        }
    }
}

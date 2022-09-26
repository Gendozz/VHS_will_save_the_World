using UnityEngine;

public class RotateAroundY : MonoBehaviour
{
    [SerializeField] private float _rotaionSpeed;

    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * _rotaionSpeed);
    }
}

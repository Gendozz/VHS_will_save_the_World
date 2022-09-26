using UnityEngine;
using Cinemachine;

public class CinemachineMainCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    [Header("За кем следит камера")]
    [SerializeField] private Transform target;

    public void ChangeTarget(Transform newTarget)
    {
        _cinemachineVirtualCamera.Follow = newTarget;
        _cinemachineVirtualCamera.LookAt = newTarget;
    }
}

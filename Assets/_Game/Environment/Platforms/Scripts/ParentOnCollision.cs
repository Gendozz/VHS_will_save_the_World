using UnityEngine;

// ��������� �������.
// TODO: ��������! ����� ���������� ����� ������ �� ������

public class ParentOnCollision : MonoBehaviour
{
    private Transform transformToParentToThis;

    private Transform parentOfOtherOnCollisionEnter;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            transformToParentToThis = playerMovement.transform;

            transformToParentToThis.parent = transform;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            transformToParentToThis = playerMovement.transform;

            transformToParentToThis.parent = parentOfOtherOnCollisionEnter;
        }
    }
}

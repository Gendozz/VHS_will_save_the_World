using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DestroyOnCollisionWithPlayer : MonoBehaviour
{
    [Header("-----     ���������     -----")]
    [Header("�������� ����� ������������ ����� ��������")]
    [SerializeField] private float destroyDelay;

    [Header("�������� ����� ��������� ����� �����������")]
    [SerializeField] private float respawnDelay;

    [Header("-----     ���������� � ���������     -----")]

    [Header("������, ������������ ���������")]
    [SerializeField] private GameObject modelPlatform;

    [Header("��������� ���������")]
    [SerializeField] private Collider collider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            StartCoroutine(DestroySelfAfterDelay(destroyDelay));
            StartCoroutine(RespawnAfterDestroyAfterDelay(destroyDelay + respawnDelay));
        }
    }

    private IEnumerator DestroySelfAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        modelPlatform.SetActive(false);
        collider.enabled = false;
    }

    private IEnumerator RespawnAfterDestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        modelPlatform.SetActive(true);
        collider.enabled = true;
    }
}

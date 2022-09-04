using UnityEngine;

public class SetActiveByActivator : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject _objectToSetActive;

    public void Activate()
    {
        _objectToSetActive.SetActive(true);
    }
}

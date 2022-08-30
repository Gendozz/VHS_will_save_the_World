using UnityEngine;

public class SetActiveByActivator : MonoBehaviour, IActivatable
{
    [SerializeField] private GameObject _objectToSetActive;

    private void Start()
    {
        _objectToSetActive.SetActive(false);
    }

    public void Activate()
    {
        _objectToSetActive.SetActive(true);
    }
}

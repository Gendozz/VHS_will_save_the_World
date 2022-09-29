using UnityEngine;

public class SpringTrap : MonoBehaviour
{

    private bool _wasContact;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMovement playerMovement) && !_wasContact)
        {
            _wasContact = true;
            print(gameObject.tag);
            Invoke(nameof(ChangeLayerOnPlayerContact), 0.5f);
        }
    }

    private void ChangeLayerOnPlayerContact()
    {
        this.gameObject.tag = "Untagged";
    }

}

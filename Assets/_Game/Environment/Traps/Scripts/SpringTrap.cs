using JSAM;
using UnityEngine;

public class SpringTrap : MonoBehaviour
{
    private bool _wasContact = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMovement playerMovement) && !_wasContact)
        {
            AudioManager.PlaySound(Sounds.SpringTrapSound);
            _wasContact = true;
            Invoke(nameof(ChangeLayerOnPlayerContact), 0.5f);
        }
    }

    private void ChangeLayerOnPlayerContact()
    {
        this.gameObject.tag = "Untagged";
    }

}

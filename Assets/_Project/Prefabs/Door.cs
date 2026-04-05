using UnityEngine;

public class Door : MonoBehaviour
{
    public bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !active)
        {
            Debug.Log(gameObject.name + " is inactive, ignoring trigger by " + other.name);
            return;
        }
        if (other.CompareTag("Player") && active)
        {
            // active = false;
            Debug.Log(gameObject.name + " triggered by " + other.name);
            transform.parent.GetComponent<Room>()?.DoorTriggered(gameObject.name, other.gameObject);
        }
    }
}
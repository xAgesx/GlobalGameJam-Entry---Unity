using UnityEngine;
using UnityEngine.Playables;

public class TriggerOpen : MonoBehaviour {
    public PlayableDirector pd;
    public bool isOpen = false;
    public PickupSystem pickupSystem;
    public GameObject door;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Key_Golden" && !isOpen) {
            
            pd.Play();
            isOpen = true;
            pickupSystem.heldItem = null;
            Debug.Log("OPEN");
        }
    }
}

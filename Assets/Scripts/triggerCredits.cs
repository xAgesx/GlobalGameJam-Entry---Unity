
using UnityEngine;

public class triggerCredits : MonoBehaviour {
    public GameObject credits;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Key_Golden") {

            credits.SetActive(true);
        }
    }
}

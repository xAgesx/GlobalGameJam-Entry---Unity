using UnityEngine;

public class ScalesHint : MonoBehaviour {

    [SerializeField]AudioSource hint;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            hint.Play();
            Destroy(gameObject);
        }
        
    }
}

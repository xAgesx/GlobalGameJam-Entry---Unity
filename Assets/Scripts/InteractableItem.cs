using UnityEngine;

public class InteractableItem : MonoBehaviour {
    public ItemData data; 
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public float GetWeight() {
        return rb != null ? rb.mass : 0f;
    }
}

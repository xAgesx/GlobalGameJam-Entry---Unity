using UnityEngine;

public class TargetHighlight : MonoBehaviour {
    public MeshRenderer targetRenderer;
    public Material highlightMaterial; // A glowing or outline material
    private Material originalMaterial;
    public PickupSystem pickupSystem;

    void Start() {
        originalMaterial = targetRenderer.material;
    }

    void Update() {
        if (pickupSystem.heldItem == null ) {
            targetRenderer.material = originalMaterial;
            return;
        }
        if (pickupSystem.heldItem.name == "Key_Golden") {
            targetRenderer.material = highlightMaterial;
        } else {
            targetRenderer.material = originalMaterial;
        }
    }
}
using UnityEngine;

public class PickupSystem : MonoBehaviour {
    [Header("Settings")]
    public Transform holdPoint;
    public PlayerInteraction playerInteraction;
    public AbilityController abilityController;
    public float interactDistance = 3f;
    public LayerMask interactableLayer;
    
    public GameObject heldItem;
    private Rigidbody heldRB;

    void Update() {
        // 1. Logic to prevent pickup in Spirit Mode
        if (Input.GetMouseButtonDown(0) && heldItem == null) {
            if (abilityController.isSpiritActive) {
                playerInteraction.instructionText.text = "Unable to pickup while in spirit world";
                playerInteraction.instructionText.color = Color.red;
            } else {
                TryPickup();
            }
        }

        if (Input.GetMouseButtonDown(1) && heldItem != null) {
            DropItem();
        }
        
        // 2. Auto-drop if the mask is put on
        if (abilityController.isSpiritActive && heldItem != null) {
            DropItem();
        }
    }

    void TryPickup() {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayer)) {
            if (hit.collider.TryGetComponent(out InteractableItem item)) {
                PickUp(hit.collider.gameObject);
            }
        }
    }

    void PickUp(GameObject obj) {
        heldItem = obj;
        heldRB = obj.GetComponent<Rigidbody>();

        heldRB.isKinematic = true;
        heldRB.useGravity = false;

        heldItem.transform.SetParent(holdPoint);

        playerInteraction.instructionText.text = "(RCLICK) To Drop";
    }

    public void DropItem() {
        if (heldItem == null) return;

        heldRB.isKinematic = false;
        heldRB.useGravity = true;

        heldItem.transform.SetParent(null);
        heldRB.AddForce(transform.forward * 2f, ForceMode.Impulse);

        heldItem = null;
        heldRB = null;

        playerInteraction.instructionText.text = "(LCLICK) To Pickup";
    }
}
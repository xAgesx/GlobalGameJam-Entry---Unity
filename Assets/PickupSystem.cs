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
        // Only allow pickup if NOT in spirit mode
        if (Input.GetMouseButtonDown(0) && heldItem == null) {
            if (abilityController.isSpiritActive) {
                // If they try to pick up while masked, show the warning
                playerInteraction.instructionText.text = "Unable to pickup while wearing the spirit mask";
            } else {
                TryPickup();
            }
        }

        if (Input.GetMouseButtonDown(1) && heldItem != null) {
            DropItem();
        }
        
        // Auto-drop logic remains
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
        // Ensure it snaps to the hand position properly
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;

        // UI update
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

        // UI reset
        playerInteraction.instructionText.text = "(LCLICK) To Pickup";
    }
}
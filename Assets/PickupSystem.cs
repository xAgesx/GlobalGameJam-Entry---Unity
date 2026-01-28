using UnityEngine;

public class PickupSystem : MonoBehaviour {
    [Header("Settings")]
    public Transform holdPoint;
    public PlayerInteraction playerInteraction;
    public PlayerMovement playerController;
    public float interactDistance = 3f;
    public LayerMask interactableLayer;
    
    public GameObject heldItem;
    private Rigidbody heldRB;

    void Update() {
        if (Input.GetMouseButtonDown(0) && heldItem == null) {
            TryPickup();
        }

        if (Input.GetMouseButtonDown(1) && heldItem != null) {
            DropItem();
        }
        
        if (playerController.isSpiritMode && heldItem != null) {
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

        // heldItem.transform.position = holdPoint.position;
        // heldItem.transform.rotation = holdPoint.rotation;
        heldItem.transform.SetParent(holdPoint);

        //UI update
        playerInteraction.instructionText.text = "(RCLICK) To Drop";

        Debug.Log("Picked up: " + heldItem.name);
    }

    public void DropItem() {
        if (heldItem == null) return;

        heldRB.isKinematic = false;
        heldRB.useGravity = true;

        heldItem.transform.SetParent(null);
        
        heldRB.AddForce(transform.forward * 2f, ForceMode.Impulse);

        heldItem = null;
        heldRB = null;

        //UI reset
        playerInteraction.instructionText.text = "(LCLICK) To Drop";
        
        Debug.Log("Item dropped.");
    }
}
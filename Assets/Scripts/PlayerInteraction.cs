using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public float interactDistance = 4f;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI instructionText;
    public LayerMask interactableLayer;

    void Update() {
        LookForItems();
    }

    void LookForItems() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer)) {
            if (hit.collider.TryGetComponent(out InteractableItem item)) {

                string name = item.data != null ? item.data.itemName : "Unknown Object";
                float weight = item.GetWeight();

                infoText.text = (weight != 0f)? $"{name} ({weight}kg)": "";
                instructionText.enabled = true;
                return;
            }
        }
        infoText.text = "";
        instructionText.enabled = false;

    }
}

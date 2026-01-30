using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {
    public float interactDistance = 4f;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI instructionText;
    public LayerMask interactableLayer;
    
    public AbilityController abilityController;

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color spiritColor = new Color(0.5f, 0.8f, 1f); 
    public Color warningColor = Color.red;

    void Update() {
        LookForItems();
    }

    void LookForItems() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer)) {
            if (hit.collider.TryGetComponent(out InteractableItem item)) {
                
                if (abilityController.isSpiritActive) {
                    // Spirit Mode
                    infoText.text = "??? (???kg)";
                    infoText.color = spiritColor;
                    
                    instructionText.text = "Can't pickup";
                    instructionText.color = warningColor;
                } 
                else {
                    // Normal Mode
                    string name = item.data != null ? item.data.itemName : "Unknown Object";
                    float weight = item.GetWeight();
                    if(weight > .2f) {
                        infoText.text = (weight != 0f) ? $"{name} ({weight}kg)" : "";
                    infoText.color = normalColor;
                    } else {
                        infoText.text = (weight != 0f) ? $"{name} ({item.data.description})" : "";
                        infoText.color = normalColor;
                    }
                    
                    
                    // instructionText.text = "(LCLICK) To Pickup";
                    instructionText.color = normalColor;
                }

                instructionText.enabled = true;
                return;
            }else if(hit.collider.TryGetComponent(out Button btn)) {
                if(Input.GetMouseButtonDown(0))
                btn.onClick.Invoke();
            }
        }
        
        infoText.text = "";
        instructionText.enabled = false;
    }
}
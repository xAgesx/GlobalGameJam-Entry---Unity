using UnityEngine;
using UnityEngine.UI;

public class indicatorHandler : MonoBehaviour {
    public Color red;
    public Color green;
    [SerializeField] Oven ovenScript;
    [SerializeField] Image img;
    void Update() {
        if (ovenScript.slider.value < ovenScript.tempThreshhold) {
            img.color = red;
        } else {
            img.color = green;
        }
    }
}

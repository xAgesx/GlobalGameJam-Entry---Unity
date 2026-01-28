using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour {
    private Image img;
    void Awake() { img = GetComponent<Image>(); }

    public float Alpha {
        get => img.color.a;
        set {
            Color c = img.color;
            c.a = value;
            img.color = c;
        }
    }
}
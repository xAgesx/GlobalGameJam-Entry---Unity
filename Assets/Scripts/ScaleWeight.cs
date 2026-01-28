
using UnityEngine;
using UnityEngine.UI;

public class ScaleWeight : MonoBehaviour {

    [SerializeField] float totalMass = 0;
    [SerializeField] GameObject weightTextUI;
    [SerializeField] GameObject weightSliderUI;
    [SerializeField] float neededWeight = 7.5f;
    [SerializeField] float lerpSpeed = 0.5f;
    float targetValue = 0f;
    Slider slider;
    [SerializeField]doorTrigger eventTrigger;
    [SerializeField] AudioSource hint;
    bool hintHasPlayed = false;

    void Start() {
        slider = weightSliderUI.GetComponent<Slider>();
    }

    void Update() {
        if (slider != null) {
            slider.value = Mathf.MoveTowards(slider.value, targetValue, lerpSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Rigidbody>()) {
            Text text = weightTextUI.GetComponent<Text>();
            if (slider == null) slider = weightSliderUI.GetComponent<Slider>();
            var sliderColor = slider.colors;

            totalMass += other.GetComponent<Rigidbody>().mass;
            text.text = totalMass.ToString() + " Kg" + "/ "+targetValue+" Kg";
            // if (!hintHasPlayed) {
            //     hint.Play();
            //     hintHasPlayed = true;
            // }

            if (totalMass > neededWeight * 2) {
                totalMass = neededWeight * 2;
                text.text = "Max Weight Exceeded !";
                text.color = Color.red;
            }

            if (totalMass == neededWeight ) {
                sliderColor.disabledColor = new Color(0f, 1f, 0f); 
                text.color = Color.green;
                //Play anim
                eventTrigger.doorTriggered?.Invoke();
            } else if (totalMass < neededWeight) {
                sliderColor.disabledColor = new Color(1f, 0.92f, 0.016f);
                text.color = Color.white;
            } else {
                sliderColor.disabledColor = new Color(1f, 0f, 0f); 
                text.color = Color.red;
            }
            
            slider.colors = sliderColor;
            targetValue = totalMass / neededWeight / 2;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.GetComponent<Rigidbody>()) {
            if (slider == null) slider = weightSliderUI.GetComponent<Slider>();
            var sliderColor = slider.colors;

            totalMass -= other.GetComponent<Rigidbody>().mass;
            weightTextUI.GetComponent<Text>().text = totalMass.ToString() + " Kg";

            if (Mathf.Abs(totalMass - neededWeight) < 0.01f) {
                sliderColor.disabledColor = new Color(0f, 1f, 0f); 
                weightTextUI.GetComponent<Text>().color = Color.green;
            } else if (totalMass < neededWeight) {
                sliderColor.disabledColor = new Color(1f, 0.92f, 0.016f); 
                weightTextUI.GetComponent<Text>().color = Color.white;
            } else {
                sliderColor.disabledColor = new Color(1f, 0f, 0f); 
                weightTextUI.GetComponent<Text>().color = Color.red;
            }

            slider.colors = sliderColor;
            targetValue = (totalMass <= 0) ? 0 : totalMass / neededWeight / 2;
        }
    }
}
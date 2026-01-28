using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Oven : MonoBehaviour {
    public Slider slider;
    [SerializeField] Text temperatureText;
    [SerializeField] Gradient tempGradient;
    [SerializeField] Image fillImage;
    [SerializeField] GameObject firewoodStack;
    [SerializeField] GameObject firewoodprefab;
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] float maxparticlesCount = 50f;
    public float targetValue;
    public float coolingRate;
    public float tempThreshhold = 0.7f;
    [SerializeField] AudioSource firecracklingAudio;
    void Update() {
        if (slider != null) {
            slider.value = Mathf.MoveTowards(slider.value, targetValue, coolingRate * Time.deltaTime);
            UpdateVisuals();
            UpdateParticles();
            if(slider.value > 0.1 && !firecracklingAudio.isPlaying) {
                firecracklingAudio.Play();
            }else if (slider.value == 0 && firecracklingAudio.isPlaying) {
                firecracklingAudio.Stop();
            }
        }
        if(firewoodStack.transform.childCount < 3) {
            Instantiate(firewoodprefab , firewoodStack.transform);
        }

    }
    public void increaseTemp(float value) {
        slider.value += value / 320;
    }
    private void UpdateVisuals() {
        float t = slider.value;
        if (fillImage != null) {
            fillImage.color = tempGradient.Evaluate(t);
        }
        if (temperatureText != null) {
            temperatureText.text = ((int)slider.value).ToString() + "°";
        }
    }
    private void UpdateParticles() {
        if (fireParticles == null) return;

        var emission = fireParticles.emission;
        emission.rateOverTime = slider.value * maxparticlesCount;

        if (slider.value > 0.05f && !fireParticles.isPlaying) fireParticles.Play();
        else if (slider.value <= 0.05f && fireParticles.isPlaying) fireParticles.Stop();
    }
}

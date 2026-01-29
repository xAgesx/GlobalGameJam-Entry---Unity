using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class AbilityController : MonoBehaviour {
    [Header("Timers")]
    public float maxSpiritEnergy = 5f;
    public float cooldownDuration = 3f;
    
    [Header("Visuals")]
    public Volume spiritVolume;
    public Image energyBarFill;
   

    private float currentEnergy;
    public bool isSpiritActive = false;
    private bool isOnCooldown = false;

    void Start() {
        currentEnergy = maxSpiritEnergy;
        UpdateVisuals(false); 
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (!isSpiritActive && !isOnCooldown) {
                EnterSpiritWorld();
            } else if (isSpiritActive) {
                ExitSpiritWorld();
            }
        }

        HandleEnergy();
    }

    void HandleEnergy() {
        if (isSpiritActive) {
            currentEnergy -= Time.deltaTime;
            if (currentEnergy <= 0) {
                currentEnergy = 0;
                ExitSpiritWorld();
                StartCooldown();
            }
        } else if (currentEnergy < maxSpiritEnergy && !isOnCooldown) {
            currentEnergy += Time.deltaTime * 0.5f; 
        }

        // Update the UI Bar
        if (energyBarFill != null) {
            energyBarFill.fillAmount = currentEnergy / maxSpiritEnergy;
        }
    }

    void EnterSpiritWorld() {
        isSpiritActive = true;
        UpdateVisuals(true);
        
    }

    void ExitSpiritWorld() {
        isSpiritActive = false;
        UpdateVisuals(false);
    }

    void StartCooldown() {
        isOnCooldown = true;
        energyBarFill.color = Color.red; 
        Invoke(nameof(ResetCooldown), cooldownDuration);
    }

    void ResetCooldown() {
        isOnCooldown = false;
        energyBarFill.color = Color.white;
    }

    void UpdateVisuals(bool active) {
        spiritVolume.enabled = active;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ovenCollider : MonoBehaviour {
    [SerializeField] Oven ovenScript;
    [SerializeField] Material transformedMat;
    [SerializeField] GameObject progressCanvasPrefab; 
    
    [SerializeField] Vector3 offset = new Vector3(0, 0.5f, 0); 
    [SerializeField] float transformationDuration = 3.0f;
    [SerializeField] bool hintHasPlayed = false;
    [SerializeField] AudioSource hint;

    void OnTriggerEnter(Collider other) {
        if (!hintHasPlayed) {
            hint.Play();
            hintHasPlayed = true;
        }
        if (other.CompareTag("Flammable")) {
            var details = other.gameObject.GetComponent<ObjectDetails>();
            if (details != null) {
                ovenScript.increaseTemp(details.details.tempIncrease);
            }
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("Potion") && ovenScript.slider.value >= ovenScript.tempThreshhold) {
            PotionTracker tracker = other.gameObject.GetComponent<PotionTracker>();
            if (tracker == null) tracker = other.gameObject.AddComponent<PotionTracker>();

            if (!tracker.isTransformed && !tracker.isCooking) {
                tracker.StartTransforming(transformationDuration, transformedMat, progressCanvasPrefab, offset);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Potion")) {
            PotionTracker tracker = other.gameObject.GetComponent<PotionTracker>();
            if (tracker != null) {
                tracker.StopTransforming();
            }
        }
    }
}

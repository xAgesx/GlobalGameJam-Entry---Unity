using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PotionTracker : MonoBehaviour {
    public bool isTransformed = false;
    public bool isCooking = false;

    private Coroutine activeRoutine;
    private GameObject activeUI;
    [SerializeField] GameObject potionPrefab;

    public void StartTransforming(float duration, Material newMat, GameObject uiPrefab, Vector3 offset) {
        isCooking = true;
        activeRoutine = StartCoroutine(TransformRoutine(duration, newMat, uiPrefab, offset));
    }

    public void StopTransforming() {
        if (isTransformed) return;

        isCooking = false;
        if (activeRoutine != null) {
            StopCoroutine(activeRoutine);
            activeRoutine = null;
        }

        if (activeUI != null) {
            Destroy(activeUI);
        }
    }

    private IEnumerator TransformRoutine(float duration, Material newMat, GameObject uiPrefab, Vector3 offset) {
        Slider progressBar = null;
        Text timer = null;

        // Create UI
        if (uiPrefab != null) {
            activeUI = Instantiate(uiPrefab, transform.position + offset, Quaternion.identity, transform);
            progressBar = activeUI.GetComponentInChildren<Slider>();
            timer = activeUI.transform.Find("Header Text").GetComponent<Text>();
            Debug.Log(timer);
        }

        float elapsed = 0;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            if (progressBar != null && timer != null) {
                progressBar.value = elapsed / duration;
                timer.text = ((int)(duration-elapsed)).ToString();
            }
            yield return null;
        }

        // Complete Transformation
        Instantiate(potionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);

        isTransformed = true;
        isCooking = false;
        if (activeUI != null) Destroy(activeUI);
    }
}
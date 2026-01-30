using UnityEngine;

public class collide : MonoBehaviour {

    [Header("References")]
    public Transform playerTransform; // Drag the Player object here
    public LevelManager levelManager; // Drag the LevelManager here

    [Header("Settings")]
    public float killDistance = 2.0f; // Distance in meters

    void Update() {
        if (playerTransform == null) return;

        // Calculate the distance between monster and player
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= killDistance) {
            Debug.Log("Monster caught player via distance check!");

            if (levelManager != null) {
                levelManager.ReloadLevel();
            } else {
                // Emergency reload if LevelManager isn't assigned
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                );
            }
        }
    }

    // Still useful to see the kill zone in the editor
    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, killDistance);
    }
}


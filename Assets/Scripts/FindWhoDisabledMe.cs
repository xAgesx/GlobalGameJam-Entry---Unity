using UnityEngine;

public class FindWhoDisabledMe : MonoBehaviour {
    void OnDisable() {
        Debug.LogError($"{gameObject.name} was just disabled!", this);
        // This line prints the "Stack Trace" which shows the path of the script call
        Debug.LogError(System.Environment.StackTrace); 
    }
}
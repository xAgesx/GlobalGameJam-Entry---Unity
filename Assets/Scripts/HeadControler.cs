
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadControler : MonoBehaviour {

    [SerializeField] InputActionAsset headController;

    void Awake() {
        headController.Disable();
    }

}

using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    public float baseSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    float speedBoost = 1f;
    Vector3 velocity;
    public bool isSpiritMode = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
    isSpiritMode = !isSpiritMode;

    if (isSpiritMode) {
        Debug.Log("Drop Item Held");
    }
}
        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        speedBoost = 1f;


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * (baseSpeed + speedBoost) * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && controller.isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

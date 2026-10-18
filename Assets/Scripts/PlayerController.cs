using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveForce = 10f;  // // force used to move the ball
    [SerializeField] ForceMode mode = ForceMode.Force;  // how the force is applied
    Rigidbody rb;
    Vector3 input;

    void Awake() => rb = GetComponent<Rigidbody>();
    // get the Rigidbody attached to the player

    void Update()
    {   // read WASD or arrow key input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // // set movement direction and limit diagonal movement
        input = Vector3.ClampMagnitude(new Vector3(h, 0f, v), 1f);
    }

    void FixedUpdate() => rb.AddForce(input * moveForce, mode); // apply movement during the physics update
}
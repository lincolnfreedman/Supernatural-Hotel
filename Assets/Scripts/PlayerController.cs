using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    PlayerInput input;
    Vector2 moveInput;
    Vector2 lookInput;
    float pitch = 0f;
    float yaw = 0f;
    Rigidbody rb;
    [SerializeField] Camera playerCamera;

    [SerializeField] float minPitch = -45f;
    [SerializeField] float maxPitch = 75f;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float sensitivity = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        input = GetComponent<PlayerInput>();

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        AimCamera();
        MovePlayer();
    }
    void AimCamera()
    {
        playerCamera.transform.position = transform.position + Vector3.up * 0.8f;
        lookInput = input.actions["Look"].ReadValue<Vector2>();

        pitch -= lookInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, maxPitch * -1, minPitch * -1);

        yaw += lookInput.x * sensitivity * Time.deltaTime;

        playerCamera.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void MovePlayer()
    {
        moveInput = input.actions["Move"].ReadValue<Vector2>();

        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 posChange = camRight * moveInput.x + camForward * moveInput.y;
        rb.MovePosition(rb.position + posChange * moveSpeed);

        transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);

    }
}
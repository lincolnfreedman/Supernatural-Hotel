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
    private bool frozen = true;
    Rigidbody rb;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject interactText;
    [SerializeField] GameObject letter;
    [SerializeField] float minPitch = -45f;
    [SerializeField] float maxPitch = 75f;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float sensitivity = 1f;
    public bool hasKey = false;
    public FreezeLight[] freezeLights;
    public Interactable currentInteractable;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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
        freezeLights = FindObjectsByType<FreezeLight>(FindObjectsSortMode.None);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        input = GetComponent<PlayerInput>();
        InputAction switchAction = input.actions["Interact"];
        switchAction.performed += ctx => Interact();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        AimCamera();
        MovePlayer();
        if(currentInteractable != null)
        {
            Debug.Log("interactable not null");
        }
    }
    void AimCamera()
    {
        if(frozen) {
            return;
        }
        playerCamera.transform.position = transform.position + Vector3.up * 0.8f;
        lookInput = input.actions["Look"].ReadValue<Vector2>();

        pitch -= lookInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, maxPitch * -1, minPitch * -1);

        yaw += lookInput.x * sensitivity * Time.deltaTime;

        playerCamera.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void MovePlayer()
    {
        if (frozen) {
            return;
        }
        moveInput = input.actions["Move"].ReadValue<Vector2>();

        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 posChange = camRight * moveInput.x + camForward * moveInput.y;
        Vector3 newPos = rb.position + posChange * moveSpeed;
        bool inLight = false;
        foreach (FreezeLight light in freezeLights)
        {
            Debug.Log("Checking light");
            if (light.PosInLight(newPos) && light.gameObject.activeSelf == true)
            {
                inLight = true;
            }
        }
        if (!inLight)
        {
            rb.MovePosition(newPos);
        }
        transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
    }
    void Interact()
    {
        if(letter.activeSelf)
        {
            letter.SetActive(false);
            interactText.SetActive(false);
            frozen = false;
            return;
        }
        currentInteractable?.Interact();
    }
    public void FreezePlayer()
    {
        frozen = true;
    }
    public void UnfreezePlayer()
    {
        frozen = false;
    }
}
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FPSPlayer : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float gravity = -9.81f;

    [Header("Mouse")]
    public float mouseSensitivity = 2f;

    [Header("Audio")]
    public AudioClip footstepClip;

    private float yRotation = 0f;
    private float yVelocity = 0f;

    private AudioSource audioSource;
    private CharacterController controller;
    private Camera playerCamera;

    void Start()
    {
        // Crear cámara en runtime
        GameObject camObj = new GameObject("PlayerCamera");
        camObj.transform.parent = this.transform;
        camObj.transform.localPosition = new Vector3(0, 1.6f, 0);

        playerCamera = camObj.AddComponent<Camera>();

        // Configurar audio
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footstepClip;
        audioSource.loop = true;

        // Controller
        controller = GetComponent<CharacterController>();

        // Bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleFootsteps();
    }

    void HandleMovement()
    {
        // Reiniciar gravedad cuando pisa el suelo
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f; // Mantener un pequeño empuje hacia abajo para estabilizar isGrounded
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Aplicar gravedad
        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;

        // Mover solo una vez por frame
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleFootsteps()
    {
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        bool isMoving = horizontalVelocity.magnitude > 0.1f && controller.isGrounded;

        if (isMoving)
        {
            if (!audioSource.isPlaying && footstepClip != null)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}
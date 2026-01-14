using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveCC : MonoBehaviour
{
    [Header("Refs")]
    public Transform cameraRoot;   // Player child: CameraRoot (pitch burada)
    public Camera playerCamera;    // Main Camera (FOV için opsiyonel)

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float gravity = -20f;

    [Header("Jump")]
    public float jumpHeight = 1.4f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    public float pitchClamp = 85f;

    private CharacterController cc;
    private Vector3 velocity;
    private float pitch;

    void Awake()
    {
        cc = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCamera != null)
            playerCamera.fieldOfView = 80f;
    }

    void Update()
    {
        // --- MOUSE LOOK (FPS) ---
        if (cameraRoot != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Yaw: player gövdesi sað/sol döner
            transform.Rotate(Vector3.up * mouseX);

            // Pitch: kamera yukarý/aþaðý (clamp)
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -pitchClamp, pitchClamp);
            cameraRoot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        // --- WASD MOVE (kamera yaw'ýna göre) ---
        float x = Input.GetAxisRaw("Horizontal"); // A-D
        float z = Input.GetAxisRaw("Vertical");   // W-S

        Vector3 moveDir;

        if (cameraRoot != null)
        {
            // cameraRoot'un forward/right'ýný alýp y düzlemine indiriyoruz
            Vector3 forward = cameraRoot.forward;
            Vector3 right = cameraRoot.right;
            forward.y = 0f; right.y = 0f;
            forward.Normalize(); right.Normalize();

            moveDir = (right * x + forward * z).normalized;
        }
        else
        {
            // fallback
            moveDir = (transform.right * x + transform.forward * z).normalized;
        }

        cc.Move(moveDir * moveSpeed * Time.deltaTime);

        // --- GRAVITY / GROUND ---
        if (cc.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        // --- JUMP (Space) ---
        if (cc.isGrounded && Input.GetKeyDown(KeyCode.Space))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        // ESC: mouse'u serbest býrak (debug)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

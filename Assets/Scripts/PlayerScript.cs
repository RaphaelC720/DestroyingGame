using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody RB;
    //camara
    public Camera myCamera;
    private float FOV = 50;
    public float Sensitivity = 2f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float maxLookAngle = 50f;

    //crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    private Sprite crosshairImage;
    public Color crosshairColor = Color.white;
    private Image crosshairObject;

    //movement
    public float walkSpeed = 5f;
    private float maxVelocityChange = 10f;
    public float jumpPower;
    public KeyCode jumpKey = KeyCode.Space;
    public bool isWalking;
    public bool isJumping;
    public bool isGrounded;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        crosshairObject = GetComponentInChildren<Image>();
        myCamera.fieldOfView = FOV; 
    }
    void Start()
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (crosshair)
        {
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor; 

        }
        else
        {
            crosshairObject.gameObject.SetActive(false);
        }

    }

    void Update()
    {
        myCamera.fieldOfView = FOV;

        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity;
        pitch -= Sensitivity * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
        transform.localEulerAngles = new Vector3(0, yaw, 0);
        myCamera.transform.localEulerAngles = new Vector3 (pitch, 0, 0);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        CheckGround();

    }
    private void FixedUpdate()
    {
        Vector3 V = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (V.x != 0 || V.z != 0 && isGrounded)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        V = transform.TransformDirection(V) * walkSpeed;

        Vector3 v = RB.linearVelocity;
        Vector3 velocityChange = (V - v);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        RB.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    [SerializeField] LayerMask groundLayer;
    private void CheckGround()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 origin = col.bounds.center;
        Vector3 direction = Vector3.down;
        float distance = col.bounds.extents.y + 0.5f;

        isGrounded = Physics.Raycast(origin, direction, out RaycastHit hit, distance, groundLayer);
       
    }
    private void Jump()
    {
        if (isGrounded)
        {
            RB.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
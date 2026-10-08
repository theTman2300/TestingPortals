using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : PortalTraveller
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float speed = 1;
    [SerializeField] float acceleration = 1;
    [SerializeField] float sensitivity = 1;
    Rigidbody rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!Input.GetMouseButton(1)) return;
        Vector2 lookInput;
        lookInput.x = Input.GetAxis("Mouse X");
        lookInput.y = Input.GetAxis("Mouse Y");
        lookInput *= sensitivity;
        if (lookInput.magnitude != 0)
        {
            cameraTransform.Rotate(new Vector3(-lookInput.y, 0, 0));
            transform.Rotate(new Vector3(0, lookInput.x, 0));
        }
    }

    void FixedUpdate()
    {
        if (!Input.GetMouseButton(1)) return;

        Vector2 moveInput;
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");


        if (rb.linearVelocity.magnitude < speed)
            rb.AddForce(acceleration * (transform.rotation * new Vector3(moveInput.x, 0, moveInput.y)));
    }

    public override void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rotation)
    {
        base.Teleport(fromPortal, toPortal, pos, rotation);
        Quaternion rotationDifference = toPortal.rotation * Quaternion.Inverse(fromPortal.rotation);
        rb.linearVelocity = rotationDifference * rb.linearVelocity;
    }
}

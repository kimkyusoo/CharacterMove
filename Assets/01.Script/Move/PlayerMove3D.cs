using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove3D : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody rb;
    private Animator anim;

    public InputAction MoveAction;
    public InputAction RunAction;
    Vector2 inputDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnEnable() { MoveAction.Enable(); RunAction.Enable(); }
    private void OnDisable() { MoveAction.Disable(); RunAction.Disable(); }

    private void Update()
    {
        moveSpeed = RunAction.IsPressed() ? 10.0f : 5.0f;
        inputDirection = MoveAction.ReadValue<Vector2>();

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y);
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if (anim != null)
        {
            float horizontalSpeed = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude;
            anim.SetFloat("Speed", horizontalSpeed);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveVec = new Vector3(inputDirection.x, 0, inputDirection.y) * moveSpeed;
        rb.linearVelocity = new Vector3(moveVec.x, rb.linearVelocity.y, moveVec.z);
    }
}
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private PlayerJump playerJump;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (anim == null || rb == null || playerJump == null) return;

        float speed = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude;
        anim.SetFloat("Speed", speed);

        anim.SetBool("IsGrounded", playerJump.groundCheck);

        float yVelocity = rb.linearVelocity.y;
        anim.SetFloat("yVelocity", yVelocity);

    }
}

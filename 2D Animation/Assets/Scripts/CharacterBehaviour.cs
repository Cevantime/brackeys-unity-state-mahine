using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float jumpForce = 400f;                            // Amount of force added when the player jumps.
    [Range(5, 40)] public float crouchSpeed = 17;            // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] public float movementSmoothing = .05f;    // How much to smooth out the movement
    [Range(20, 60)] public float runSpeed = 40f;
    public bool airControl = false;                           // Whether or not a player can steer while jumping;
    public LayerMask whatIsGround;                            // A mask determining what is ground to the character
    public Transform groundCheck;                         // A position marking where to check if the player is grounded.
    public Transform ceilingCheck;                            // A position marking where to check for ceilings
    public Collider2D crouchDisableCollider;
    public Animator animator;


    public const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public const float ceilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    public Rigidbody2D body;
    private StateMachine stateMachine;

    public float horizontalMove = 0.0f;

    public State running;
    public State jumping;
    public State crouching;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        running = new Running(stateMachine, this);
        jumping = new Jumping(stateMachine, this);
        crouching = new Crouching(stateMachine, this);
        stateMachine.ChangeState(running);
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        stateMachine.currentState.LogicUpdate(Time.deltaTime);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate(Time.fixedDeltaTime);
        Vector3 localScale = transform.localScale;
        if (body.velocity.magnitude > 0.1f)
        {
            if (body.velocity.x > 0)
            {
                localScale.x = 1;
            }
            else if (body.velocity.x < 0)
            {
                localScale.x = -1;
            }
        }

        transform.localScale = localScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float jumpForce = 700f;
    [Range(5, 40)] public float crouchSpeed = 17;
    [Range(0, .3f)] public float movementSmoothing = .05f;
    [Range(20, 60)] public float runSpeed = 40f;
    public bool airControl = false;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform ceilingCheck;
    public Collider2D crouchDisableCollider;
    public const float groundedRadius = .2f;
    public const float ceilingRadius = .2f;
    public Animator animator;
    public Rigidbody2D body;


    [HideInInspector] public float horizontalMove = 0.0f;

    private StateMachine stateMachine;

    public State running;
    public State jumping;
    public State crawling;

    void Awake()
    {
        stateMachine = new StateMachine();
        running = new Running(stateMachine, this);
        jumping = new Jumping(stateMachine, this);
        crawling = new Crawling(stateMachine, this);
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
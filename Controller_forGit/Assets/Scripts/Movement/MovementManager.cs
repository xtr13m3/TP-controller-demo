using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    #region Movement
    public float curSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float crouchedWalkSpeed = 2, crouchedWalkBackSpeed = 1;
    public float runSpeed = 7, runBackSpeed = 5;


    [HideInInspector] public Vector3 direction;
    public float horizontal, vertical;
    CharacterController cc;
    #endregion

    #region GroundCheck
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    #endregion

    #region Gravity
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 10;
    [HideInInspector] public bool jumped;
    Vector3 velocity;
    #endregion


    #region States
    public MovementBaseState prevState;
    public MovementBaseState currentState;

    public Idle idleState = new Idle();
    public Walk walkState = new Walk();
    public Crouch crouchState = new Crouch();
    public Run runState = new Run();
    public Jump jumpState = new Jump();
    #endregion

    [HideInInspector] public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        SwitchState(idleState);
    }

    void Update()
    {
        Move();
        Gravity();

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        direction = transform.forward * vertical + transform.right * horizontal;

        cc.Move(direction.normalized * curSpeed * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, cc.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        cc.Move(velocity * Time.deltaTime);
    }

    public void JumpForce() => velocity.y += jumpForce;

    public void Jumped() => jumped = true;

}

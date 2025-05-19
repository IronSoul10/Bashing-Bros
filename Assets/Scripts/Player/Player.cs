using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;
    private InputAction m_sprintAction;
    private InputAction m_interactAction;


    Vector2 movePlayer;
    Vector2 lookPlayer;

    private Animator animator;
    private Rigidbody rb;

    [SerializeField] private float speedWalk = 5f;
    [SerializeField] private float speedRun = 10f;
    [SerializeField] private float jumpForce = 5f;

    private bool isWalking = false;
    private bool isJumping = false;
    private bool sprintActive;
    float currentSpeed;
    [HideInInspector] public bool pressed;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        m_moveAction = inputActions.FindAction("Move");
        m_lookAction = inputActions.FindAction("Look");
        m_jumpAction = inputActions.FindAction("Jump");
        m_sprintAction = inputActions.FindAction("Sprint");
        m_interactAction = inputActions.FindAction("Interact");

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        movePlayer = m_moveAction.ReadValue<Vector2>();
        lookPlayer = m_lookAction.ReadValue<Vector2>();

        if (m_jumpAction.WasPressedThisFrame() && !isJumping)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Walking();
    }
    public void Jump()
    {
        if (isJumping) return;

        isJumping = true;
        rb.AddForceAtPosition(new Vector3(0, jumpForce, 0), Vector3.up, ForceMode.Impulse);
        animator.SetTrigger("IsJump");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    private void Walking()
    {
        Vector3 moveDirection = (transform.forward * movePlayer.y + transform.right * movePlayer.x);

        isWalking = moveDirection.magnitude > 0;

        Sprint();

        rb.MovePosition(rb.position + currentSpeed * Time.deltaTime * moveDirection);

        animator.SetBool("IsRun", isWalking);

        // Cambiar la escala en el eje X según la dirección del movimiento
        if (isWalking)
        {
            if (movePlayer.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            else if (movePlayer.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void Sprint()
    {

        sprintActive = isWalking && m_sprintAction.IsPressed();
        currentSpeed = sprintActive ? speedRun : speedWalk;
    }

    public bool Interact()
    {
        pressed = m_interactAction.WasPressedThisFrame();
        return pressed;
    }

}





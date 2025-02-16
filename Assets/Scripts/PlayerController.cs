using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float moveSpeed;

    Rigidbody rb;

    //PlayerInput playerInput;

    // --> sin Player Input: usamos directamente la clase generada por el Input System
    PlayerControls playerControls;

    Vector2 movement;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Jump.canceled += Jump;
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();    
        //playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        ReadInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        movement.Normalize();
        Vector2 move = movement * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + new Vector3(move.x, 0, move.y));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // --> con PlayerInput
        //if (!context.performed) 
        //    return;

        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    void ReadInput()
    {
        // --> con PlayerInput
        //movement = playerInput.actions["Move"].ReadValue<Vector2>();

        movement = playerControls.Player.Move.ReadValue<Vector2>();
    }
}

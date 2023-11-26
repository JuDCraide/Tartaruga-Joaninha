using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator playerAnimator;

    [Header("Ground Movement")]
    [SerializeField] float moveSpeed = 1.0f;
    float moveDirection = 0;
    [SerializeField] bool turnedRight = true;

    [Header("Ground Jump")]
    [SerializeField] float jumpForce = 10.0f;
    bool isJumping = false;
    private float jumpCounter;
    private float jumpCounter2;
    public float jumpDelay = 0.10f;
    public float jumpTime = 0.35f;


    [Header("Ground Detect")]
    public LayerMask groundLayer;
    [SerializeField] private Transform feetPosition;
    [SerializeField] float feetRadius;
    private bool grounded = true;

    private GameInputActions playerControls;
    private InputAction move;
    private InputAction jump;

    private void Awake() {
        playerControls = new GameInputActions();
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable() {
        move = playerControls.Player.GroundMovement;
        move.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += jumpPlayer;
    }

    private void OnDisable() {
        move.Disable();
        jump.Disable();
    }
    public bool isGrounded() {
        if (Physics2D.OverlapCircle(feetPosition.position, feetRadius, groundLayer)) {
            grounded = true;
        }
        else {
            grounded = false;
        }

        return grounded;
    }

    void jumpPlayer(InputAction.CallbackContext context) {
        if (grounded && !isJumping) {
            playerAnimator.SetBool("isJumping", true);
            isJumping = true;
            //rb.velocity = Vector2.up * jumpForce;
            jumpCounter = jumpTime;
            jumpCounter2 = 0;
        }

    }

    void FlipPlayer() {
        if ((turnedRight && moveDirection < 0f) || (!turnedRight && moveDirection > 0f)) {
            turnedRight = !turnedRight;
            GetComponent<SpriteRenderer>().flipX = !turnedRight;
        }
    }

    void Update() {
        moveDirection = move.ReadValue<float>();
        FlipPlayer();
        playerAnimator.SetBool("isWalking", moveDirection != 0);
        isGrounded();

        if (isJumping) {
            if (jumpCounter > (jumpTime - jumpDelay)) {
                playerAnimator.SetBool("isJumping", true);
                jumpCounter -= Time.deltaTime;
                jumpCounter2 += Time.deltaTime;
            }
            else if (jumpCounter > 0) {
                playerAnimator.SetBool("isJumping", true);
                rb.velocity = Vector2.up * jumpForce;
                jumpCounter -= Time.deltaTime;
                jumpCounter2 += Time.deltaTime;
            }
            else {
                isJumping = false;
            }
        }
        if (jump.WasReleasedThisFrame()) {
            isJumping = false;
        }
        if (grounded && !isJumping) {
            playerAnimator.SetBool("isJumping", false);
        }
    }


    void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
}

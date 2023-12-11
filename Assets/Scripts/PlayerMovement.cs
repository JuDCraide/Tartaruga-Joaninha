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
    public Sound jumpSound;

    [Header("Ground Detect")]
    public LayerMask groundLayer;
    [SerializeField] private Transform feetPosition;
    [SerializeField] float feetRadius = 0.2f;
    private bool grounded = true;

    [Header("Water Movement")]
    public LayerMask riverWater;
    [SerializeField] float swimSpeed = 3.0f;
    private bool onWater = false;
    float swimDirectionHorizontal = 0;
    float swimDirectionVertical = 0;
    [SerializeField] private Transform swimPosition;
    [SerializeField] float swimRadius = 0.2f;
    public Sound swimSound;

    [Header("Hat")]
    private Vector3 hatInitialPosition;
    private Quaternion hatInitialRotation;
    [SerializeField] private GameObject hatObject;

    private GameInputActions playerControls;
    private InputAction move;
    private InputAction jump;
    private InputAction swimH;
    private InputAction swimV;

    private void Awake() {
        playerControls = new GameInputActions();
        SetHatSprite();
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        GameManager.instance.setPlayer(gameObject);

        hatInitialPosition = hatObject.transform.localPosition;
        hatInitialRotation = hatObject.transform.rotation;
    }

    private void OnEnable() {
        move = playerControls.Player.GroundMovement;
        move.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += jumpPlayer;

        swimH = playerControls.PlayerWater.WaterMovementHorizontal;
        swimV = playerControls.PlayerWater.WaterMovementVertical;
        swimH.Enable();
        swimV.Enable();
    }

    private void OnDisable() {
        move.Disable();
        jump.Disable();
        swimH.Enable();
        swimV.Disable();
    }

    public bool isOnWater() {
        if (Physics2D.OverlapCircle(swimPosition.position, swimRadius, riverWater)) {
            if (onWater == false) {
                AudioManager.instance.Play(swimSound);
            }
            onWater = true;
        }
        else {
            onWater = false;
        }

        return onWater;
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
        if (grounded && !isJumping && !onWater) {
            AudioManager.instance.Play(jumpSound);
            playerAnimator.SetBool("isJumping", true);
            isJumping = true;
            jumpCounter = jumpTime;
            jumpCounter2 = 0;
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    void FlipPlayer() {
        if ((turnedRight && moveDirection < 0f) || (!turnedRight && moveDirection > 0f) ||
            (turnedRight && swimDirectionHorizontal < 0f) || (!turnedRight && swimDirectionHorizontal > 0f)) {
            turnedRight = !turnedRight;
            GetComponent<SpriteRenderer>().flipX = !turnedRight;
            FlipPlayerHat();
        }
    }

    void FlipPlayerHat() {
        Vector3 hatPos = hatInitialPosition;
        Quaternion hatRot = hatInitialRotation;
        if (!turnedRight) {
            hatPos = new Vector3(-hatInitialPosition.x, hatInitialPosition.y, hatInitialPosition.z);
            hatRot = Quaternion.Euler(hatRot.x, hatRot.y, hatRot.z);
        }
        hatObject.GetComponent<SpriteRenderer>().flipX = !turnedRight;
        hatObject.transform.localPosition = hatPos;
        hatObject.transform.rotation = hatRot;
    }

    public void SetHatSprite() {
        if (Hats.selectedHat == null) {
            hatObject.SetActive(false);
        }
        else {
            hatObject.GetComponent<SpriteRenderer>().sprite = Hats.selectedHat.sprite; 
            hatObject.SetActive(true);
        }
    }

    void Update() {
        FlipPlayer();
        isGrounded();
        isOnWater();

        swimDirectionHorizontal = swimH.ReadValue<float>();
        swimDirectionVertical = swimV.ReadValue<float>();
        moveDirection = move.ReadValue<float>();
        if (onWater && !grounded) {
            transform.rotation = Quaternion.identity;
            playerAnimator.SetBool("isSwiming", true);
            playerAnimator.SetBool("isJumping", false);
        }
        else {
            playerAnimator.SetBool("isSwiming", false);
            playerAnimator.SetBool("isWalking", moveDirection != 0);
        }

        if (isJumping) {
            if (jumpCounter > (jumpTime - jumpDelay)) {
                playerAnimator.SetBool("isJumping", true);
                jumpCounter -= Time.deltaTime;
                jumpCounter2 += Time.deltaTime;
                transform.rotation = Quaternion.identity;
            }
            else if (jumpCounter > 0) {
                playerAnimator.SetBool("isJumping", true);
                rb.velocity = Vector2.up * jumpForce;
                jumpCounter -= Time.deltaTime;
                jumpCounter2 += Time.deltaTime;
                transform.rotation = Quaternion.identity;
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
        if (onWater) {
            rb.velocity = new Vector2(swimDirectionHorizontal * swimSpeed, swimDirectionVertical * swimSpeed);
        }
        else {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator playerAnimator;
    
    bool turnedRight = true;
    [SerializeField]
    float moveSpeed = 1.0f;
    float moveDirection = 0;

    private GameInputActions playerControls;
    private InputAction move;

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
    }

    private void OnDisable() {
        move.Disable();
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
    }


    void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
}

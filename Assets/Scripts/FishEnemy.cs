using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishEnemy : MonoBehaviour {

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator enemyAnimator;

    [Header("Ground Movement")]
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float moveDirection = -1;
    [SerializeField] bool turnedRight = false;

    [Header("Wall Detect")]
    public LayerMask wallLayer;
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;
    [SerializeField] private Transform topPosition;
    [SerializeField] private Transform bottomPosition;
    [SerializeField] float wallDetectRadius = 0.2f;
    [SerializeField] private bool verticalMovement;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        if (verticalMovement) {
            sr.flipX = false;
        }
    }

    void FlipHorizontal() {
        bool leftCollided = Physics2D.OverlapCircle(leftPosition.position, wallDetectRadius, wallLayer);
        bool rightCollided = Physics2D.OverlapCircle(rightPosition.position, wallDetectRadius, wallLayer);

        if ((turnedRight && rightCollided) || (!turnedRight && leftCollided)) {
            moveDirection *= -1;
            turnedRight = !turnedRight;
            sr.flipX = turnedRight;
        }
    }

    void FlipVertical() {
        bool topCollided = Physics2D.OverlapCircle(topPosition.position, wallDetectRadius, wallLayer);
        bool bottomCollided = Physics2D.OverlapCircle(bottomPosition.position, wallDetectRadius, wallLayer);

        if ( bottomCollided ||  topCollided) {
            moveDirection *= -1;
        }
    }

    void Update() {
        if (verticalMovement) {
            FlipVertical();
        }
        else {
            FlipHorizontal();
        }
    }


    void FixedUpdate() {
        if (verticalMovement) {
            rb.velocity = new Vector2(rb.velocity.x, moveDirection * moveSpeed);
        }
        else {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }
    }
}

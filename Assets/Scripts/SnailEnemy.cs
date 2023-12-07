using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnailEnemy : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator enemyAnimator;

    [Header("Ground Movement")]
    [SerializeField] float moveSpeed = 1.0f;
    float moveDirection = -1;
    [SerializeField] bool turnedRight = false;

    [Header("Wall Detect")]
    public LayerMask wallLayer;
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;
    [SerializeField] float wallDetecdRadius = 0.01f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }


    void Flip() {
        bool leftCollided = Physics2D.OverlapCircle(leftPosition.position, wallDetecdRadius, wallLayer);
        bool rightCollided = Physics2D.OverlapCircle(rightPosition.position, wallDetecdRadius, wallLayer);
        Debug.Log(turnedRight.ToString() + " " +  leftCollided.ToString() + " " + rightCollided.ToString());

        if ( (turnedRight && rightCollided) || (!turnedRight && leftCollided)) {
            moveDirection *= -1;
            turnedRight = !turnedRight;
            GetComponent<SpriteRenderer>().flipX = turnedRight;
        }
    }

    void Update() {
        Flip();
    }


    void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
}

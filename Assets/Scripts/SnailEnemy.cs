using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnailEnemy : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator enemyAnimator;

    [Header("Ground Movement")]
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float moveDirection = -1;
    [SerializeField] bool turnedRight = false;

    [Header("Wall Detect")]
    public LayerMask wallLayer;
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;
    [SerializeField] float wallDetectRadius = 0.2f;

    [Header("Hide")]
    public LayerMask playerLayer;
    [SerializeField] float hideDetectRadius = 0.2f;
    [SerializeField] private Transform topPosition;
    [SerializeField] private float IFramesDuration;
    private DamagePlayer dp;
    private float lastMoveDirection = -1;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        dp = GetComponent<DamagePlayer>();
    }

    private void TopColision() {
        if (Physics2D.OverlapCircle(topPosition.position, hideDetectRadius, playerLayer)) {
            StartCoroutine(Hide());
        }
    }

    private IEnumerator Hide() {
        lastMoveDirection = moveDirection;
        moveDirection = 0;
        dp.enabled = false;

        enemyAnimator.SetBool("isHide", true);
        yield return new WaitForSeconds(IFramesDuration);

        enemyAnimator.SetBool("isAppear", true);
        yield return new WaitForSeconds(IFramesDuration/2);

        enemyAnimator.SetBool("isHide", false);
        enemyAnimator.SetBool("isAppear", false);
        moveDirection = lastMoveDirection;
        dp.enabled = true;
    }

    void Flip() {
        bool leftCollided = Physics2D.OverlapCircle(leftPosition.position, wallDetectRadius, wallLayer);
        bool rightCollided = Physics2D.OverlapCircle(rightPosition.position, wallDetectRadius, wallLayer);

        if ((turnedRight && rightCollided) || (!turnedRight && leftCollided)) {
            moveDirection *= -1;
            turnedRight = !turnedRight;
            GetComponent<SpriteRenderer>().flipX = turnedRight;
        }
    }

    void Update() {
        Flip();
        TopColision();
    }


    void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
}

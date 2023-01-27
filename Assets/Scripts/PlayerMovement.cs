using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] GameObject playerProjectile;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D myFeetCollider;
    bool isAlive = true;
    bool allowFire = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;

        Run();
        FlipSprite();
        JumpingAnimation();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            return;

        if (value.isPressed)
        {
            myRigidbody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void JumpingAnimation()
    {
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Platform")))
            myAnimator.SetBool("isJumping", true);
        else
            myAnimator.SetBool("isJumping", false);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }
    
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }

    void Die()
    {
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;

        if (!allowFire) return;

        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        allowFire = false;

        Instantiate(playerProjectile, gun.position, transform.rotation);
        yield return new WaitForSeconds(fireRate);
        allowFire = true;
    }

    public float GetRunSpeed()
    {
        return runSpeed;
    }
}

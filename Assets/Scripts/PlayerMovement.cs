using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float shootingRate = 0.5f;
    [SerializeField] GameObject playerProjectile;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D myFeetCollider;
    bool isAlive = true;
    bool allowShooting = true;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

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
            audioPlayer.PlayJumpClip();
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
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("triggerDeath");

            audioPlayer.PlayPlayerDeathClip();
            myRigidbody2D.velocity = Vector3.zero;
            myCapsuleCollider2D.enabled = false;

            FindObjectOfType<DeathAndRespawn>().KillAndRespawn(gameObject);

            Destroy(gameObject, 1f);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;

        if (!allowShooting) return;

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        allowShooting = false;

        audioPlayer.PlayGuitarShootingClip();
        Instantiate(playerProjectile, gun.position, transform.rotation);
        yield return new WaitForSeconds(shootingRate);

        allowShooting = true;
    }

    public float GetRunSpeed()
    {
        return runSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float shootingRate = 0.5f;
    [SerializeField] float invincibilityDuration = 1f;
    [SerializeField] GameObject playerProjectile;
    [SerializeField] Transform gun;
    public int playerNumber;

    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D myFeetCollider;
    SpriteRenderer mySpriteRenderer;

    bool isAlive = true;
    bool allowShooting = true;
    bool isDisabled = false;
    bool isSpriteBlinking = false;
    bool isInvincible = false;

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
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        Controls();
    }

    private static void Controls()
    {
        var myPlayerInput2 = PlayerInput.all[1];
        var myUser2 = myPlayerInput2.user;

        var myPlayerInput1 = PlayerInput.all[0];
        var myUser1 = myPlayerInput1.user;


        myUser1.UnpairDevices();
        myUser1.ActivateControlScheme("Keyboard&Mouse");
        InputUser.PerformPairingWithDevice(Keyboard.current, myUser1);

        myUser2.UnpairDevices();
        myUser2.ActivateControlScheme("Keyboard&Mouse");
        InputUser.PerformPairingWithDevice(Keyboard.current, myUser2);
    }

    // Update is called once per frame

    private void FixedUpdate()
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
        if (isDisabled) return;

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (isDisabled) return;

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

    public void Die()
    {
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")) && !isInvincible)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        isAlive = false;
        myAnimator.SetTrigger("triggerDeath");

        audioPlayer.PlayPlayerDeathClip();
        myRigidbody2D.velocity = Vector3.zero;
        moveInput = Vector2.zero;
        myCapsuleCollider2D.enabled = false;

        FindObjectOfType<DeathAndRespawn>().KillAndRespawn(gameObject, playerNumber);
    }

    public IEnumerator Invincible()
    {
        isInvincible = true;
        InvokeRepeating("BlinkSprite", 0, invincibilityDuration / 32);

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
        CancelInvoke();
    }

    void BlinkSprite()
    {
        isSpriteBlinking ^= true;
        mySpriteRenderer.enabled = isSpriteBlinking;
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;
        if (isDisabled) return;

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

    public void DisableControls()
    {
        isDisabled = true;
    }

    public void TransitionPlayer(Vector3 offCameraSpawnPosition)
    {
        //OLD: If properly transitioning
        //transform.position = new Vector3(offCameraSpawnPosition.x, offCameraSpawnPosition.y, 1f);
        myRigidbody2D.velocity = Vector2.zero;

        transform.position = offCameraSpawnPosition;
        moveInput = Vector2.zero;
    }

    public void EnableControls()
    {
        isDisabled = false;
    }
}

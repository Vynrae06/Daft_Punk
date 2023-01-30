using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Britney : MonoBehaviour
{
    [SerializeField] float health = 10;

    [SerializeField] GameObject shot;
    [SerializeField] Transform gun;
    [SerializeField] float shootCooldown = 1;
    [SerializeField] float beginActionDelay = 5;

    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    Rigidbody2D myRigidbody2D;

    bool isDashing = false;
    bool isAlive = true;

    // Same duration as animation
    // TODO: PROBLEM HAPPENS WHEN BRITNEY DASHES FIRSTS
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashSpeed = 1f;
    [SerializeField] Transform leftPosition;
    [SerializeField] Transform rightPosition;
    Transform nextPosition;

    AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        transform.position = rightPosition.position;
        nextPosition = leftPosition;
    }

    void Update()
    {
        Die();
    }

    private void FixedUpdate()
    {
        Dash();
    }

    void BritneyWarpingAudio()
    {
        audioPlayer.PlayBritneyWarpingClip();
    }

    void BritneyBitchAudio()
    {
        audioPlayer.PlayBritneyBitchClip();
    }

    public void ActionSelection()
    {
        switch (Random.Range(0, 5))
        {
            case 0:
            case 1:
            case 2:
                myAnimator.SetTrigger("triggerShoot");
                break;
            case 3:
            case 4:
                myAnimator.SetTrigger("triggerDash");
                break;
        }
    }

    void Shoot()
    {
        Instantiate(shot, gun.position, transform.rotation);
        audioPlayer.PlayBritneyShootClip();
    }

    void Dash()
    {
        if (isDashing && isAlive)
        {
            Vector3 movement = (nextPosition.position - transform.position).normalized;
            myRigidbody2D.MovePosition(transform.position + movement * dashSpeed * Time.deltaTime);
        }
    }

    public void DashStart()
    {
        isDashing = true;
        audioPlayer.PlayBritneyDashClip();
    }

    public void DashOver()
    {
        SwitchDashNextPosition();
        Flip();
        isDashing = false;
    }

    void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    void SwitchDashNextPosition()
    {
        if (nextPosition == leftPosition)
        {
            nextPosition = rightPosition;
        }
        else if (nextPosition == rightPosition)
        {
            nextPosition = leftPosition;
        }
    }

    public void TakeHit()
    {
        health -= 1;
        if (health > 0)
        {
            audioPlayer.PlayBritneyHitTakenClip();
        }
    }

    void Die()
    {
        if (health <= 0 && isAlive)
        {
            isDashing = false;
            isAlive = false;
            myCapsuleCollider.enabled = false;
            audioPlayer.PlayBritneyDeathClip();
            myAnimator.SetTrigger("triggerDeath");
        }
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1);
    }
}

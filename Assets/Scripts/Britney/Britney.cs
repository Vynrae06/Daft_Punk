using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Britney : MonoBehaviour
{
    [SerializeField] int health = 10;

    [SerializeField] GameObject shot;
    [SerializeField] Transform gun;

    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    Rigidbody2D myRigidbody2D;

    bool isDashing = false;
    bool isAlive = true;

    [SerializeField] float dashSpeed = 1f;
    [SerializeField] Transform leftPosition;
    [SerializeField] Transform rightPosition;

    Transform nextPosition;
    AudioPlayer audioPlayer;

    [SerializeField] ParticleSystem deathVFX;

    HealthBar healthBar;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        HealthBar[] healthBars = Resources.FindObjectsOfTypeAll<HealthBar>();
        healthBar = healthBars[0];
        healthBar.gameObject.SetActive(true);

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

    void EnableCapsuleCollider2D()
    {
        myCapsuleCollider.enabled = true;
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

    void DashCharge()
    {
        audioPlayer.PlayBritneyChargingDashClip();
    }

    void DashStart()
    {
        isDashing = true;
        audioPlayer.PlayBritneyDashClip();
    }

    void DashOver()
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
            healthBar.SetHealth(health);
            GetComponent<SimpleFlash>().Flash();
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
            Instantiate(deathVFX, transform.position, Quaternion.Euler(-90, 0, 0));
            myAnimator.SetTrigger("triggerDeath");
        }
    }

    void DestroyBritney()
    {
        healthBar.gameObject.SetActive(false);
        FindObjectOfType<VictoryText>().ShowText();

        audioPlayer.PlayVictoryTheme();
        Destroy(gameObject);
    }

    void PlayBritneyTheme()
    {
        audioPlayer.PlayBritneyTheme();
    }
}

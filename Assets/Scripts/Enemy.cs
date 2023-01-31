using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float enemyDeathTime = 0.8f;
    [SerializeField] float health = 1;

    bool isAlive = true;

    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D myBoxCollider2D;
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
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!isAlive) return;

        myRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
    }

    public void TakeHit()
    {
        if (isAlive)
        {
            health -= 1;
            if (health > 0)
            {
                audioPlayer.PlayEnemyHitTakenClip();
                GetComponent<SimpleFlash>().Flash();
            }
            else
            {
                Die();
            }
        }
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);
    }

    public void Die()
    {
        myAnimator.SetTrigger("triggerDeath");

        isAlive = false;
        audioPlayer.PlayEnemyDeathClip();
        myRigidbody2D.velocity = Vector3.zero;
        myCapsuleCollider2D.enabled = false;
        myBoxCollider2D.enabled = false;
        Destroy(gameObject, enemyDeathTime);
    }
}
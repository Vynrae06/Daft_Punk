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
    bool canDoAction = true;

    // Same duration as animation
    // TODO: PROBLEM HAPPENS WHEN BRITNEY DASHES FIRSTS
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashSpeed = 1f;
    [SerializeField] Transform leftPosition;
    [SerializeField] Transform rightPosition;
    Transform nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        //StartCoroutine(SpawnBritney());
        nextPosition = rightPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    private void FixedUpdate()
    {
        if (canDoAction && isAlive) StartCoroutine(ActionSelection());
        Dash();
    }

    IEnumerator SpawnBritney()
    {
        yield return new WaitForSeconds(beginActionDelay);
    }

    private IEnumerator ActionSelection()
    {
        canDoAction = false;
        switch (Random.Range(0, 5))
        {
            case 0:
            case 1:
            case 2:
                yield return StartShooting();
                break;
            case 3:
            case 4:
                yield return StartDashing();
                break;
            default:
                yield return new WaitForSeconds(1);
                break;
        }
        canDoAction = true;
    }

    IEnumerator StartShooting()
    {
        myAnimator.SetTrigger("triggerShoot");
        yield return new WaitForSeconds(shootCooldown);
    }

    public void Shoot()
    {
        Instantiate(shot, gun.position, transform.rotation);
    }


    IEnumerator StartDashing()
    {
        myAnimator.SetTrigger("triggerDash");

        yield return new WaitForSeconds(dashDuration);

        SwitchDashNextPosition();
        Flip();

        StartCoroutine(DashCooldown());
    }

    void Dash()
    {
        if (isDashing)
        {
            Vector3 movement = (nextPosition.position - transform.position).normalized;
            myRigidbody2D.MovePosition(transform.position + movement * dashSpeed * Time.deltaTime);
        }
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
    }

    public void ChargeDashDone()
    {
        isDashing = true;
    }

    public void DashOver()
    {
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
    }

    void Die()
    {
        if (health <= 0)
        {
            isAlive = false;
            myAnimator.SetTrigger("triggerDeath");
            //StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1);
    }
}

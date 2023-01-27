using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask layer;
    public float gravityUp = 7;
    public float gravityDown = -5;
    public float jumpHeightMax = 0.4f;

    float gravity;
    float jumpHeight;
    RaycastHit2D hit;

    SpriteRenderer spriteRenderer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OldMovement();
    }

    private void OldMovement()
    {
        TouchingGround();
        Move();
        transform.Translate(transform.up * gravity * Time.fixedDeltaTime);
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Run", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("Run", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpHeight < jumpHeightMax)
            {
                jumpHeight += 1 * Time.deltaTime;
                gravity = gravityUp;
            }
        }
        else
        {
            jumpHeight = 1f;
        }
    }

    bool CheckCollision()
    {
        hit = Physics2D.Raycast(transform.position, transform.up * -1, distance, layer);
        return hit.collider != null;
    }

    void TouchingGround()
    {
        if (CheckCollision())
        {
            jumpHeight = 0;
            gravity = 0;
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", true);
            gravity = gravityDown;
        }
    }
}

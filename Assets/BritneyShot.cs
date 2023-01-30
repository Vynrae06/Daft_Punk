using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BritneyShot : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float selfDestructTime = 1.5f;

    Britney britney;
    float xSpeed;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        britney = FindObjectOfType<Britney>();
        xSpeed = britney.transform.localScale.x * projectileSpeed;
        transform.localScale = new Vector3(britney.transform.localScale.x, transform.localScale.y, transform.localScale.z);

        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.Die();

            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructTime);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}



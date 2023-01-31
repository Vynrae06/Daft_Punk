using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int startingPosition;
    [SerializeField] Transform[] points;

    int nextPointIndex;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPosition].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[nextPointIndex].position) < 0.02f)
        {
            nextPointIndex++;
            if(nextPointIndex == points.Length)
            {
                nextPointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[nextPointIndex].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null, true);
        }
    }
}

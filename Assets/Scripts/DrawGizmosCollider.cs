using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosCollider : MonoBehaviour
{
    [SerializeField] Color color;
    private void OnDrawGizmos()
    {
        BoxCollider2D myBoxCollider2D = GetComponent<BoxCollider2D>();
        Vector2 size = myBoxCollider2D.size;

        Gizmos.color = color;

        Gizmos.DrawWireCube(transform.position , size);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Shot shot;
    public float shootRate = 0.5f;

    bool allowFire = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && allowFire)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        allowFire = false;

        Shot tempBullet = Instantiate(shot, transform.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        allowFire = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAndRespawn : MonoBehaviour
{
    [SerializeField] float respawnTimer;
    [SerializeField] GameObject playerPurple;
    [SerializeField] Transform[] checkPoints;

    int checkPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillAndRespawn(GameObject gameObject)
    {
        StartCoroutine(IKillAndRespawn(gameObject));
    }

    IEnumerator IKillAndRespawn(GameObject gameObject)
    {
        Destroy(gameObject, respawnTimer);

        yield return new WaitForSeconds(respawnTimer);

        GameObject newPlayer = Instantiate(playerPurple, checkPoints[checkPointIndex].position, Quaternion.identity);
        FindObjectOfType<CameraFollow>().followObject = newPlayer;
    }

    void IncrementCheckPoint()
    {
        checkPointIndex++;
    }
}

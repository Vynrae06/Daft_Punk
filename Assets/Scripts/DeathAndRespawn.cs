using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAndRespawn : MonoBehaviour
{
    [SerializeField] float respawnTimer;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] Transform[] checkPoints;

    int checkPointIndex = 0;

    public void KillAndRespawn(GameObject gameObject, int playerNumber)
    {
        StartCoroutine(IKillAndRespawn(gameObject, playerNumber));
    }

    IEnumerator IKillAndRespawn(GameObject gameObject, int playerNumber)
    {
        if (gameObject != null)
        {
            Destroy(gameObject, respawnTimer);

            yield return new WaitForSeconds(respawnTimer);

            GameObject newPlayer = new GameObject();

            switch (playerNumber)
            {
                case 1:
                    newPlayer = Instantiate(player1, checkPoints[checkPointIndex].position, Quaternion.identity);
                    break;
                case 2:
                    newPlayer = Instantiate(player2, checkPoints[checkPointIndex].position, Quaternion.identity);
                    break;
            }
            StartCoroutine(newPlayer.GetComponent<Player>().Invincible());
        }

        // OLD: IF split screen
        //FindObjectOfType<CameraFollow>().followObject = newPlayer;
    }

    public void IncrementCheckPoint()
    {
        checkPointIndex++;
    }
}

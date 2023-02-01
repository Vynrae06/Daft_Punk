using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAndRespawn : MonoBehaviour
{
    [SerializeField] float respawnTimer;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] Transform startCheckpoint;
    Transform checkPoint;

    private void Start()
    {
        checkPoint = startCheckpoint;
    }

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

            switch (playerNumber)
            {
                case 1:
                    GameObject newPlayer1 = Instantiate(player1, checkPoint.position, Quaternion.identity);
                    StartCoroutine(newPlayer1.GetComponent<Player>().Invincible());
                    break;
                case 2:
                    GameObject newPlayer2 = Instantiate(player2, checkPoint.position, Quaternion.identity);
                    StartCoroutine(newPlayer2.GetComponent<Player>().Invincible());
                    break;
            }
        }
    }

    public void SetNewCheckpoint(Transform newCheckpoint)
    {
        checkPoint = newCheckpoint;
    }
}

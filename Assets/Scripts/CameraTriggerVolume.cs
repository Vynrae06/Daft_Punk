using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerVolume : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera nextCamera;
    [SerializeField] Transform nextSpawnPoint;
    [SerializeField] BoxCollider2D transitionSeparator;
    [SerializeField] float transitionPause = 1f;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] float player2TransitionOffset = 1.5f;

    BoxCollider2D myBoxCollider2D;

    private void Start()
    {
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameobject = collision.gameObject;
        if (gameobject.CompareTag("Player"))
        {
            myBoxCollider2D.enabled = false;
            CameraSwitcher.SwitchCamera(nextCamera);

            PlayerMovement player = gameobject.GetComponent<PlayerMovement>();

            StartCoroutine(TransitionScene());
        }
    }

    IEnumerator TransitionScene()
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();

        DestroyPlayerProjectiles();

        yield return StartCoroutine(TransitionPlayers(players));

        transitionSeparator.enabled = true;

        FindObjectOfType<DeathAndRespawn>().IncrementCheckPoint();
    }

    IEnumerator TransitionPlayers(PlayerMovement[] players)
    {
        foreach (PlayerMovement player in players)
        {
            Destroy(player.gameObject);
        }

        List<GameObject> newPlayers = new List<GameObject>();

        newPlayers.Add(Instantiate(player1, new Vector3(nextSpawnPoint.position.x, nextSpawnPoint.position.y, 1f), Quaternion.identity));
        newPlayers.Add(Instantiate(player2, new Vector3(nextSpawnPoint.position.x + player2TransitionOffset, nextSpawnPoint.position.y, 1f), Quaternion.identity));

        foreach(GameObject newPlayer in newPlayers)
        {
            newPlayer.GetComponent<PlayerMovement>().DisableControls();
        }

        yield return new WaitForSeconds(transitionPause);

        foreach (GameObject newPlayer in newPlayers)
        {
            newPlayer.GetComponent<PlayerMovement>().EnableControls();
        }
    }

    void DestroyPlayerProjectiles()
    {
        PlayerProjectile[] playersProjectiles = FindObjectsOfType<PlayerProjectile>();
        foreach(PlayerProjectile playerProjectile in playersProjectiles)
        {
            Destroy(playerProjectile.gameObject);
        }
    }
}

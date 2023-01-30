using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerVolume : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera nextCamera;
    [SerializeField] Transform nextSpawnPoint;
    [SerializeField] BoxCollider2D transitionSeparator;
    [SerializeField] float transitionDelay = 1f;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] float player2TransitionOffset = 1.5f;

    [Header("Britney")]
    [SerializeField] bool spawnBritney;
    [SerializeField] GameObject britney;
    [SerializeField] float spawnBritneyDelay = 1f;

    BoxCollider2D myBoxCollider2D;
    AudioPlayer audioPlayer;

    private void Start()
    {
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameobject = collision.gameObject;
        if (gameobject.CompareTag("Player"))
        {
            myBoxCollider2D.enabled = false;
            CameraSwitcher.SwitchCamera(nextCamera);

            Player player = gameobject.GetComponent<Player>();

            if (spawnBritney)
            {
                audioPlayer.StopMusic(transitionDelay);
                Invoke("SpawnBritney", spawnBritneyDelay);
            }

            StartCoroutine(TransitionScene());
        }
    }

    IEnumerator TransitionScene()
    {
        Player[] players = FindObjectsOfType<Player>();

        DestroyPlayerProjectiles();

        yield return StartCoroutine(TransitionPlayers(players));

        transitionSeparator.enabled = true;

        FindObjectOfType<DeathAndRespawn>().IncrementCheckPoint();
    }

    void SpawnBritney()
    {
        Instantiate(britney);
    }

    IEnumerator TransitionPlayers(Player[] players)
    {
        foreach (Player player in players)
        {
            if (player != null)
            {
                Destroy(player.gameObject);
            }
        }

        List<GameObject> newPlayers = new List<GameObject>();

        newPlayers.Add(Instantiate(player1, new Vector3(nextSpawnPoint.position.x, nextSpawnPoint.position.y, 1f), Quaternion.identity));
        newPlayers.Add(Instantiate(player2, new Vector3(nextSpawnPoint.position.x + player2TransitionOffset, nextSpawnPoint.position.y, 1f), Quaternion.identity));

        foreach(GameObject newPlayer in newPlayers)
        {
            newPlayer.GetComponent<Player>().DisableControls();
        }

        yield return new WaitForSeconds(transitionDelay);

        foreach (GameObject newPlayer in newPlayers)
        {
            newPlayer.GetComponent<Player>().EnableControls();
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

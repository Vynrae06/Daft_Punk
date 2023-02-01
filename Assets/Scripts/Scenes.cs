using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    bool gameLoaded = false;

    void Update()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && Input.GetKey(KeyCode.Return) && !gameLoaded)
        {
            gameLoaded = true;
            FindObjectOfType<AudioPlayer>().PlayGameStart();
            StartCoroutine(LoadGameCoroutine());
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator LoadGameCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
}

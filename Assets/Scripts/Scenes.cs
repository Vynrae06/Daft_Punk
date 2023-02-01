using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    bool gameLoaded = false;
    bool loading = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            LoadGame();
            LoadLoadingScreen();
        }
    }

    public void LoadGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && !gameLoaded)
        {
            gameLoaded = true;
            FindObjectOfType<AudioPlayer>().PlayGameStart();
            StartCoroutine(LoadGameCoroutine());
        }
    }

    void LoadLoadingScreen()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && !loading)
        {
            loading = true;
            LoadingScreen();
        }
    }

    public void LoadingScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(2);
    }

    IEnumerator LoadGameCoroutine()
    {
        FindObjectOfType<AudioPlayer>().StopMusic(1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
}

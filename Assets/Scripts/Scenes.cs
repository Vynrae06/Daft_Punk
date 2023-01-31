using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    bool gameLoaded = false;
    // Update is called once per frame
    void Update()
    {
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && Input.GetKey(KeyCode.Return) && !gameLoaded)
        {
            gameLoaded = true;
            FindObjectOfType<AudioPlayer>().PlayGameStart();
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

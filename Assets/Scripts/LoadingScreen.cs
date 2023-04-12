using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] float loadMainMenuDelay;
    [SerializeField] GameObject SceneM;
    AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLoading());
        //audioPlayer.PlayLoadingTheme();
    }

    IEnumerator StartLoading()
    {
        float time = 0;
        while (time < loadMainMenuDelay)
        {
            GetComponent<Slider>().value = Mathf.Lerp(0, 1, time / loadMainMenuDelay);
            time += Time.deltaTime;
            yield return null;
        }
        SceneM.transform.GetComponent<Scenes>().LoadMainMenu();
    }
}

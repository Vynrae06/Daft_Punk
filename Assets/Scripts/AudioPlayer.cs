using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip[] playerGuitarShotClips;
    [SerializeField] [Range(0f, 1f)] float playerShotVolume;

    [Header("Death")]
    [SerializeField] AudioClip enemyDeathClip;
    [SerializeField] [Range(0f, 1f)] float enemyDeathVolume;

    [SerializeField] AudioClip playerDeathClip;
    [SerializeField] [Range(0f, 1f)] float playerDeathClipVolume;

    [Header("Jumping")]
    [SerializeField] AudioClip jumpingClip;
    [SerializeField] [Range(0f, 1f)] float jumpingClipVolume;



    public void PlayGuitarShootingClip()
    {
        if(playerGuitarShotClips != null)
        {
            int playerGuitarShotClipIndex = Random.Range(0, playerGuitarShotClips.Length);
            AudioSource.PlayClipAtPoint(playerGuitarShotClips[playerGuitarShotClipIndex], Camera.main.transform.position, playerShotVolume);
        }
    }

    public void PlayPlayerDeathClip()
    {
        PlaySingleClip(playerDeathClip, playerDeathClipVolume);
    }

    public void PlayEnemyDeathClip()
    {
        PlaySingleClip(enemyDeathClip, enemyDeathVolume);
    }

    public void PlayJumpClip()
    {
        PlaySingleClip(jumpingClip, jumpingClipVolume);
    }

    void PlaySingleClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, volume);
    }
}

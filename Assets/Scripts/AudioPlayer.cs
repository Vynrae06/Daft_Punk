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

    [Header("Britney")]
    [SerializeField] AudioClip jumpingBritneyClip;
    [SerializeField][Range(0f, 1f)] float jumpingBritneyClipVolume;

    [SerializeField] AudioClip shootingBritneyClip;
    [SerializeField][Range(0f, 1f)] float shootingBritneyClipVolume;

    [SerializeField] AudioClip dashingBritneyClip;
    [SerializeField][Range(0f, 1f)] float dashingBritneyClipVolume;

    [SerializeField] AudioClip chargingDashBritneyClip;
    [SerializeField][Range(0f, 1f)] float chargingDashBritneyClipVolume;

    [SerializeField] AudioClip warpingingBritneyClip;
    [SerializeField][Range(0f, 1f)] float warpingBritneyClipVolume;

    [SerializeField] AudioClip bitchBritneyClip;
    [SerializeField][Range(0f, 1f)] float bitchBritneyClipVolume;

    [SerializeField] AudioClip hitTakenBritneyClip;
    [SerializeField][Range(0f, 1f)] float hitTakenBritneyClipVolume;

    [SerializeField] AudioClip deathBritneyClip;
    [SerializeField][Range(0f, 1f)] float deathBritneyClipVolume;

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

    public void PlayJumpClip()
    {
        PlaySingleClip(jumpingClip, jumpingClipVolume);
    }

    public void PlayEnemyDeathClip()
    {
        PlaySingleClip(enemyDeathClip, enemyDeathVolume);
    }

    // BRITNEY SECTION

    public void PlayBritneyWarpingClip()
    {
        PlaySingleClip(warpingingBritneyClip, warpingBritneyClipVolume);
    }

    public void PlayBritneyBitchClip()
    {
        PlaySingleClip(bitchBritneyClip, bitchBritneyClipVolume);
    }

    public void PlayBritneyChargingDashClip()
    {
        PlaySingleClip(chargingDashBritneyClip, chargingDashBritneyClipVolume);
    }

    public void PlayBritneyDashClip()
    {
        PlaySingleClip(dashingBritneyClip, dashingBritneyClipVolume);
    }

    public void PlayBritneyHitTakenClip()
    {
        PlaySingleClip(hitTakenBritneyClip, hitTakenBritneyClipVolume);
    }

    public void PlayBritneyDeathClip()
    {
        PlaySingleClip(deathBritneyClip, deathBritneyClipVolume);
    }

    public void PlayBritneyShootClip()
    {
        PlaySingleClip(shootingBritneyClip, shootingBritneyClipVolume);
    }

    // GROUPING FUNCTIONS

    void PlaySingleClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, volume);
    }
}

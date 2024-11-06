using ScriptableObjects;
using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{

    [SerializeField] private SoundSO slashSound;
    [SerializeField] private SoundSO gotHitSound;
    [SerializeField] private SoundSO LostSound;
    [SerializeField] private SoundSO walkingSound;
    [SerializeField] private SoundSO dashSound;
    [SerializeField] private SoundSO jumpSound;



    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerCombo playerCombo;
    [SerializeField] private PlayerMovement playerMovement;

    void Start()
    {
        playerCombo.OnSlash += PlayerSlash;
        playerHealth.gotHit += PlayGotHit;
        playerHealth.OnDead += PlayDead;
        playerMovement.onDash += PlayDash;
        playerMovement.onJump += PlayJump;
    }

    public void PlayerSlash()
    {
        SoundPooling.Instance.CreateSound(slashSound, 0, 0);
    }
    public void PlayGotHit()
    {
        SoundPooling.Instance.CreateSound(gotHitSound,0 ,0);
    }

    public void PlayDead()
    {
        SoundPooling.Instance.CreateSound (LostSound, 0 ,0);
    }

    public void PlayWalking()
    {
        SoundPooling.Instance.CreateSound(walkingSound,0,0);
    }

    public void PlayDash()
    {
        SoundPooling.Instance.CreateSound(dashSound, 0, 0);
    }

    public void PlayJump()
    {
        SoundPooling.Instance.CreateSound(jumpSound, 0, 0);
    }

}

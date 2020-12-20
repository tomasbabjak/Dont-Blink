using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource animationPlayerSound;
    private AudioSource eyeThrowSound;
    private AudioSource energyDrinkSound;

    private EyeItemController eyeItemController;
    private SpeedPowerUpController speedPowerUpController;

    public AudioClip[] footsteps;
    public AudioClip throwItem;
    public AudioClip openCan;

    void Start()
    {

        eyeItemController = GetComponent<EyeItemController>();
        eyeItemController.onEyeThrow += throwItemSound;

        speedPowerUpController = GetComponent<SpeedPowerUpController>();
        speedPowerUpController.onDrinkUse += openCanSound;

        animationPlayerSound = gameObject.AddComponent<AudioSource>();
        animationPlayerSound.volume = 0.1f;

        eyeThrowSound = gameObject.AddComponent<AudioSource>();
        eyeThrowSound.clip = throwItem;

        energyDrinkSound = gameObject.AddComponent<AudioSource>();
        energyDrinkSound.clip = openCan;

    }

    private void PlayerFootstepSound()
    {
        animationPlayerSound.clip = footsteps[UnityEngine.Random.Range(0, footsteps.Length)];
        animationPlayerSound.Play();
    }


    private void throwItemSound()
    {
        eyeThrowSound.Play();
    }

    private void openCanSound()
    {
        energyDrinkSound.Play();
    }

}

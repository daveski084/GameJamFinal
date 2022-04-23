using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour
{
    private AudioSource [] sounds;
  
    private AudioSource bottleSFX;
    private AudioSource safeSound;
    private AudioSource barrierSound;
    private AudioSource chaseSound;




    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();

        bottleSFX = sounds[0];
        safeSound = sounds[1];
        barrierSound = sounds[2];
        chaseSound = sounds[3];
    }

    public void PlayBottleSound()
    {
        bottleSFX.Play();
    }

    public void PlaySafeSound()
    {
        safeSound.Play();
    }
   
    public void PlayBarrierSound()
    {
        barrierSound.Play(); 
    }

    public void PlayChaseMusic()
    {
        chaseSound.Play(); 
    }

    public void StopChaseMusic()
    {
        chaseSound.Stop(); 
    }

   
}

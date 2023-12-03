using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource plate1Sound;
    [SerializeField] private AudioSource plate2Sound;
    [SerializeField] private AudioSource plate3Sound;
    [SerializeField] private AudioSource plate4Sound;
    [SerializeField] private AudioSource level1KeyPickupSound;
    [SerializeField] private AudioSource bouncePadSound;
    [SerializeField] private AudioSource memorySound;

    public void PlayPlate1()
    {
        plate1Sound.Play();
    }
    
    public void PlayPlate2()
    {
        plate2Sound.Play();
    }
    
    public void PlayPlate3()
    {
        plate3Sound.Play();
    }
    
    public void PlayPlate4()
    {
        plate4Sound.Play();
    }

    public void PlayLevel1KeyPickup()
    {
        level1KeyPickupSound.Play();
    }

    public void PlayBouncePad()
    {
        bouncePadSound.Play();
    }
    public void PlayMemory()
    {
        memorySound.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public AudioSource mySounds;
    public AudioClip hoverSound;
    public AudioClip pressedSound;

    public void HoverSound()
    {
        mySounds.PlayOneShot(hoverSound);
    }

    public void PressedSound()
    {
        mySounds.PlayOneShot(pressedSound);
    }
}

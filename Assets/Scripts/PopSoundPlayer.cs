using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopSoundPlayer : MonoBehaviour
{
    public AudioSource audioPlayer;

    void OnCollisionEnter(Collision c)
    {
        if (c.impulse.magnitude > 0.25f)
        {
            audioPlayer.Play();
        }
    }
}

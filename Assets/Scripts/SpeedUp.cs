using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public AudioSource audioPlayer;
    
    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<Rigidbody>() != null) 
        {
            audioPlayer.Play();
            c.GetComponent<Rigidbody>().AddForce(c.transform.forward * 300, ForceMode.Impulse);
        }
    }
}

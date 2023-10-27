using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public AudioSource audioPlayer;
    
    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<Rigidbody>() != null) {
            audioPlayer.Play();
            c.GetComponent<Rigidbody>().AddRelativeForce(transform.forward*500, ForceMode.Impulse);
        }
    }
}

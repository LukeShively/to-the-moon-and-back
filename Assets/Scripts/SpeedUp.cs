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
            c.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
        }
        
        // if (c.GetComponent<Rigidbody>() != null)
        // {
        //     audioPlayer.Play();
        //     Rigidbody playerRigidbody = c.GetComponent<Rigidbody>();
        //     
        //     // // Get the player's current position
        //     // Vector3 playerPosition = playerRigidbody.transform.position;
        //     // // Specify an offset distance in the forward direction
        //     // float offsetDistance = 2.0f; // You can change this value
        //     // // Calculate the position in the forward direction
        //     // Vector3 positionInFront = playerPosition + transform.forward * offsetDistance;
        //     // positionInFront.y = 0f;
        //     // // Vector3 rotatedForward = (Quaternion.Euler(0, 180, 0) * transform.forward).normalized;
        //     // playerRigidbody.AddForce(positionInFront * 50, ForceMode.Impulse);
        //     // c.GetComponent<Rigidbody>().AddForce(c.transform.forward*1000, ForceMode.Impulse);
        //
        //     Vector3 newDirection = Quaternion.Euler(0, 180, 0) * transform.forward;
        //     
        //     playerRigidbody.AddRelativeForce(newDirection * 50, ForceMode.Impulse);
        // }
    }
}

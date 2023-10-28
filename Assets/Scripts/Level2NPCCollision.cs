using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level2NPCCollision : MonoBehaviour
{
    private Level2NPCController _level2NpcController;

    void Start()
    {
        _level2NpcController = transform.parent.gameObject.GetComponent<Level2NPCController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if collided with the player
        if (other.gameObject.CompareTag("Player"))
        {
            _level2NpcController.TriggerPlayerExplainingDialogue();
        }
    }

}
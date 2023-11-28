using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPlayerCollision : MonoBehaviour
{
    private Level1NPCController _level1NpcController;

    void Start()
    {
        _level1NpcController = transform.parent.gameObject.GetComponent<Level1NPCController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // if collided with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // if currently in initial state, then change to be talking to player
            if (_level1NpcController.state == Level1NPCController.AIStateMachine.DefaultWaypoint)
            {
                _level1NpcController.TriggerPlayerTalkingDialogue();
            } 
            // if currently in the state where moved to new puzzle waypoint (and the path is over - meaning the walk is complete)
            if (_level1NpcController.state == Level1NPCController.AIStateMachine.PuzzleWaypoint 
                && (_level1NpcController.navMeshAgent.remainingDistance <= 0.1 && !_level1NpcController.navMeshAgent.pathPending))
            {
                _level1NpcController.TriggerPlayerExplainingDialogue();
            }
        }
    }
    
}

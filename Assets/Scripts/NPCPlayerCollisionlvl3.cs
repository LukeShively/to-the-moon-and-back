using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlayerCollisionlvl3 : MonoBehaviour
{
    private Level3NPCController _level3NpcController;

    void Start()
    {
        _level3NpcController = transform.parent.gameObject.GetComponent<Level3NPCController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // if collided with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // if currently in initial state, then change to be talking to player
            if (_level3NpcController.state == Level3NPCController.AIStateMachine.DefaultWaypoint)
            {
                _level3NpcController.TriggerPlayerTalkingDialogue();
            } 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyCollision : MonoBehaviour
{
    private int state = 0;
    [SerializeField] private GameObject player;
    private PlayerController _playerController;
    private Level4NPCController _level4NpcController;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = player.gameObject.GetComponent<PlayerController>();
        _level4NpcController = transform.parent.gameObject.GetComponent<Level4NPCController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerController.coins == 5) {
            state = 1;
        }
        // if collided with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // if currently in initial state, then change to be talking to player
            if (state == 0)
            {
                _playerController.StopMovement();
                _level4NpcController.TriggerFirstDialogue();
            } 
            // if currently in the state where moved to new puzzle waypoint (and the path is over - meaning the walk is complete)
            if (state == 1)
            {
                _playerController.StopMovement();
                _level4NpcController.TriggerGivenCoinsDialogue();
            }
        }
    }
}

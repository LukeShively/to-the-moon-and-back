using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyCollision : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private PlayerController _playerController;
    private Level4NPCController _level4NpcController;
    
    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = player.gameObject.GetComponent<PlayerController>();
        _level4NpcController = transform.parent.gameObject.GetComponent<Level4NPCController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerController.coins == 5) {
            state = 2;
            _playerController.coins++;
        }
        // if collided with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // if currently in initial state, then change to be talking to player
            if (state == 0)
            {
               _level4NpcController.setState(0);
               state = 1;
            } 
            // if currently in the state where moved to new puzzle waypoint (and the path is over - meaning the walk is complete)
            if (state == 2)
            {
               _level4NpcController.setState(1);
               state = 1; 
            }
        }
    }
}

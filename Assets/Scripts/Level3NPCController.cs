using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level3NPCController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    
    // AI state machine
    public enum AIStateMachine
    {
        DefaultWaypoint,
        TalkingToPlayer,
    }
    public AIStateMachine state;

    [SerializeField] private bool helpingPlayer;
    [SerializeField] private GameObject dialogueBoxPanel;
    [SerializeField] private GameObject talkingDialogueTMP;
    [SerializeField] private bool doneTalking;
    [SerializeField] private GameObject player;
    private PlayerController _playerController;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
        state = AIStateMachine.DefaultWaypoint; // start with default waypoint
        dialogueBoxPanel.SetActive(false);
        talkingDialogueTMP.SetActive(false);
        _playerController = player.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        // ai state machine logic
        switch (state)
        {
            case AIStateMachine.DefaultWaypoint:
                doneTalking = false;
                // transition to talking (dialogue) if player has interacted with NPC
                if (helpingPlayer)
                {
                    state = AIStateMachine.TalkingToPlayer;
                }
                break;
            case AIStateMachine.TalkingToPlayer:
                //_playerController.StopMovement();
                helpingPlayer = true;
                if (doneTalking)
                {
                    //_playerController.StartMovement();
                }
                break;
            default:
                break;
        }
    }

    public void EndTalking()
    {
        doneTalking = true;
    }

    public void TriggerPlayerTalkingDialogue()
    {
        // collided with player (from child object trigger)
        helpingPlayer = true;
        dialogueBoxPanel.SetActive(true);
        talkingDialogueTMP.SetActive(true);
    }

}

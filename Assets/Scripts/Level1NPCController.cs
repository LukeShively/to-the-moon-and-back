using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level1NPCController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    private Animator _animator;

    public GameObject[] defaultWaypoints;
    public GameObject puzzleWaypoint;
    public int currWaypoint;
    
    // AI state machine
    public enum AIStateMachine
    {
        DefaultWaypoint, // moving back and forth between 2 static waypoints
        PuzzleWaypoint, // moving towards the waypoint located near the puzzle
        TalkingToPlayer, // talking to the player initially
        ExplainingToPlayer // explaining the puzzle to the player after moved towards the 'puzzle waypoint'
    }
    public AIStateMachine state;

    [SerializeField] private bool helpingPlayer;
    [SerializeField] private GameObject dialogueBoxPanel;
    [SerializeField] private GameObject talkingDialogueTMP;
    [SerializeField] private GameObject explainingDialogueTMP;
    [SerializeField] private float speedToPuzzle;
    [SerializeField] private bool doneTalking;
    [SerializeField] private GameObject player;
    private PlayerController _playerController;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        currWaypoint = -1; // prepare for first waypoint
        SetNextWaypoint();
        state = AIStateMachine.DefaultWaypoint; // start with default waypoints
        dialogueBoxPanel.SetActive(false);
        talkingDialogueTMP.SetActive(false);
        explainingDialogueTMP.SetActive(false);
        _playerController = player.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        // enable animation
        _animator.SetFloat("speed", navMeshAgent.speed);
        
        if (navMeshAgent.remainingDistance <= 0f && !navMeshAgent.pathPending)
        {
            // initiate next waypoint
            SetNextWaypoint();
        }
        // ai state machine logic
        switch (state)
        {
            case AIStateMachine.PuzzleWaypoint:
                doneTalking = false;
                _playerController.StartMovement();
                helpingPlayer = true;
                // set speed, so can walk to new puzzle waypoint
                navMeshAgent.speed = speedToPuzzle;
                break;
            case AIStateMachine.DefaultWaypoint:
                doneTalking = false;
                _playerController.StartMovement();
                // transition to talking (dialogue) if player has interacted with NPC
                if (helpingPlayer)
                {
                    state = AIStateMachine.TalkingToPlayer;
                }
                break;
            case AIStateMachine.TalkingToPlayer:
                _playerController.StopMovement();
                helpingPlayer = true;
                navMeshAgent.ResetPath();
                navMeshAgent.speed = 0f;
                // transition to puzzle waypoint if dialogue has finished
                if (doneTalking)
                {
                    state = AIStateMachine.PuzzleWaypoint;
                }
                break;
            case AIStateMachine.ExplainingToPlayer:
                _playerController.StopMovement();
                helpingPlayer = true;
                navMeshAgent.speed = 0f;
                // allow the player to move again when dialogue is finished
                if (doneTalking)
                {
                    _playerController.StartMovement();
                }
                break;
            default:
                break;
        }
    }

    private void SetNextWaypoint()
    {
        if (defaultWaypoints.Length == 0 || puzzleWaypoint == null)
        {
            // error handling for empty array
            Debug.LogError("There are no Waypoints initialized.");
            return;
        }

        if (helpingPlayer)
        {
            // go to puzzle waypoint after interaction with player
            navMeshAgent.SetDestination(puzzleWaypoint.transform.position);
        }
        else
        {
            if (currWaypoint == defaultWaypoints.Length - 1)
            {
                // loop back to 0
                currWaypoint = 0;
            }
            else
            {
                currWaypoint++;
            }
            navMeshAgent.SetDestination(defaultWaypoints[currWaypoint].transform.position);
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

    public void TriggerPlayerExplainingDialogue()
    {
        // collided with player (from child object trigger)
        helpingPlayer = true;
        state = AIStateMachine.ExplainingToPlayer;
        dialogueBoxPanel.SetActive(true);
        explainingDialogueTMP.SetActive(true);
    }
}

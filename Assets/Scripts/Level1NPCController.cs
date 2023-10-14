using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level1NPCController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    // private Animator animator;

    public GameObject[] defaultWaypoints;
    public GameObject puzzleWaypoint;
    public int currWaypoint;
    // public GameObject trackingIndicator;
    
    // AI state machine
    public enum AIStateMachine
    {
        DefaultWaypoint,
        PuzzleWaypoint,
        TalkingToPlayer,
        ExplainingToPlayer
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
        // animator = GetComponent<Animator>();
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
        // animator.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        
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
                // TODO need to keep this state until end
                navMeshAgent.speed = speedToPuzzle;
                // if (navMeshAgent.remainingDistance <= 0f && !navMeshAgent.pathPending)
                // {
                //     state = AIStateMachine.ExplainingToPlayer;
                // }
                break;
            case AIStateMachine.DefaultWaypoint:
                doneTalking = false;
                _playerController.StartMovement();
                // helpingPlayer = false;
                // TODO check for transition to talking player state
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
                // TODO check for transition to puzzle waypoint state
                if (doneTalking)
                {
                    state = AIStateMachine.PuzzleWaypoint;
                }
                break;
            case AIStateMachine.ExplainingToPlayer:
                _playerController.StopMovement();
                helpingPlayer = true;
                navMeshAgent.speed = 0f;
                if (doneTalking)
                {
                    _playerController.StartMovement();
                }
                break;
            default:
                break;
        }
        
        // // update position of tracking indicator object
        // trackingIndicator.transform.position = new Vector3(navMeshAgent.destination.x,
        //     trackingIndicator.transform.position.y, navMeshAgent.destination.z);
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

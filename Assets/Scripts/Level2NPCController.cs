using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level2NPCController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    // TODO add animations
    // private Animator animator;

    [SerializeField] private GameObject dialogueBoxPanel;
    [SerializeField] private GameObject explainingDialogueTMP;
    [SerializeField] private bool doneTalking;
    [SerializeField] private GameObject player;
    //private PlayerController _playerController;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
        dialogueBoxPanel.SetActive(false);
        explainingDialogueTMP.SetActive(false);
        //_playerController = player.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        // enable animation
        // animator.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }

    public void EndTalking()
    {
        doneTalking = true;
    }

    public void TriggerPlayerExplainingDialogue()
    {
        // collided with player (from child object trigger)
        dialogueBoxPanel.SetActive(true);
        explainingDialogueTMP.SetActive(true);
    }
}
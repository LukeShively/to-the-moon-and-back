using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level2NPCController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBoxPanel;
    [SerializeField] private GameObject explainingDialogueTMP;
    [SerializeField] private bool doneTalking;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject level2HUDText;
    private PlayerController _playerController;
    

    void Start()
    {
        doneTalking = true; // set to true (since haven't interacted with level yet)
        dialogueBoxPanel.SetActive(false);
        explainingDialogueTMP.SetActive(false);
        level2HUDText.SetActive(false);
        _playerController = player.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!doneTalking)
        {
            _playerController.StopMovement();
        }
        else
        {
            _playerController.StartMovement();
        }
    }

    public void EndTalking()
    {
        doneTalking = true;
        level2HUDText.SetActive(true);
        
    }

    public void TriggerPlayerExplainingDialogue()
    {
        // collided with player (from child object trigger)
        doneTalking = false;
        dialogueBoxPanel.SetActive(true);
        explainingDialogueTMP.SetActive(true);
    }
}
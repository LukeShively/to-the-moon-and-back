using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level3NPCController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBoxPanel;
    [SerializeField] private GameObject talkingDialogueTMP;
    [SerializeField] private bool doneTalking;
    [SerializeField] private GameObject player;
    private PlayerController _playerController;
    private bool hasInteracted;

    void Start()
    {
        hasInteracted = false;
        doneTalking = true; // since hasn't been triggered yet
        dialogueBoxPanel.SetActive(false);
        talkingDialogueTMP.SetActive(false);
        _playerController = player.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        // suspend movement when in dialogue
        if (!doneTalking)
        {
            _playerController.StopMovement();
        }
        else if (hasInteracted)
        {
            // since 2 NPCs, need to sure other NPC not overriding the StopMovement request of the first NPC
            _playerController.StartMovement();
        }
    }

    public void EndTalking()
    {
        doneTalking = true;
    }

    public void TriggerPlayerExplainingDialogue()
    {
        // collided with player (from child object trigger)
        hasInteracted = true;
        doneTalking = false;
        dialogueBoxPanel.SetActive(true);
        talkingDialogueTMP.SetActive(true);
    }

}

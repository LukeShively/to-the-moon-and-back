using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4NPCController : MonoBehaviour
{
    [SerializeField] private bool doneTalking;
    [SerializeField] private GameObject dialogueBoxPanel;
    [SerializeField] private GameObject firstDialogue;
    [SerializeField] private GameObject givenCoinsDialogue;
    //[SerializeField] private bool doneTalking;
    [SerializeField] private GameObject player;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
    dialogueBoxPanel.SetActive(false);
    firstDialogue.SetActive(false);
    _playerController = player.gameObject.GetComponent<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (doneTalking)
            {
                _playerController.StartMovement();
            }
    }
    public void TriggerFirstDialogue()
    {
        // collided with player (from child object trigger)
        doneTalking = false;
        _playerController.StopMovement();
        dialogueBoxPanel.SetActive(true);
        firstDialogue.SetActive(true);
        
    }

    public void TriggerGivenCoinsDialogue()
    {
    
        // collided with player (from child object trigger)
        _playerController.StopMovement();
        dialogueBoxPanel.SetActive(true);
        givenCoinsDialogue.SetActive(true);
    }
    public void EndTalking()
    {
        doneTalking = true;
    }
}

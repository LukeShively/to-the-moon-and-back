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
    [SerializeField] private GameObject cloudKey;

    private PlayerController _playerController;
    public int state = -1;

    // Start is called before the first frame update
    void Start()
    {
        doneTalking = false;
        cloudKey.SetActive(false);
        dialogueBoxPanel.SetActive(false);
        firstDialogue.SetActive(false);
        givenCoinsDialogue.SetActive(false);
        _playerController = player.gameObject.GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case (0):
                _playerController.StopMovement();
                dialogueBoxPanel.SetActive(true);
                firstDialogue.SetActive(true);
                if (doneTalking) {
                    _playerController.StartMovement();
                    Debug.Log("wat");
                    state = 3;
                    firstDialogue.SetActive(false);
                    dialogueBoxPanel.SetActive(false);
                }
                break;
            case (1):
                _playerController.StopMovement();
                dialogueBoxPanel.SetActive(true);
                givenCoinsDialogue.SetActive(true);
                if (doneTalking) {
                    _playerController.StartMovement();
                    Debug.Log("wat2");
                    state = 3;
                    cloudKey.SetActive(true);
                    givenCoinsDialogue.SetActive(false);
                    dialogueBoxPanel.SetActive(false);
                }
                break;
            default:
                doneTalking = false;
                break;
        }
        /*if (state == 0) {
            
            _playerController.StopMovement();
            dialogueBoxPanel.SetActive(true);
            firstDialogue.SetActive(true);
            if (doneTalking) {
                _playerController.StartMovement();
                Debug.Log("wat");
                state = 2;
            }
        }
        if (state == 1) {
            _playerController.StopMovement();
            dialogueBoxPanel.SetActive(true);
            givenCoinsDialogue.SetActive(true);
            if (doneTalking) {
                _playerController.StartMovement();
                Debug.Log("wat2");
                state = 2;
            }
        }
        */
           

    }
    /*public void TriggerFirstDialogue()
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
    */
    public void EndTalking()
    {
        doneTalking = true;
    }
    public void setState(int x) {
        state = x;
    }
}

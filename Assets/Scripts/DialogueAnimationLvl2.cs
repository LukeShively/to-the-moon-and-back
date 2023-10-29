﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueAnimationLvl2 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBoxPanel;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private float typingSpeed = 30f;

    [SerializeField] private float currentTypingSpeed;
    [SerializeField] private int currentElementIndex;
    [SerializeField] private string currentText;
    [SerializeField] private bool isTyping;

    [SerializeField] private GameObject level2NPC;
    private Level2NPCController _level2NpcController;

    private void Start()
    {
        dialogueText.SetText("");
        isTyping = false;
        currentText = "";
        currentElementIndex = 0;
        currentTypingSpeed = typingSpeed;
        if (dialogueLines.Length > 0)
        {
            // start typing effect
            StartCoroutine(TypeDialogue(dialogueLines[currentElementIndex]));
        }
        _level2NpcController = level2NPC.GetComponent<Level2NPCController>();
    }

    private void Update()
    {
        // allow for next dialogue line to be triggered
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            currentTypingSpeed = typingSpeed;
            NextDialogueElement();
        }

        // Check for a left mouse button click to skip to the end of the current line
        if (Input.GetKeyUp(KeyCode.Mouse0) && isTyping)
        {
            // Skip to the end of the current line (by making it very fast)
            currentTypingSpeed = typingSpeed * 100f;
        }
    }

    private void NextDialogueElement()
    {
        if (currentElementIndex < dialogueLines.Length - 1)
        {
            // keep going through dialogue lines
            currentElementIndex++;
            currentText = "";
            StartCoroutine(TypeDialogue(dialogueLines[currentElementIndex]));
        }
        else
        {
            // Handle the end of the dialogue
            _level2NpcController.EndTalking();
            Debug.Log("Done Talking");
            dialogueBoxPanel.SetActive(false);
            dialogueText.enabled = false;
            gameObject.SetActive(false);
            currentElementIndex = 0;
        }
    }

    // typing visual effect
    private IEnumerator TypeDialogue(string text)
    {
        isTyping = true;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            currentText += text[charIndex];
            dialogueText.text = currentText;
            charIndex++;

            yield return new WaitForSeconds(1 / currentTypingSpeed);
        }
        isTyping = false;
    }
}
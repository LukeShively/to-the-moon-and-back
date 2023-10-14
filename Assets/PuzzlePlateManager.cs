using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlateManager : MonoBehaviour
{
    [SerializeField] private int plateNumber;
    private PuzzleManager _puzzleManager;
    
    void Start()
    {
        _puzzleManager = transform.parent.gameObject.GetComponent<PuzzleManager>();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _puzzleManager.TriggerPlate(plateNumber);
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private int totalPlates;
    private int[] _platesActivated;
    private readonly int[] _correctOrder = { 1, 2, 3, 4 };
    [SerializeField] private int _currentPlate;
    [SerializeField] private GameObject level1Key;
    [SerializeField] private GameObject audioManager;
    private AudioManager _audioManager;
    
    private void Start()
    {
        _platesActivated = new int[totalPlates];
        _currentPlate = 0;
        level1Key.SetActive(false);
        _audioManager = audioManager.GetComponent<AudioManager>();
    }

    public void TriggerPlate(int plateNumber)
    {
        Debug.Log("Stepped on plate " + plateNumber);
        _platesActivated[_currentPlate] = plateNumber;
        _currentPlate++;
    }

    public void PlayPlateSound(int plateNumber)
    {
        switch (plateNumber)
        {
            case 1:
                _audioManager.PlayPlate1();
                break;
            case 2:
                _audioManager.PlayPlate2();
                break;
            case 3:
                _audioManager.PlayPlate3();
                break;
            case 4:
                _audioManager.PlayPlate4();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (_currentPlate == totalPlates)
        {
            Debug.Log("Finished puzzle, checking solution...");
            if (ValidSolution())
            {
                Debug.Log("You win.");
                // TODO enable the key
                RestartGame();
                level1Key.SetActive(true);
            }
            else
            {
                _platesActivated = new int[totalPlates];
                Debug.Log("Wrong, you lose.");
                RestartGame();
            }
        }
    }

    private bool ValidSolution()
    {
        for (int i = 0; i < _platesActivated.Length; i++)
        {
            if (_correctOrder[i] != _platesActivated[i])
            {
                return false;
            }
        }
        return true;
    }

    private void RestartGame()
    {
        _currentPlate = 0;
        EnablePlates();
    }

    private void EnablePlates()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            childObject.SetActive(true);
        }
    }
}

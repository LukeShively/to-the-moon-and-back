using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    private bool toggled = true;
    

    public GameObject screen;
    private void Start()
    {
        screen.SetActive(false);

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Escape key toggles the pause menu and freeze the game when active
        {
            screen.SetActive(toggled);
            Time.timeScale = Convert.ToInt16(!toggled);

            toggled = !toggled;
        }
    }
}


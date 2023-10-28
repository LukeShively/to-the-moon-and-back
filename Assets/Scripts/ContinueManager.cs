using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject screen;
    public void ContinueGame()
    {
        Time.timeScale = 1;
        screen.SetActive(false);
    }
}

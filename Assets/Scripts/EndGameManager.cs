using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject endScreenFadeOut;

    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        endScreenFadeOut.SetActive(false);
        _animator = endScreenFadeOut.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void EndTheGame()
    {
        endScreenFadeOut.SetActive(true);
    }

    void Update()
    {
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
        if (state.normalizedTime >= 1.0f)
        {
            Debug.Log("Launched Final Scene");
            // animation is done playing
            SceneManager.LoadScene("FinalScene");
        }
    }
}

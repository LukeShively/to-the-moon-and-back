using UnityEngine;
using System.Collections;
using TMPro;

public class MemoryCountController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController _playerController;

    public TextMeshProUGUI memoryCountText;
    public GameObject allMemoriesCollectedText;


    void Start()
	{
        _playerController = player.gameObject.GetComponent<PlayerController>();
        RemoveMemoryCountText();
        allMemoriesCollectedText.SetActive(false);
    }

	void Update()
	{
        // show count text upon collecting first memory
        if (_playerController.level2MemoryCount > 0)
        {
            SetMemoryCountText();
        }

        // remove count text after collecting all 5 memories
        if (_playerController.level2MemoryCount == 5)
        {
            RemoveMemoryCountText();
            allMemoriesCollectedText.SetActive(true);
        }
    }

    void RemoveMemoryCountText()
    {
        memoryCountText.text = "";
    }

    void SetMemoryCountText()
    {
        memoryCountText.text = "Memories Collected: " + _playerController.level2MemoryCount.ToString();
    }

    //void AllMemoriesCollectedText()
    //{
    //    memoryCountText.text = "You have collected all 5 memories!";
    //}
}
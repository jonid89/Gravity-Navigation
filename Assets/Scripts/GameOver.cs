using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text levelReached;
    
    private int levelCount = 1;

    void Start()
    {
        levelCount = GameSettings.Instance.GetLevelCount();
        levelReached.text = "Level Reached: " + levelCount;

        GameSettings.Instance.GetGameOverPopUp(this.gameObject);

        this.gameObject.SetActive(false);
    }

    public void Restart()
    {
        if (levelCount < 3) GameSettings.Instance.RestartGame();
        else AdManager.Instance.ShowRewardedAd();
    }

}

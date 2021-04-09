using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static int Level = 0;
    public Text scoreText;
    public Text levelText;
    public int score;
    public int coinsCount = 0;
    public int maxCoins;
    
    void Start()
    {
        maxCoins = 2 + (Level / 5);
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
        levelText.text = "Lvl : " + Level;
//        DontDestroyOnLoad(instance);
        score = 0;
    }

    public void GameOver()
    {
        if (score > 20 * (Level + 1))
            Level++;
        coinsCount = 0;
        Debug.Log("Score : " + score + " - Level : " + Level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}

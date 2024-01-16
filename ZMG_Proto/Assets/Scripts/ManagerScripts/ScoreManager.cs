using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreInstance;
    public TextMeshProUGUI myScoreText;
    public TextMeshProUGUI playerDeathScore;
    public int score;
    // Update is called once per frame
    void Update()
    {
        myScoreText.text = score.ToString() + "";
        playerDeathScore.text = score.ToString() + "";
    }

    private void Awake()
    {
        scoreInstance = this;
    }

    public void AddPoints(int pointValue)
    {
        score = score + pointValue;
        myScoreText.text = score.ToString() + "";
    }
}

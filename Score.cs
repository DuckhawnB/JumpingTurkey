using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    //the first score is 0
    public static int score = 0;
    public static int bestScore = 0;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }
}

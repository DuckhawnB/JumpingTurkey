using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;

public class CloudOnceService : MonoBehaviour
{
    public static CloudOnceService instance;

    void Awake()
    {
        TestSingleton();
    }

    void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SubmitScoreToLeaderboard(int score)
    {
        Leaderboards.HighScore.SubmitScore(score);
    }
}

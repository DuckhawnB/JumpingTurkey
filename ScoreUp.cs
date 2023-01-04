using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUp : MonoBehaviour
{
    bool isEnter = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isEnter)
        {
            Score.score++;
            isEnter = true;

            //once the player passes the score check point, the score is not incrementing anymore.
            if (isEnter)
            {
                Score.score += 0;
            }
        }
    }
}

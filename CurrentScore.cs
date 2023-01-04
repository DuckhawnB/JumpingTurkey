using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = Score.score.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewImage : MonoBehaviour
{
    [SerializeField] GameObject newImage;

    public static NewImage instance;
    public static bool isNewImageOn; 


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

    public void ActiveImage()
    {
        if (isNewImageOn)
        {
            newImage.SetActive(true);
            Debug.Log(isNewImageOn);
        }
    }

    public void DeactivateImage()
    {
        if (!isNewImageOn)
        {
            newImage.SetActive(false);
            Debug.Log(isNewImageOn);
        }
    }



}

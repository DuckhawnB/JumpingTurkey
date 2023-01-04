using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObstacle : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] float timeDiff;
    [SerializeField] float positionX;
    [SerializeField] float randomPositionMin;
    [SerializeField] float randomPositionMax;
    [SerializeField] float destroyTime;
    public GameObject newObstacle;
    float timer = 0;


    // Update is called once per frame
    void Update()
    {
        ObstacleGenerator();
    }


    void ObstacleGenerator()
    {
        timer += Time.deltaTime;

        //after the time difference
        if (timer > timeDiff)
        {
            //instantiate the gameobject
            newObstacle = Instantiate(obstacle);

            //random position for obstacles
            newObstacle.transform.position = new Vector3(positionX, Random.Range(randomPositionMin, randomPositionMax), 0);
            timer = 0;
            Destroy(newObstacle, destroyTime);
        }
    }
}

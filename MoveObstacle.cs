using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [SerializeField] float obstacleSpeed = 1f;

    void Update()
    {
        transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;  //(-1,0,0)
    }

    public float GetObstacleSpeed()
    {
        return obstacleSpeed;
    }
}

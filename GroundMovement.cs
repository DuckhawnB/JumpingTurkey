using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed = 1f;
    MoveObstacle moveObstacle;

    void Awake()
    {
        moveObstacle = FindObjectOfType<MoveObstacle>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //the background and the ground animation speed
        animator.speed = (moveObstacle.GetObstacleSpeed()) / moveSpeed;
    }
}

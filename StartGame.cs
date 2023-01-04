using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject instruction;
    PlayerMovement playerMovement;


    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        //if any key or touch scrren is pressed
        if (Keyboard.current.anyKey.isPressed || Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (playerMovement.isAlive)
            {
                //enable the scripts to make and move the obstacles
                GameObject.Find("Obstacle Tilemap Grid").GetComponent<MoveObstacle>().enabled = true;
                GameObject.Find("Obstacle Generator").GetComponent<MakeObstacle>().enabled = true;


                //destroy the instruction
                Destroy(instruction);
            }
        }
    }
}

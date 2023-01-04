using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] bool applyCameraShake;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeetCollider;
    Vector2 minBounds;
    Vector2 maxBounds;
    Animator myAnimator;
    LevelManager levelManager;
    CameraShake cameraShake;
    MakeObstacle makeObstacle;
    JoyButton joyButton;
    AudioPlayer audioPlayer;
    NewImage newImage;
    public bool isAlive = true;
    bool isGrounded = true;
    float timer;
    bool timerOn;


    void Awake()
    {
        newImage = FindObjectOfType<NewImage>();
        makeObstacle = FindObjectOfType<MakeObstacle>();
        joyButton = FindObjectOfType<JoyButton>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        levelManager = FindObjectOfType<LevelManager>();

        //camera already has getComponent type
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void Start()
    {
        InitBounds();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        timer = 0;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Timer();
        Die();
        Animation();
    }

    //camera bound
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    //if the player on the "ground" or "edge" layers
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            other.collider.gameObject.layer == LayerMask.NameToLayer("Edge"))
        {
            isGrounded = true;
        }
    }

    // public void Jump(InputAction.CallbackContext context)
    // {
    //     if (!isAlive) { return; }
    //     if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Edge"))) { return; }
    //     if (context.started)
    //     {
    //         timerOn = true;
    //     }
    //     if (context.canceled)
    //     {
    //         myRigidbody.velocity += new Vector2(0f, jumpSpeed * (timer * 2));
    //         timerOn = false;
    //         isGrounded = false;
    //     }
    // }

    //player jump
    // value.Get<float>() >= 0.1f
    // void OnJump(InputValue value)
    // {
    //     if (!isAlive) { return; }
    //     if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Edge"))) { return; }
    //     if (value.Get<float>() >= 0.1f)
    //     {
    //         timerOn = true;
    //         Debug.Log("ispressed");
    //     }
    //     else
    //     {
    //         myRigidbody.velocity += new Vector2(0f, jumpSpeed * (timer * 2));
    //         timerOn = false;
    //         isGrounded = false;
    //         Debug.Log("released");
    //     }
    // }

    public void OnPointerDown()
    {
        if (!isAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Edge"))) { return; }
        timerOn = true;
    }

    public void OnPointerUp()
    {
        myRigidbody.velocity += new Vector2(0f, jumpSpeed * (timer * 2));
        timerOn = false;
        isGrounded = false;
    }

    //timer for charging jump
    void Timer()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }


    //move
    // public void Move(InputAction.CallbackContext value)
    // {
    //     if (!isAlive) { return; }
    //     moveInput = value.ReadValue<Vector2>();
    // }

    //move
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }


    //player's speed
    void Run()
    {
        // computer
        // Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);

        // joystick
        Vector2 playerVelocity = new Vector2(UltimateJoystick.GetHorizontalAxis("Movement") * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;


        //can play only in the camera bound
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    // die
    void Die()
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Obstacle")))
        {
            isAlive = false;
            Destroy(gameObject);

            //death effect
            deathEffect.transform.position = player.transform.position;
            deathEffect.Play();

            //camera shake effect
            ShakeCamera();

            //stop moving obstacles
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Respawn");
            for (int i = 0; i < obstacles.Length; i++)
            {
                obstacles[i].GetComponent<MoveObstacle>().enabled = false;
            }

            //stop the background and ground animators
            GameObject.Find("background").GetComponent<Animator>().enabled = false;
            GameObject.Find("Ground Tilemap Grid").GetComponent<Animator>().enabled = false;

            //dying sound
            audioPlayer.PlayDyingClip();

            //if currnet score is bigger than the best score, change it to best score
            if (Score.score > Score.bestScore)
            {
                Score.bestScore = Score.score;

                //appear "NEW" 
                NewImage.isNewImageOn = true;
                newImage.ActiveImage();
            }
            else
            {
                NewImage.isNewImageOn = false;
                newImage.DeactivateImage();
            }

            //load game over scene
            levelManager.LoadGameOver();

            //rate
            CloudOnceService.instance.SubmitScoreToLeaderboard(Score.score);
        }
    }

    //jumping animation
    void Animation()
    {
        //if the player is jumping, play jumping animation
        if (isGrounded == false)
        {
            myAnimator.SetBool("isJumping", true);
        }

        //if the player is running, play running animation
        if (isGrounded == true)
        {
            myAnimator.SetBool("isJumping", false);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 15f)] public float maxJumpHeight = 4f;
    [Range(0f, 15f)] public float minJumpHeight = 1f;
    [Range(0f, 15f)] public float timeToJumpApex = 0.4f;
    [Range(0f, 15f)] public float moveSpeed = 5f;

    [Range(0f, 15f)] public float wallSlideSpeedMax = 5f;
    [Range(0f, 5f)] public float wallStickTime = 0.25f;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    float timeToWallUnstick;


    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    float gravityScale;
    float maxJumpVelocity;
    float minJumpVelocity;

    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;
    public Animator animator;

    void Start()
    {
        controller = gameObject.GetComponent<Controller2D>();
        animator = gameObject.GetComponent<Animator>();

        gravityScale = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravityScale) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravityScale) * minJumpHeight);
        print("Gravity: " + gravityScale + "Jump Velocity: " + maxJumpVelocity);
    }

    void Update()
    {
        if (PlayerState.instance.current == PlayerState.States.DYING)
        {
            velocity = Vector2.zero;
            return;
        }

        Vector2 input = PauseMenu.IsGamePaused ? Vector2.zero : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirX = (controller.collisions.left) ? -1 : 1;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(
            velocity.x,
            targetVelocityX,
            ref velocityXSmoothing,
            (controller.collisions.bellow ? accelerationTimeGrounded : accelerationTimeAirborne)
        );

        if (controller.collisions.above || controller.collisions.bellow)
        {
            velocity.y = 0;
        }

        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.bellow && velocity.y < 0)
        {
            wallSliding = true;
            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;
                if (input.x != wallDirX && input.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("Jump") != 0)
        {
            if (wallSliding)
            {
                if (wallDirX == input.x)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                    Debug.Log("wallJumpClimb");
                }
                else if (input.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                    Debug.Log("wallJumpOff");
                }
                else
                {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                    Debug.Log("wallLeap");
                }
            }
            if (controller.collisions.bellow)
            {
                velocity.y = maxJumpVelocity;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetAxisRaw("Jump") == 0)
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        velocity.y += gravityScale * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);

        if (controller.collisions.above || controller.collisions.bellow)
        {
            velocity.y = 0;
        }

        animator.SetFloat("faceDir", controller.collisions.faceDir);
    }
}

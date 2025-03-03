using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator animator;
    //My player 
    Vector2 playerVector; 
    //Speed so we can go fast
    public float Speed = 2.0f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //Getting my component and putting it in playerInput
        playerRigidbody = GetComponent<Rigidbody2D>();

        //adding my update move vector mwthod to my MoveEvent action
        PlayerInputActions.MoveEvent += UpdateMoveVector;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Updating my movement with my moveVector
        Movement();
    }
    void Movement()
    {
        //Moving my rigidbody2d player position getting my rigidbody position and adding my player vector 
        playerRigidbody.MovePosition(playerRigidbody.position + playerVector  * Time.fixedDeltaTime * Speed);
    }
    void UpdateMoveVector(Vector2 inputMovement)
    {
        //Making my moveVector equal to Movement 
        playerVector = inputMovement;

        PlayerAnimation();
    }
    void PlayerAnimation()
    {
        //If my player vector is vector (0,0), set my moving bool to false 
        if (playerVector == Vector2.zero)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            //If my player vector isn't vector zero, set moving to true
            animator.SetBool("Moving", true);
            //Setting my floats X & Y to player x and y position
            animator.SetFloat("X", playerVector.x);
            animator.SetFloat("Y", playerVector.y);
        }
    }
    private void OnDisable()
    {
        //Unsubscribing my MoveEvent from UpdateMoveVector
        PlayerInputActions.MoveEvent -= UpdateMoveVector;
    }
}

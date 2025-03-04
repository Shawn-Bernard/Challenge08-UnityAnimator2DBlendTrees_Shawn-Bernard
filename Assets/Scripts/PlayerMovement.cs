using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator animator;
    //My player 
    Vector2 playerVector;

    Vector2 lastPlayerVector;
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
    void UpdateMoveVector(Vector2 inputVector)
    {
        //Setting my last position to the player vector before I change player vector
        lastPlayerVector = playerVector;

        //Making my moveVector equal to input vector
        playerVector = inputVector;

        //Calling my player animation method after
        PlayerAnimation();
    }
    void PlayerAnimation()
    {
        //If my player vector is vector (0,0), set my moving bool to false 
        if (playerVector == Vector2.zero)
        {
            //Throwing in my last player vector into my x and y when not moving 
            SetAnimatorValues(lastPlayerVector,false);
        }
        else
        {
            //If my player vector isn't vector zero, set moving to true
            //Setting my floats X & Y to player x and y position
            SetAnimatorValues(playerVector,true);
        }
    }
    //Takes in a vector for the last player vector and new player vector
    void SetAnimatorValues(Vector2 playerVector,bool Bool)
    {
        //Player vector.x or y will give a float instead of a vector
        animator.SetFloat("X", playerVector.x);
        animator.SetFloat("Y", playerVector.y);
        animator.SetBool("Moving", Bool);
    }
    private void OnDisable()
    {
        //Unsubscribing my MoveEvent from UpdateMoveVector
        PlayerInputActions.MoveEvent -= UpdateMoveVector;
    }
}

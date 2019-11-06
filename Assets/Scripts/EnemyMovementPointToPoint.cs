using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPointToPoint : MonoBehaviour
{

    public float runningSpeed = 1.5f;
    private Rigidbody2D rigidbody2D;
    

    public bool turnAround;

    



    private void Awake()
    {
    
        rigidbody2D = GetComponent<Rigidbody2D>();

    }


   
    private void FixedUpdate()
    {

        float currentRunningSpeed;// = runningSpeed;



        if (turnAround == true)
        {
            currentRunningSpeed = -runningSpeed;
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            currentRunningSpeed = runningSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

    

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {

                rigidbody2D.velocity = new Vector2(currentRunningSpeed, rigidbody2D.velocity.y);
    
           
        }
            

    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeDirection : MonoBehaviour
{
    public bool movingForward = true;



    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {


        if (otherCollider.tag == "SwitchDirection")
        {

            //gameObject.GetComponentInParent<EnemyMovementPointToPoint>().turnAround = !gameObject.GetComponentInParent<EnemyMovementPointToPoint>().turnAround;

            
            if (movingForward == true)
            {
                gameObject.GetComponentInParent<EnemyMovementPointToPoint>().turnAround = true;

                //EnemyMovementPointToPoint.turnAround = true;
            }
            else
            {
                gameObject.GetComponentInParent<EnemyMovementPointToPoint>().turnAround = false;
               // EnemyMovementPointToPoint.turnAround = false;

            }

            movingForward = !movingForward;

           // EnemyMovementPointToPoint.turnAround = !EnemyMovementPointToPoint.turnAround;
           
        } 

    }


}

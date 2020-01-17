using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
            if (otherCollider.tag == "Player")
        {
            LevelGenerator_v2.sharedInstance.AddLevelBlock();
            LevelGenerator_v2.sharedInstance.RemoveOldestLevelBlock();
        }
        


    }
}

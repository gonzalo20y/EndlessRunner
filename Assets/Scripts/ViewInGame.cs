using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewInGame : MonoBehaviour
{

    int currentObjects; 
    public Text scoreLabel;
    public Text maxScoreLabel;
    public Text collectablesLabel;


    float maxscore;
    float travelledDistance;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(GameManager.sharedInstance.currentGameState == GameState.inGame || GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            currentObjects = GameManager.sharedInstance.collectedObjects;
            collectablesLabel.text = currentObjects.ToString(); 

        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            travelledDistance = PlayerController.sharedInstance.GetDistance();
            scoreLabel.text = "SCORE\n" +  travelledDistance.ToString("f1") + " m";

         
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            
            travelledDistance = PlayerController.sharedInstance.GetDistance();
            scoreLabel.text = "SCORE\n" + travelledDistance.ToString("f1") + " m";


            maxscore = PlayerPrefs.GetFloat("maxscore", 0);
            maxScoreLabel.text = " MAX SCORE\n" + maxscore.ToString("F1") + " m";
        } 
        



    }
}

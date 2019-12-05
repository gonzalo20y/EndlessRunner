using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelectedPlayer : MonoBehaviour
{

    PlayerController Controller;

    [SerializeField] GameObject[] Sprites;

    public int currentSprite = 0;

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();

        LoadSprite();
    }


    void LoadSprite()
    {
        for (int i = 0; i < Sprites.Length; i++)
        {
            if (currentSprite == i)
            {
                Sprites[i].SetActive(true);
                Controller.playerAnimator = Sprites[i].GetComponent<Animator>();
                Controller.childActivated = i;
            } else
            {
                Sprites[i].SetActive(false);
            }
        }
        
    }


  
}

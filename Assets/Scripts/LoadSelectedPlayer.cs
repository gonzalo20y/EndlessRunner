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
        Sprites[currentSprite].SetActive(true);
        Controller.playerAnimator = Sprites[currentSprite].GetComponent<Animator>();
    }
  
}

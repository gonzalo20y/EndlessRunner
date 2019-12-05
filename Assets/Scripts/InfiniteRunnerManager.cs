using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRunnerManager : MonoBehaviour
{
    //Variables publicas
    public bool InfMode_Activated;
    public float speed;     //Speed juego

    //Variables privadas
    PlayerController Pcontroller;
    CameraFollow cam;
    void Start()
    {
        Pcontroller = FindObjectOfType<PlayerController>();
        cam = FindObjectOfType<CameraFollow>();
        Pcontroller.InfRunner = InfMode_Activated;
        cam.InfRunnerCam = InfMode_Activated;
    }

    void Update()
    {
        //Aqui le podemos meter hasta mas velocidad con tiempo
        if (InfMode_Activated)
        {
            Pcontroller.runningSpeed = speed;
            cam.speed = speed;
        }

    }
}

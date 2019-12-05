using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Objetivo al que sigue
    public Transform target;

    // A que distancia sigue al personaje
    public Vector3 offset = new Vector3(0.1f, 0f, -10f);

    //Tiempo al inicio que la camara espera para seguir al target
    public float dampTime = 0.3f;

    //Velocidad de la camara (ector3.zero => al inicio esta parada)
    public Vector3 velocity = Vector3.zero;

    public static CameraFollow sharedInstance;

    [HideInInspector] public bool InfRunnerCam;         //Variable para controlar si es infinite runner
    [HideInInspector]public float speed;         //Variable para controlar si es infinite runner
    Vector3 startPos;

    void Awake()
    {
        //La actualizacion de frames es constante (pedimos 60 frames/s)
        Application.targetFrameRate = 60;  
        sharedInstance = this;
        startPos = transform.position;

    }


    //Le pedimos lo mismo que al Update, pero sin el SmoothDamp
    public void ResetCameraPosition()
    {
        if (InfRunnerCam)
        {
            this.transform.position = startPos;
            this.velocity = Vector3.zero;
        }
        else
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);

            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));

            Vector3 destination = point + delta;

            destination = new Vector3(target.position.x - offset.x, offset.y, offset.z); //new Vector3(target.position.x, offset.y, offset.z);

            //Nos lleva directamente al punto de inicio
            this.transform.position = destination;
        }

    }




    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            //Point (punto objetivo al que hay que ir).  CAMARA y PERSONAJE viven en dos mundos distintos (distintas coordenadas,velocidades...)
            // WorldToViewportPoint ==> devuelve de la escena WORDL, a las coordenadas de VIEWPORTPOINT
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);

            // Delta (lo que se mueve la camara para que el personaje este SIEMPRE en el centro)
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));

            //Point (Donde esta ahora la camara) + Delta (cantidad de movimiento) ==>
            Vector3 destination = point + delta;

            //Redefinimos destination para mantener el eje y/z fijos y que solo se mueva en x
            destination = new Vector3(target.position.x - offset.x, offset.y, offset.z); //new Vector3(target.position.x, offset.y, offset.z);

            if (InfRunnerCam)
            {

                Vector3 newPos = new Vector3(transform.position.x + (speed * Time.deltaTime), offset.y, offset.z);
                this.transform.position = Vector3.SmoothDamp(this.transform.position,
                                                           newPos, ref velocity, dampTime);
            }
            else
            {
                // Asignamos posicion de la camara a este destino. SmoothDamp=> Suaviza el movimiento
                this.transform.position = Vector3.SmoothDamp(this.transform.position,
                                                           destination, ref velocity, dampTime);
            }
        } 


        
        
    }
}

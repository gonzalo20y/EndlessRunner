using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerData : MonoBehaviour
{

    
    
    void Start()
    {
        //Chequear que es la primera vez que se abre el juego
        if (PlayerPrefs.GetInt("JuegoEmpezado") == 0) {

            PlayerPrefs.GetInt("JuegoEmpezado", 1);             //Setear a uno indicando que el juego ha sido iniciado con anterioridad. Basicamente es para testear
            PlayerPrefs.SetInt("CurrentCharacter", 0);          //Setear como sprite escogido el primero, es decir, marcar el primero como "por defecto"
            PlayerPrefs.SetFloat("Coins", 0);                   //Setear los coins a cero
            PlayerPrefs.SetInt("CharacterDesbloqueado0", 0);    //Desbloquear primer character

        }


        
    }

    
    void Update()
    {
        
    }
}

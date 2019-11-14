using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [Header("Variables")]
    public Text money_txt;                  //Texto que muestra el dinero actual

    [Header("Objetos")]
    public GameObject[] OBJ_Characters;     //Tabla para guardar los diferentes objetos que se pueden comprar en la tienda
    [Header("Precios")]
    public int[] prices_Characters;       // Tabla para guardar los preciosa de los diferentes objetos a comprar


    //Other variables
    int currentCoins;                     //Cantidad de dinero actual que tiene el jugador y que se muestra en pantalla

    void Start()
    {
        currentCoins = PlayerPrefs.GetInt("Coins");
        /*
        //SOLO TESTEO
        PlayerPrefs.SetInt("CurrentCharacter", 0);                  //Seleccionar el primer personaje
        PlayerPrefs.SetInt("CharacterDesbloqueado0", 1);            //Tener desbloqueado el primer personaje
        */
        UpdateButtons();
    }


    void Update()
    {
        money_txt.text = currentCoins.ToString() + "$";       //Redondear el precio y mostrarlo en pantalla con el simbolo del dolar


        //TESTEO Add 50 money
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddMoney();
        }
        //TESTEO Reset all
        if (Input.GetKeyDown(KeyCode.U))
        {
            ResetAll();
        }
    }

    #region Funciones
    void UpdateMoney(int amount)
    {
        //Actualizar el dinero. Este script se puede utilizar tanto para actualizar el precio como para 
        currentCoins += amount;

        PlayerPrefs.SetInt("Coins", currentCoins);

        UpdateButtons();

    }

    void UpdateButtons()
    {
        //Bucle para actualizar los precios 
        for (int i = 0; i < OBJ_Characters.Length; i++)
        {
            
            //Chequear si el objeto no esta comprado
            if (PlayerPrefs.GetInt("CharacterDesbloqueado" + i) == 0)
            {
                //Coger el tercer hijo (2) que equivale al texto y actualizar el nuevo precio
                OBJ_Characters[i].transform.GetChild(2).GetComponent<Text>().text = prices_Characters[i].ToString();
                OBJ_Characters[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Buy";

                //Chequear si el usuario tiene suficiente dinero
                if (currentCoins >= prices_Characters[i])
                {
                    OBJ_Characters[i].transform.GetChild(1).GetComponent<Image>().color = Color.green;
                    OBJ_Characters[i].transform.GetChild(1).GetComponent<Button>().interactable = true;
                } else
                {
                    OBJ_Characters[i].transform.GetChild(1).GetComponent<Image>().color = Color.red;
                    OBJ_Characters[i].transform.GetChild(1).GetComponent<Button>().interactable = false;
                }
                
            }
            else
            {
                OBJ_Characters[i].transform.GetChild(2).GetComponent<Text>().text = "";     //Quitar el precio
                if (PlayerPrefs.GetInt("CurrentCharacter") == i) {
                    OBJ_Characters[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Selected";
                    OBJ_Characters[i].transform.GetChild(1).GetComponent<Image>().color = Color.grey;
                    OBJ_Characters[i].transform.GetChild(1).GetComponent<Button>().interactable = false;
                }
                else
                {
                    OBJ_Characters[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Select";
                    OBJ_Characters[i].transform.GetChild(1).GetComponent<Image>().color = Color.white;
                    OBJ_Characters[i].transform.GetChild(1).GetComponent<Button>().interactable = true;
                }
            }
            
        }
    }

    public void BuyCharacter(int number) {
        bool unlocked = (PlayerPrefs.GetInt("CharacterDesbloqueado" + number.ToString()) == 1) ? true : false;

        if (unlocked)
        {
            //Seleccionar personaje
            PlayerPrefs.SetInt("CurrentCharacter", number);
            Debug.Log("Character " + number + " Seleccionado");
        }
        else
        {
            //Chequear dinero para poner video por X dinero
            if (currentCoins >= prices_Characters[number])
            {
                Debug.Log("Character " + number + " Desbloqueado");

                //Desbloquear character y setear character
                PlayerPrefs.SetInt("CharacterDesbloqueado" + number, 1);
                PlayerPrefs.SetInt("CurrentCharacter", number);

                UpdateMoney(prices_Characters[number] * -1);
            }
            else
            {
                Debug.Log("No tienes dinero");

                //buy_Panel.SetActive(true);            //Abrir Panel de anuncio
                
            }
        }


        UpdateButtons();
    }
    #endregion

    #region TESTEO

    void ResetAll()
    {
        Debug.Log("RESETEO");
        PlayerPrefs.SetInt("CurrentCharacter", 0);                 
        PlayerPrefs.SetInt("CharacterDesbloqueado0", 1);

        PlayerPrefs.SetInt("CharacterDesbloqueado1", 0);
        PlayerPrefs.SetInt("CharacterDesbloqueado2", 0);
        PlayerPrefs.SetInt("CharacterDesbloqueado3", 0);

        UpdateButtons();
    }

    void AddMoney()
    {
        UpdateMoney(50);
    }

    #endregion
}

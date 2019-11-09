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
    public float[] prices_Characters;       // Tabla para guardar los preciosa de los diferentes objetos a comprar

    //Other variables
    float currentCoins;                     //Cantidad de dinero actual que tiene el jugador y que se muestra en pantalla

    void Start()
    {
        currentCoins = PlayerPrefs.GetFloat("Coins");
        UpdatePrices();
    }


    void Update()
    {
        money_txt.text = Mathf.RoundToInt(currentCoins).ToString() + "$";       //Redondear el precio y mostrarlo en pantalla con el simbolo del dolar
    }

    #region Funciones
    void UpdateMoney(float amount)
    {
        //Actualizar el dinero. Este script se puede utilizar tanto para actualizar el precio como para 
        currentCoins += amount;

        PlayerPrefs.SetFloat("Coins", currentCoins);

    }

    void UpdatePrices()
    {
        //Bucle para actualizar los precios
        for (int i = 0; i < OBJ_Characters.Length; i++)
        {
            //Coger el tercer hijo (2) que equivale al texto y actualizar el nuevo precio
            OBJ_Characters[i].transform.GetChild(2).GetComponent<Text>().text = prices_Characters[i].ToString();
        }
    }
    #endregion
}

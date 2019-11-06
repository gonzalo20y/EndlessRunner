using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}


public class Collectables : MonoBehaviour
{

    public CollectableType type = CollectableType.money;


    bool isCollected = false;
    public int value = 0;

    void Show()  //Muestra el objeto y animacion en pantalla
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        isCollected = false;
    }
    
    void Hide()  //Oculta el objeto y la animacion en pantalla
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        
    }

    void Collect()  //Si esta recogido el objeto, apuntalo y ocultalo
    {
        isCollected = true;
        Hide();

        switch (this.type)
        {
            case CollectableType.money:
                GameManager.sharedInstance.CollectObjects(value);
                break;
            case CollectableType.healthPotion:
                PlayerController.sharedInstance.CollectHealth(value);
                break;
            case CollectableType.manaPotion:
                PlayerController.sharedInstance.CollectMana(value);
                break;


        }


        GameManager.sharedInstance.CollectObjects(value); 
    }




    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
       if (otherCollider.CompareTag("Player"))
        {
            Collect();
        }
        

    }
}

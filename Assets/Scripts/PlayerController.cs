using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController sharedInstance;


    public float jumpForce = 5f;
    private Rigidbody2D playerRigidbody2D;
    public Animator playerAnimator;
    public float runningSpeed = 5f;
    public Vector3 startPosition;
    float travelledDistance;
    bool freezePosition = false;
    float currentMaxScore;

    private int healthPoints, manaPoints; 
    public int maxHeatlhPoints = 100;
    public int maxManaPoints = 50;

    public float extraImpulse = 2f;

    


    private void Awake()
    {

        sharedInstance = this;
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        startPosition = this.transform.position; //Antes de iniciar el juego, obtenemos el valor de donde empieza el personaje
    }





    // Start is called before the first frame update
    public void StartGame()
    {
        playerAnimator.SetBool("isAlive", true); // Dar valor al inicio de los campos de la animacion del player. "Set."
        playerAnimator.SetBool("isGrounded", true); // Los parametros "----" Son como se llaman las condiciones en el animator del player
        playerAnimator.SetBool("isSlide", false);
        //this.playerRigidbody2D.constraints = RigidbodyConstraints2D.Fre;

        this.transform.position = startPosition;

        healthPoints = 100;
        manaPoints = 50;

    
            
        
        

    }

    // Update is called once per frame
    void Update()
    { //Accedo a la clase GameManager.sharedInstance y compruevo el currentState. Si es igual a ameState.inGame, te dejo saltar 
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {

           


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                playerAnimator.SetBool("isSlide", false);
            }
            //Setea por frame la condicion de true o false de "IsTouchingGround"
            playerAnimator.SetBool("isGrounded", IsTouchingGround());

            if (Input.GetMouseButtonDown(1))
            {
                SpecialJump();
            }

            if (Input.GetKey(KeyCode.R))
            {
                Slide();
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {

           if (Input.GetKey(KeyCode.D))
            {
                MoveRightHorizontal();
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            }
           if (Input.GetKey(KeyCode.A))
            {
                MoveLeftHorizontal();

                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                //gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
        }
    }

   


    void MoveRightHorizontal()
    {
        if (playerRigidbody2D.velocity.x < runningSpeed)
        {
            playerRigidbody2D.velocity = new Vector2(runningSpeed, playerRigidbody2D.velocity.y);
        }
    }
    void MoveLeftHorizontal()
    {
        if (playerRigidbody2D.velocity.x > -runningSpeed)
        {
  
            playerRigidbody2D.velocity = new Vector2(-runningSpeed, playerRigidbody2D.velocity.y);
           
        }

    }

    void Jump()
    {
        if (IsTouchingGround())
        {
            playerRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }


    void Slide()
    {
      
            playerAnimator.SetBool("isSlide", true);

    }

    void SpecialJump()
    {
        if (IsTouchingGround()&& manaPoints >= 10)
        {
            playerRigidbody2D.AddForce(Vector2.up * jumpForce * extraImpulse, ForceMode2D.Impulse);
            manaPoints -= 10;
        }
        
        
    }


   /* void Jump()         PRIMER JUMP SIN FALLOS, TAN SOLO ERA SIMPLE
        {
            if (IsTouchingGround())
            {
                playerRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        Debug.Log("Has salido en Jump()");
    }*/
   

    public LayerMask groundLayer; //Creamos variable para asignar la capa del suelo

    bool IsTouchingGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer))
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void Kill()
    {
       
        
        //Notificamos al GameManager que cambiamos el estado de juego a GameOver()
        GameManager.sharedInstance.GameOver();
        this.playerAnimator.SetBool("isAlive", false);



        //La puntuacion maxima se comprueba despues de morir
        currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0); // Puntuacion max actual se guarda con PlayerPrefs.GetFloat ("clave", valor)
        PlayerPrefs.SetFloat("scoreActual", GetDistance());     //Setea en una variable global, la Score en el momento de la muerte
        if ( currentMaxScore< GetDistance()) //Comprovamos la puntuacion max con la distancia en esta partida
        {
            PlayerPrefs.SetFloat("maxscore", GetDistance()); // Si es mayor, la guardamos en su "Clave" + nuevo valor
        }


    }




    public float GetDistance()
    {

        travelledDistance = Vector2.Distance(new Vector2(startPosition.x, 0),
                                            new Vector2(this.transform.position.x, 0));

        return travelledDistance; //TAMBIEN SE PUEDE ==> this.transform.position.x  -  startPosition.x  (final - inicio) 

    }


    public void CollectHealth(int points) // Points es la variable que voy a pasarle a la funcion para sumar
    {
        healthPoints += points;

        if (healthPoints >= maxHeatlhPoints)
        {
            healthPoints = maxHeatlhPoints;
        }



    }


    public void CollectMana(int points)
    {
        manaPoints += points;

        if (manaPoints >= maxManaPoints)
        {
            manaPoints = maxManaPoints;
        }

    }


    public int GetMana()  //Esta funcion se queda con los puntos de mana en cada momento para utilizarla como variable publica
    {
        return manaPoints;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    menu,
    inGame,
    gameOver
}




public class GameManager : MonoBehaviour
{
    // Esta variable nos dice el estado en el que inicia el juego
    public GameState currentGameState = GameState.menu;
    // Esta variable permite que el GameManager (script), se cree asi mismo antes de iniciar el juego. No puede haber + de uno
    public static GameManager sharedInstance;



    public Canvas menuCanvas, inGameCanvas, gameOverCanvas;

    public int collectedObjects = 0;









    private void Awake()
    {
        sharedInstance = this;
        
    }


    private void Start()
    {
        BackToMenu();
    }


   


    private void Update()
    {
        // Input.GetButtonDown("Start")  ==> Edit/Preferences/Input/Axes/ Nuevo campo creado al que llamamos Start
        //(Input.GetKeyDown(KeyCode.S) Para que inicie con una sola tecla
        if (Input.GetButtonDown("Start") && currentGameState != GameState.inGame)      
        {
            StartGame();
            Debug.Log("Has presionado S");
        }
        if (Input.GetButtonDown("Pause"))
        {
            BackToMenu();
        }
    }

    public void StartGame()
    {

        Debug.Log("Has entrado en StartGame()");
        SetGameState(GameState.inGame);
        

        CameraFollow.sharedInstance.ResetCameraPosition();
        PlayerController.sharedInstance.gameObject.SetActive(true);

        if (PlayerController.sharedInstance.transform.position.x > 2)
        {
            LevelGenerator.sharedInstance.RemoveAllTheBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }

        PlayerController.sharedInstance.StartGame();

        collectedObjects = 0;
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        PlayerController.sharedInstance.gameObject.SetActive(false);

    }


    public void BackToMenu()
    {
        SetGameState(GameState.menu);


    }


    public void ExitGame() 
    {
#if UNITY_EDITOR //El motor reconoce las opciones desde donde se ejecuta (PC,IOS,ANDROID)
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //Este metodo cambia el estado de juego
    void SetGameState (GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            menuCanvas.enabled = true;
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = false;

           /* menuCanvas.gameObject.SetActive(true);               // DESABILITA EL OBJETO PADRE CANVAS (IKER)
            inGameCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(false);*/
        }
        else if (newGameState == GameState.inGame)
        {
            inGameCanvas.enabled = true;
            menuCanvas.enabled = false;
            gameOverCanvas.enabled = false;

            /* menuCanvas.gameObject.SetActive(false);            // DESABILITA EL OBJETO PADRE CANVAS (IKER)
             inGameCanvas.gameObject.SetActive(true);               //TRUCO DE DESACTIVAR NAVEGACION ENTRE BOTONES
             gameOverCanvas.gameObject.SetActive(false);*/
        }
        else if (newGameState == GameState.gameOver)
        {
            gameOverCanvas.enabled = true;
            menuCanvas.enabled = false;
            inGameCanvas.enabled = false;

            /* menuCanvas.gameObject.SetActive(false);            // DESABILITA EL OBJETO PADRE CANVAS (IKER)
             inGameCanvas.gameObject.SetActive(false);
             gameOverCanvas.gameObject.SetActive(true);*/
        }

        /*if (menuCanvas.enabled)
        {
            menuCanvas.gameObject.SetActive(true);
        } else
        {
            menuCanvas.gameObject.SetActive(false);
        }*/


        this.currentGameState = newGameState;
    }


    public void CollectObjects(int objectValue) // Aumnenta el numero de objetos (empiezo = 0) + los recogidos (segun sus valores)
    {
        collectedObjects += objectValue;        //Dinero Actual In-game

        //Sumar al dinero total
        int dineroTotal = PlayerPrefs.GetInt("Coins");
        dineroTotal += objectValue;
        PlayerPrefs.SetInt("Coins", dineroTotal);

    }

    

        
}






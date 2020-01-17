using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator_v2 : MonoBehaviour
{

    public static LevelGenerator_v2 sharedInstance; // Sigo sin entender porque utilizar esto tan complejo para un juego tan sencillo

    public Transform levelStartPoint;
    [Header("Distancias - Siempre superior a la anterior")]
    public float startMedium = 150;     //Fase 2
    public float endEasy = 200;         //Fase 3
    public float startHard = 300;       //Fase 4
    public float endMedium = 400;       //Fase 5
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();
    [Header("Easy")]
    public List<LevelBlock> basicLevelBlocks = new List<LevelBlock>();
    [Header("Medium")]
    public List<LevelBlock> mediumLevelBlocks = new List<LevelBlock>();
    [Header("Hard")]
    public List<LevelBlock> hardLevelBlocks = new List<LevelBlock>();


    //Private variables
    int MaxIndex;
    float currentDist;
    int fase = 1;

    private void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        GenerateInitialBlocks();
        MaxIndex = basicLevelBlocks.Count;
    }

    private void Update()
    {
        currentDist = GameObject.FindObjectOfType<ViewInGame>().travelledDistance;
    }

    #region Funciones Privadas
    public void AddLevelBlock()
    {
        CheckDistance();
        // Random.Range => Un numero aleatorio entre los datos que le das
        int randomIndex = Random.Range(0, MaxIndex);
        LevelBlock currentBlock = (LevelBlock)Instantiate(SelectBlock(randomIndex));            //Funcion que devuelve el bloque

        //LevelBlock currentBlock = (LevelBlock)Instantiate(basicLevelBlocks[randomIndex]);
        currentBlock.transform.SetParent(this.transform, false);

        Vector3 spawnPosition = Vector3.zero;

        if (currentBlocks.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        }
        else
        {
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        Vector3 correction = new Vector3(spawnPosition.x - currentBlock.startPoint.position.x,
                                         spawnPosition.y - currentBlock.startPoint.position.y, 0);

        currentBlock.transform.position = correction;
        currentBlocks.Add(currentBlock);

    }


    public void RemoveOldestLevelBlock()
    {
        LevelBlock oldestBlock = currentBlocks[0];
        currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);
    }


    public void RemoveAllTheBlocks()
    {
        while (currentBlocks.Count > 0)
        {
            RemoveOldestLevelBlock();
        }
    }


    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }

    }

    private LevelBlock SelectBlock(int number)
    {
        //Debug.Log(fase + " Random:" + number);
        if (fase == 1)
        {
            return basicLevelBlocks[number];
            
        }
        
        if (fase == 2)
        {
            if (number < basicLevelBlocks.Count)
            {
                return basicLevelBlocks[number];
            }
            else
            {
                return mediumLevelBlocks[number - basicLevelBlocks.Count];
            }
        }
        if (fase == 3)
        {

            return mediumLevelBlocks[number];

        }
        if (fase == 4)
        {
            if (number < mediumLevelBlocks.Count)
            {
                return mediumLevelBlocks[number];
            }
            else
            {
                return hardLevelBlocks[number - mediumLevelBlocks.Count];
            }
        }

        if (fase == 5)
        {
            return hardLevelBlocks[number];
        }
        
        return basicLevelBlocks[0];     //por si acaso devolver este
    }


    private void CheckDistance()
    {
        //Chequear para pasar de fases y actualizar el index
        if (fase == 1)
        {
            //Check for fase 2
            fase = currentDist >= startMedium ? 2:1;
            MaxIndex = basicLevelBlocks.Count;
            
        }
        if (fase == 2)
        {
            //Check for fase 3
            fase = currentDist >= endEasy ? 3 : 2;
            MaxIndex += mediumLevelBlocks.Count;
        }
        if (fase == 3)
        {
            //Check for fase 4
            fase = currentDist >= startHard ? 4 : 3;
            MaxIndex -= basicLevelBlocks.Count;
        }
        if (fase == 4)
        {
            //Check for fase 5
            fase = currentDist >= endMedium ? 5 : 4;
            MaxIndex += hardLevelBlocks.Count;
        } 

        if(fase == 5)
        {
            MaxIndex -= mediumLevelBlocks.Count;
        }

    }
    #endregion
}

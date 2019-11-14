using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScenes : MonoBehaviour
{
    public void LoadSelectedScene(string nameScene) {

        SceneManager.LoadScene(nameScene);
    }
}

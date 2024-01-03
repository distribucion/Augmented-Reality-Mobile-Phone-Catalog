using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScripComenzar : MonoBehaviour
{
    public void Jugar() 
    {
        SceneManager.LoadScene(1);
    }

    public void Salir() 
    {
        Debug.Log("Salir..");
        Application.Quit();
    }
}

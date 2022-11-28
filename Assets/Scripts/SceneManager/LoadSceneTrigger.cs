using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour
{
    
    //Variables
    //Indice de la escena que quiero cargar
    public int nextSceneIndex;
    //Duracion de los fade
    public float fadeDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.LoadScene(nextSceneIndex, fadeDuration);
        }
    }
}

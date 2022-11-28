using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    //Crear un singleton de la clase,
    //Tenemos que instanciarnos a nosotros mismos
    
    //Private static nombredelaclase
    private static SceneController instance;
    
    //Variable accesibles
    public Image fadeImage;
    public TextMeshProUGUI text;
    
    //Controlar que no exista mas de una instancia mia
    private void Awake()
    {
        //Si no estamos instanciados , lo hacemos
        if (instance == null)
        {
            //Me instancio
            instance = this;
            //Hacemos que no se pueda destruir
            DontDestroyOnLoad(gameObject);
            //Desactivamos la imagen, por si anteriormente estuviese
            //activa
            fadeImage.enabled = false;
            text.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //Metodo para ejecutar la carga de escenas
    public static void LoadScene(int index, float duration)
    {
        //Ejecutaremos un corutina
        instance.StartCoroutine(instance.FadeScene(
            index,
            duration
        ));
    }

    IEnumerator FadeScene(int index, float duration)
    {
        //Activar la imagen
        fadeImage.enabled = true;
        
        //FadeIn
        float a = 0.0f;
        //Bucle mientras que a > 0.0
        while (a < 1.0f)
        {
            a += (1.0f / duration) * Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }

        text.enabled = true;
        //Cargar de forma asincrona la escena que nos indique
        AsyncOperation ao = SceneManager.LoadSceneAsync(index);
        //vamos a controlar el estado de la carga de la escena
        while (ao.isDone == false)
        {
            //Le digo a la corutina que no siga
            // es decir qu no haga el fade out
            yield return null;
        }

        yield return new WaitForSeconds(2);
        text.enabled = false;
        //Fade Out
        //Float que guarde el alpha
         a = 1.0f;
        //Bucle mientras que a > 0.0
        while (a > 0.0f)
        {
            a -= (1.0f / duration) * Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }

        fadeImage.enabled = false;
    }
}

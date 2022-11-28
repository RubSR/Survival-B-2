using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorManager : MonoBehaviour
{
    //Image
    public Image image;
    //Velocidad para fade
    public float flashSpeed;
    //Creamos una corutina
    private Coroutine fadeaway;
    void Start()
    {
        
    }
    // Lo llamara desde el player para hacer
    // el fade
    //Metodo flash, que comprueba si se esta
    //ejecutando la corutina
    public void Flash()
    {
        //tenemos que comprobar si ya se esta haciendo 
        // un fade
        if (fadeaway != null)
        {
            StopCoroutine(fadeaway);
        }
        // Reset de la imagen
        image.enabled = true;
        image.color = Color.white;
        // Iniciar la corrutina
        fadeaway = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        //Float que guarde el alpha
        float a = 1.0f;
        //Bucle mientras que a > 0.0
        while (a > 0.0f)
        {
            a -= (1.0f / flashSpeed) * Time.deltaTime;
            image.color = new Color(1.0f, 1.0f, 1.0f, a);
            yield return null;
        }

        image.enabled = false;

    }
}

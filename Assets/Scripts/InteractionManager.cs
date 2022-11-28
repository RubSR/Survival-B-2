using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    //Variables
    //1 . Ratio de disparo del raycast, cada cuanto tiempo voy a disparar
    public float checkRate = 0.05f;
    //2. Variable donde nos guardamos cuando se hizo el ultimo disparo
    private float lastCheckTime;
    //3. Distancia del raycast
    public float maxCheckDistance;
    //4. LayerMask con que tiene que interactuar el raycast
    public LayerMask layerMask;
    
    //5. camara
    public Camera cam;

    private GameObject objetoActual;
    private IInteractuable interactuableActual;

    public TextMeshProUGUI mensajeEnPantalla;
    

    // Update is called once per frame
    void Update()
    {
        // Time.time devulve el tiempo actual de juego
        // // Vamos a disparar el rayo cuando la diferencia entre
        //Time.time y  lastChektime sea >= checkRate
        if (Time.time - lastCheckTime > checkRate)
        {
            //1.Guardo el momento de este disparo
            lastCheckTime = Time.time;
            //2.Creamos un rayo desde el centro de la pantalla y
            // en perpendicular
            Ray rayo = cam.ScreenPointToRay(
                new Vector3(Screen.width/2, Screen.height/2, 0)
                );
            //3.Nos creamos un punto de hit con el rayo 
            RaycastHit choque;
            //4. Disparamos el rayo
            if (Physics.Raycast(rayo, out choque, maxCheckDistance, layerMask))
            {
                // es el objeto anterior?
                // si no lo es, nos lo guardamos como el actual
                if (choque.collider.gameObject != objetoActual)
                {
                    objetoActual = choque.collider.gameObject;
                    interactuableActual = choque.collider.GetComponent<IInteractuable>();
                    //2.Mostrar el mensaje de recoger en pantalla
                    
                    SetMensaje();

                }
            }
            else
            {
                objetoActual = null;
                interactuableActual = null;
                mensajeEnPantalla.gameObject.SetActive(false);

            }

        }
    }
        //Mostrar en pantalla de Recoger objeto
    private void SetMensaje()
    {
     
        //1.Activamos en Texto
        mensajeEnPantalla.gameObject.SetActive(true);
        //2. Le pasamos el texto que tiene que mostrar
        mensajeEnPantalla.text =
            string.Format("<b> [E] </b> {0}", 
                interactuableActual.GetInteractPromt());
    }
    
    //Metodo que sera disparado por el input system al presionar la E
    public void AlPresionarLaTeclaE(InputAction.CallbackContext context)
    {
        //Comprobar si se acaba de presionar
        if (context.started && interactuableActual != null)
        { //1. A traves de la interfaz vamos a ejecutar
            // el metodo OnInteract que la interfaz 
            // obliga a implementar a los objetos IInteractuables
            interactuableActual.OnInteract();
            interactuableActual = null;
            objetoActual = null;
            mensajeEnPantalla.gameObject.SetActive(false);

        }
    }
}

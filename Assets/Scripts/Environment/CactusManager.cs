using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CactusManager : MonoBehaviour
{
    //Daño a repartir
    public float damage;
    //Cada cuanto tiempo voy a repartir daño
    public float damageRate;
    //Lista donde almacenaremos los objetos que 
    // esten en colision con el cactus
    private List<GameObject> thingsToDamage = new List<GameObject>();
    
    //Cuando un gameobject entra en colision lo añadimos
    // a la lista si es player o enemigo
    private void OnCollisionEnter(Collision collision)
    {
        // Si es player o enemigo añadimos
        if (collision.gameObject.CompareTag("Player"))
        {
            thingsToDamage.Add(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            thingsToDamage.Add(collision.gameObject);
        }
        
    }

    private void Start()
    {
        //Iniciar la corutina
        StartCoroutine(DealDamage());
    }

    //Si sale de la colision, lo quitamos de la lista
    private void OnCollisionExit(Collision other)
    {
        if (thingsToDamage.Contains(other.gameObject))
        {
            thingsToDamage.Remove(other.gameObject);
        }
    }
    
    
    //Corutina
    IEnumerator DealDamage()
    {
        //Bucle infinito que se ejecutará cada
        // damageRatio segundos
        while (true)
        {
            //Recorrer la lista y pegar a los de la lista
            for (int i = 0; i < thingsToDamage.Count; i++)
            {
                switch (thingsToDamage[i].gameObject.tag)
                {
                    case "Player":
                        thingsToDamage[i]
                            .GetComponent<PlayerNeeds>()
                            .TakePhysicalDamage(damage);
                        break;
                    case "Enemy":
                        break;
                }
            }
            //Hacemos que el bucle while espere para ejecutar
            //la siguiente vuelta
            yield return new WaitForSeconds(damageRate);

        }
        
    }
}

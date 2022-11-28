using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour , IInteractuable
{
    //Variable para establecer su correspondiente SO
    public ItemDataSO item;

    public string GetInteractPromt()
    {
        return "Recoger " + item.displayName;
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}

//Interfaz
public interface IInteractuable
{
    //Debemos definir metodos comunes
    // que los objetos que extiendan de la interfaz 
    // esten olbigados a tener los metodos
    // OJO aqui solo los definimos
    //1. Para mostrar el mensaje en pantalla del objeto
    string GetInteractPromt();
    //2. Para interactuar
    void OnInteract();
}

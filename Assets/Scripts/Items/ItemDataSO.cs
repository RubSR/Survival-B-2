using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esto es para hacrer que aparezca en menu de creacion de assets
[CreateAssetMenu(fileName = "Item", menuName = "Nuevo pepe")]
public class ItemDataSO : ScriptableObject
{
    [Header("Info")] 
    public string displayName;

    public string description;
    public TipoObjeto tipo;
    //Necesitamos guardarnos el sprite que aparece como icono del objeto
    // en nuestro inventario
    public Sprite icon;
    //Necesitamos un prefab que sera un Gameobject con un 
    // modelo 3D que servirá como modelo  
    // al  dropearlo
    public GameObject dropPrefab;

    [Header("Stackear")] 
    public bool canStack;

    public int maxStackAmount;
}

// Para especificar un lista de cosas
// Simplemente para darles un tipo diferenciador
// totalmente al gusto del programador parecido al tag
//
public enum TipoObjeto
{
    // Tipo recurso para diferenciar los que siravan para craftear
    Resource,
    // Tipo Equipable,los objetos que podemos equiparnos
    Equipable,
    // Tipo consumible, los que se consumen para ganar alguna stat
    Consumable
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    //Varibles------------
    public ItemSlotUI[] uiSlots;
    //Manejamos los objetos en otro array
    public ItemData[] objetos;
    
    //Poder abrir y cerrar el inventario
    //->Activar y desactivar el Canvas de inventario
    public GameObject ventanaInventario;
    //Posicion desde donde se dropearan los objetos
    public Transform posicionDrop;

    [Header("Item seleccionado")] 
    private ItemSlotUI slotSelccionado;

    private int posicionSlotSeleccionado;
    
    //TExtos
    public TextMeshProUGUI nombreItem;
    public TextMeshProUGUI descItem;
    public TextMeshProUGUI statsnombreItem;
    public TextMeshProUGUI statscantidadItem;
    
    //Botones
    public GameObject usarBoton;
    public GameObject equiparBoton;
    public GameObject desequiparBoton;
    public GameObject dropBoton;
    
    //Variabl para guardar la posicion el objeto seleccionado
    private int posicionObjeto;
    
    //Scripts externos
    private PlayerController controllers;
    private PlayerNeeds necesidades;
    






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//Clase intermedia para crear objeto a partir de los 
//objetos que hay en el mundo de tal manera
// que solo me guardare sus datos (SO)
// y la cantidad para controlar la cantidaa de objetos 
// de ese tipo que tengo el inventario
public class ItemData
{
    public ItemDataSO itemData;
    public int cantidad;
}

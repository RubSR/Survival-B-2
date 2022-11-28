using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNeeds : MonoBehaviour
{
    
   //Nos creamos objetos de tipo Need por cada barra
   public Need health;
   public Need hunger;
   public Need water;
   public Need sleep;
   [Header("Colores de advertencia")] 
   public Color lleno;

   public Color medio;

   public Color cuarto;

   public Gradient colores;
   
   //Varibles propias
   public float hungerHealthDecay;
   public float thirstHealthDecay;
   
   //DamageIndicator
   [Header("Script de Damage Indicator")] 
   public DamageIndicatorManager damageIndicator;
    void Start()
    {
        //Establecer los valores iniciales
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        water.curValue = water.startValue;
        sleep.curValue = sleep.startValue;
        
        //Establecemos colores al inicio
        health.uiBar.color = colores.Evaluate(1);


    }

    // Update is called once per frame
    void Update()
    {
        //Desgaste cada 1 segundo de las barras
        hunger.Subtract(hunger.decayRate 
                        * Time.deltaTime);
        water.Subtract(water.decayRate *
                       Time.deltaTime);
        sleep.Add(sleep.regenRate 
                  * Time.deltaTime );
        
        // Desgaste de la barra de vida si hay hambre y/o sed
        if (hunger.curValue == 0.0f)
        {
            health.Subtract(hungerHealthDecay 
                            * Time.deltaTime);
        }

        if (water.curValue == 0.0f)
        {
            health.Subtract(thirstHealthDecay 
                            * Time.deltaTime);
        }
        //Checkeamos si estoy muerto
        if (health.curValue == 0.0f)
        {
            Die();
        }
        
        // Actualizar las barras
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        water.uiBar.fillAmount = water.GetPercentage();
        sleep.uiBar.fillAmount = sleep.GetPercentage();
        //Cambiar colores
        health.uiBar.color = colores.Evaluate(health.GetPercentage());

    }// Fin del Update
    //Acciones relacionadas
    //Curarnos
    public void Heal(float amount)
    {
        health.Add(amount);
    }
    
    //Comer
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    //Beber
    public void Drink(float amount)
    {
        water.Add(amount);
    }
    //Dormir
    public void Sleep(float amount)
    {
        sleep.Subtract(amount);
    }
    //Recibir daño
    public void TakePhysicalDamage(float amount)
    {
        
        //Nos quitamos vida
        damageIndicator.Flash();
        health.Subtract(amount);
    }
    //Morir
    public void Die()
    {
        Debug.Log("Estoy muerto");
    }
    
    
    
    
    
}

// Clase/Objeto de tipo necesidad
[System.Serializable]
public class Need 
{
    // Para guardar el valor actual y maneja el tamaño
    // de la barra
    [HideInInspector]
    public float curValue;
    //Para controlar que no nos pasamos
    public float maxValue;
    // Valor inicial
    public float startValue;
    //Ratio de regeneracion
    public float regenRate;
    //Ratio de desgaste
    public float decayRate;
    // La barra
    public Image uiBar;
    
    //Metodos comunes
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount
            , maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount,
            0.0f
        );
    }
    // Regla de tres para el fillAmount
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}

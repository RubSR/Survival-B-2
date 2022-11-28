using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variables
    // vector2 para recoger el valor del movimiento del raton
    private Vector2 mouseDelta;
    [Header("Sensibilidad de la cámara")]
    public float lookSensitivity;
    private float camCurXRot;
    [Header("Contenedor de camara")]
    public GameObject cameraContainer;
    // Variables para limitar el giro en vertical
    [Header("Limite vertical")]
    public float minXLook;
    public float maxXLook;
    [Header("Movimiento")] 
    public float moveSpeed;

    public float jumpForce;

    public LayerMask groundLayerMask;
    // Guardar el vector2 que devuelve el input system
    private Vector2 curMovementInput;
    
    //Componentes
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }


    // Metodo llamado cuando movemos el raton 
    // Lo maneja InputSystem
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    
    // Metodo llamado cuando presionamos las teclas de mov.
    // Lo maneja input system y devuelve Vector2
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Estamos presionando alguno boton de mov?
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    private void Start()
    {
       //Escondemos el puntero del raton al iniciar el juego
       Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
       // Vamos a calcular la direccion de mov
       // relativa a donde estemos mirando
       Vector3 dir = transform.forward
                     * curMovementInput.y
                     + transform.right *
                     curMovementInput.x;
       dir *= moveSpeed;
       // Restablecer la Y para poder saltar
       dir.y = rig.velocity.y;
        //Mover el rgbd
        rig.velocity = dir;
    }

    private void CameraLook()
    {
        //Rotamos el CameraContainer arriba y abajo
        camCurXRot += mouseDelta.y * lookSensitivity;
        // Comprobar que camCurXRot no pase de sus valores limitados
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        // Muevo la camara
        cameraContainer.transform.localEulerAngles = 
            new Vector3(-camCurXRot, 0, 0);
        //rotamos al player
        transform.localEulerAngles +=
            new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(
                ),
            new Ray(transform.position
                    + (-transform.forward * 0.2f), Vector3.down),
            new Ray(transform.position
                    + (transform.right * 0.2f), Vector3.down),
            new Ray(transform.position
                    + (-transform.right * 0.2f), Vector3.down),
        };
        //Recorremos el array y comprobamos la colision del cada rayo
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;

    }
    //Metodo para visualizar los rayos en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position
                       + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position
                       + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position
                       + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position
                       + (-transform.right * 0.2f), Vector3.down);

    }

    // Metodo que nos devuelve si hemos apretado 
    // el boton de salto
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // es el primer frame en l que hemos apretado saltar?
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
            {
                //Saltamos
                rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

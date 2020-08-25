using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Follow : MonoBehaviour
{
    public GameObject Player; // Referencia al elemento jugador 
    private Vector3 CamaraSeguir; // vector que calcula la posicion que separa al jugador con la camara

 

    [Range(0.01f, 1.0f)] // crea un float entre 0 y 1
    public float SmoothFactor = 0.5f;

    
  
    void Start()
    {
        CamaraSeguir = transform.position - Player.transform.position; // distancia que separa la camara del jugador
    }

   
    void Update()
    {

        if (Player != null)
        {
            Vector3 newPos = Player.transform.position + CamaraSeguir; // posicion donde esta el jugador despues de moverse

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor); // vamos actualizando la posicion de la camara con respecto al jugador
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeguirPlayer : MonoBehaviour
{
    

    public NavMeshAgent Agente;
    public Transform PosicionJugador;


   
    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();
    }

 
    public void ActualizarDestino(Vector3 posicion)
    {
        Agente.destination = posicion;
        Agente.isStopped = false;
    }

 
    public void DetenerAgente()
    {
        Agente.isStopped = true;
    }

    public bool LLegarDestino()
    {
        bool llegar = false;
        if (Agente.remainingDistance <= Agente.stoppingDistance && !Agente.pathPending)
        {
            llegar = true;
        }
        return llegar;
    }


    
}

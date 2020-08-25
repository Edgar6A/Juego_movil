using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVision : MonoBehaviour
{

    private float RangoVision = 50f;
    //public Vector3 offset = new Vector3(0f,1f,0f);

        

    private SeguirPlayer NavMesh;


    private void Start()
    {
        NavMesh = GetComponent<SeguirPlayer>();
    }

    public bool VerJugador(out RaycastHit hit, bool MirarHaciaJugador = false)
    {
        
       // Vector3 VectorDireccion;


        /*
        if (MirarHaciaJugador)
        {
            VectorDireccion = NavMesh.PosicionJugador.position - transform.position;
        }
        else
        {
            VectorDireccion = transform.forward; 
        }
        */

        
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) && hit.collider.CompareTag("Player"))
        {
            MirarHaciaJugador = true; 
        }

        return MirarHaciaJugador;
    }
}

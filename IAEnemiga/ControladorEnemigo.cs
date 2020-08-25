using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControladorEnemigo : MonoBehaviour
{

    public Transform[] PuntosMapa;
    private int SiguientePunto = 0;

    public SeguirPlayer Mover;
    public ControladorVision Ver;
    public Transform PosicionPlayer;
    public GameObject DetectorPlayer;
    private int contador = 0;
    public float DistanciaJugador, DistanciaArquero, DistanciaMele;
    public Vector3 Posicion;
    private bool JugadorVisto;
    public bool Arquero;
  




    //public LayerMask layerMask;
    // public Vector3 direction = Vector3.up;



    //public float maxDistance = 10;
    //public Camera Cam;
    //public NavMeshAgent agent;




    private void Start()
    {
        Mover = GetComponent<SeguirPlayer>();
        Ver = GetComponent<ControladorVision>();
        JugadorVisto = false;
    }


    private void Update()
    {
        // distancia del enemigo contra el jugador, se puede usar para poner de posicion del enemigo que se pare a la mitad de distancia que hay entre ellos dos
        // en vez de usar un cubo ppedmos usar la distancia que hay entre ellos dos para pararse mas cerca, en la mitad justo o donde veamos
        DistanciaJugador = Vector3.Distance(PosicionPlayer.position, transform.position);

        Posicion = new Vector3(PosicionPlayer.transform.position.x + 1.5f, 0, PosicionPlayer.transform.position.z + 1.5f);

        //Sistema para movimiento enememigo de tipo arquero
        if(Ver.VerJugador(out RaycastHit hit))
        {
            JugadorVisto = true;
        }

        if (JugadorVisto == true && Arquero == true)
        {
            if (DistanciaJugador >= 10)
            {
                Mover.ActualizarDestino(Posicion);
            }

            if (DistanciaJugador < 10)
            {
                Mover.DetenerAgente();
            }

        }

        //Sistema para movimiento enememigo de tipo mele
        if (JugadorVisto == true && Arquero == false)
        {
            Mover.ActualizarDestino(Posicion);
            if (Mover.LLegarDestino())
            {
                Mover.DetenerAgente();
            }

        }


        //Para que haga una ruta de puntos por el mapa
        /*
        ActualizarPuntoMapa();
        if (Mover.LLegarDestino())
        {
            SiguientePunto = (SiguientePunto + 1) % PuntosMapa.Length;
            ActualizarPuntoMapa();
            //Mover.DetenerAgente();
        }
        */





    }


    private void ActualizarPuntoMapa()
    {
        //SiguientePunto = 0;
        Mover.ActualizarDestino(PuntosMapa[SiguientePunto].position);

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control_Character : MonoBehaviour
{
    [Header("control de movilidad del Jugador")]
    private Rigidbody rig;
    public float RotateSpeed = 100;
    public float TranslateSpeed= 10;
    public Animator PersonajeArturo;

    private bool SeMueveArturo;




    //vida
    [Header("Vida del Jugador")]
    public int VidaMax = 4;
    public int VidaActual;

    [Header("Tipos de daños")]
    public int DañoBase = 1;
    public int DañoFuerte = 2;
    public int DañoSuperFuerte = 3;

    
    private int QueMeGolpeo;

    

    
   

    private void Start()
    {
        VidaActual = VidaMax;
        


    }


    // Update is called once per frame
   void Update()
    {

        //hace que podamos mover al jugador
        float mover = Input.GetAxis("Vertical") * TranslateSpeed * Time.deltaTime;
        transform.Translate(mover, 0, 0);


        float rotation = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        Debug.Log("La vida actual es de " + VidaActual);


        // hace que el personaje cambie entre la animacion de moverse y pararse
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            SeMueveArturo = true;
            PersonajeArturo.SetBool("SeMueve", SeMueveArturo);

        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            SeMueveArturo = false;
            PersonajeArturo.SetBool("SeMueve", SeMueveArturo);
        }
    }


   




    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DañoBasico")
        {
            Debug.Log("TE ATACO UN NOMO");
            QueMeGolpeo = DañoBase;
            RecibiendoDaño();
        }
        if (other.gameObject.tag == "DañoFuerte")
        {
            Debug.Log("TE ATACO UN orco");
            QueMeGolpeo = DañoFuerte;
            RecibiendoDaño();
        }
        if (other.gameObject.tag == "DañoSuperFuerte")
        {
            Debug.Log("coree perra corre");
            QueMeGolpeo = DañoSuperFuerte;
            RecibiendoDaño();
        }
    }
    public void RecibiendoDaño()
    {
        
        VidaActual = VidaActual - QueMeGolpeo;
        if (VidaActual <= 0)
        {

            Hasmuerto();
        }
    }
    public void Hasmuerto()
    {
        Start();
        SceneManager.LoadScene("Pantalla_muerte");
    }
}

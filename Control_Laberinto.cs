using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Control_Laberinto : MonoBehaviour
{
    public GameObject camaraPrincipal, camaraArriba,  camaraExplanada, CamaraVistaIzquierda, CamaraVistaDerecha, CamaraLateralDerecha, CamaraFondo; // referencias al objeto camara de nuestro nivel
    public Control_Personaje ScripPersonaje; // enlace con el script del ersonaje 

  
    private void Update()
    {
      
       
        //estrucutra que nos permite activar o desactivar la camara dependiendo de la zona del laberinto donde estemos
       switch (ScripPersonaje.NumeroCamara)
        {
            //camara principal
            case 0:
                camaraArriba.SetActive(false);
                camaraExplanada.SetActive(false);
                CamaraVistaIzquierda.SetActive(false);
                CamaraVistaDerecha.SetActive(false);
                CamaraLateralDerecha.SetActive(false);
                CamaraFondo.SetActive(false);
                camaraPrincipal.SetActive(true);
                break;

            //camara desde arriba
            case 1:
                camaraArriba.SetActive(true);
                camaraPrincipal.SetActive(false);
                CamaraVistaDerecha.SetActive(false);
                CamaraLateralDerecha.SetActive(false);
                CamaraVistaIzquierda.SetActive(false);
                camaraExplanada.SetActive(false);
                break;

            //camara explanada
            case 2:
                camaraArriba.SetActive(false);
                camaraPrincipal.SetActive(false);
                CamaraVistaDerecha.SetActive(false);
                CamaraLateralDerecha.SetActive(false);
                CamaraVistaIzquierda.SetActive(false);
                camaraExplanada.SetActive(true);
                break;
                //camara sin salida muro izquierda
            case 3:
                camaraArriba.SetActive(false);
                camaraPrincipal.SetActive(false);
                camaraExplanada.SetActive(false);
                CamaraVistaDerecha.SetActive(false);
                CamaraLateralDerecha.SetActive(false);
                CamaraVistaIzquierda.SetActive(true);
                break;
                //camara sin salida muro derecha
            case 4:
                camaraArriba.SetActive(false);
                camaraPrincipal.SetActive(false);
                camaraExplanada.SetActive(false);
                CamaraVistaIzquierda.SetActive(false);
                CamaraLateralDerecha.SetActive(false);
                CamaraVistaDerecha.SetActive(true);
                break;
                // camara vista lateral izquierda
            case 5:
                camaraArriba.SetActive(false);
                camaraPrincipal.SetActive(false);
                camaraExplanada.SetActive(false);
                CamaraVistaIzquierda.SetActive(false);
                CamaraVistaDerecha.SetActive(false);
                CamaraLateralDerecha.SetActive(true);
                break;
                //camara fondo izquierda
            case 6:
                camaraArriba.SetActive(false);
                camaraPrincipal.SetActive(false);
                camaraExplanada.SetActive(false);
                CamaraVistaIzquierda.SetActive(false);
                CamaraVistaDerecha.SetActive(false);
                CamaraLateralDerecha.SetActive(false);
                CamaraFondo.SetActive(true);
                break;
        }
    }

        // estructura que nos permite saber cuando el personaje entra en que zona del laberinto
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "entrada")
        {
            ScripPersonaje.NumeroCamara = 1;
        }


        if (other.name == "muro")
        {
            ScripPersonaje.NumeroCamara = 3;
        }

        if (other.name == "muroFrontal")
        {
            ScripPersonaje.NumeroCamara = 4;
        }


        if (other.name == "vistaDerecha")
        {
            ScripPersonaje.NumeroCamara = 5;
        }

        if (other.name == "VistaIzquierda")
        {
            ScripPersonaje.NumeroCamara = 2;
        }


        if (other.name == "Principal")
        {
            ScripPersonaje.NumeroCamara = 6;
        }

        if (other.name == "muroFin")
        {
            ScripPersonaje.NumeroCamara = 2;
        }


        if (other.name == "muroFondo")
        {
            ScripPersonaje.NumeroCamara = 6;
        }


        if (other.name == "muroAntesFondo")
        {
            ScripPersonaje.NumeroCamara = 3;
        }

        

        if (other.name == "gancho")
        {
            ScripPersonaje.NumeroCamara = 2;
        }

        if (other.name == "SalaDiagonal")
        {
            ScripPersonaje.NumeroCamara = 1;
        }

        if (other.name == "SalaDiagonalIzquierda")
        {
            ScripPersonaje.NumeroCamara = 2;
        }

        if (other.name == "SalaDiagonalDerecha")
        {
            ScripPersonaje.NumeroCamara = 2;
        }

        if (other.name == "RectaFinal")
        {
            ScripPersonaje.NumeroCamara = 1;
        }


        if (other.name == "Meta")
        {
            SceneManager.LoadScene("Victoria");
        }

        if (other.name == "MuroCallejon")
        {
            ScripPersonaje.NumeroCamara = 2;
        }

        if (other.name == "muroAntesCallejon")
        {
            ScripPersonaje.NumeroCamara = 2;
        }



    }


    // estructura que nos permite saber cuando el personaje sale de cada zona del laberinto
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "entrada")
        {
            ScripPersonaje.NumeroCamara = 1;
        }

        if (other.name == "explanada")
        {
            ScripPersonaje.NumeroCamara = 1;
        }

        if (other.name == "muro")
        {
            ScripPersonaje.NumeroCamara = 1;
        }

        if (other.name == "muroFrontal")
        {
            ScripPersonaje.NumeroCamara = 1;
        }

        if (other.name == "vistaDerecha")
        {
            ScripPersonaje.NumeroCamara = 1;
        }

        if (other.name == "VistaIzquierda")
        {
            ScripPersonaje.NumeroCamara = 1;
        }

        if (other.name == "Principal")
        {
            ScripPersonaje.NumeroCamara = 6;
        }

        if (other.name == "muroFin")
        {
            ScripPersonaje.NumeroCamara = 1;
        }

        if (other.name == "muroAntesFondo")
        {
            ScripPersonaje.NumeroCamara = 2;
        }

       




        if (other.name == "SalaDiagonal")
        {
            ScripPersonaje.NumeroCamara = 0;
        }


        if (other.name == "SalaDiagonalIzquierda")
        {
            ScripPersonaje.NumeroCamara = 0;
        }

        if (other.name == "SalaDiagonalDerecha")
        {
            ScripPersonaje.NumeroCamara = 0;
        }

        if (other.name == "RectaFinal")
        {
            ScripPersonaje.NumeroCamara = 0;
        }

    
    }

}

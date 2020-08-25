using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Control_Scena : MonoBehaviour
{
    public void Iniciar()
    {
        SceneManager.LoadScene("Nivel1");
    }


    public void NivelFinal()
    {
        SceneManager.LoadScene("Nivel2");
    }


    public void Reiniciar()
    {
        SceneManager.LoadScene("Escenario");
    }

    public void Principal()
    {
        SceneManager.LoadScene("Principal");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Intro()
    {
        SceneManager.LoadScene("Intro");
    }


    // atravesamos la puerta de salida del nievel 1
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            NivelFinal();
        }
    }
}



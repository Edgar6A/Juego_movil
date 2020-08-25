using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MostrarTiempo : MonoBehaviour
{
    

    private int SegundosNivel1, MinutosNivel1,SegundosNivel2,MinutosNivel2;
    private int MinutosTotales, SegundosTotales, SegundosTotalesAmostrar;

    private Text TiempoNivel1, TiempoNivel2, TiempoTotal;
    private Button Volver;

    void Start()
    {
        TiempoNivel1 = GameObject.Find("TiempoNivel1").GetComponent<Text>();
        TiempoNivel2 = GameObject.Find("TiempoNivel2").GetComponent<Text>();
        TiempoTotal = GameObject.Find("TiempoTotal").GetComponent<Text>();
        Volver = GameObject.Find("Volver").GetComponent<Button>();
    }

    void Update()
    {
            // Valores que se muestran para el tiempo del primer nivel
            SegundosNivel1 = PlayerPrefs.GetInt("SegundosTotalesNivel1", 0);
            MinutosNivel1 = PlayerPrefs.GetInt("MinutosTotalesNivel1", 0);
            if (MinutosNivel1 == 1)
            {
                string minutos = "" + MinutosNivel1;
                string segundos = "" + SegundosNivel1;
                TiempoNivel1.text = "Has tardado " + minutos + " minuto " + " y " + segundos + " segundos " + " en completar el Nivel 1";
            }
            else
            {
                string minutos = "" + MinutosNivel1;
                string segundos = "" + SegundosNivel1;
                TiempoNivel1.text = "Has tardado " + minutos + " minutos " + " y " + segundos + " segundos " + " en completar el Nivel 1";
            }


            // valores que se muestran para el tiempo del segundo nivel
            SegundosNivel2 = PlayerPrefs.GetInt("SegundosTotalesNivel2", 0);
            MinutosNivel2 = PlayerPrefs.GetInt("MinutosTotalesNivel2", 0);

            if (MinutosNivel2 == 1)
            {
                string minutos = "" + MinutosNivel2;
                string segundos = "" + SegundosNivel2;
                TiempoNivel1.text = "Has tardado " + minutos + " minuto " + " y " + segundos + " segundos " + " en completar el Nivel 2";
            }
            else
            {
                string minutos = "" + MinutosNivel2;
                string segundos = "" + SegundosNivel2;
                TiempoNivel2.text = "Has tardado " + minutos + " minutos " + " y " + segundos + " segundos " + " en completar el Nivel 2";
            }

        // funcion que se ejecuta para calcular el tiempo total de todos los niveles y mostrarlo en el ultimo cuadro de texto
        CalcularTiempo();
        string minTotal = "" + MinutosTotales;
        string segTotal = "" + SegundosTotalesAmostrar;
        TiempoTotal.text = "Tu tiempo total es de " + minTotal +  " minutos " + " y " + segTotal + " segundos";
        }
    



    // metodo que nos calcula el tiempo total entre los dos niveles
    private void CalcularTiempo()
    {
        MinutosTotales = MinutosNivel1 + MinutosNivel2;
        SegundosTotales = SegundosNivel1 + SegundosNivel2;
        if (SegundosTotales >= 60) {
            SegundosTotalesAmostrar = SegundosTotales % 60;
            MinutosTotales += 1;
        }

    }
}

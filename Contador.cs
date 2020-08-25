using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{

    private Text Reloj;
    
    private float segundos;
    private string TiempoSegundos,TiempoMinutos;
    private float minutos;


    
    
    void Start()
    {
        Reloj = GetComponent<Text>();
        TiempoMinutos = "00";
        minutos = 0;
    }

    
    void Update()
    {
        segundos += Time.deltaTime; // te va sumando uno en tiempo real

        int seg = (int)segundos; // lo cambio a entero para que no me salgan los decimales
        PlayerPrefs.SetInt("SegundosTotalesNivel1", seg); // variable que guarda el valor de los segundos durante toda la ejecucion de la aplicación

        if  (seg <= 9 )
        {
            TiempoSegundos = "0" + seg.ToString();
        }
        else
        {
            TiempoSegundos = "" + seg.ToString();
        }


        if (seg == 60)
        {
            int enviarMinutos = (int)minutos;
            PlayerPrefs.SetInt("MinutosTotalesNivel1", enviarMinutos); // variable que guarda el valor de los minutos durante toda la ejecucion de la aplicación
            minutos++;
            int min = (int)minutos;

            if (min <= 9)
            {
                TiempoMinutos = "0" + min.ToString();
            }
            else
            {
                TiempoMinutos = "" + min.ToString();
            }

            
            segundos = 0; // se reinician los segundos cuando llegan a 60
            
        }

        Reloj.text = TiempoMinutos + ":" + TiempoSegundos;
    }

}

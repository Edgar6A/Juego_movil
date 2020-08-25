using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ContadorNivel2 : MonoBehaviour
{
    private Text Reloj;

    private float segundos;
    private string TiempoSegundos, TiempoMinutos;
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

        int seg = (int)segundos;
        PlayerPrefs.SetInt("SegundosTotalesNivel2", seg);
        if (seg <= 9)
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
            PlayerPrefs.SetInt("MinutosTotalesNivel2", enviarMinutos);
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

            segundos = 0;

        }

        Reloj.text = TiempoMinutos + ":" + TiempoSegundos;
    }



}

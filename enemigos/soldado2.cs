﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldado2 : MonoBehaviour
{
    private Transform miTransformacion;
    private Rigidbody rb;
    private GameObject soldado;
    private float velocidad = -5;


    // Start is called before the first frame update
    void Start()
    {
        miTransformacion = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        soldado = GameObject.Find("soldado2");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mover = new Vector3(velocidad, 0, 0);
        miTransformacion.Translate(mover * 1 * Time.deltaTime);
    }



    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "obstaculo")
        {
            if (other.gameObject.name == "cason")
            {
                velocidad = 0;
                velocidad = -5;
            }


            if (other.gameObject.name == "casita")
            {
                velocidad = 0;
                velocidad = 5;
            }

        }

        if (other.gameObject.name == "Player")
        {
            Destroy(soldado);
        }


    }

}

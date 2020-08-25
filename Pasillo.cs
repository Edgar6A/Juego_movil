using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasillo : MonoBehaviour
{
    public GameObject camara,camaraPrincipal; // referencia al objeto camara

    // Start is called before the first frame update
    void Start()
    {
        camara = GameObject.Find("Soporte Camara"); // inicializamos la referencia 
        camaraPrincipal = GameObject.Find("Main Camera"); // inicializamos la referencia 
    }

    private void OnTriggerEnter(Collider other) // metodo que comprueba si han atravesado el objeto con el triger activado
    {
        if (other.name == "Player") // referencia al objeto con el nombre Player
        {
            DesplazarCamara();
        }
        

        
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            SeguirCamara();
        }
    }


    private void DesplazarCamara()
    {
        camara.GetComponent<Player_Follow>().enabled = false; // desactivamos el script que hace que la camara se mueva sola
        // creamos los nuevos datos  de las posiciones donde va a ir la camara
        double Px = -44.41;
        double Py = 71.25;
        double Pz = -28.95;
        float PosicionejeX = System.Convert.ToSingle(Px);
        float PosicionejeY = System.Convert.ToSingle(Py);
        float PosicionejeZ = System.Convert.ToSingle(Pz);
        Vector3 moverPosicion = new Vector3(PosicionejeX, PosicionejeY, PosicionejeZ);
        double xx = 35.7;
        double Ry = 81.5;
        double Rz = -2.7;
        float RotacionEjeX = System.Convert.ToSingle(xx);
        float RotacionnEjeY = System.Convert.ToSingle(Ry);
        float RotacionEjeZ = System.Convert.ToSingle(Rz);
        float ancho = 13;
        Vector3 moverRotacion = new Vector3(RotacionEjeX, RotacionnEjeY, RotacionEjeZ);
        camara.transform.position = moverPosicion; // invocamos el metodo que hace que la camara se mueva
        camara.transform.rotation = Quaternion.Euler(RotacionEjeX, RotacionnEjeY, RotacionEjeZ);  // invocamos el metodo que hace que la camara rote
        camaraPrincipal.GetComponent<Camera>().orthographicSize = ancho; // le damos un zoom extra a la camara
    }

    private void SeguirCamara()
    {
        camara.transform.rotation = Quaternion.Euler(40, 0, 0); // volvemos a volver a girar la camara a la posicion a la que estaba antes de entrar al pasillo
        float zoom = 8; // zoom por defecto
        camaraPrincipal.GetComponent<Camera>().orthographicSize = zoom;
        camara.GetComponent<Player_Follow>().enabled = true; // volvemos a activar el script de seguimiento
    }




}

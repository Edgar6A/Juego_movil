using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Muro : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject salida; // objeto muro

    void Start()
    {
        salida = GameObject.Find("salida"); // referencia al objeto creado muro 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // identifica si hay una colision con el muro

    void OnTriggerEnter(Collider other)
    {
        // hace que el muro desaparezca directamente
        // muro.SetActive(false);
        if (other.tag == "Player")
        {
            Destroy(salida); // el muro desaparece al tocarlo
            SceneManager.LoadScene("Victoria");
        }
        
    }




}

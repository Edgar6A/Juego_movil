using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entrada : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pared; // objeto muro

    void Start()
    {
        pared = GameObject.Find("p1"); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    // identifica si hay una colision con el muro
    void OnCollisionEnter(Collision col)
    {
        // hace que el muro desaparezca directamente
        // muro.SetActive(false);
        Destroy(pared); // el muro desaparece al tocarlo
    }
}

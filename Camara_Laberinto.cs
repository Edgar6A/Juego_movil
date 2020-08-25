using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Laberinto : MonoBehaviour
{
    public GameObject Player;
    private Vector3 CamaraSeguir;



    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        CamaraSeguir = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Player != null)
        {
            Vector3 newPos = Player.transform.position + CamaraSeguir;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        }
    }


}

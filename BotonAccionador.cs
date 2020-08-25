using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonAccionador : MonoBehaviour
{
    public Animator Anim;
    public bool accionador;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Accionador();

        }
    }


    public void Accionador()
    {
        if(accionador == true)
        {
            Anim.SetBool("OnAnim", true);
            accionador = false;
        }
        else
        {
            Anim.SetBool("OnAnim", false);
            accionador = true;
        }
        
    }
}

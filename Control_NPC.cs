using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public class Control_NPC : MonoBehaviour
{
    [Header("NPC")]
    //public GameObject NPC;
    Animator Anim;
    public bool isEnemy; //Si es enemigo = true(tick), sino lo es falso.
    private int giro;
    //Acciones
    bool EnMovimiento = false;
    bool Golpeado = false;


    [Header("Temporizador")]
    public float Tiempo = 10.00f; //Es el tiempo que le damos al temporizador
    public float TimeToRest = 0;   //tiempo que falta para que finalize el temporizador
    float Num_Random;


    // Start is called before the first frame update

    void Start()
    {
        Tiempo = Random.Range(5f, 10f);
        TimeToRest = Tiempo; //Igualamos TimeToRest a  Tiempo para que esta variable se modifique pero no altere a  Tiempo.

        Anim = GetComponent<Animator>();
        //GirarNpc();
        Accion_Aleatoria_Npc();
    }
    private void Update()
    {
        ActivandoTemporizador();
    }
    private void Accion_Aleatoria_Npc()
    {
        if (Golpeado == false)
        {
            Num_Random = Random.Range(0, 10);
        }


        Debug.Log(Num_Random);
        if (Num_Random < 1)
        {
            Anim.SetBool("caminar", false);
            Anim.SetBool("Golpeado", false);
            EnMovimiento = false;
            Golpeado = false;
        }
        else
        {
            Anim.SetBool("caminar", true);
            Anim.SetBool("Golpeado", false);
            EnMovimiento = true;
            Golpeado = false;
        }
        TimeToRest = Tiempo;
    }
    void ActivandoTemporizador()
    {
        TimeToRest -= Time.deltaTime; //hace que el disminuya el tiempo


        if (TimeToRest <= 1) // Si TimeToRest es más pequeño o igual a 0, activa lo que esta dentro.
        {
            Accion_Aleatoria_Npc();
        }
    }
    private void GirarNpc(int direccion)
    {
        giro = direccion;
        transform.rotation = Quaternion.Euler(0, giro, 0);
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "obstaculo")
        {
            if (other.gameObject.name == "casaPrincipio")
            {
                Anim.SetBool("caminar", false);
                int direccion = -90;
                GirarNpc(direccion);
            }

            if (other.gameObject.name == "muro_castillo")
            {
                Anim.SetBool("caminar", false);
                int direccion = 90;
                GirarNpc(direccion);
            }

            if (other.gameObject.name == "Largo_Inicio")
            {
                Anim.SetBool("caminar", false);
                int direccion = 0;
                GirarNpc(direccion);
            }

            if (other.gameObject.name == "Largo_Fin")
            {
                Anim.SetBool("caminar", false);
                int direccion = 180;
                GirarNpc(direccion);
            }


            if (other.gameObject.name == "cason")
            {
                Anim.SetBool("caminar", false);
                int direccion = 270;
                GirarNpc(direccion);
            }

            if (other.gameObject.name == "casita")
            {
                Anim.SetBool("caminar", false);
                int direccion = 90;
                GirarNpc(direccion);
            }

            if (other.gameObject.name == "CasaFin")
            {
                Anim.SetBool("caminar", false);
                int direccion = 270;
                GirarNpc(direccion);
            }

            if (other.gameObject.name == "CasaFinal")
            {
                Anim.SetBool("caminar", false);
                int direccion = 90;
                GirarNpc(direccion);
            }

            if (other.gameObject.name == "CasaIzquierda")
            {
                Anim.SetBool("caminar", false);
                int direccion = 90;
                GirarNpc(direccion);
            }

            if (other.gameObject.name == "Casachoque")
            {
                Anim.SetBool("caminar", false);
                int direccion = 270;
                GirarNpc(direccion);
            }

        }

        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Pantalla_muerte");
        }

    }


}

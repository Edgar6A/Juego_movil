using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Control_Enemy : MonoBehaviour
{
    [Header("VidaEnemigo")]
    public int startingHealth = 100; //
    public int currentHealth;
    public GameObject SliderEnemy;
    public Slider healthSlider;
    public Slider OrageSlider;
    public Transform pivot;
    public Material EnemeyMat;
    Color flashColour = Color.red;
    bool damaged;
    float flashSpeed = 5f;
    [Header("Tipo de Enemigo (arquero = false enemigo cuerpo a cuerpo)")]
    public bool Arquero;
    public GameObject Weapon;
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public float WalkSpeed;
    public float RunSpeed;
    Animator Anim;

    GameObject player;
    Control_Personaje C_P;
    private bool playerInRange;
    public float timer;

    [Header("Rango de vision")]
    public float RangoVision = 20f;
    public float DistanciaAtaqueCuerpo = 3;
    public float DistanciaAtaqueDistancia = 10;
    public NavMeshAgent Agente;

    public Transform[] PuntosMapa;
    private int SiguientePunto = 0;


    private int contador = 0;
    public float DistanciaJugador;
    Vector3 Posicion;
    private bool JugadorVisto;
    bool isdead;
    bool skill;
    public AudioSource GritoVerJugador;
    int look = 0;
    bool stop;
    // Start is called before the first frame update
    void Start()
    {

        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
        healthSlider.value = startingHealth;

        OrageSlider.maxValue = startingHealth;
        OrageSlider.value = startingHealth;


        player = GameObject.FindGameObjectWithTag("Player");
        C_P = player.GetComponent<Control_Personaje>();

        Anim = GetComponentInChildren<Animator>();
        Agente = GetComponent<NavMeshAgent>();

        JugadorVisto = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Definimos el tiempo que pasa para atacar al jugador dede la ultima vez.
        if (isdead == false)
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && playerInRange && currentHealth > 0)
            {
                Attack();
            }


            SliderEnemy.transform.rotation = Quaternion.Euler(40f, pivot.rotation.eulerAngles.y, 0f);


            // Vamos a calcular la distancia que hay entre el enemigo y el player.

            DistanciaJugador = Vector3.Distance(player.transform.position, transform.position);

            Posicion = new Vector3(player.transform.position.x /*+ 1.5f*/, 0, player.transform.position.z /*+ 1.5f*/);

            //Sistema para movimiento enememigo de tipo arquero

            if (VerJugador(out RaycastHit hit) && DistanciaJugador < RangoVision || DistanciaJugador < 6)
            {

                JugadorVisto = true;
                if (look == 0)
                {
                    GritoVerJugador.Play();
                    look = 1;
                }

                Anim.SetBool("isLookPlayer", true);

                Agente.speed = RunSpeed;
            }


            if (JugadorVisto == true && Arquero == true)
            {
                if (skill == false)
                {
                    DetenerAgente();
                    skill = true;
                }
                else
                {
                    if (DistanciaJugador >= DistanciaAtaqueDistancia)
                    {
                        ActualizarDestino(Posicion);
                        Anim.SetBool("CanAttack", false);
                    }

                    if (DistanciaJugador < DistanciaAtaqueDistancia)
                    {
                        DetenerAgente();
                        Anim.SetBool("CanAttack", true);
                    }

                }


            }

            //Sistema para movimiento enememigo de tipo Cuerpo a cuerpo
            if (JugadorVisto == true && Arquero == false)
            {
                if (skill == false)
                {
                    DetenerAgente();
                    StartCoroutine(moveToPlayer());
                }
                else
                {
                    if (DistanciaJugador >= DistanciaAtaqueCuerpo)
                    {
                        ActualizarDestino(Posicion);
                        Anim.SetBool("CanAttack", false);
                        Weapon.SetActive(false);
                    }

                    if (DistanciaJugador < DistanciaAtaqueCuerpo)
                    {
                        DetenerAgente();
                        Anim.SetBool("CanAttack", true);
                        Weapon.SetActive(true);
                    }
                }
            }

            if (stop == false)
            {//Para que haga una ruta de puntos por el mapa
                if (JugadorVisto == false)
                {
                    Weapon.SetActive(false);
                    skill = false;
                    Anim.SetBool("isLookPlayer", false);
                    Anim.SetBool("isWalk", true);
                    Agente.speed = WalkSpeed;

                    ActualizarPuntoMapa();
                    if (LLegarDestino())
                    {
                        DetenerAgente();
                        SiguientePunto = (SiguientePunto + 1) % PuntosMapa.Length;
                        //
                    }

                }

                if (DistanciaJugador >= RangoVision)
                {
                    JugadorVisto = false;
                    look = 0;


                }

            }





        }


        else
        {
            DetenerAgente();
        }

    }







    void Attack()
    {

        timer = 0f;
        playerInRange = false;

        if (C_P.currentHealth > 0)
        {

            DetenerAgente();

        }
    }
    //Con esta sentencia indicamos hacia que puntos vamos del mapa.
    private void ActualizarPuntoMapa()
    {
        //SiguientePunto = 0;
        ActualizarDestino(PuntosMapa[SiguientePunto].position);

    }

    //Si el personaje se mueve o no.
    public void ActualizarDestino(Vector3 posicion)
    {
        Agente.destination = posicion;
        Agente.isStopped = false;

    }


    public void DetenerAgente()
    {
        Agente.isStopped = true;
        Anim.SetBool("isWalk", false);
        stop = true;
        if (isdead == false && JugadorVisto == false)
        {
            StartCoroutine(volveraAndar(5));
        }
        //StartCoroutine(volveraAndar());
    }

    public bool LLegarDestino()
    {
        bool llegar = false;
        if (Agente.remainingDistance <= Agente.stoppingDistance && !Agente.pathPending)
        {
            llegar = true;
            Debug.Log("eL AGENTE LLEGO A SU PUNTO DE ENCUENTRO");
        }
        return llegar;
    }



    public bool VerJugador(out RaycastHit hit, bool MirarHaciaJugador = false)
    {


        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) && hit.collider.CompareTag("Player"))
        {
            MirarHaciaJugador = true;
        }

        return MirarHaciaJugador;
    }



    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Missile") // If collider is tagged as missile
        {
            C_P = player.GetComponent<Control_Personaje>();
            TakeDamage(C_P.attackDamage);
            if (JugadorVisto == false)
            {
                this.transform.LookAt(player.transform);

            }
        }
    }
    public void TakeDamage(int amount) //amount = "cantidad de daño que recibe el jugador"
    {
        EnemeyMat.color = flashColour;
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        StartCoroutine(ChangeColorDamage());
        DetenerAgente();
        StartCoroutine(volveraAndar(0.4f));
        if (currentHealth > 0)
        {
            StartCoroutine(RestOrangeLife());
        }
        else { OrageSlider.value = currentHealth; }

        if (currentHealth <= 0)
        {
            Death();
        }






    }
    void Death()
    {
        //Aqui declaramos el comportamiento de la muerte
        isdead = true;
        DetenerAgente();
        Anim.SetBool("isDead", true);
        Destroy(this.gameObject, 2.5f);
    }
    IEnumerator RestOrangeLife()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);

        //After we have waited 5 seconds print the time again.
        OrageSlider.value = currentHealth;


    }
    IEnumerator volveraAndar(float cont)
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(cont);

        //After we have waited 5 seconds print the time again.

        stop = false;
        Debug.Log("Cambiando objetivo");


    }
    IEnumerator ChangeColorDamage()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.2f);


        EnemeyMat.color = Color.white;

    }
    IEnumerator moveToPlayer()
    {
        yield return new WaitForSeconds(0.5f);

        skill = true;
        stop = false;
    }
}
  


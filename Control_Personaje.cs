using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Control_Personaje : MonoBehaviour
{
    public int NumeroCamara = 0;

    public bool ANDROID_On;
    [Header("Control Velocidad")]
    public float Velocidad_Translacion = 10;
    public float velocidad_Rotacion;
    public float gravityScale;

    [Header("Componentes Avatar")]
    public GameObject Avatar;
    public Transform pivot;
    public CharacterController controller;
    public Animator Anim;
    private Vector3 moveDirection;

    [Header("Vida Jugador")]
    public int startingHealth = 100; //
    public int currentHealth;
    public Text Text_Health;
    public Slider healthSlider;
    public Slider RedSlider;
    public Image damageImage;
    public Image barColor;
    public AudioSource Hurt;
    public AudioSource AudioCriticalState;
    public AudioClip DamageLow;
    public AudioClip DamageCritical;
    public AudioSource Dead;

    float flashSpeed = 5f;
    Color flashColour = Color.red;

    int changecolor = 0;
    float tempo = 0.2f;
    public float TimeResto = 1f;

    bool isDead;
    bool damaged;

    public bool CriticalState; //si es verdaddero es rojo sino es amarrilo
    [Header("Recuperación vida")]
    public int NumCure = 200;
    public float InicialTimeCure = 2f;
    public float TimetoNextCure;
    public AudioSource SonidoCuracion;
    bool sonidoActivad = false;

    [Header("Habilidades")]
    public bool Shield_bool;
    public bool Light_bool;
    public bool AtackManual;

    [Header("Escudo Propiedades")]
    public float TimeJostickBlock = 2f;
    public float VidaEscudoIncio = 20f; //
    public float vidaEscudo;
    public Slider SliderShild;
    bool ShildActivado;

    [Header("Efectos Visuales Avatar")]
    public GameObject CureEfect;
    public GameObject Dirrecion;
    public GameObject Shield_GO;
    public GameObject JostickBlock;
    //public GameObject Light_GameObject;
    public Animator BallFire_Anim;

    
    [Header("AtaqueBasico Propiedades")]
    public GameObject FireDirectionObj;
    public Transform spawnPosition;
    public Slider TimeRecargaAttack;
    public Slider ColorAttack;
    public float RecargaTime = 30f;
    public float speedTimeRecarga = 5;
    bool AtaqueManualActivado;
    public Transform ManualspawnPosition;
    public float speedProyectile = 500;
    public int attackDamage = 100;
    RaycastHit hit;
    private Vector3 moveDirection2;

    [Header("Armas")]
    public GameObject[] projectiles;
    public int currentProjectile;
    public GameObject[] ArmasCuerpo;
    public int currentArmaCuerpo;
    public float rangeRadius = 10;
    private void Awake()
    {
        ArmasCuerpo[currentArmaCuerpo].SetActive(false);

        FireDirectionObj.SetActive(false);

        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
        healthSlider.value = startingHealth;

        RedSlider.maxValue = startingHealth;
        RedSlider.value = startingHealth;
        EstadoColorLife();

        SliderShild.maxValue = VidaEscudoIncio;

        SliderShild.gameObject.SetActive(false);
        Shield_GO.SetActive(false);
        Shield_bool = false;
        JostickBlock.SetActive(false);

        TimetoNextCure = InicialTimeCure;

    }
    // Update is called once per frame
    void Update()
    {
        RecargarAtaque();
        //---------------------AQUI COMIENZA LOS COMPONETES DE LA VIDA------------------------------//
        Text_Health.text = "" + currentHealth;


        EstadoColorLife();
        if(currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
        if (damaged)
        {
            damageImage.color = flashColour; 
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            healthSlider.value = currentHealth;
            //RedSlider.value = currentHealth;

        }
        damaged = false;

        //------------------------------ Regeneracion de la vida del Jugador--------------------------------------------//
        if (currentHealth < startingHealth && damaged == false && isDead == false)
        {
           
            TimetoNextCure -= Time.deltaTime;
            if(TimetoNextCure < 0.5)
            {
                CureEfect.SetActive(true);
                if (sonidoActivad== false && TimetoNextCure < 0.2)
                {
                    SonidoCuracion.Play();
                    sonidoActivad = true;
                }
                
            }

            if (TimetoNextCure < 0)
            {
                
                currentHealth += NumCure;
                RedSlider.value = currentHealth;
                healthSlider.value = currentHealth;
                TimetoNextCure = InicialTimeCure;
                sonidoActivad = false;
            }
            

        }
        if (CureEfect.activeSelf == true)
        {
            StartCoroutine(DesactivarCureEffect());
            

        }



            //---------------------AQUI COMIENZA LOS COMPORTAMIENTOS DE MOVIMIENTO DEL JUGADOR------------------------------//
        if (Shield_bool== false)
        {

            //Comportamientos de mover al personaje
            if(ANDROID_On == false)
            {

                switch (NumeroCamara)
                {
                    case 0: 
                        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;

                    case 1: 
                        moveDirection = (transform.forward * -Input.GetAxis("Horizontal")) + (transform.right * Input.GetAxis("Vertical"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 2: 
                        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 3: 
                        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 4: 
                        moveDirection = (transform.forward * -Input.GetAxis("Vertical")) + (transform.right * -Input.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 5:
                        moveDirection = (transform.forward * -Input.GetAxis("Vertical")) + (transform.right * -Input.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 6: 
                        moveDirection = (transform.forward * -Input.GetAxis("Vertical")) + (transform.right * -Input.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;
                }


                controller.Move(moveDirection * Time.deltaTime);

                Dirrecion.SetActive(false);
                //Mover el jugador en diferentes direcciones sigun la camara
                
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    Dirrecion.SetActive(true);
                    transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);//Con esta sentencia puedo mover la camara y no el personaje.
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                    Avatar.transform.rotation = Quaternion.Slerp(Avatar.transform.rotation, newRotation, velocidad_Rotacion * Time.deltaTime);
                    Dirrecion.transform.rotation = newRotation;
                }


                //Con esto hacemos que el personaje tenga gravedad.
                moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
                controller.Move(moveDirection * Time.deltaTime);

                //para la animacion de mover

                
                Anim.SetFloat("isRun", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
            }
            else
            {


                switch (NumeroCamara)
                {
                    case 0: //bien
                        moveDirection = (transform.forward * CrossPlatformInputManager.GetAxis("Vertical")) + (transform.right * CrossPlatformInputManager.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;

                    case 1: //bien
                        moveDirection = (transform.forward * -CrossPlatformInputManager.GetAxis("Horizontal")) + (transform.right * CrossPlatformInputManager.GetAxis("Vertical"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 2: //bien
                        moveDirection = (transform.forward * CrossPlatformInputManager.GetAxis("Vertical")) + (transform.right * CrossPlatformInputManager.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 3: //bien
                        moveDirection = (transform.forward * CrossPlatformInputManager.GetAxis("Vertical")) + (transform.right * CrossPlatformInputManager.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 4: //bien
                        moveDirection = (transform.forward * -CrossPlatformInputManager.GetAxis("Vertical")) + (transform.right * -CrossPlatformInputManager.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 5:
                        moveDirection = (transform.forward * -CrossPlatformInputManager.GetAxis("Vertical")) + (transform.right * -CrossPlatformInputManager.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;


                    case 6: //bien
                        moveDirection = (transform.forward * -CrossPlatformInputManager.GetAxis("Vertical")) + (transform.right * -CrossPlatformInputManager.GetAxis("Horizontal"));
                        moveDirection = moveDirection.normalized * Velocidad_Translacion;
                        break;
                }


                controller.Move(moveDirection * Time.deltaTime);

                Dirrecion.SetActive(false);
                //Mover el jugador en diferentes direcciones sigun la camara
                if (CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetAxis("Vertical") != 0)
                {
                    Dirrecion.SetActive(true);

                    transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);//Con esta sentencia puedo mover la camara y no el personaje.
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                    Avatar.transform.rotation = Quaternion.Slerp(Avatar.transform.rotation, newRotation, velocidad_Rotacion * Time.deltaTime);

                    Dirrecion.transform.rotation = newRotation;



                }


                //Con esto hacemos que el personaje tenga gravedad.
                moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
                controller.Move(moveDirection * Time.deltaTime);

                //para la animacion de mover


                Anim.SetFloat("isRun", (Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical")) + Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal"))));
                

            }

        }
        else 
        {
            
            VidaEscudo();
            if(JostickBlock.activeSelf == false)
            {
                if(ANDROID_On == false)
                {
                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    {


                        SliderShild.gameObject.SetActive(false);
                        Anim.SetBool("onShield", false);
                        StartCoroutine(EscudoResiduo());
                        Shield_bool = false;
                        JostickBlock.SetActive(false);
                    }
                }
                else
                {
                    if (CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetAxis("Vertical") != 0)
               // if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {


                    SliderShild.gameObject.SetActive(false);
                    Anim.SetBool("onShield", false);
                    StartCoroutine(EscudoResiduo());
                    Shield_bool = false;
                    JostickBlock.SetActive(false);
                }

                }
                

            }
           


        }
        ////// delcaramos el ataque
        moveDirection2 = (transform.forward * CrossPlatformInputManager.GetAxis("Vertical_2")) + (transform.right * CrossPlatformInputManager.GetAxis("Horizontal_2"));
        moveDirection2 = moveDirection2.normalized * Velocidad_Translacion;

        if (CrossPlatformInputManager.GetAxis("Horizontal_2") != 0 || CrossPlatformInputManager.GetAxis("Vertical_2") != 0)
        //if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            AtackManual = true;
            AtaqueManualActivado = true;
            FireDirectionObj.SetActive(true);
            Quaternion newRotation2 = Quaternion.LookRotation(new Vector3(moveDirection2.x, 0f, moveDirection2.z));

            FireDirectionObj.transform.rotation = newRotation2;

        }
        else if(CrossPlatformInputManager.GetAxis("Horizontal_2") == 0 || CrossPlatformInputManager.GetAxis("Vertical_2") == 0)
        {
            AtackManual = false;
            FireDirectionObj.SetActive(false);
            FireDirectionObj.transform.Rotate(0, 0, 0);


        }

        
        

       

        if (Light_bool == true)
        {
            StartCoroutine(ApagarAnimLuz());
        }
       

        //Light_GameObject.transform.rotation = Quaternion.Euler(40f, pivot.rotation.eulerAngles.y, 0f); //CON ESTA SENTICIA HACEMOS QUE EL COMPONENTE LIGHT SIEMPRE MIRE A LA CAMARA.

       
        FireDirectionObj.transform.Rotate(0, 0, 0); // Restart Rotation
    }

    public void RecargarAtaque()
    {
        if(RecargaTime <30)
        {
            RecargaTime += Time.deltaTime * speedTimeRecarga;

            TimeRecargaAttack.value = RecargaTime;
            if (RecargaTime >= 10 && RecargaTime <20)
            {
                ColorAttack.value = 10;
            }
            else if (RecargaTime >= 20 && RecargaTime < 30)
            {
                ColorAttack.value = 20;

            }else if(RecargaTime < 10)
            {
                ColorAttack.value = 0;
            }
        }
        else if (RecargaTime >= 30)
        {
            RecargaTime = 30;
            ColorAttack.value = RecargaTime;
        }
        


        
    }

    public void AttackPlayer()
    {
        if (Shield_bool == false && RecargaTime >=10)
        {
           
           
            Anim.SetBool("onAttack", true);
            //IntanciarProyectil();
            StartCoroutine(AppagarAnimAttack());
        }
        else if (Shield_bool == true)
        {
            ControlEscudo();
            AttackPlayer();
        }
        

    }
    public void IntanciarProyectil()
    {
        RecargaTime -= 10;
        ColorAttack.value = RecargaTime;
        TimeRecargaAttack.value = RecargaTime;
        if (AtackManual == true || AtaqueManualActivado == true)
        {
            AtaqueManualActivado = false;
            GameObject projectile = Instantiate(projectiles[currentProjectile], ManualspawnPosition.position, ManualspawnPosition.transform.rotation) as GameObject; //Spawns the selected projectilc
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speedProyectile); //Set the speed of the projectile by applying force to the rigidbody
            Debug.Log("El PERSONAJE ATACO De forma manual");

        }
        else
        {
            // Sistema para futuros enemigos a distancia
            //Encontrar todos los GameObjects que tenga un collider dentro de mi rango de disparo.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeRadius);
            if(hitColliders.Length!=0)
            {
                //Programar la logica contra los posibles objetivos
                //Bucle entre todos los objetivos anteriores para encontrar el enemigo mas cercano

                float minDistance = int.MaxValue;
                int index = - 1;

                for (int i = 0; i <hitColliders.Length; i ++)
                {
                    if(hitColliders[i].tag == "Enemy")
                    {
                        //Estoy seguro que he chocado contra un enemigo
                        float distance = Vector3.Distance(hitColliders[i].transform.position, this.transform.position);
                        if(distance < minDistance)
                        {
                            index = i;
                            minDistance = distance;
                        }
                    }
                }

                if(index<0)
                {
                    spawnPosition.transform.rotation = Quaternion.Euler(0f, Avatar.transform.rotation.eulerAngles.y, 0f);
                    GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, spawnPosition.rotation) as GameObject; //Spawns the selected projectilc
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speedProyectile); //Set the speed of the projectile by applying force to the rigidbody
                    Debug.Log("El PERSONAJE ATACO De forma automatica y no encontro enemigos");
                    return;
                }
                else
                {
                    //si estoy aqui esque tenemos un objetivo al que disparar 
                    Transform target = hitColliders[index].transform;
                    spawnPosition.LookAt(target);
                    //
                    GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, spawnPosition.rotation) as GameObject; //Spawns the selected projectilc
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speedProyectile); //Set the speed of the projectile by applying force to the rigidbody
                    Debug.Log("El PERSONAJE ATACO De forma automatica y encontro enemigo");

                }

                


            }

        }

    }
    public void IntanciarArmaCuerpoaCuerpo()
    {
        RecargaTime -= 10;
        ColorAttack.value = RecargaTime;
        TimeRecargaAttack.value = RecargaTime;
        if (AtackManual == true || AtaqueManualActivado == true)
        {
            AtaqueManualActivado = false;
            ArmasCuerpo[currentArmaCuerpo].SetActive(true);
            Debug.Log("El PERSONAJE ATACO De forma manual");

        }
        else
        {
            ArmasCuerpo[currentArmaCuerpo].SetActive(true);
            Debug.Log("El PERSONAJE ATACO De forma automatica");

        }

    }
    public void OcultarArmaCuerpo()
    {

        ArmasCuerpo[currentArmaCuerpo].SetActive(false);
    }

    public void ControlEscudo()
    {
        if(Shield_bool == false)
        {
            
            Anim.SetBool("onShield", true);
            StartCoroutine(EscudoActivacion());
        }
        else
        {
            Shield_GO.SetActive(false);
            SliderShild.gameObject.SetActive(false);
            ShildActivado = false;
            Shield_bool = false;
            Anim.SetBool("onShield", false);
            JostickBlock.SetActive(false);

        }
    }
    

    void VidaEscudo()
    {
        ShildActivado = true;
        vidaEscudo -= Time.deltaTime;
        

        SliderShild.value = vidaEscudo;
       if (vidaEscudo < 0)
       {
            ControlEscudo();
                
        }
        if (vidaEscudo <= VidaEscudoIncio-TimeJostickBlock)
        {
            JostickBlock.SetActive(false);
        }
    }
    public void ControlLuz()
    {
        if (Light_bool == false)
        {
            Anim.SetBool("onLight", true);
            StartCoroutine(EncenderLuces());
        }
        else
        {
            BallFire_Anim.SetBool("LightOff", true);
            Anim.SetBool("onLight", false);
            StartCoroutine(ApagarLuz());

        }
    }

    public void TakeDamage(int amount) //amount = "cantidad de daño que recibe el jugador"
    {
        
        if (Shield_bool == false && ShildActivado == false)
        {
            damaged = true;
            currentHealth -= amount;
            TimetoNextCure = InicialTimeCure*2;
            //Hurt.Play();
            if (currentHealth > 0)
            {
                StartCoroutine(RestRedLife());
            }
            else { RedSlider.value = currentHealth; }

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }

        }
        
        



    }


    void Death()
    {
        isDead = true;
        Dead.Play();
        //Aqui declaramos el comportamiento de la muerte
        SceneManager.LoadScene("Pantalla_muerte");
    }
    

    void EstadoColorLife()
    {
         

        int e = startingHealth / 2;

        if (currentHealth <= startingHealth && currentHealth >= e || currentHealth >= startingHealth)
        {
            barColor.color = Color.green;
            if (AudioCriticalState.isPlaying == true)
            {

                AudioCriticalState.Stop();
                CriticalState = false;
            }

        }
        else if (currentHealth < e && currentHealth >= e/2)
        {
            
            //barColor.color = Color.yellow;
            tempo = 0.2f;
            if(AudioCriticalState.isPlaying == false && CriticalState == false)
            {
                
                AudioCriticalState.clip = DamageLow;
                AudioCriticalState.Play();
            }
            else if(AudioCriticalState.isPlaying == true && CriticalState == true)
            {
                AudioCriticalState.Stop();
                CriticalState = false;
                AudioCriticalState.clip = DamageLow;
                AudioCriticalState.Play();

            }
           
            if (changecolor ==0)
            {
                
                StartCoroutine(ChangeToYelow());
            }
            else if (changecolor == 1)
            {
                
                StartCoroutine(ChangeToGreen());
            }
           
            

        }
        else if (currentHealth > 0 && currentHealth < e / 2)
        {
            //barColor.color = Color.red;
            tempo = 0.1f;
            
            if (AudioCriticalState.isPlaying == true && CriticalState == false)
            {
                AudioCriticalState.Stop();
                CriticalState = true;
                AudioCriticalState.clip = DamageCritical;
                AudioCriticalState.Play();
            }
            

            if (changecolor == 0)
            {
                StartCoroutine(ChangeToRed());
            }
            else if (changecolor == 1)
            {

                StartCoroutine(ChangeToGreen());
            }
            
        }
        else
        {
            AudioCriticalState.Stop();
        }

        
    }


    IEnumerator AppagarAnimAttack()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);
        Anim.SetBool("onAttack", false);


    }
    IEnumerator ApagarAnimLuz()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);
        Anim.SetBool("onLight", false);
        

    }
    IEnumerator EncenderLuces()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.3f);
        BallFire_Anim.SetBool("LightOff", false);
        Light_bool = true;
        //Light_GameObject.SetActive(true);

    }
    IEnumerator ApagarLuz()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.2f);
        Anim.SetBool("onLight", false);
        //After we have waited 5 seconds print the time again.
        //Light_GameObject.SetActive(false);
        Light_bool = false;

    }
    IEnumerator EscudoActivacion()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.2f);

        //After we have waited 5 seconds print the time again.
        vidaEscudo = VidaEscudoIncio;
        SliderShild.gameObject.SetActive(true);
        JostickBlock.SetActive(true);
        Shield_GO.SetActive(true);
        Shield_bool = true;

    }
    IEnumerator EscudoResiduo()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);

        //After we have waited 5 seconds print the time again.
        Shield_GO.SetActive(false);
        ShildActivado = false;

    }

    IEnumerator RestRedLife()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.8f);

        //After we have waited 5 seconds print the time again.
        RedSlider.value = currentHealth;

    }
    



    IEnumerator ChangeToGreen()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(tempo);

        //After we have waited 5 seconds print the time again.
        barColor.color = Color.green;
        changecolor = 0;
        
    }
    IEnumerator ChangeToYelow()
    {
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(tempo);

        //After we have waited 5 seconds print the time again.
        
        barColor.color = Color.yellow;
        changecolor = 1;
        
      
        
    }
   
    IEnumerator ChangeToRed()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(tempo);

        //After we have waited 5 seconds print the time again.
        damaged = true;
        barColor.color = Color.red;
        changecolor = 1;
        
       
    }

    IEnumerator DesactivarCureEffect()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1f);

        //After we have waited 5 seconds print the time again.
        CureEfect.SetActive(false);



    }

    



}

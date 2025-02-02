using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealthController : MonoBehaviour
{   
    //Necesario para crear instancia junto con el método Awake()
    public static CharacterHealthController instance;

    public int currentHealth;
    public int maxHealth;

    //Duración invencibilidad
    public float invincibleLenght;
    //Contador invencibilidad
    private float invincibleCounter;

    private SpriteRenderer spriteRenderer;
    public Animator animator;

    //Tiempo de espera antes de reiniciar la escena
    public float respawnDelay;

    public bool isDead = false;

    public AudioSource audioDeath;


    private void Awake()
    {
        instance = this;        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Al crear el player siempre tendrá máxima vida
        isDead = false;
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Si le acaban de hacer daño será mayor que 0
        if(invincibleCounter > 0)
        {   
            //Empieza el conteo
            invincibleCounter -= Time.deltaTime;

            //Cuando invencibleCounter llegue a 0, ya no habrá invencibilidad y restauramos el color del sprite a normal
            if (invincibleCounter <= 0) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                    }
        }
    }

    public void DealDamage()
    {
        //Si no hay de invencibilidad, baja una vida y salta la animación de herido
        if(invincibleCounter <= 0)
        {
            currentHealth--;
            Character.instance.animator.SetTrigger("Hurt");

            //Si la vida llega a 0, muere
            if (currentHealth <= 0)
            {
               Death();
            }
            else {
                //Asigna el tiempo de invencibilidad y modifica el sprite del player durante ese tiempo
                invincibleCounter = invincibleLenght;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, .5f);
            }

            //Actualiza el canvas de vida
            UIHealthController.instance.UpdateHealthDisplay();
        }
       
    }

    public void HealPlayer()
    {
        currentHealth++;
        //Si la vida está a tope se queda como está
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        //Actualiza el canvas de vida
        UIHealthController.instance.UpdateHealthDisplay();
    }

    private IEnumerator RespawnAfterDelay()
    {
       
        //Espera un tiempo especificado antes de reiniciar
        yield return new WaitForSeconds(respawnDelay);

        //Reinicia la escena en la que estamos
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Death()
    {
        audioDeath.Play();
        animator.SetTrigger("Death");
        currentHealth = 0;

          //Recoge el nombre de la escena actual
          string currentScene = SceneManager.GetActiveScene().name;

        //Si estamos en la primera escena, reiniciamos las monedas a 0
        if (currentScene == "FirstScene") 
        {
            LevelManager.instance.coinsCollected = 0;
        }
        //Si estamos en la segunda escena, restauramos las monedas con las que acabamos la escena anterior
        else if (currentScene == "SecondScene") 
        {
            LevelManager.instance.RestoreCoinsAtSceneStart();
            UIHealthController.instance.UpdateCoinCount();
        }

        isDead = true;
        //Animación de muerte
        Character.instance.DeathAnim();

        //Desactiva los controles al morir
        Character.instance.controlsEnabled = false;

        //Inicia la corrutina para esperar antes de reiniciar la escena
        StartCoroutine(RespawnAfterDelay());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si cae en el vacio o sobre los pinchos se muere de una
        if (other.CompareTag("Spikes") || other.CompareTag("FallZone"))
        {
            Death();
        }

        if (other.CompareTag("EnemySword"))
        {
            DealDamage();
        }
    }
}

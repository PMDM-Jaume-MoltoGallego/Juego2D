using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Velocidad del enemigo
    public float speed = 2f;
    //Distancia para detectar al jugador
    public float detectionRange = 5f;
    //Distancia para atacar al jugador
    public float attackRange = 1f;
    //Vida del enemigo
    private int currentHealth = 3; 

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    //Para evitar múltiples ataques seguidos
    private bool isAttacking = false;  

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Encuentra al jugador por su tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            //Si el jugador está cerca, el enemigo lo sigue
            if (distance < attackRange && !isAttacking)
            {
                Attack();
            }
            else if (distance < detectionRange && distance >= attackRange)
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        //Solo moverse si no está atacando
        if (!isAttacking)  
        {
            animator.SetTrigger("Grounded");
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

            //Voltea sprite 
            if (direction.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void Attack()
    {
        if (!CharacterHealthController.instance.isDead)
        {
            isAttacking = true;
            //Detiene al enemigo mientras ataca
            rb.linearVelocity = Vector2.zero;  
            animator.SetTrigger("Attack");


            //Espera 1 segundo antes de poder atacar de nuevo
            Invoke("ResetAttack", 1f);  
        }
        else
        {
            animator.SetTrigger("Grounded");
        }
       
    }
    private void ResetAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            currentHealth--;
            animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        //Frena el movimiento del enemigo al morir
        rb.simulated = false;
        animator.SetTrigger("Death");
        //Destruye al enemigo pasados 2 segundos
        Destroy(gameObject, 2f);
    }
}

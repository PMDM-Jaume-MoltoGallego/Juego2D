using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character instance;

    public float Speed;
    public float lateralMovement;
    public float jumpMovement = 400.0f;
    public Transform groundCheck;
    public Animator animator;
    private Rigidbody2D rigidbody2d;
    public bool grounded = true;

    //Controla la dirección del personaje
    private bool facingRight = true;
    //Controla los controles
    public bool controlsEnabled = true;
    private float movementButton;
    public AudioSource audioJump;
    public AudioSource audioAttack;

    public void Awake()
    {
        instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        //Aseguro que los controles estén disponibles al empezar
        controlsEnabled = true; 
    }

    // Update is called once per frame
    void Update()
    {
        //Si los controles están desactivados, sale del update
        if (!controlsEnabled)
            return; 

        grounded = Physics2D.Linecast(transform.position,
       groundCheck.position,
       LayerMask.GetMask("Ground"));

            //TODO BORRAR ESTA CONDICIÓN PARA QUE FUNCIONE EN MÓVILES
            //if (grounded && Input.GetButtonDown("Jump"))
        //{
          //  rigidbody2d.AddForce(Vector2.up * jumpMovement);
            //audioJump.Play();
        //}
       
        //

        if (grounded)
                animator.SetTrigger("Grounded");
            else
           
        animator.SetTrigger("Jump");

            //TODO PARA PC
            //Speed = lateralMovement * Input.GetAxis("Horizontal");
            //TODO PARA MÓVILES
            Speed = lateralMovement * movementButton;
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
            animator.SetFloat("Speed", Mathf.Abs(Speed));

            //Gira el personaje al cambiar de dirección, para que no siempre mire a la derecha
            if (Speed > 0 && !facingRight)
            {
                Flip();
            }
            else if (Speed < 0 && facingRight)
            {
                Flip();
            }

        //TODO BORRAR ESTA CONDICIÓN PARA QUE FUNCIONE EN MÓVILES
        //Gestiona un segundo botón para atacar con las espada
       // if (Input.GetButtonDown("Fire1"))
        //{
            //Permite atacar incluso si está en Hurt
          //  if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurt")) 
            //{
              //  audioAttack.Play();
                //animator.SetTrigger("Attack");
                
            //}
        //}
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; 
        transform.localScale = localScale;
    }

    // ADD - MOBILE
    public void Jump()
    {
        if (grounded)
        {
            rigidbody2d.AddForce(Vector2.up * jumpMovement);
            audioJump.Play();
        }
            
    }

    public void Move(float amount)
    {
        movementButton = amount;
    }

    public void Attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurt"))
        {
            audioAttack.Play();
            animator.SetTrigger("Attack");

        }
    }

    public void DeathAnim()
    {
        animator.SetTrigger("Death");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MobilePlatform"))
        transform.SetParent(other.transform);
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MobilePlatform"))
        transform.SetParent(null);
    }
    
}

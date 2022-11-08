    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;


    public class Player : MonoBehaviour 
{    

    private int vida;
    private int vidaMaxima = 3;

    [SerializeField] Image vidaOn;
    [SerializeField] Image vidaOff;

    [SerializeField] Image vidaOn2;
    [SerializeField] Image vidaOff2;

    public float Speed;
    public float JumpForce;
    public bool Jumping;
    private Rigidbody2D rig;
    Animator anim;
    
    void Start()
    {
        vida = vidaMaxima;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        //Direita
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("run", true);
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        //Esquerda
         if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("run", true);
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        //Parado
         if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("run", false);
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
            
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && Jumping )
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            Jumping = false;
            anim.SetBool("Jump", true);
            anim.SetBool("Run", false);
            

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            Jumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Inimigo"))
        {
          Dano();
        }
}

     private void Dano()
       {
         vida -= 1;

         if(vida == 2)
         {
           vidaOn2.enabled = true;
           vidaOff2.enabled = false;
         }
            else
         
            {
                vidaOn2.enabled = false;
                vidaOff2.enabled = true;
            }

            if(vida == 1)
            {
               vidaOn2.enabled = true;
               vidaOff2.enabled = false;

               vidaOn.enabled = true;
               vidaOff.enabled = false;
            }
            else
            {
             vidaOn.enabled = false;
             vidaOff.enabled = true;
            }
        }
        public void AddHealth(float _value)
        {
         Dano();
        }
}








    

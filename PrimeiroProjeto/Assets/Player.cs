using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool Jumping;
    private Rigidbody2D rig;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
}
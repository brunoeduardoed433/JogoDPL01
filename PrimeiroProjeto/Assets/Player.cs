    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;


    public class Player : MonoBehaviour 
{    

    public static int vida;
    private int vidaMaxima = 3;

    public Button restartButton;


    [SerializeField] Image CoracaoVazio2;
    [SerializeField] Image CoracaoCheio2;

    [SerializeField] Image CoracaoVazio3;
    [SerializeField] Image CoracaoCheio3;

    [SerializeField] Image CoracaoVazio1;
    [SerializeField] Image CoracaoCheio1;

    [SerializeField] Image Gameover;

    public float Speed;
    public float JumpForce;
    public bool Jumping;
    private Rigidbody2D rig;
    Animator anim;
    
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    void Start()
    {
        Gameover.enabled = false;
        vida = vidaMaxima;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        restartButton.gameObject.SetActive(false); 
    }

    void Update()
    {
        estouJogando();
    }
    
    public void estouJogando(){
        if(vida!=0){
        Move();
        Jump();
        }
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
          Debug.Log(vida);
        }
        if(col.gameObject.CompareTag("Health"))
        {
          Destroy(col.gameObject);
          recuperaVida();
        }
}

     private void recuperaVida(){
        if(vida !=3 && vida >0 ){
            vida++;
            Debug.Log(vida);
            atualizaCoracao();
        }

     } 
     private void atualizaCoracao(){
            switch(vida){
                case 3:
                CoracaoCheio1.enabled = true;
                CoracaoCheio2.enabled = true;
                CoracaoCheio3.enabled = true;
                CoracaoVazio1.enabled = false;
                CoracaoVazio2.enabled = false;
                CoracaoVazio3.enabled = false;
                break;
            case 2:
                CoracaoCheio1.enabled = true;
                CoracaoCheio2.enabled = true;
                CoracaoCheio3.enabled = false;
                CoracaoVazio1.enabled = false;
                CoracaoVazio2.enabled = false;
                CoracaoVazio3.enabled = true;
                break;
            
                case 1:
                CoracaoCheio1.enabled = true;
                CoracaoCheio2.enabled = false;
                CoracaoCheio3.enabled = false;
                CoracaoVazio1.enabled = false;
                CoracaoVazio2.enabled = true;
                CoracaoVazio3.enabled = true;
                break;
            
                case 0:
                CoracaoCheio1.enabled = false;
                CoracaoCheio2.enabled = false;
                CoracaoCheio3.enabled = false;
                CoracaoVazio1.enabled = true;
                CoracaoVazio2.enabled = true;
                CoracaoVazio3.enabled = true;
                Debug.Log("GAME OVER!!!");
                restartButton.gameObject.SetActive(true); 
                Gameover.enabled = true;
                restartButton.onClick.AddListener(RestartGame);
                break;
            }
            
        

     }

     private void Dano()
       {
         if(vida<=3 && vida>0){
         vida -= 1;
         atualizaCoracao();
         }
        } 
}








    

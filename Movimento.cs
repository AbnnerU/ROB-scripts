using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private bool olhandoDireita = true;
    private Rigidbody2D rigidbodyObjeto;
    private float inputMovimento;
    [HideInInspector]
    public bool parando;
    //particulas
    public Transform particulaPosicao;
    public GameObject particula;

    //Detectores
    public string estado="No ar";
    private bool  estaNaParede, alturaMinimaParaEstaParede;
    public float alturaMinimaGrudarParede;
    public LayerMask layer;


    //Movimento horizontal
    public float velocidadeMaxima;
    public float tempoParaVelocidadeMaxima;
    public float valorDesaceleracao;
    private float aceleracao;
    private float desaceleracao;
    private float velocidadeAtual;
    public float velocidadeMinimaQuandoVirar;
    public float velocidadeQuandoVirar;
    bool pisandoNochao;

    //Pulo
    private bool estaPulando;
    public float forcaDoPulo;
    private bool ativouPulo;
    private float jumpTimer;
    private bool jumpDelay;
    

    //Wall jump
    private bool pulouDaparede;
    public float velocidadeDeslizamento;
    private bool soltarParede;
    public float delayParaComeçarDeslizar;
    public  GameObject detectorParede;
    private bool parede;
    private bool deslizar;
    private bool desabilitar;
    private bool impedirControle;


    //Velocidade queda
    public float velocidadeQueda;

    //animacoes
    Animator anim;
    

    void Start()
    {
        parando = false;
        anim = GetComponent<Animator>();
        parede = detectorParede.GetComponent<Detector_parede>().estaNaParede;
        velocidadeDeslizamento = -velocidadeDeslizamento;
        desabilitar = true;
        impedirControle = false;
        rigidbodyObjeto = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (Pause.AumentarVElocidade)
        {
            velocidadeMaxima = 7;
        }

        if (detectorParede.GetComponent<Detector_parede>() != null)
        {
            parede = detectorParede.GetComponent<Detector_parede>().estaNaParede;
        }
        if (desabilitar == false)
        {
            DestaivarColider();
        }

        alturaMinimaParaEstaParede = Physics2D.Raycast(transform.position, -Vector2.up, alturaMinimaGrudarParede, layer);
        if (alturaMinimaParaEstaParede)
        {
            parede = false;
            estaNaParede = false;
        }
        //Wall jump

        if (parede==true)
        {

            estaNaParede = true;

            if (inputMovimento != 0 && soltarParede==false) // Para de deslizar
            {
                deslizar = false;

            }
            else if (inputMovimento == 0 && soltarParede == false)  // Desliza
            {
                deslizar = true;

            }
            if (Input.GetButtonDown("Jump") && soltarParede == false) // Pulou da parede
            {   
                pulouDaparede = true;
            }
             if (Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
            {
                soltarParede = true;
            }

        }
        else
        {
            estaNaParede = false;
            rigidbodyObjeto.gravityScale = 1;
        }

        //movimentação horizontal
        if (impedirControle == false)
        {
            
                inputMovimento = Input.GetAxis("Horizontal");

                
            if (estaNaParede == false && parede==false)
            {
                
                if (inputMovimento > 0 && estaNaParede == false)
                {
                    aceleracao = velocidadeMaxima / tempoParaVelocidadeMaxima * Time.deltaTime;
                    velocidadeAtual = Mathf.MoveTowards(velocidadeAtual, velocidadeMaxima, aceleracao);

                    rigidbodyObjeto.velocity = new Vector2(velocidadeAtual, rigidbodyObjeto.velocity.y);
                    desaceleracao = 0;
                    rigidbodyObjeto.drag = desaceleracao;
                    parando = false;
                }
                else if (inputMovimento < 0 && estaNaParede == false)
                {
                    aceleracao = velocidadeMaxima / tempoParaVelocidadeMaxima * Time.deltaTime;
                    velocidadeAtual = Mathf.MoveTowards(velocidadeAtual, velocidadeMaxima, aceleracao);

                    rigidbodyObjeto.velocity = new Vector2(-velocidadeAtual, rigidbodyObjeto.velocity.y);
                    desaceleracao = 0;
                    rigidbodyObjeto.drag = desaceleracao;
                    parando = false;
                }

                else if (inputMovimento == 0 && estado == "Chão" && pisandoNochao==true)
                {
                    velocidadeAtual = rigidbodyObjeto.velocity.x;
                    desaceleracao = valorDesaceleracao;
                    rigidbodyObjeto.drag = desaceleracao;
                    parando = false;
                }
                else
                {
                    desaceleracao = 0;
                    rigidbodyObjeto.drag = desaceleracao;
                    parando = true;
                }
            }
            
        }

        //Pulo
        if (Input.GetButtonDown("Jump")&& estaNaParede==false)
        {
            
            ativouPulo = true;
            estaPulando = true;
        }
        if (Input.GetButtonDown("Jump") && rigidbodyObjeto.velocity.y < 0.5f && estaNaParede == false)
        {
            
            jumpTimer = Time.time + 0.3f;
        }
        if (jumpTimer > Time.time && estado == "Chão" && estaNaParede == false)
        {
           
            jumpDelay = true;
            anim.SetBool("Chao", true);
        }



        //Detectores    





        
        //animacoes
        if (estaNaParede == false)
        {
            anim.SetFloat("Velocidade", Mathf.Abs(inputMovimento));
        }
        if(estado== "Na parede"|| estado =="Deslizando")
        {
           
            anim.SetBool("Na parede", true);
            anim.SetBool("Chao", false);
        }
        else 
        {
            anim.SetBool("Na parede", false);
           
        }
        
    }



    void FixedUpdate()
    {
       

        //Movimeto horizontal

        if (velocidadeAtual < 0)
        {
            velocidadeAtual = 0;
        }

        if (olhandoDireita == false && inputMovimento > 0 && estaNaParede == false)
        {
            if(velocidadeQuandoVirar >= velocidadeAtual)
            {
                velocidadeAtual = velocidadeMinimaQuandoVirar;
            }
            else if (velocidadeQuandoVirar <= velocidadeAtual || velocidadeAtual == velocidadeMaxima)
            {
                velocidadeAtual = velocidadeQuandoVirar;
            }
            
            Flip();
        }
        else if (olhandoDireita == true && inputMovimento < 0 && estaNaParede==false)
        {
            if (velocidadeQuandoVirar >= velocidadeAtual)
            {
                velocidadeAtual = velocidadeMinimaQuandoVirar;
            }
            else if (velocidadeQuandoVirar <= velocidadeAtual || velocidadeAtual==velocidadeMaxima)
            {
                velocidadeAtual = velocidadeQuandoVirar;
            }
            Flip();
        }


        //Pulo
        
        if (ativouPulo)
        {            
            ativouPulo = false;
            if (estado == "Chão" && estaNaParede == false)
            {
                PuloParticula();
                rigidbodyObjeto.drag = 0;
                rigidbodyObjeto.velocity = new Vector2(rigidbodyObjeto.velocity.x, 0);
                rigidbodyObjeto.velocity = new Vector2(rigidbodyObjeto.velocity.x, forcaDoPulo);
                estado = "No ar";
                anim.SetBool("Chao", false);
            }
            else if (estado == "No ar" || estado== "Pulou da parede" && estaNaParede == false)
            {
                if (estado == "No ar")
                {
                    PuloParticula();
                }
                StopCoroutine(MudarEstado(0.2f));
                rigidbodyObjeto.velocity = new Vector2(rigidbodyObjeto.velocity.x, 0);
                rigidbodyObjeto.velocity = new Vector2(rigidbodyObjeto.velocity.x, forcaDoPulo);
                estado = "Segundo pulo";
                anim.Play("Pulo",0,0);
            }

        }


        if (jumpDelay) //Toleracia Pulo
         {
            anim.SetBool("Chao", false);
            jumpDelay = false;
            rigidbodyObjeto.drag = 0;
            rigidbodyObjeto.velocity = new Vector2(rigidbodyObjeto.velocity.x, 0);
            rigidbodyObjeto.velocity = new Vector2(rigidbodyObjeto.velocity.x, forcaDoPulo);
            estado = "No ar";

        }
 


        //Wall jump
        if (estaNaParede)
        {
            estado = "Na parede";
            if (deslizar == false && pulouDaparede == false && soltarParede == false)
            {
                
                rigidbodyObjeto.velocity = new Vector2(0, 0);
                rigidbodyObjeto.gravityScale = 0;
                estado = "Na parede";
            }
            else if (deslizar == true && pulouDaparede==false && soltarParede==false)
            {
                
                rigidbodyObjeto.velocity = new Vector2(0, velocidadeDeslizamento);
                rigidbodyObjeto.gravityScale = 1;
                estado = "Deslizando";
            }
            else if (pulouDaparede)
            {
                
                estado = "Na parede";
                pulouDaparede = false;
               
                
                if (olhandoDireita && inputMovimento<=0)
                {
                    rigidbodyObjeto.gravityScale = 1;
                    rigidbodyObjeto.drag = 0;
                    rigidbodyObjeto.velocity = new Vector2(0, 0);
                    DestaivarColider();
                    estaNaParede = false;
                    impedirControle = true;
                    Invoke("HabilitarControles", 0.5f);
                    rigidbodyObjeto.AddForce(new Vector2(-velocidadeMaxima, forcaDoPulo), ForceMode2D.Impulse);
                    //rigidbodyObjeto.velocity = new Vector2(-velocidadeMaxima, forcaDoPulo);
                    estado = "Pulou da parede";
                    StartCoroutine(MudarEstado(0.2f));
                    Flip();
                }
                else if (!olhandoDireita && inputMovimento >= 0)
                {
                    rigidbodyObjeto.gravityScale = 1;
                    rigidbodyObjeto.drag = 0;
                    rigidbodyObjeto.velocity = new Vector2(0, 0);
                    DestaivarColider();
                    estaNaParede = false;
                    impedirControle = true;
                    Invoke("HabilitarControles", 0.5f);
                    rigidbodyObjeto.AddForce(new Vector2(velocidadeMaxima, forcaDoPulo), ForceMode2D.Impulse);
                   // rigidbodyObjeto.velocity = new Vector2(velocidadeMaxima, forcaDoPulo);
                    estado = "Pulou da parede";
                    StartCoroutine(MudarEstado(0.2f));
                    Flip();
                }
            }
            if (soltarParede)
            {
                estaNaParede = false;
                impedirControle = true;
                Invoke("HabilitarControles", 0.7f);
                if (olhandoDireita)
                {
                    DestaivarColider();
                    Flip();
                    rigidbodyObjeto.AddForce(new Vector2(-1, -1), ForceMode2D.Impulse);
                }
                else
                {
                    DestaivarColider();
                    rigidbodyObjeto.AddForce(new Vector2(1, -1), ForceMode2D.Impulse);
                    Flip();
                }
                soltarParede = false;
                estado = "No ar";

            }
        }

        

    

        //Velocidadae queda
        if (rigidbodyObjeto.velocity.y < 0 && estaNaParede == false)
        {
            rigidbodyObjeto.velocity += Vector2.up * Physics2D.gravity.y * (velocidadeQueda - 1) * Time.deltaTime;
        }
       


       
        

    }

    public void PuloParticula()
    {
        GameObject part = Instantiate(particula, particulaPosicao.position, Quaternion.identity);
    }

    void HabilitarControles()
    {
        impedirControle = false;
    }

    public void Desabilitar()
    {
        impedirControle = true;
    }


    void DestaivarColider()
    {
        desabilitar = !desabilitar;
        detectorParede.SetActive(desabilitar);
    }
    void Flip()
    {
        if (estaNaParede == false)
        {
            olhandoDireita = !olhandoDireita;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }

    }



    private void OnDrawGizmos()
    {
    
        Gizmos.color = Color.blue;
        //detectores
       
        Gizmos.color = Color.green;
        
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - alturaMinimaGrudarParede));
    }

    

    IEnumerator MudarEstado(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        estado = "No ar";
        StopCoroutine(MudarEstado(0.2f));
    }

    void SairdoChao()
    {
        estado = "No ar";
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 12 && estaNaParede==false && estado!= "Pulou da parede" )
        {
            pisandoNochao = true;
            estado = "Chão";
            anim.SetBool("Chao", true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 12)
        {
            pisandoNochao = false;
        }
    }
    //DELAYS


    //Retornos
    public float GetInputHorizontal()
    {
        return inputMovimento; 
    }
    public bool GetAtivouPulo()
    {
        return estaPulando;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_movel : MonoBehaviour
{
    private bool ida,volta;
    public bool movimentoVertical;
    public bool movimentoHorizontal;
    private Vector2 pontoInicial;
    public int pontoFinal;
    public bool movimentoEmLoop;
    public bool acionadoPorBotao;
    public GameObject botaoDeReferencia;
    public float velocidadePlataforma;
    private Vector2 opontoFinal;
    private bool ativar;
     void Reset()
    {
        pontoInicial = transform.position;
    }
    void Start()
    {
        pontoInicial = transform.position;
        if (movimentoHorizontal)
        {
            opontoFinal = new Vector2(transform.position.x + pontoFinal, transform.position.y);
        }
        if (movimentoVertical)
        {
            opontoFinal = new Vector2(transform.position.x, transform.position.y + pontoFinal);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (acionadoPorBotao)
        {
            if (botaoDeReferencia != null)
            {
                ativar = botaoDeReferencia.GetComponent<Botao>().botaoAtivado;
            }

            if (ativar)
            {
                if (movimentoHorizontal)
                {
                    MovimentoHorizontal();
                }
                if (movimentoVertical)
                {
                    MovimentoVertical();
                }
            }

        }
        else
        {
            if (movimentoHorizontal)
            {
                MovimentoHorizontal();
            }
            if (movimentoVertical)
            {
                MovimentoVertical();
            }
        }
    }
    
    void MovimentoVertical()
    {
        if (movimentoEmLoop)
        {
            VerificarPosicao();
            if (ida)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, opontoFinal.y), velocidadePlataforma * Time.deltaTime);
            }
            if (volta)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, pontoInicial.y), velocidadePlataforma * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, opontoFinal.y), velocidadePlataforma * Time.deltaTime);
        }
    }

    void MovimentoHorizontal()
    {
        if (movimentoEmLoop)
        {
            VerificarPosicao();

            if (ida)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(opontoFinal.x, transform.position.y), velocidadePlataforma * Time.deltaTime);
            }
            if (volta)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(pontoInicial.x, transform.position.y), velocidadePlataforma * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(opontoFinal.x, transform.position.y), velocidadePlataforma * Time.deltaTime);
        }
    }

    void VerificarPosicao()
    {
        if (movimentoHorizontal)
        {
            if (transform.position.x == pontoInicial.x)
            {
                ida = true;
                volta = false;

            }
            else if (transform.position.x == opontoFinal.x)
            {
                ida = false;
                volta = true;

            }
        }
        if (movimentoVertical)
        {
            if (transform.position.y == pontoInicial.y)
            {
                ida = true;
                volta = false;

            }
            else if (transform.position.y == opontoFinal.y)
            {
                ida = false;
                volta = true;

            }
        }
    }

    void OnDrawGizmos()
    {
        if (movimentoHorizontal)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector2(pontoInicial.x, transform.position.y), new Vector2(opontoFinal.x, transform.position.y));
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector2(pontoInicial.x, transform.position.y), 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector2(opontoFinal.x, transform.position.y), 0.2f);
        }
        if (movimentoVertical)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector2(pontoInicial.x, pontoInicial.y), new Vector2(transform.position.x, opontoFinal.y));
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector2(pontoInicial.x, pontoInicial.y), 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector2(transform.position.x, opontoFinal.y), 0.2f);
        }
    }

    private void OnValidate()
    {
        pontoInicial = new Vector2(transform.position.x, transform.position.y);
        if (movimentoHorizontal)
        {
            opontoFinal = new Vector2(transform.position.x + pontoFinal, transform.position.y);
        }
        if (movimentoVertical)
        {
            opontoFinal = new Vector2(transform.position.x, transform.position.y + pontoFinal);
        }
        if (movimentoHorizontal)
        {
            movimentoVertical = false;
        }
        if (movimentoVertical)
        {
            movimentoHorizontal = false;
        }
    }

     void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
           collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            collision.transform.SetParent(gameObject.transform, true);    
        }      
    }

    private void OnCollisionExit2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            collision.transform.SetParent(gameObject.transform, true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinho_botao : MonoBehaviour
{
 
    public GameObject botaoDeReferencia;
    private BoxCollider2D colider;
    private SpriteRenderer sprite;
    private bool ativar;
    public bool comecarAtivado;   
    private GameObject corpo;
    void Start()
    {
        
        colider = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (botaoDeReferencia != null)
        {
            ativar = botaoDeReferencia.GetComponent<Botao>().botaoAtivado; // Verificar se o botão de referencia 1 esta acionado
        }
            if (ativar)
            {
                colider.enabled = !comecarAtivado;
                sprite.enabled =  !comecarAtivado;
                
            }
            else
            {
                colider.enabled = comecarAtivado;
                sprite.enabled = comecarAtivado;
                
            }

        if (colider.enabled == false)
        {
            if (corpo != null)
            {
                corpo.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                corpo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.01f);
                corpo.transform.parent.gameObject.transform.parent = null;
                corpo = null;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Boneco morto"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            collision.transform.parent.gameObject.transform.parent = gameObject.transform;
            corpo = collision.gameObject;
        }
    }
}

  


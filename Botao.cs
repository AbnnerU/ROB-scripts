using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    public bool botaoAtivado;
    public bool temporizador;
    public float tempo;
    private BoxCollider2D colider;
    private SpriteRenderer sprite;
    private bool manterEstado;
    void Start()
    {
        manterEstado = false;
        colider = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
      
         if (botaoAtivado)
        {
            
            sprite.enabled = false;
            if (temporizador && manterEstado==false)
            {
                StartCoroutine(Desativar(tempo));
            }
        }
        else
        {
            StopAllCoroutines();
            
            sprite.enabled = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Boneco morto"))
        {
            manterEstado = true;
            botaoAtivado = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boneco morto"))
        {
            manterEstado = false;
          
        }
    }

    IEnumerator Desativar(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        botaoAtivado = false;
    }
   
}

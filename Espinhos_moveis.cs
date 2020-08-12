using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinhos_moveis : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float tempoAtivado;
    public float tempoDesativado;
    public bool comecarAtivado;
    [HideInInspector]
    public bool ativado;    
    private GameObject corpo;   
    private SpriteRenderer sprite;
    private BoxCollider2D colider;
    AudioSource som;
    bool jaTocou;
    IEnumerator corotina;
    GameObject objeto;
    void Start()
    {
        som = GetComponent<AudioSource>();
        jaTocou = false;
        //Tempos defalt
        if(tempoDesativado==0 && tempoAtivado == 0)
        {
            tempoAtivado = 1;
            tempoDesativado = 1;
        }

        if (comecarAtivado)
        {
            ativado = true;
        }
        else
        {
            ativado = false;
        }
       
        colider = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (ativado)
        {
            StartCoroutine(AtivarEDesativar(tempoAtivado));
            sprite.enabled = true;
            colider.enabled = true;
            if (jaTocou == false)
            {
                jaTocou = true;
                som.Play();
            }
        }
        else if (ativado == false)
        {
            jaTocou = false;
            sprite.enabled = false;
            colider.enabled = false;
            if (corpo != null)
            {
                corpo.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                corpo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.01f);
                corpo.transform.parent.gameObject.transform.parent = null;
                objeto = null;
                corpo = null;
            }
            StartCoroutine(AtivarEDesativar(tempoDesativado));
            
        }

    }

     void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.CompareTag("Boneco morto"))
        {
            corotina = DelayFreezar();   
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine(corotina);
            collision.transform.parent.gameObject.transform.parent = gameObject.transform;
            corpo = collision.gameObject;
        }
    }

    IEnumerator DelayFreezar()
    {
        yield return new WaitForSeconds(1.1f);
        corpo.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        StopCoroutine(corotina);
    }
    
        
    

    IEnumerator AtivarEDesativar(float tempo)
    {
       
        yield return new WaitForSeconds(tempo);
        ativado = !ativado;
        StopAllCoroutines();
    }
   
}

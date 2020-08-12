using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinhos : MonoBehaviour
{
    public bool corpoGrudaEDesgruda;
    public bool corpoMortoNaoSolta;
    public float tempoParaDesgrudar;
    [HideInInspector]
    public bool desgrudar;
    private bool soltar;
    private Rigidbody2D rigidbodyDoCorpo;
    private GameObject corpo;
    public bool soltarEmPontoEspecifico;
    public GameObject pontoParaSoltar;

    void Start()
    {
       
    }

    void Update()
    {
        if (corpoMortoNaoSolta == false)
        {
            if (soltarEmPontoEspecifico == false)
            {
                if (corpo != null && corpoGrudaEDesgruda)
                {
                    StartCoroutine(DelayParaSoltar(tempoParaDesgrudar));
                    if (desgrudar)
                    {
                        StopAllCoroutines();
                        corpo.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        corpo.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        corpo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.01f);
                        corpo.transform.parent.gameObject.transform.parent = null;
                        desgrudar = false;
                        corpo = null;

                    }
                }
            }
            else
            {
                soltar = pontoParaSoltar.GetComponent<Ponto_para_soltar>().desgrudarCorpo;
                print(soltar);
                if (soltar)
                {
                    
                    if (corpo != null)
                    {
                        StopAllCoroutines();
                        corpo.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        corpo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.01f);
                        corpo.transform.parent.gameObject.transform.parent = null;
                        corpo = null;
                    }
                }

            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Boneco morto"))
        {

                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                collision.transform.parent.gameObject.transform.parent = gameObject.transform;
                corpo = collision.gameObject;  
            
        }

    }
    


    //Delay
    IEnumerator DelayParaSoltar(float delay)
    {
        yield return new WaitForSeconds(delay);
        desgrudar = true;
  
    }
   
}

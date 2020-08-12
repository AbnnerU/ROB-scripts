using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject jogador;
    public ParticleSystem particula;
    public float forca;
    Animator anim;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Porta inicio").GetComponent<Animator>();
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jogador = null;
            jogador = GameObject.FindGameObjectWithTag("Player");
            
            jogador.GetComponent<Movimento>().enabled = false;
            Invoke("AtivarMovimento", 1.3f);
            jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            anim.Play("Porta abrindo");
            if (particula.isPlaying == false)
            {
                particula.Play();
            }
            jogador.GetComponent<Rigidbody2D>().AddForce(new Vector2(forca, 0),ForceMode2D.Impulse);
            
        } 
    }

    void AtivarMovimento()
    {
        if(jogador!=null)
        jogador.GetComponent<Movimento>().enabled = true;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porta : MonoBehaviour
{
    Animator anim;
    bool abrir;
    AudioSource som;
    // Start is called before the first frame update
    void Start()
    {
        som = GetComponent<AudioSource>();
        abrir = false;
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (abrir)
        {
            anim.Play("Porta fechando");
        }
    }

    public void AbrirPorta()
    {
       
        abrir = true;
    }

    public void ManterFechada()
    {
        abrir = false;
    }
    public void SomAbrindo()
    {
        som.Play();
    }
}

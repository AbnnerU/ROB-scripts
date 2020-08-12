using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaFinal : MonoBehaviour
{
    bool portaAberta;
    public Animator porta;
    // Start is called before the first frame update
    void Start()
    {
        portaAberta = false;
    }

    // Update is called once per frame
      void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
                portaAberta = true;
                porta.Play("Porta abrindo");
            
            
        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            portaAberta = false;
                porta.Play("Porta fechando");
            
        }
    }

   
}

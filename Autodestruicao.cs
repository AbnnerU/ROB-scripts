using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruicao : MonoBehaviour
{

    public GameObject explosao;
    private bool podeDestruir;
    private void Start()
    {
        podeDestruir = false;
        
        StartCoroutine(PodeAutodestruir(1.5f));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && podeDestruir )
        {

             Instantiate(explosao, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            DialogosManeger.numeroMortesJogador++;


        }    
    }

   IEnumerator PodeAutodestruir(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        podeDestruir = true;
    }
}

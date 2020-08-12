using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocarCena : MonoBehaviour
{
    public string nomeCena;
    public static string cena;
    // Start is called before the first frame update
    private void Start()
    {
        cena = nomeCena;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogosManeger.finalFase = true;
            DialogosManeger.cena = nomeCena;
           
        }
    }

   
}

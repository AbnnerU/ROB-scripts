using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_parede : MonoBehaviour
{
    public bool estaNaParede;
    
    void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 8 || collision.gameObject.layer==12)
        {
            estaNaParede = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 12)
        {
            estaNaParede = false;
        }
    }
}

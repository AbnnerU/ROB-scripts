using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [HideInInspector]
    public bool plataforma;
    private void Start()
    {
        plataforma = false;
    }
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            plataforma = true;
            print("ROLA");
            gameObject.transform.parent.gameObject.transform.SetParent(collision.gameObject.transform, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            plataforma = false;
            gameObject.transform.parent.gameObject.transform.SetParent(null, true);
        }
    }
}

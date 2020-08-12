using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cientistas : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogosManeger.finalFase == true)
        {
            anim.Play("Saindo");
        } 
    }



    public void IniciaIdle()
    {
        anim.Play("Idle");
    }
}

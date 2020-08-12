using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    Animator anim;
    public bool movimento;
    public bool pulo;
    public bool doubleJump;
    public bool explodir;
    public bool movimentoParedeA;
    public bool movimentoParedeD;
    public bool soltar;
    public bool wallJump;
    
    int proxima;
   
    // Start is called before the first frame update
    private void Awake()
    {
        //if (pulo == true && movimento==false && )
    }
    void Start()
    {
        
        anim = GetComponent<Animator>();
        
        proxima = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movimento == false && proxima==0)
        {                          
            proxima = 1;
            
        }

        if (pulo == false && proxima == 1)
        {
            proxima = 2;
            
        }

        if (explodir == false && proxima == 2)
        {
            proxima = 3;
            
        }
        if (movimentoParedeA == false && proxima == 3)
        {
            proxima = 4;
            
        }
        if (movimentoParedeD == false && proxima == 4)
        {
            proxima = 5;
            
        }
        if (wallJump == false && proxima == 5)
        {
            proxima = 6;
            
        }
        if (doubleJump == false && proxima == 6)
        {
            proxima = 7;
        }
        if (soltar == false && proxima == 7)
        {
            proxima = 0;
            
        }

        //ANIIMACOES
        
        if (movimento == true && proxima == 0)
        {
            anim.Play("Movimento");
        }

        if (pulo == true && proxima == 1)
        {
            anim.Play("Pulo");
        }

        if (explodir == true && proxima == 2)
        {
            anim.Play("Explodir");
        }
        if (movimentoParedeA == true && proxima == 3)
        {
            anim.Play("Monitor parede A");
        }
        if (movimentoParedeD == true && proxima == 4)
        {
            anim.Play("Monitor parede D");
        }
        if (wallJump == true && proxima == 5)
        {
            anim.Play("Wall jump");
        }
        if (doubleJump == true && proxima == 6)
        {
            anim.Play("Double jump");
        }
        if (soltar == true && proxima == 7)
        {
            anim.Play("Soltar");
        }
    }





    public void Proxima()
    { 
        proxima++;
        
    }
    public void Zerar()
    { 
        proxima = 0;

    }
}

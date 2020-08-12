using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisao : MonoBehaviour
{
   
  
    public GameObject prefabPlayerMorto;
   
   

     

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Espinhos"))
        {
            
            if (DialogosManeger.spawner == false)
            {

                //direita
                if (collision.gameObject.transform.rotation.z >= 0.7f && collision.gameObject.transform.rotation.z < 0.8f)
                {
                    print("DIREITAA");
                    GameObject morto = Instantiate(prefabPlayerMorto, new Vector2(transform.position.x+0.25f, transform.position.y ), Quaternion.identity);
                }
                //esquerda
                else if(collision.gameObject.transform.rotation.z <0.7f )
                {

                    GameObject morto = Instantiate(prefabPlayerMorto, new Vector2(transform.position.x-0.25f, transform.position.y ), Quaternion.identity);
                }
                //cima
                else if (collision.gameObject.transform.rotation.z == 1)
                {
                    GameObject morto = Instantiate(prefabPlayerMorto, new Vector2(transform.position.x, transform.position.y + 0.25f), Quaternion.identity);
                }
                else
                {
                    GameObject morto = Instantiate(prefabPlayerMorto, new Vector2(transform.position.x, transform.position.y+0.5f), Quaternion.identity);
                 }
                
                DialogosManeger.spawner = true;
            }
            Destroy(gameObject);
            DialogosManeger.numeroMortesJogador++;
        }
       
    }
    
}

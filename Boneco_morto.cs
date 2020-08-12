using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boneco_morto : MonoBehaviour
{
    public ParticleSystem particula1;
    public ParticleSystem particula2;
    public GameObject explosao;
    private bool estaNoChao;
    public Transform verificadorChao;
    public float area_circulo_verificador;
    public LayerMask layer;
    private Vector3 escala;
    private bool colidindoPlataforma;
     void Start()
    {
        colidindoPlataforma = GetComponent<Ragdoll>().plataforma;
        escala = gameObject.transform.localScale;
        LigarParticulas();
    }
    void Update()
    {
        colidindoPlataforma = GetComponent<Ragdoll>().plataforma;
        //Autodestruicao
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(explosao, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       
        if (estaNoChao)
        {
            gameObject.GetComponent<Rigidbody2D>().drag = 17;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().drag = 0;
        }
        //Detectores   
        estaNoChao = Physics2D.OverlapCircle(verificadorChao.position, area_circulo_verificador, layer)|| Physics2D.OverlapCircle(verificadorChao.position, area_circulo_verificador, 12);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(verificadorChao.position, area_circulo_verificador );
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Espinhos"))
        {
            if (collision.gameObject.transform.rotation.z != 1)
            {
                transform.rotation = collision.gameObject.transform.rotation;
            }
        }

        if (collision.gameObject.CompareTag("Player") && colidindoPlataforma)
        {
            collision.gameObject.transform.SetParent(gameObject.transform.parent.gameObject.transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null, true);
        }
    }



    void LigarParticulas()
    {
        particula1.Play();
        particula2.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaBotão : MonoBehaviour
{
    public GameObject botaoDeReferencia;
    public bool comecarFechada;
    bool ativar;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        print(comecarFechada);
        anim = GetComponent<Animator>();
        if (comecarFechada)
        {
            anim.Play("Porta fechando");
        }
        else
        {
            anim.Play("Porta abrindo");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (botaoDeReferencia != null)
        {
            ativar = botaoDeReferencia.GetComponent<Botao>().botaoAtivado; // Verificar se o botão de referencia 1 esta acionado
        }
        if (ativar)
        {
            if (comecarFechada)
            {
                anim.Play("Porta abrindo");
            }
            else
            {
                anim.Play("Porta fechando");
            }

        }
        else
        {
            if (comecarFechada)
            {
                anim.Play("Porta fechando");
            }
            else
            {
                anim.Play("Porta abrindo");
            }

        }
    }
}

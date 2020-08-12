using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class DialogosManeger : MonoBehaviour
{
    private int tempoAtual;
    private Animator fundoAnimado;
    public Image imagemEstagiaio;
    public Image imagemVelho;
    public Image imagemEstagiarioRob;
    public Image imagemVelhoRob;
    public Image imagemROB;
    // public Animator caixaDialogoRob;
    private GameObject caixaDialogoRob;
    private GameObject caixaDialogo;
    public Text nomeTextoRob;
    public Text falasTextoRob;
    public float delayParaComecarFalasRob;
    private bool naoFalou;
    private int idFalaRob;
    public static bool spawner;
    private bool robFalando;
    // public Animator caixaDialogoAnimator;
   
    public Text nomeTexto;
    public Text falasTexto;
    public GameObject posicaoSpawn;
    public GameObject prefabJogador;
    public float delayPassarFase;
    public static string cena;
    public static bool finalFase;
    public static int numeroMortesJogador;
    private bool podePula;
    private int idFala;
    private int numeroMortesAnterior;   
    public GameObject[] falasMorte;
    public GameObject[] falasRob;
    public Queue<string> nomes;
    public Queue<string> falas;
    public Queue<string> queuenomeRob;
    public Queue<string> queuefalasRob;
    public Queue<int> queueTempoFalaRob;
    public GameObject falaAoPassarFase;
    void Start()
    {
        caixaDialogo = GameObject.FindGameObjectWithTag("Caixa dialogo");
        caixaDialogoRob = GameObject.FindGameObjectWithTag("Caixa dialogo rob");
        print(caixaDialogo);
        print(caixaDialogoRob);
        caixaDialogo.SetActive(false);
        caixaDialogoRob.SetActive(false);
        if(GameObject.FindGameObjectWithTag("Fundo animado") != null)
        {
            fundoAnimado = GameObject.FindGameObjectWithTag("Fundo animado").GetComponent<Animator>();
        }
        robFalando = false;
        idFalaRob = 0;
        naoFalou = true;
        spawner = false;
        podePula = false;
        finalFase = false;
        idFala = 0;
        numeroMortesJogador = 0;
        numeroMortesAnterior = 0;
        nomes = new Queue<string>();
        falas = new Queue<string>();
        queuenomeRob = new Queue<string>();
        queuefalasRob = new Queue<string>();
        queueTempoFalaRob = new Queue<int>();
        IniciarDialogo(falasMorte[idFala].GetComponent<Falas>().dialogos);
    }

    void Update()
    {
        if (spawner == true)
        {
            StartCoroutine(Coisa(0.5f));
        }
      
            if (numeroMortesJogador > numeroMortesAnterior && finalFase==false && robFalando==false)
            {

                idFala++;
                numeroMortesAnterior = numeroMortesJogador;
            if (idFala <= falasMorte.Length - 1)
            {
                IniciarDialogo(falasMorte[idFala].GetComponent<Falas>().dialogos);
               
            }
            else if (idFala > falasMorte.Length - 1 && finalFase == false)
            {
                if (fundoAnimado != null)
                {
                    fundoAnimado.Play("Fundo movendo",0,0);
                }
                GameObject player = Instantiate(prefabJogador, posicaoSpawn.transform.position, Quaternion.identity);
            }
            }

            if (finalFase && naoFalou)
            {
                IniciarDialogo(falaAoPassarFase.GetComponent<Falas>().dialogos);
                Destroy(GameObject.FindGameObjectWithTag("Player"));
            }


       if (podePula && Pause.jogoPausado==false)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {                
                ProximaFala();              
            }
        }
        
    }

    public void IniciarDialogo(Dialogos dialogo)
    {
        if (finalFase)
        {
            naoFalou = false;
        }
        if (idFala <= falasMorte.Length - 1)
        {
            if (falasMorte[idFala].GetComponent<Falas>().jogadorSpawnarDepoisFala == false && finalFase == false)
            {
                if (fundoAnimado != null)
                {
                    fundoAnimado.Play("Fundo movendo", 0, 0);
                }
                GameObject player = Instantiate(prefabJogador, posicaoSpawn.transform.position, Quaternion.identity);
            }
        }
        nomes.Clear();
        falas.Clear();
        foreach(string conversas in dialogo.sentecas)
        {
            falas.Enqueue(conversas);
        }
        foreach (string personagem in dialogo.nome)
        {
            nomes.Enqueue(personagem);
        }


        podePula = true; 

        ProximaFala();
    }

    public void ProximaFala()
    {
        
        if (falas.Count == 0 && nomes.Count == 0)//Quando nao tiver mais falas
        {
            podePula = false;
            caixaDialogo.SetActive(false);
            if (idFala <= falasMorte.Length - 1 && finalFase==false)//Spanar jogador depois da fala
            {
                if (falasMorte[idFala].GetComponent<Falas>().jogadorSpawnarDepoisFala == true)
                {
                  if (fundoAnimado != null)
                {
                    fundoAnimado.Play("Fundo movendo", 0, 0);
                }
                    GameObject player = Instantiate(prefabJogador, new Vector2(posicaoSpawn.transform.position.x, posicaoSpawn.transform.position.y), Quaternion.identity);
                }
            }
            if (finalFase)
            {
                StartCoroutine(PassarFase(delayPassarFase));
            }
            //Fala Rob
            if (idFalaRob <= falasRob.Length - 1)
            {
                Invoke("FalasRob", delayParaComecarFalasRob);
            }

        }
        else
        {       
            string falaAtual = falas.Dequeue();
            string nomeAtual = nomes.Dequeue();
            if (falaAtual != "")
            {
                //caixaDialogoAnimator.SetBool("CaixaAtivada", true);
                caixaDialogo.SetActive(true);
            }
            else if(falaAtual == "")
            {
                ProximaFala();
            }


            nomeTexto.text = nomeAtual;
            falasTexto.text = falaAtual;
            
            //Imagem do lada das falas

        if(nomeAtual== "Estagiário")
            {                
                imagemEstagiaio.enabled = true;
                imagemVelho.enabled = false;
                ////////////////////////////
                imagemROB.enabled = false;
                imagemEstagiarioRob.enabled = false;
                imagemVelhoRob.enabled = false;

            }
            else if (nomeAtual == "Cientista")
            {
                imagemEstagiaio.enabled = false;
                imagemVelho.enabled = true;
                ////////////////////////////
                imagemROB.enabled = false;
                imagemEstagiarioRob.enabled = false;
                imagemVelhoRob.enabled = false;
            }
          
        }
       
    }

    //Falas ROB
    void FalasRob()
    {
        robFalando = true;
      
        IniciarFalaRob(falasRob[idFalaRob].GetComponent<Falas>().dialogos);
        idFalaRob++;       
    }

    void IniciarFalaRob(Dialogos dialogo)
    {
        
        queuenomeRob.Clear();
        queuefalasRob.Clear();
        queueTempoFalaRob.Clear();
        foreach (string conversas in dialogo.sentecas)
        {
                queuefalasRob.Enqueue(conversas);
        }
        foreach (string personagem in dialogo.nome)
        {
            queuenomeRob.Enqueue(personagem);
        }
        foreach (int tempoFala in dialogo.tempoFalaRob)
        {
            queueTempoFalaRob.Enqueue(tempoFala);
        }
            ProximaFalaRob();       
    }
    
    void ProximaFalaRob()
    {
        
      
        if (queuenomeRob.Count == 0 && queuefalasRob.Count == 0)//Quando nao tiver mais falas
        {
            //caixaDialogoRob.SetBool("CaixaAtivada", false);
            caixaDialogoRob.SetActive(false);
            robFalando = false;
        }
        else
        {
            string falaAtual = queuefalasRob.Dequeue();
            string nomeAtual = queuenomeRob.Dequeue();
             tempoAtual = queueTempoFalaRob.Dequeue();
            nomeTextoRob.text = nomeAtual;
            falasTextoRob.text = falaAtual;
            

            //Imagem do lado das falas
            if(nomeAtual== "ROB")
            {
                imagemROB.enabled = true;
                imagemEstagiarioRob.enabled = false;
                imagemVelhoRob.enabled = false;
                ////////////////////////////
                imagemEstagiaio.enabled = false;
                imagemVelho.enabled = false;
            }
            else if(nomeAtual == "Cientista")
            {
                imagemROB.enabled = false;
                imagemEstagiarioRob.enabled = false;
                imagemVelhoRob.enabled = true;
                ////////////////////////////
                imagemEstagiaio.enabled = false;
                imagemVelho.enabled = false;
            }
            else if (nomeAtual == "Estagiário")
            {
                imagemROB.enabled = false;
                imagemEstagiarioRob.enabled = true;
                imagemVelhoRob.enabled = false;
                ////////////////////////////
                imagemEstagiaio.enabled = false;
                imagemVelho.enabled = false;
            }

            if (falaAtual != "")
            {
                //caixaDialogoRob.SetBool("CaixaAtivada", true);
                caixaDialogoRob.SetActive(true);
            }
            else if (falaAtual == "")
            {
                ProximaFalaRob();
            }
            StartCoroutine(ChamarProximaFalaRob(tempoAtual));
        }
    }

    IEnumerator ChamarProximaFalaRob(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        ProximaFalaRob();

        StopCoroutine(ChamarProximaFalaRob(tempoAtual));
    }


    IEnumerator PassarFase(float tempo)
    {
        
        yield return new WaitForSeconds(tempo);
        SceneManager.LoadScene(cena);
        StopCoroutine(PassarFase(delayPassarFase));
    }

    IEnumerator Coisa(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        spawner = false;
    }
}

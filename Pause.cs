using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    [HideInInspector]
    public string faseAtual;
    public static bool jogoPausado;
    public GameObject UIPause;
    public GameObject botaoPause;
    public static bool CHEATS;
    public static bool AumentarVElocidade;
    public GameObject cheats;
    // Start is called before the first frame update
    void Start()
    {
        cheats.SetActive(false);
        CHEATS = false;
        AumentarVElocidade = false;
        faseAtual = SceneManager.GetActiveScene().name;
        jogoPausado = false;
        Time.timeScale = 1f;
        UIPause.SetActive(false);
        botaoPause.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }

        // Cheats
        if (Input.GetKeyDown(KeyCode.L))
        {           
            CHEATS = true;
            cheats.SetActive(true);
        }
        if (CHEATS)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
               
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                DialogosManeger.finalFase = true;
                DialogosManeger.cena = TrocarCena.cena;
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                AumentarVElocidade = true;
            }
        }
    }

    public void Continuar()
    {
        jogoPausado = false;
        Time.timeScale = 1f;
        UIPause.SetActive(false);
        botaoPause.SetActive(true);
    }

    public void Pausar()
    {
        jogoPausado = true;
        Time.timeScale = 0f;
        UIPause.SetActive(true);
        botaoPause.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salvar()
    {
        SaveSystem.Salvar(this);
    }
}

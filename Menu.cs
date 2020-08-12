using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public Button continuar;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        if (SaveSystem.Carregar().nomeFase != "Seção 1 Level 1")
        {
            continuar.enabled = true;
            ColorBlock color = continuar.colors;
            color.normalColor = new Color(254, 254, 254, 255);
            continuar.colors = color;
        }
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void CenaMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Controles()
    {
        SceneManager.LoadScene("Controles");
    }

    public void Jogar()
    {
        if (SaveSystem.Carregar() == null)
        {
            SceneManager.LoadScene("Seção 1 Level 1");
        }
        else
        {
            print(SaveSystem.Carregar().nomeFase);
            SceneManager.LoadScene(SaveSystem.Carregar().nomeFase);
        }
    }

    public void Reiniciar()
    {
        SaveSystem.Reiniciar();
        Jogar();
    }

    public void Sair()
    {
        Application.Quit();
    }
}

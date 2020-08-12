using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
   public string nomeFase;

    public PlayerData (Pause pause)
    {
        nomeFase = pause.faseAtual;
    }

    public PlayerData(string nome)
    {
        nomeFase = nome;
    }
    
}

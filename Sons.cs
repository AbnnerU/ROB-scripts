using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sons : MonoBehaviour
{
    
    private AudioSource som;
    public AudioClip playerPulo;
     void Start()
    {
        
        som = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void SomCaindo()
    {
        
        som.Stop();
    }
    void SomAndando()
    {
        som.Stop();
    }
    public void SomParado()
    {
        
        som.Stop();
    }
    void somPulando()
    {
     
        som.clip = playerPulo;
        som.volume = 0.8f;
        som.Play();
    }
}

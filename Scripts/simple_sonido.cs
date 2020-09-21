using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_sonido : MonoBehaviour
{
    private audiocontroller audiocontroller_aux;
    
    [Header("Sonido A Reproducir...")]
    public string sonido_reproducir;
    
    void Start()
    {
        audiocontroller_aux=FindObjectOfType<audiocontroller>();
        if(sonido_reproducir!=null&&audiocontroller_aux!=null)
        {
            audiocontroller_aux.reproducir(sonido_reproducir);
        }
        else Debug.Log("Es nulo sonido para portal");
    }
}

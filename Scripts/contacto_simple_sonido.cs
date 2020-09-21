using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contacto_simple_sonido : MonoBehaviour
{
    private audiocontroller audiocontroller_aux;
    
    [Header("Sonido A Reproducir...")]
    public string sonido_reproducir;
    
    [Header("Sonido A Detener...")]
    public string sonido_detener;
    
    [Header("Paramos TODO?")]
    public bool para_todo;
    
    [Header("Destruir Objeto?")]
    public GameObject destruir_obj;

    [Header("Crear Objeto?")]
    public GameObject crear_obj;

    [Header("Destruirme?")]
    public bool destruir;

    void Start()
    {
        audiocontroller_aux=FindObjectOfType<audiocontroller>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        //if(other.gameObject.tag == "Player"||other.gameObject.tag == "Enemigo")
        if(other.gameObject.tag == "Player")
        {
            if(para_todo)
            {
                audiocontroller_aux.detener_todo();
            }
            else
            {
                if(sonido_detener!=null && audiocontroller_aux!=null)
                {
                    audiocontroller_aux.detener(sonido_detener);
                }
            }

            if(sonido_reproducir!=null && audiocontroller_aux!=null)
            {
                audiocontroller_aux.reproducir(sonido_reproducir);
            }

            if(destruir_obj!=null)
            {
                Destroy(destruir_obj);
            }
            
            if(crear_obj!=null)
            {
                Instantiate(crear_obj,transform.position,Quaternion.identity);
            }

            if (destruir)
            {
                Destroy(gameObject);
            }
        }
    }
}

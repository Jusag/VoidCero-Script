using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boton_salir_al_menu : MonoBehaviour
{
    private GameController gamecontroller_aux;
    private audiocontroller audiocontroller_aux;
    private changer_fades fade_aux;
    
    public int escena;
    
    void Start()
    {
        gamecontroller_aux=FindObjectOfType<GameController>();
        audiocontroller_aux=FindObjectOfType<audiocontroller>();
        fade_aux=FindObjectOfType<changer_fades>();
    }

    public void saliralmenu()
    {
        gamecontroller_aux.maximo_puntaje_control();
        StartCoroutine(temporizador());
    }
    IEnumerator temporizador()
    {
        fade_aux.anegro();
        gamecontroller_aux.cambiar_muerto_estado(true);
        if(gamecontroller_aux.estado_pausa())
        {
            gamecontroller_aux.pause_game();
        }
        gamecontroller_aux.control_desactivar();
        gamecontroller_aux.jugadoractual().GetComponent<Rigidbody>().velocity = Vector3.zero; 
        audiocontroller_aux.detener_todo();
        
        yield return new WaitForSeconds(2.5f);
        
        gamecontroller_aux.cambiarscenavalor(escena);
        gamecontroller_aux.cambiarscena();
    }  
}

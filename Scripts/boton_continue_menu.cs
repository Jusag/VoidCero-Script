using UnityEngine;
using UnityEngine.SceneManagement;

public class boton_continue_menu : MonoBehaviour
{
    private GameController gamecontroller_aux;
    private audiocontroller audiocontroller_aux;
    private changer_fades fade_aux;
    //public bool primera;
    public int escena;
    void Start()
    {
        gamecontroller_aux=FindObjectOfType<GameController>();
        audiocontroller_aux=FindObjectOfType<audiocontroller>();
        fade_aux=FindObjectOfType<changer_fades>();
    }

    public void continuar()
    {
        gamecontroller_aux.botoncontinue();
        
        audiocontroller_aux.detener("Menu_Intro");
        gamecontroller_aux.tuto(false);
        gamecontroller_aux.cambiarscenavalor(escena);

        if(fade_aux!=null)
        {
            fade_aux.anegro();
        }

        gamecontroller_aux.cambiarscena();
        gamecontroller_aux.continuarjuego();
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changer_fades : MonoBehaviour
{
    private Animator animacion_local;
    private bool bool_local;
    private int scenadestino;
    private GameController gameController_aux;
    // Start is called before the first frame update
    void Start()
    {
        gameController_aux=FindObjectOfType<GameController>();
        animacion_local=GetComponent<Animator>();
        if(gameController_aux==null)
        {
            Debug.Log("Es NULOOOOO");
        }
    }

    public void anegro()
    {
        animacion_local.SetBool("a_negro",true);
    }
    public void ablanco()
    {
        animacion_local.SetBool("a_negro",false);
    }
    
    public void cambiarvalorscena(int aux)
    {
        gameController_aux.cambiarscenavalor(aux);
        //scenadestino=aux;
    }
    public void cambiar_escena()
    {
        gameController_aux.cambiarscena();   
        //SceneManager.LoadScene(scenadestino);
    }
}

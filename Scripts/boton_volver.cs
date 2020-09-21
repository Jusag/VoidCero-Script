using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boton_volver : MonoBehaviour
{
    private GameController gamecontrol_aux;
    void Start()
    {
        gamecontrol_aux=GameObject.Find("GameController").GetComponent<GameController>();
    }
    public void pausa()
    {
        gamecontrol_aux.pause_game();
    }
}
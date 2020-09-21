using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boton_erase : MonoBehaviour
{
    private GameController gamecontroller_aux;
    void Start()
    {
        gamecontroller_aux = FindObjectOfType<GameController>();
    }
    public void erase_button()
    {
        gamecontroller_aux.erase_all();
        this.gameObject.SetActive(false);
    }
}

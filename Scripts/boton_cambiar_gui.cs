using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boton_cambiar_gui : MonoBehaviour {

    public GameObject origen;
    public GameObject destino;

    
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void cambiar_GUI()
    {
        if(origen.activeSelf&&!destino.activeSelf){
        origen.SetActive(false);
        destino.SetActive(true);
        }
    }
}

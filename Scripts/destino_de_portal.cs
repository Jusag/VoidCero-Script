using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destino_de_portal : MonoBehaviour
{
    private GameController gamecontroller_aux;
	private audiocontroller audiocontroller_aux;
    public GameObject crear_entrada;
    public GameObject crear_salida;

    public GameObject destino;
    void Start()
    {
        gamecontroller_aux=FindObjectOfType<GameController>();
		audiocontroller_aux=FindObjectOfType<audiocontroller>();  
    }

    void OnTriggerEnter(Collider other)
  	{
        if(other.gameObject.tag=="Player")
        {
            if(crear_entrada!=null)
				{
					Instantiate(crear_entrada,transform.position,Quaternion.identity);
				}
            
            other.gameObject.transform.position=destino.transform.position;
            
            if(crear_salida!=null)
				{
					Instantiate(crear_salida,destino.transform.position,Quaternion.identity);
				}
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitador_viento : MonoBehaviour
{
    public float magnitud_limite;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Entro Jugador");
            Rigidbody rb= other.gameObject.GetComponent<Rigidbody>();
            if(rb.velocity.magnitude>magnitud_limite)
			{
                rb.velocity = rb.velocity.normalized * magnitud_limite;
			}
        }
    }
}

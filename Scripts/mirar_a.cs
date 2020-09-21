using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirar_a : MonoBehaviour
{
    private Camera camara;
    private GameController gamecontroller_aux;

    // Start is called before the first frame update
    void Start()
    {
        gamecontroller_aux=FindObjectOfType<GameController>();  
        camara=gamecontroller_aux.camara_actual();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camara.transform);    
    }
}

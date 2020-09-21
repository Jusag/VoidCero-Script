using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_sonido_particular : MonoBehaviour
{
    private audiocontroller audiocontroller_aux;
    private bool control_uno;

    // Start is called before the first frame update
    void Start()
    {
        audiocontroller_aux=FindObjectOfType<audiocontroller>();
        control_uno = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!audiocontroller_aux.estado_sonido_retorno() && control_uno)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            control_uno = false;
        }
        else if(audiocontroller_aux.estado_sonido_retorno() && !control_uno)
        {
            gameObject.GetComponent<AudioSource>().Play();
            control_uno = true;
        }

    }
}

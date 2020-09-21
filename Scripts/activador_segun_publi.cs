using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activador_segun_publi : MonoBehaviour
{
    private Adcaller_with_plugin control_ads_activas;
    
    void Start()
    {
        control_ads_activas=GameObject.Find("GameController").GetComponent<Adcaller_with_plugin>();
        control();
    }

    // Update is called once per frame
    /* void Update()
    {
        
    } */
    private void control()
    {
        gameObject.SetActive(control_ads_activas.activar_botones());
    }
}

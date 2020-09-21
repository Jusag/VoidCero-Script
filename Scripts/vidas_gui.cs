using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidas_gui : MonoBehaviour
{
    private int vidas_actuales;
    void Start()
    {
        
    }
    public void vidas_actuales_met(int x)
    {
        
        vidas_actuales=x;


        for (int i=0; i < vidas_actuales ; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }    
    }
    public void quitar_vidas(int aux_vidas)
    {
        gameObject.transform.GetChild(aux_vidas-1).gameObject.SetActive(false);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buscadorads : MonoBehaviour
{
    public bool adcaller;
    public bool premio;
    private GameObject gamecontrol_aux;
    private Adcaller ads_aux;
    private Adcaller_with_plugin ads_aux2;

    private 
    // Start is called before the first frame update
    void Start()
    {
        gamecontrol_aux=GameObject.Find("GameController");
        ads_aux=gamecontrol_aux.GetComponent<Adcaller>();
        ads_aux2=gamecontrol_aux.GetComponent<Adcaller_with_plugin>();
        
        if(gamecontrol_aux==null)
        {
            Debug.Log("gamecontrol_aux Es NULO");
        }
        if(ads_aux==null)
        {
            //Debug.Log("ADS Es NULO");
        }
    }

    // Update is called once per frame
    public void botoneta_ads() //script que se ejecuta cuando se precionan botones para publicidad
    {
        if(ads_aux2.activar_botones()){
            if(adcaller){ads_aux.Adshower();}
            if(!adcaller)
            {
            if(!premio)
                {
                    ads_aux2.reproducirAdInicial();
                }
            }
            if(!adcaller)
            {
                if(premio)
                {
                    ads_aux2.reproducirAdReward();
                }
            }
        }
        else 
        {
            vidasscrip aux_vidas = gamecontrol_aux.GetComponent<GameController>().estado_vidasencript();
            aux_vidas.incre_vida();
            gamecontrol_aux.GetComponent<GameController>().continue_game();
        }
    }
    public bool estado_de_ads()
    {
        return ads_aux.estado_general();
    }
}

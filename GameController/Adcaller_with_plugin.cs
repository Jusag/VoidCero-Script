using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class Adcaller_with_plugin : MonoBehaviour, IUnityAdsListener
{
    private string playStoreID = "3269531"; //El ID si es android
    private string appStoreID = "3269530"; //El ID si es IOS
    private string videoSimple ="video"; //video que se puede evitar
    private string videoReward ="rewardedVideo"; //video premio de 30 seg
    private GameController gameController_aux;
    public bool esTiendaGoogle;
    public bool esTestAd;
    
    

    // Start is called before the first frame update
    void Start()
    {
        gameController_aux=GetComponent<GameController>();
        Advertisement.AddListener(this);
        iniciarAd();
    }
    private void iniciarAd()
    {
        if(esTiendaGoogle)
        {
            Advertisement.Initialize(playStoreID, esTestAd);return;
        }
        Advertisement.Initialize(appStoreID, esTestAd);
    }
    // Update is called once per frame
    public void reproducirAdInicial()
    {
        if(!Advertisement.IsReady(videoSimple)){return;}
        Advertisement.Show(videoSimple);
    }
    public void reproducirAdReward()
    {
        if(!Advertisement.IsReady(videoReward)){return;}
        Advertisement.Show(videoReward);
    }
    
    public void OnUnityAdsReady(string placementId)
    {}
    public void OnUnityAdsDidError(string placementId)
    {}
    public void OnUnityAdsDidStart(string placementId)
    {}
    
    public void OnUnityAdsDidFinish(string placementId,ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Failed:{}
                break;
            case ShowResult.Skipped:{}
                break;
            case ShowResult.Finished:
            {
                if(placementId == videoReward)
                {
                //Debug.Log("Video Recompensa");
                    gameController_aux.continue_game_desp_publi();
                }
                if(placementId == videoSimple)
                {
                    //Debug.Log("Video Evitable");
                }
            }
            break;
        }
    }
    public bool activar_botones()
    {
        if (!esTiendaGoogle && !esTestAd)
        {
            return false;
        }
        else return true;
    }
}

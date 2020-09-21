using UnityEngine;
using System.Collections;
using UnityEngine.Monetization;

public class Adcaller : MonoBehaviour
{
    private string adid = "3269531"; //El ID si es android o ios
    private string videoad ="video"; //tipo de publicidad
    private bool control_ad_estado;
    public bool usar_ad;

    // Start is called before the first frame update
    void Start()
    {
        Monetization.Initialize(adid, true);
    }

    // Update is called once per frame
    public void Adshower()
    {
        if(Monetization.IsReady(videoad)&&usar_ad)
        //if(Monetization.IsReady(videoad))
        {
            ShowAdPlacementContent ad =null;
            ad = Monetization.GetPlacementContent(videoad) as ShowAdPlacementContent;
            if(ad!=null)
            {
                ad.Show();
            }
        }
    }
    public bool ad_listo()
    {
        control_ad_estado = Monetization.IsReady(videoad);
        return control_ad_estado;
    }
    public bool estado_general()
    {
        return ad_listo()&&usar_ad;
    }
}

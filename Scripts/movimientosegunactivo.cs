using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientosegunactivo : MonoBehaviour {
	public GameObject detonador;
	public float velocidad;
	public Vector3 origen;
	public Vector3 destino;
    public bool reverseMove = false;
	public bool soloida;
	public bool destruirsolamente;
    private float startTime;
    private float journeyLength;    

	
    void Start () {
		
		origen=new Vector3(-11.5f, -12.3f, 0);
		destino=new Vector3(-20.6f, -8.51f, 0);
        startTime = Time.time;
		journeyLength = Vector3.Distance(origen, destino);
    }
  
	void Update () {
		
		if(detonador==null){
			if(destruirsolamente)
			{
				Destroy(gameObject);
			}
			else{
				float distCovered = (Time.time - startTime) * velocidad;
				float fracJourney = distCovered / journeyLength;
				if(!soloida){
					if (!reverseMove)
					{
						transform.position = Vector3.Lerp(origen, destino, fracJourney);
					}
					else
					{
						transform.position = Vector3.Lerp(destino, origen, fracJourney);
					}
					if ((Vector3.Distance(transform.position, destino) == 0.0f || Vector3.Distance(transform.position, origen) == 0.0f)) //Checks if the object has travelled to one of the points
					{
						if (reverseMove)
						{
							reverseMove = false;
						}
						else
						{
							reverseMove = true;
						}
						startTime = Time.time;
					}
				}
				else
				{
					transform.position = Vector3.Lerp(origen, destino, fracJourney);
				}
			}
			
		}
	}
}

using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class audiocontroller : MonoBehaviour
{
    //variables de control
    private bool estadomusica=true;
    private bool estadosonido=true;
    private float volumen_aux;
    private static audiocontroller masteraudio_control;
    public sonido[] sonidos;
    void Awake()
    {
        if(masteraudio_control==null){
			masteraudio_control=this;
		}
		else if(masteraudio_control!=this)
			{
				Destroy(gameObject);
			}
		DontDestroyOnLoad(masteraudio_control);

        foreach(sonido s in sonidos ){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatBlend;
            if(s.spatBlend == 1)
            {
                s.source.rolloffMode = AudioRolloffMode.Linear;
            }
            s.source.maxDistance = s.max_distance;
        }

        ////////////////
        int musica_int = PlayerPrefs.GetInt("musica" , 1);
        if(musica_int == 1 )
        {
            estadomusica = true;
        }
        else
        {
            if(musica_int == 0 )
            {
                estadomusica = false;
            }
        }
        int sonido_int = PlayerPrefs.GetInt("sonido" , 1);
        if(sonido_int == 1 )
        {
            estadosonido=true;
        }
        else
        {
            if(sonido_int == 0 )
            {
                estadosonido=false;
            }
        }        
    }

    void Start()
    {
        
    }

    public void reproducir(string nombre)
    {
        if(estadomusica)
        {
            sonido s= Array.Find(sonidos, sonido=>sonido.nombre==nombre);
            if(s==null){
                return;
            }
            else
            {
                if(s.esmusica())
                {
                    if(estadomusica){
                        if(s.fadein)
                        { 
                            volumen_aux=s.reset_vol();
                            s.source.Play();
                            s.source.volume=0;
                            StartCoroutine(fadeSoundIn(s));
                        }
                        else
                        {
                            s.source.Play();
                        }
                    }
                }
            }
        }
        if(estadosonido)
        {
            sonido s= Array.Find(sonidos, sonido=>sonido.nombre==nombre);
            if(s==null)
            {
                return;
            }
            else
            {
                if(!s.esmusica())
                {
                    if(s.fadein)
                    { 
                        volumen_aux=s.reset_vol();
                        s.source.Play();
                        s.source.volume=0;
                        StartCoroutine(fadeSoundIn(s));
                    }
                    else
                    {
                        s.source.Play();
                    }
                }
            }
        }
    }
    public void detener(string nombre)
    {
        sonido s= Array.Find(sonidos, sonido=>sonido.nombre==nombre);
        if(s==null){
            return;
        }
        else
        {
            if(s.esmusica())
            {
                if(estadomusica)
                {
                    if(s.fadeout)
                    {
                        volumen_aux=s.reset_vol();
                        StartCoroutine(fadeSoundOut(s));
                    }
                    else
                    {
                        s.source.Stop();
                    }
                }
            }
            else
            {
                if(estadosonido)
                {
                    s.source.Stop();
                }
            }
        }
    }

    public void detener_todo()
    {
        for(int i=0;i<sonidos.Length;i++)
        {
            volumen_aux=sonidos[i].source.volume;
            if(sonidos[i].esmusica())
            {
               StartCoroutine(fadeSoundOut(sonidos[i]));
            }
                     
            sonidos[i].source.volume=sonidos[i].reset_vol();
        }
    }
    IEnumerator fadeSoundIn(sonido aux)
    {
        if(aux.fadein)
        {
            while(aux.source.volume<volumen_aux)
            {
                aux.source.volume+=Time.deltaTime/aux.fade_vel;
                yield return null;
            }
            aux.reset_vol();
        }
    }
    IEnumerator fadeSoundOut(sonido aux)
    {
        if(aux.fadeout)
        {
            while(aux.source.volume>0.01f)
            {
                aux.source.volume-=Time.deltaTime/aux.fade_vel;
                yield return null;
            }
        }
        aux.source.volume=volumen_aux;
        aux.source.Stop();

    }


    public void estado_musica(bool aux)
    {
        estadomusica=aux;
        if(!estadomusica)
        {
            for(int i=0;i<sonidos.Length;i++)
            {
                if(sonidos[i].esmusica()){
                    sonidos[i].source.Stop();
                }
            }
        }
    }
    public void estado_sonido(bool aux)
    {
        estadosonido=aux;
        if(!estadomusica)
        {
            for(int i=0;i<sonidos.Length;i++)
            {
                if(sonidos[i].esmusica()){
                    sonidos[i].source.Stop();
                    sonidos[i].reset_vol();
                }
            }
        }
    }

    public bool estado_musica_retorno()
    {
        return estadomusica;
    }
    
    public bool estado_sonido_retorno()
    {
        return estadosonido;
    }

    /* void OnDrawGizmosSelected()
    {
        foreach(sonido s in sonidos )
        {
            if(s.source.spatialBlend == 1f)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(gameObject.transform.position, s.source.maxDistance);
            }
        }
    } */
}

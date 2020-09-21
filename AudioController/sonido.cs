using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class sonido
{
    //p
    public AudioClip clip;
    public string nombre;
    public bool musica;
    
    [Header("Fade IN/OUT y Velocidad")]
    public bool fadein;
    public bool fadeout;
    public float fade_vel;

    [Range(0f,1f)]
    public float volumen;
    [Range(0f,1f)]
    public float volumen_por_defecto;
    [Range(.1f,3f)]
    public float pitch;
    public bool loop;
    [Range(0f,1f)]
    public float spatBlend;
    public float max_distance;

    
    
    [HideInInspector]
    public AudioSource source;

    public bool esmusica()
    {
        return musica;
    }
    public float reset_vol()
    {
        return volumen_por_defecto;
    }
   
}

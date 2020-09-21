using System.Collections;
using UnityEngine;

public class vidasscrip : MonoBehaviour
{
    private string vid_encript;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void incre_vida()
    {
        int aux_int=Random.Range(0,4); //b,c,i,k respectivamente
        switch(aux_int)
        {
            case 0:vid_encript+='b';
            break;
            case 1:vid_encript+='c';
            break;
            case 2:vid_encript+='i';
            break;
            case 3:vid_encript+='k';
            break;
        }
    }
    public void decre_vida()
    {
        int aux_int=Random.Range(0,4); //a,l,j,o respectivamente
        switch(aux_int)
        {
            case 0:vid_encript+='a';
            break;
            case 1:vid_encript+='l';
            break;
            case 2:vid_encript+='j';
            break;
            case 3:vid_encript+='o';
            break;
        }
    }
    public int vidas_actuales()
    {
        int vidas=0;
        char[] char_auxiliar = vid_encript.ToCharArray();
        for(int i=0;i<char_auxiliar.Length;i++)
        {
            vidas+=control_decript(char_auxiliar[i]);
        }
        return vidas;
    }
    private int control_decript(char aux)
    {
        if(aux=='b' || aux=='c' || aux=='i' || aux=='k')
        {
            return 1;
        }
        if(aux=='a' || aux=='l' || aux=='j' || aux=='o')
        {
            return -1;
        }
        return 0;
    }

    public int decript_publico(string aux_string)
    {
        int vidas = 0;
        char[] char_auxiliar = aux_string.ToCharArray();
        for (int i = 0; i < char_auxiliar.Length; i++)
        {
            vidas += control_decript(char_auxiliar[i]);
        }
        return vidas;
    }

    public string encrudo()
    {
        return vid_encript;
    }

    public void definir_vidas(string aux)
    {
        vid_encript = aux;
    }
}

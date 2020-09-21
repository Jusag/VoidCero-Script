using System.Collections;
using UnityEngine;

public class puntitoscrip : MonoBehaviour
{
    private string puntos_string;

    /*
    public void sum_punt(char aux)
    {
        //puntaje_actual+=(string)aux;
    }
    public void rest_punt(char aux)
    {
        //Z resta de a 1
    }
    */

    private void Start()
    {
        
        /* Debug.Log(puntos_string+"");
        Debug.Log(decript());
        Debug.Log("Reencriptado queda...");
        encript(decript());
        Debug.Log(puntos_string+""); */
    }
    
    
    
    //private int decript(char auxlocal)
    public int decript()
    {
        int puntos=0;
        char[] char_auxiliar = puntos_string.ToCharArray();
        for(int i=0;i<char_auxiliar.Length;i++)
        {
            puntos+=control_decript(char_auxiliar[i]);
        }
        return puntos;
    }
    public void encript(int valor)
    {
        control_encript(valor);
    }
    

    private int control_decript(char aux)
    {
        if(aux=='b' || aux=='c' || aux=='i' || aux=='k')
        {
            return 5;
        }
        if(aux=='a' || aux=='l' || aux=='j' || aux=='o')
        {
            return 15;
        }
        if(aux=='e' || aux=='h' || aux=='m' || aux=='n')
        {
            return 10;
        }
        if(aux=='p' || aux=='d' || aux=='g' || aux=='j')
        {
            return 20;
        }
        if(aux=='t' || aux=='u' || aux=='x' || aux=='z')
        {
            return 35;
        }
        if(aux == 'v')
        {
            return 0;
        }
        return 0;
    }

    
    private void control_encript(int aux_local)
    {
        int aux_int;
        if(aux_local==5)
        {
            aux_int=Random.Range(0,4); //b,c,i,k respectivamente
            //Debug.Log(aux_int+"");
            switch(aux_int)
            {
                case 0:puntos_string+='b';
                break;
                case 1:puntos_string+='c';
                break;
                case 2:puntos_string+='i';
                break;
                case 3:puntos_string+='k';
                break;
            }
        }
        if(aux_local==15)
        {
            aux_int=Random.Range(0,4); //a,l,j,o respectivamente
            switch(aux_int)
            {
                case 0:puntos_string+='a';
                break;
                case 1:puntos_string+='l';
                break;
                case 2:puntos_string+='j';
                break;
                case 3:puntos_string+='o';
                break;
            }
        }
        if(aux_local==10)
        {
            aux_int=Random.Range(0,4); //e,h,m,n respectivamente
            switch(aux_int)
            {
                case 0:puntos_string+='e';
                break;
                case 1:puntos_string+='h';
                break;
                case 2:puntos_string+='m';
                break;
                case 3:puntos_string+='n';
                break;
            }
        }
        if(aux_local==20)
        {
            aux_int=Random.Range(0,4); //p,d,g,f respectivamente
            switch(aux_int)
            {
                case 0:puntos_string+='p';
                break;
                case 1:puntos_string+='d';
                break;
                case 2:puntos_string+='g';
                break;
                case 3:puntos_string+='f';
                break;
            }
        }
        if(aux_local==35)
        {
            aux_int=Random.Range(0,4); //e,h,m,n respectivamente
            switch(aux_int)
            {
                case 0:puntos_string+='t';
                break;
                case 1:puntos_string+='u';
                break;
                case 2:puntos_string+='x';
                break;
                case 3:puntos_string+='z';
                break;
            }
        }
    }
    public void limpiar()
    {
        puntos_string="";
    }

    public int decript_publico(string aux_string)
    {
        int puntos=0;
        char[] char_auxiliar = aux_string.ToCharArray();
        for(int i=0 ; i<char_auxiliar.Length ; i++)
        {
            puntos += control_decript( char_auxiliar[i] );
        }
        return puntos;
    }
    
    public string encrudo()
    {
        return puntos_string;
    }
    public void definir_puntos(string aux)
    {
        puntos_string = aux;
    }
}
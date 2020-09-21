 using UnityEngine;
 using System.Collections;
 
 public class ParticleSystemAutoDestroy : MonoBehaviour 
 {
     public ParticleSystem ps;
     public void Start() 
     {
     }
     public void Update() 
     {
        //DESTRUYO EL OBJETO AL ACABAR EL CICLO DE LA ANIMACION
         if(ps)
         {
             if(!ps.IsAlive())
             {
                 Destroy(gameObject);
             }
         }
     }
 }

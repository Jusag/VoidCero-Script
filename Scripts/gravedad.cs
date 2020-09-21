using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravedad : MonoBehaviour {
	public string Objetivo="Player";
    public GameObject ObjectivoGO;
    public float PullRadius; // Radius to pull
    public float GravitationalPull; // Pull force
    public float MinRadius; // Minimum distance to pull from
    public float DistanceMultiplier; // Factor by which the distance affects force
    public LayerMask LayersToPull;
    // Function that runs on every physics frame
    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, PullRadius, LayersToPull);
        foreach (var collider in colliders)
        {
			if(collider.gameObject.tag == "Player"){
                //SI EL OBJECTO AFECTADO ES JUGADOR ENTONCES AL GAMEOBJECT DE JUGADOR LE DOY FUERZA DE ATRACCION
				Rigidbody rb = collider.GetComponent<Rigidbody>();
				Vector3 direction = transform.position - collider.transform.position;
				if (direction.magnitude < MinRadius) continue;
				float distance = direction.sqrMagnitude*DistanceMultiplier + 1; // The distance formula
				rb.AddForce(direction.normalized * (GravitationalPull / distance) * rb.mass * Time.fixedDeltaTime);
			}
            else {
                if(ObjectivoGO!=null){
                    //SI EL OBJECTO AFECTADO ES JUGADOR ENTONCES AL GAMEOBJECT DE JUGADOR LE DOY FUERZA DE ATRACCION
                    Rigidbody rb = ObjectivoGO.GetComponent<Rigidbody>();
                    Vector3 direction = transform.position - collider.transform.position;
                    if (direction.magnitude < MinRadius) continue;
                    float distance = direction.sqrMagnitude*DistanceMultiplier + 1; // The distance formula
                    rb.AddForce(direction.normalized * (GravitationalPull / distance) * rb.mass * Time.fixedDeltaTime);
                }
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PullRadius);
    }
}

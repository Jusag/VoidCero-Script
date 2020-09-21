using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitar : MonoBehaviour {
	public GameObject objetivo;
	public float speed;
	void Start () {
	}
	void Update () {
		transform.RotateAround(objetivo.transform.position,Vector3.forward,speed*Time.deltaTime);
		transform.LookAt(objetivo.transform);
	}
	private void orbita()
	{
		

	}
}

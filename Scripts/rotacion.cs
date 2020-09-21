using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacion : MonoBehaviour {

	public float x,y,z;
	public float speed;
	void Start () {
	}
	void Update () {
		transform.Rotate (new Vector3 (x, y, z) * (speed*Time.deltaTime), Space.Self);
	}
}

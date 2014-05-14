using UnityEngine;
using System.Collections;

public class RotateThis : MonoBehaviour {

	public Vector3 angle;
	public float speed;

	void Update(){
		transform.Rotate(angle.normalized * (speed * Time.deltaTime));
	}
}

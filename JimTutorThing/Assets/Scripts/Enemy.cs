using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Vector3 targetPos;
	public float speed;

	void Update() {
		transform.position += (targetPos - transform.position).normalized * Time.deltaTime * speed;

		transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos - transform.position);
	}

	public void Kill() {
		gameObject.SetActive(false);
	}

}

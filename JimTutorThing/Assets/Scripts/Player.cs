using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] ObjectPool bulletPool;
	[SerializeField] Transform gunTransform;

	private Vector3 mousePos = Vector3.zero;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		gunTransform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gunTransform.position);


		if (Input.GetMouseButtonDown(0)) {
			GameObject bullet = bulletPool.Spawn(bulletPool.transform.position);
			bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gunTransform.position);
			bullet.GetComponent<Bullet>()?.Fire(mousePos - gunTransform.position, 5);
		}
	}
}

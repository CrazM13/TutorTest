using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour {

	[SerializeField] private float detectionRange = 0;

	private Vector2 direction;
	private float speed;

	void Update() {
		RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRange, Vector2.zero);

		Enemy enemyHit = hit.collider?.GetComponent<Enemy>();
		if (enemyHit) {
			enemyHit.Kill();
			gameObject.SetActive(false);
		}

		transform.position += (Vector3)direction * Time.deltaTime * speed;

		Vector2 screenPos = Camera.main.WorldToViewportPoint(transform.position);
		if (screenPos.x < -0.1f || screenPos.x > 1.1f || screenPos.y < -0.1f|| screenPos.y > 1.1f) gameObject.SetActive(false);
	}

	public void Fire(Vector2 direction, float force) {
		this.direction = direction.normalized;
		this.speed = force;
	}

	private void OnDrawGizmos() {
		Handles.DrawWireDisc(transform.position, Vector3.forward, detectionRange);
	}

}

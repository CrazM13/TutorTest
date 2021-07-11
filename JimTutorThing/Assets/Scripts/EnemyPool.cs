using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyPool : ObjectPool {

	[SerializeField] private Transform target;

	[SerializeField] private Bounds spawnRange;


	[SerializeField] private float spawnTime;

	private float timer = 0;

	void Update() {
		timer += Time.deltaTime;
		if (timer >= spawnTime) {
			timer -= spawnTime;

			Vector2 spawnPoint = new Vector2(Random.Range(spawnRange.min.x, spawnRange.max.x), Random.Range(spawnRange.min.y, spawnRange.max.y));

			Spawn(spawnPoint);
		}
	}

	public override GameObject Spawn(Vector3 position) {
		GameObject newEnemy = base.Spawn(position);
		newEnemy.GetComponent<Enemy>().targetPos = target.position;
		newEnemy.transform.SetParent(transform);

		return newEnemy;
	}

	private void OnDrawGizmos() {
		Handles.DrawWireCube(spawnRange.center, spawnRange.size);
	}

}

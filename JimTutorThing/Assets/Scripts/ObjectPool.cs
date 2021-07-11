using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	[SerializeField] private GameObject prefab;
	[SerializeField] private int count;

	private List<GameObject> activeObjects;
	private Queue<GameObject> inactiveObjects;

	void Start() {
		activeObjects = new List<GameObject>();
		inactiveObjects = new Queue<GameObject>();

		for (int _ = 0; _ < count; _++) {
			GameObject newObject = Instantiate(prefab);
			newObject.SetActive(false);
			newObject.transform.SetParent(transform);
			inactiveObjects.Enqueue(newObject);
		}
	}

	private void Update() {
		for (int i = activeObjects.Count - 1; i >= 0; i--) {
			if (!activeObjects[i].activeInHierarchy) {
				DestroyIndex(i);
			}
		}
	}

	public virtual GameObject Spawn(Vector3 position) {
		if (inactiveObjects.Count <= 0) return null;

		GameObject newObject = inactiveObjects.Dequeue();
		activeObjects.Add(newObject);
		newObject.transform.position = position;
		newObject.transform.rotation = Quaternion.identity;

		newObject.SetActive(true);

		return newObject;
	}

	public virtual void Destroy(GameObject obj) {
		int index = activeObjects.FindIndex((o) => o == obj);

		inactiveObjects.Enqueue(activeObjects[index]);
		activeObjects.RemoveAt(index);
	}

	private void DestroyIndex(int index) {
		inactiveObjects.Enqueue(activeObjects[index]);
		activeObjects.RemoveAt(index);
	}

}

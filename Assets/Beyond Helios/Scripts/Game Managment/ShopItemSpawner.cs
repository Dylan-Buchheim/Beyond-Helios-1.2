using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpawner : MonoBehaviour {

	GameObject currentItem = null;

	public void SpawnItem(GameObject item){
		if (currentItem == null) {
			currentItem = Instantiate (item, transform.position, transform.rotation);
			currentItem.transform.parent = gameObject.transform;
		} else {
			Destroy (currentItem);
			currentItem = Instantiate (item, transform.position, transform.rotation);
			currentItem.transform.parent = gameObject.transform;
		}
	}
}

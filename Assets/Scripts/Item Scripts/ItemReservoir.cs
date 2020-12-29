using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReservoir : MonoBehaviour
{
	[System.Serializable]
	class ItemModels
	{
		[SerializeField]
		GameObject[] models;

		public int Length { get => models.Length; }
		public GameObject this[int i] => models[i];
	}

	[SerializeField]
	ItemType itemGenerated;

	[SerializeField]
	GameObject itemObjectRef = null;

	[SerializeField]
	ItemModels[] itemModels;

	[SerializeField]
	string[][] test;

	bool playerInArea = false;
	GameObject playerRef;

	void Update()
	{
		if (playerInArea && Input.GetButtonDown("Action"))
		{
			if (playerRef.GetComponent<Player>().ObjectCarrying == null)
			{
				GameObject obj = Instantiate(itemObjectRef, playerRef.GetComponent<Player>().CarryPosition.transform.position, Quaternion.identity);
				CarryObject item = obj.GetComponent<CarryObject>();
				item.SetItemModel(itemModels[(int)itemGenerated][(int)Random.Range(0, itemModels[(int)itemGenerated].Length)]);
				item.PickUp(playerRef);
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			playerInArea = true;
			playerRef = collider.gameObject;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			playerInArea = false;
		}
	}
}

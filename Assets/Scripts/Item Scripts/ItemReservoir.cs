using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReservoir : MonoBehaviour
{
	[SerializeField]
	ItemType itemGenerated;

	[SerializeField]
	GameObject itemObjectRef = null;

	[SerializeField]
	GameObject[] itemModels;


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
				item.SetItemModel(itemModels[Random.Range(0, itemModels.Length)]);
				item.Type = itemGenerated;
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

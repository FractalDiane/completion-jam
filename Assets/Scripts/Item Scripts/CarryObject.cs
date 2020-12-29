using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
	[SerializeField]
	string itemName = "Thing";

	[SerializeField]
	ItemType type;

	[SerializeField]
	float pickupTime = 0.5f;

	[SerializeField]
	float slowdownFactor = 1.0f;

	[SerializeField]
	AudioClip pickupSound = null;

	[SerializeField]
	AudioClip putdownSound = null;

	bool playerInArea = false;
	bool beingCarried = false;
	GameObject playerRef = null;

	float startY;

	GameObject dropPoint = null;

	public ItemType Type { get => type; set => type = value; }

	void Start()
	{
		//startY = transform.position.y;
	}

	void Update()
	{
		if (playerInArea && Input.GetButtonDown("Action"))
		{
			PickUp(playerRef);
		}
	}

	public void PickUp(GameObject playerReference)
	{
		if (!beingCarried && playerReference.GetComponent<Player>().ObjectCarrying == null)
		{
			AudioManager.instance.PlaySFX(pickupSound);
			Player player = playerReference.GetComponent<Player>();
			player.ObjectCarrying = gameObject;
			transform.position = player.CarryPosition.transform.position;
			transform.SetParent(player.CarryPosition.transform);
			beingCarried = true;
		}
		else if (dropPoint != null)
		{
			AudioManager.instance.PlaySFX(putdownSound);
			transform.SetParent(null);
			transform.position = dropPoint.transform.position;
			Player player = playerReference.GetComponent<Player>();
			player.ObjectCarrying = null;
			beingCarried = false;
		}
	}

	public void SetItemModel(GameObject model)
	{
		GameObject modelInst = Instantiate(model, transform.position, Quaternion.identity);
		modelInst.transform.parent = transform;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			playerInArea = true;
			playerRef = collider.gameObject;
		}
		else if (collider.gameObject.tag == "ItemDropPoint")
		{
			dropPoint = collider.gameObject;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			playerInArea = false;
		}
		else if (collider.gameObject.tag == "ItemDropPoint")
		{
			dropPoint = null;
		}
	}
}

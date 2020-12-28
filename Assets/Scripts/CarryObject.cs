using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
	[SerializeField]
	string itemName = "Thing";

	[SerializeField]
	float pickupTime = 0.5f;

	[SerializeField]
	float slowdownFactor = 1.0f;

	bool playerInArea = false;
	bool beingCarried = false;
	GameObject playerRef = null;

	float startY;

	void Start()
	{
		startY = transform.position.y;
	}

	void Update()
	{
		if (playerInArea && Input.GetButtonDown("Action"))
		{
			if (!beingCarried)
			{
				Player player = playerRef.GetComponent<Player>();
				player.ObjectCarrying = gameObject;
				transform.position = player.CarryPosition.transform.position;
				transform.SetParent(player.CarryPosition.transform);
				beingCarried = true;
			}
			else
			{
				transform.SetParent(null);
				transform.position = new Vector3(transform.position.x, startY, transform.position.z);
				Player player = playerRef.GetComponent<Player>();
				player.ObjectCarrying = null;
				beingCarried = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
	bool playerInArea = false;
	GameObject playerRef;

	void Update()
	{
		if (playerInArea && Input.GetButtonDown("Action"))
		{
			if (playerRef.GetComponent<Player>().ObjectCarrying != null)
			{
				Player player = playerRef.GetComponent<Player>();
				Destroy(player.ObjectCarrying);
				player.ObjectCarrying = null;
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
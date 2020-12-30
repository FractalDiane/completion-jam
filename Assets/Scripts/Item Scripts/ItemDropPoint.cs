using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropPoint : MonoBehaviour
{
	bool occupied = false;
	GameObject item = null;
	[SerializeField] RequestAI kid;

	public bool Occupied { set => occupied = value; }
	public GameObject Item { get => item; set => item = value; }

	public void callCheckRequirements()
	{
		if (!kid.GapActive)
		{
			kid.CheckRequirement(item.GetComponent<CarryObject>().Type);
		}
	}
}

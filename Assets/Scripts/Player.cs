using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	const float RotationInterpolateSpeed = 10.0f;

	new Rigidbody rigidbody;
	Animator animator;

	[SerializeField]
	GameObject model = null;

	[SerializeField]
	GameObject carryPosition = null;

	float horizontal;
	float vertical;
	float speed = 12f;
	Quaternion modelRotation;

	GameObject objectCarrying;
	GameObject personEscorting;

	public GameObject CarryPosition { get => carryPosition; }
	public GameObject ObjectCarrying { get => objectCarrying; set => objectCarrying = value; }

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
		Vector3 target = new Vector3(horizontal, 0f, vertical);
		Vector3 result = target * speed;

		if (vertical != 0 || horizontal != 0)
		{
			modelRotation = Quaternion.LookRotation(target, Vector3.up);
		}
		
		Quaternion newRot = Quaternion.Slerp(model.transform.rotation, modelRotation, RotationInterpolateSpeed * Time.deltaTime);

		rigidbody.velocity = result;
		model.transform.rotation = newRot;
	}
}
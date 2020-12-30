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

	[SerializeField]
	float speed = 12f;
	Quaternion modelRotation;

	float runBlend = 0f;

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

		animator.SetFloat("Blend", runBlend);
		animator.SetBool("Holding", objectCarrying != null);
	}

	void FixedUpdate()
	{
		Vector3 target = new Vector3(horizontal, 0f, vertical);
		Vector3 result = target.normalized * speed;
		if (result.magnitude > 0)
		{
			runBlend = Mathf.Clamp(runBlend + 4f * Time.deltaTime, 0f, 1f);
		}
		else
		{
			runBlend = Mathf.Clamp(runBlend - 4f * Time.deltaTime, 0f, 1f);
		}

		if (vertical != 0 || horizontal != 0)
		{
			modelRotation = Quaternion.LookRotation(target, Vector3.up);
		}
		
		Quaternion newRot = Quaternion.Slerp(model.transform.rotation, modelRotation, RotationInterpolateSpeed * Time.deltaTime);

		rigidbody.velocity = result;
		model.transform.rotation = newRot;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (objectCarrying != null && collider.gameObject.tag == "Kid")
		{
			if (!collider.gameObject.GetComponent<RequestAI>().GapActive && objectCarrying.GetComponent<CarryObject>().Type != ItemType.Doctor)
			{
				RequestAI kid = collider.gameObject.GetComponent<RequestAI>();
				kid.CheckRequirement(objectCarrying.GetComponent<CarryObject>().Type);
				Destroy(objectCarrying);
				objectCarrying = null;
			}
			
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{
	public enum BodyPart { HEAD, CHEST}

	[SerializeField] BodyPart bodyPart;
	BodyManager bodyManager;

	private void Start()
	{
		GetComponent<Collider>().isTrigger = true;
		bodyManager = gameObject.transform.root.GetComponent<BodyManager>();
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "hand")
		{
			switch (bodyPart)
			{
				case BodyPart.HEAD:
					bodyManager.OnTouchHead.Invoke();
					break;
				case BodyPart.CHEST:
					bodyManager.OnTouchChest.Invoke();
					break;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{

	}
}

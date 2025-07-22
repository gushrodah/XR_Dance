using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{
	public enum BodyPart { HEAD, CHEST}

	[SerializeField] BodyPart bodyPart;
	BodyManager bodyManager;

	public UnityEvent headTouched;

	private void Start()
	{
		GetComponent<Collider>().isTrigger = true;
		bodyManager = FindAnyObjectByType<BodyManager>();
		//bodyManager = gameObject.transform.root.GetComponent<BodyManager>();
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "hand")
		{
			switch (bodyPart)
			{
				case BodyPart.HEAD:
					bodyManager.OnTouchHead.Invoke();
					headTouched.Invoke(); 
					break;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{

	}
}

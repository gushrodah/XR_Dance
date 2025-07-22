using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BodyManager : MonoBehaviour
{
	public UnityEvent OnTouchHead, OnTouchChest;

	private void OnEnable()
	{
		OnTouchHead.AddListener(OnHeadTouchEvent);
		OnTouchHead.AddListener(OnChestTouchEvent);
	}

	void OnHeadTouchEvent()
	{
		Debug.Log("touched HEAD");
	}

	void OnChestTouchEvent()
	{
		Debug.Log("touched CHEST");
	}
}

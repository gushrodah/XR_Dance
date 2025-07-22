using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BodyManager : MonoBehaviour
{
	public UnityEvent OnTouchHead, OnClapped, OnLeftStep, OnRightStep;
	[SerializeField]bool foundLeft, foundRight;
	[SerializeField] GameObject handPrefab;
	[SerializeField] Transform head, rHand, lHand, rHip, lHip, lHead, rHead, lAnkle, rAnkle;

	[SerializeField] float clapDistance = .1f;
	[SerializeField] float groundHeight = 0;
	[SerializeField] ParticleSystem trailLeft, trailRight;
	[SerializeField] ParticleSystem stompLeft, stompRight;
	[SerializeField] ParticleSystem clapPS;

	// to mark only first clap
	bool isClapping = false;
	bool isRightFootUp = false;
	bool isLeftFootUp = false;

	public Transform Head => head;
	public Transform LHand => lHand;
	public Transform LHip => lHip;
	public Transform LHead => lHead;
	public Transform RHip => lHip;
	public Transform RHand => rHand;
	public Transform LAnkle => lAnkle;
	public Transform RAnkle => rAnkle;


	private void OnEnable()
	{
		OnTouchHead.AddListener(OnHeadTouchEvent);
	}

	private void Update()
	{
		// set collider triggers
		if (!foundLeft)
		{
			foundLeft = FindAndAdd("LEFT_WRIST");
		}
		if (!foundRight)
		{
			foundRight = FindAndAdd("RIGHT_WRIST");
		}
		// find transforms
		if (!head || !rHand || !lHand || !rHip || !lHip || !lHead || !rHead || !lAnkle || !rAnkle)
		{
			SetBodyTransforms();
			SetTrails();
			SetStomps();
		}
		else {

			// clap check
			if (Vector3.Distance(rHand.position, LHand.position) < clapDistance && !isClapping)
			{
				Debug.Log("CLAPPED");
				OnClapped.Invoke();
				isClapping = true;

				clapPS.transform.position = (rHand.position + lHand.position) /2;
				clapPS.Play();
			}
			else if (Vector3.Distance(rHand.position, LHand.position) >= clapDistance)
			{
				//Debug.Log(Vector3.Distance(rHand.position, LHand.position));
				isClapping = false;
			}
			//left stomp check
			if (lAnkle.transform.position.y > groundHeight)
			{
				isLeftFootUp = true;
			}
			else if (lAnkle.transform.position.y <= groundHeight && isLeftFootUp)
			{
				//Debug.Log("left STEP");
				OnLeftStep.Invoke();
				isLeftFootUp = false;
				stompLeft.Play();
			}
			//right stomp check
			if (rAnkle.transform.position.y > groundHeight)
			{
				isRightFootUp = true;
			}
			else if (lAnkle.transform.position.y <= groundHeight && isRightFootUp)
			{
				//Debug.Log("right STEP");
				OnRightStep.Invoke();
				isRightFootUp = false;
				stompRight.Play();
			}
		}
	}

	void SetBodyTransforms()
	{
		head = GameObject.Find("NOSE").transform;
		rHand = GameObject.Find("RIGHT_WRIST").transform;
		lHand = GameObject.Find("LEFT_WRIST").transform;
		rHip = GameObject.Find("RIGHT_HIP").transform;
		lHip = GameObject.Find("LEFT_HIP").transform;
		lHead = GameObject.Find("RIGHT_EAR").transform;
		rHead = GameObject.Find("LEFT_EAR").transform;
		lAnkle = GameObject.Find("LEFT_HEEL").transform;
		rAnkle = GameObject.Find("RIGHT_HEEL").transform;
	}

	void SetTrails()
	{
		trailLeft.transform.parent = LHand;
		trailLeft.transform.localPosition = Vector3.zero;
		trailRight.transform.parent = RHand;
		trailRight.transform.localPosition = Vector3.zero;
	}

	void SetStomps()
	{
		stompLeft.transform.parent = lAnkle;
		stompLeft.transform.localPosition = Vector3.zero;
		stompRight.transform.parent = RAnkle;
		stompRight.transform.localPosition = Vector3.zero;
	}

	bool FindAndAdd(string _name)
	{
		GameObject g = GameObject.Find(_name);
		if (g != null)
		{
			GameObject hand = Instantiate(handPrefab);
			hand.GetComponent<PosOffset>().SetTarget(g.transform);
			return true;
		}
		return false;
	}

	bool trailOn = false;
	void OnHeadTouchEvent()
	{
		Debug.Log("touched HEAD");

		trailOn = !trailOn;
		// toggle trails
		if (trailOn)
		{
			trailLeft.Play();
			trailRight.Play();
		}
		else
		{
			trailLeft.Stop();
			trailRight.Stop();
		}
	}

}

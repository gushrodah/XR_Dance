using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using UnityEngine.Events;

public class InteractableEvent : MonoBehaviour
{
    [SerializeField] PokeInteractable interactable;
	bool on = true;
	[SerializeField] UnityEvent onClick;
	[SerializeField] List<GameObject> ToggelObjs = new List<GameObject>();

	private void OnEnable()
	{
		interactable.WhenStateChanged += UpdateVisualState;
	}

	private void UpdateVisualState(InteractableStateChangeArgs args)
	{
		switch (interactable.State)
		{
			case InteractableState.Select:
				onClick.Invoke();
				on = !on;
				foreach (var o in ToggelObjs)
				{
					o.SetActive(on);
				}

				break;
			case InteractableState.Hover:
				break;
			case InteractableState.Normal:
				break;
			case InteractableState.Disabled:
				break;
			default:
				break;
		}
	}
}

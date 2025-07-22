using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    [SerializeField] BodyManager bodyManager;
    [SerializeField] AudioLoudnessDetection ld;

	private void OnEnable()
	{
		ld.onNextBeat.AddListener(SpawnVisual);
	}

	private void OnDisable()
	{
		ld.onNextBeat.RemoveListener(SpawnVisual);
	}

	public enum BodyPart
    {
        HEAD, RHAND, LHAND, RHIP, LHIP, RHEAD, LHEAD, HIP, CHEST
    }

    [Serializable]
    public struct BeatVisual
    {
        public BodyPart bodyPart;
        public GameObject prefab;
    }

    [SerializeField] List<BeatVisual> visuals;

    int cur = 0;

    void SpawnVisual()
    {
        BeatVisual bv = GetNextBeatVisual();

		GameObject g = Instantiate(bv.prefab);
		switch (bv.bodyPart)
		{
			case BodyPart.HEAD:
                g.transform.position = bodyManager.Head.position;
				break;
			case BodyPart.RHAND:
				g.transform.position = bodyManager.RHand.position;
				break;
			case BodyPart.LHAND:
				g.transform.position = bodyManager.LHand.position;
				break;
			default:
				break;
		}
    }

    public BeatVisual GetNextBeatVisual()
    {
        BeatVisual visual = visuals[cur];
        cur++;
        if (cur >= visuals.Count) cur = 0;
        return visual;
    }
}

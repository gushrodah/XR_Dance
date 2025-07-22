using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AudioLoudnessDetection : MonoBehaviour
{
    [SerializeField] int sampleWindow;
    [SerializeField] AudioSource source;
    [SerializeField] float scalar = 100;
    [SerializeField] AudioClip micClip;
    [SerializeField] float threshhold = 1;

    [SerializeField] bool aboveThreshold = false, pastCooldown = true;
    [SerializeField] float coolDown = .2f;

    public UnityEvent onNextBeat;

	private void Start()
    {
        MicToAudioClip();
    }

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
        
	}

	void MicToAudioClip()
    {
        string micName = Microphone.devices[1];
        Debug.Log(micName);
        micClip = Microphone.Start(micName, true, 20, AudioSettings.outputSampleRate); 
        source.clip = micClip;
    }

    float GetLoudnessFromMic()
    {
        if(micClip == null) return 0;
        return LoudnessFromClip(Microphone.GetPosition(Microphone.devices[1]), micClip);
    }

    float LoudnessFromClip(int pos, AudioClip clip)
    {
        int startPos = pos - sampleWindow;
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPos);

        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }

    private void Update()
    {
        float scale = GetLoudnessFromMic() * scalar;
        if (scale > threshhold && !aboveThreshold && pastCooldown)
        {
            //Debug.Log("HIT");
            onNextBeat.Invoke();
			aboveThreshold = true;
            StartCoroutine("cooldownTimer");
        }
        else if(scale < threshhold) aboveThreshold = false;
        //Debug.Log(scale);
    }

    IEnumerator cooldownTimer()
	{
		pastCooldown = false;
		yield return new WaitForSeconds(coolDown);
        pastCooldown = true;
    }
}

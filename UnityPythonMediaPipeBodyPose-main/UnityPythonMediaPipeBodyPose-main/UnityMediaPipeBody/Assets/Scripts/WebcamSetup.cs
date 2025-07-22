using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamSetup : MonoBehaviour
{
    WebCamTexture tex;
    void Start()
    {
        WebCamDevice cur = new WebCamDevice();
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name); 
			cur = devices[0];
        }
        tex = new WebCamTexture(cur.name);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = tex;
        tex.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraPostControl : MonoBehaviour
{
    public float bloomIntensity = 50f;
    
    private PostProcessVolume postProcessVolume;
    private Bloom bloom;

    private void Awake()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out bloom);
    }

    // Use this for initialization
    void Start ()
    {
        bloom.intensity.value = bloomIntensity;        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

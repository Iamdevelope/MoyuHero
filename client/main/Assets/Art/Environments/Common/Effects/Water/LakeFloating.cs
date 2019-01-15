using UnityEngine;
using System.Collections;

public class LakeFloating : MonoBehaviour {

    public float WaveHeight = 0.001f;
    public float WaveFrequence = 2.0f;


    float LocalY = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        LocalY = this.transform.localPosition.y - Mathf.Sin(Time.time * WaveFrequence) * WaveHeight;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, LocalY, this.transform.localPosition.z);
       
	}
}

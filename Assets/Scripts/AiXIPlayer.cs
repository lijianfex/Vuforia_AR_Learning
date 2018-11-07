using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiXIPlayer : MonoBehaviour {
    [Range(0,10f)]
    public float upSpeed;
	
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        if(transform.localPosition.y<0)
        {
            transform.Translate(upSpeed*Vector3.up * Time.deltaTime);
        }
        
	}
}

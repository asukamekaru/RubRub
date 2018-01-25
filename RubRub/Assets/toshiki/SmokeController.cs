using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour {

    const float EfectFifeTime = 2.0f;
    float TimeCounter = 0.0f;
	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
        TimeCounter += Time.deltaTime; 
        if(TimeCounter >= EfectFifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}

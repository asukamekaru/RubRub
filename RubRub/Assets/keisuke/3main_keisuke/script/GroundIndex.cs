using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GroundC;
public class GroundIndex : MonoBehaviour {
    private int MaxGround = 3;
    public GameObject up;
    public GameObject down;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GroundC.GroundCount.Ground > MaxGround && 
            this.transform.parent == up.transform  &&
            this.transform.GetSiblingIndex() == 0      )
        {
            Debug.Log(this.name + " = " + this.transform.GetSiblingIndex() );
            //this.transform.parent = down.transform;
            this.gameObject.GetComponent<CubeControl2>().enabled = true;
        }
	}
}

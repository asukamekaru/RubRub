//=================================================
// GameMainScene <= 今立っている地面の名前を取得するスクリプト
// AsukaMekaru
// 2017/11/22
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollGroundName : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name + " CollGroundNames.cs");
        MainManager.sNowGround = collision.gameObject.name;
        MainManager.sNowGroundTag = collision.gameObject.tag;
    }

}

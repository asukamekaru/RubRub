using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollGroundName2 : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name + " CollGroundNames.cs");
        MainManager2.sNowGround = collision.gameObject.name;
        MainManager2.sNowGroundTag = collision.gameObject.tag;
    }
}

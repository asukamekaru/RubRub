using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallGround2 : MonoBehaviour {

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
        MainManager2.CreateGround = collision.gameObject.GetComponent<CubeControl2>();
        MainManager2.sCreateGroundName = collision.gameObject.name;
    }

}

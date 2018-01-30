//=================================================
// GameMainScene <= 画像を回転させるスクリプト
// AsukaMekaru
// 2017/01/16
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollimage : MonoBehaviour {

    [Header("回転速度")]
    [SerializeField]
    private float RollSpeed;

    MainManager mainmanager = GameObject.Find("MainManager").GetComponent<MainManager>();

    // Update is called once per frame
    void Update () {

        transform.Rotate(new Vector3(0, RollSpeed, 0));

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}

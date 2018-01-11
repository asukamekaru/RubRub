//=================================================
// GameMainScene <= 壁を作っていいかを確認するスクリプト
// AsukaMekaru
// 2017/11/23
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallGround : MonoBehaviour
{
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
        MainManager.CreateGround = collision.gameObject.GetComponent<CubeControl>();
        MainManager.enemyWarp = collision.gameObject.GetComponent<EnemyWarp>();
        MainManager.sCreateGroundName = collision.gameObject.name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarp : WallBase
{
    //string TagName = "Enemy";
    //string WallTagName = "EnemyWall";
    bool EnemyCollisionFlg;
    GameObject Enemy = null;

    void Update()
    {
        Debug.Log("WallType" + WallType);
        if (EnemyCollisionFlg)
        {
            EnemyWarp(this.gameObject, WallName[WallType - 1], Enemy);
        }
    }

    //オブジェクトが衝突したとき
    public void OnCollisionEnter(Collision collision)
    {
        if (this.tag == "EnemyWall")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy = collision.gameObject;
                EnemyCollisionFlg = true;
            }
        }

    }
}

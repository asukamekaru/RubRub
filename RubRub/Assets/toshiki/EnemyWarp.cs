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
        if (EnemyCollisionFlg)
        {
           
        }
    }

    //オブジェクトが衝突したとき
    public void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.tag == "EnemyWall")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy = collision.gameObject;
                EnemyCollisionFlg = true;
                EnemyWarp(this.gameObject, WallName[WallType - 1], collision.gameObject);
            }
        }

    }
}

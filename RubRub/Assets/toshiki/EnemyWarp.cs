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
        if(EnemyCollisionFlg)
        {
            if (NearObjectRetrieval(this.gameObject, TagName[WallType - 1]))//TagNameの要素が２つしかないため　WallType - 1
            {
                NearTriggerObject = WallType;
            }
            EnemyWarp(this.gameObject, WallName[WallType - 1], NearTriggerObject);
        }
    }

   
    //オブジェクトが衝突したとき
    public void OnCollisionEnter(Collision collision)
    {
        if (this.tag == "EnemyWall")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                EnemyCollisionFlg = true;
            }
        }

    }
}

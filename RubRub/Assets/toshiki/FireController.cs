using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    string TargetTagName = "FireWall";//= new string { "FireWall" };
    const float LifeLimit = 50.0f;
    public float LifeTimeCoun;

    void Update()
    {
        if (FindFireWall(this.gameObject, TargetTagName))
        {
            LifeTimeCoun += Time.deltaTime;
            if(LifeTimeCoun >= LifeLimit)
            {
                Destroy(this.gameObject);
            }
        }
    }

    //火の時間消失関数
    bool FindFireWall(GameObject nowObject, string tagName)
    {
        float ObjectNeardistance = 2.3f;    //近いの判定基準
        float tmpDistance = 0;           //距離用一時変数
        float nearDistance = 0;          //最も近いオブジェクトの距離
        GameObject targetObj = null; //オブジェクト
        bool TriggerObjectNear = false;     //戻り値

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDistance = Vector3.Distance(obs.transform.position, nowObject.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDistance == 0 || nearDistance > tmpDistance)
            {
                nearDistance = tmpDistance;
                targetObj = obs;
            }
        }

        if (nearDistance <= ObjectNeardistance && nearDistance != 0)
        {
            TriggerObjectNear = true;
        }
        //最も近かったオブジェクトを返す
        return TriggerObjectNear;
    }

}

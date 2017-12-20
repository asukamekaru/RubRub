using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class WallBase : MonoBehaviour
{

    public int WallType = -1;    //0、普通の壁　1、ファイアウォール　2、エネミーウォール
    public static int NearTriggerObject = -1; //近くにあるオブジェクト -1、なし　1、ファイア　2、エネミー
    public string[] TagName = new string[] { "Fire", "Enemy" };
    public string[] WallName = new string[] { "FireWall", "FireWall" };

    //各タイプごとにタグを変更
    public void WallTagChange(int Type, GameObject Wall)
    {
        switch (Type)
        {
            case 0:
                Wall.tag = "NormalWall";
                break;
            case 1:
                Wall.tag = "FireWall";
                break;
            case 2:
                Wall.tag = "EnemyWall";
                break;

        }
    }
    //指定されたタグの中で最も近いものを取得
    public bool NearObjectRetrieval(GameObject nowObj, string tagName)
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
            tmpDistance = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDistance == 0 || nearDistance > tmpDistance)
            {
                nearDistance = tmpDistance;
                targetObj = obs;
            }

        }
        if (tmpDistance <= ObjectNeardistance && tmpDistance != 0)
        {
            TriggerObjectNear = true;
        }
        Debug.Log("TriggerObjectNear " + TriggerObjectNear);
        //最も近かったオブジェクトを返す
        return TriggerObjectNear;
    }

    public void ObjectCreate(GameObject thisGameObject/*自身のオブジェクト*/, string TagName/*タグの名前*/
        , int ObjectCreateFlg/*近くにあるオブジェクト　-1 何もなし, 0　fire, 1 enemy*/)
    {
        CubeControl obsTriggerNear;//検索対象オブジェクトの<CubeControl>一時的保管場所
        int ObjectCount = 0;
        GameObject TriggerObject; //検索結果のオブジェクトを入れるやつ

        //foreach (GameObject obs in GameObject.FindGameObjectsWithTag(TagName))
        //{
        //    ObjectCount++;
        //    obsTriggerNear = obs.GetComponent<CubeControl>();
        //    if (obsTriggerNear.NearTriggerObject != -1)
        //    {
        //        TriggerObject = obs;
        //    }
        //}
        //検索結果が自分のオブジェクトではなく、同じタグのオブジェクトが2つ以上存在しており
        //自身の周りには対象のトリガーオブジェクトがない
        //if(thisGameObject != TriggerObject && ObjectCount >= 2 && ObjectCreateFlg == -1)
        //{
        //    //Instantiate(obj, thisGameObject.transform.position);
        //}
    }
}

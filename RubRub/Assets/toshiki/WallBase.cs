using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class WallBase : MonoBehaviour
{
    public int WallType = -1;    //0、普通の壁　1、ファイアウォール　2、エネミーウォール
    public int NearTriggerObject = -1; //近くにあるオブジェクト -1、なし　1、ファイア　2、エネミー
    public string[] TagName = new string[] { "Fire", "Enemy" };
    public string[] WallName = new string[] { "FireWall", "EnemyWall" };


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
        GameObject targetObj = null; //Debug用オブジェクト
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
        if (nearDistance <= ObjectNeardistance && nearDistance != 0)
        {
            TriggerObjectNear = true;
        }
        //近いオブジェクトがあるか否かを返す
        return TriggerObjectNear;
    }

    //インスタンスを生成
    public void ObjectCreate(GameObject thisGameObject/*自身のオブジェクト*/, string TagName/*タグの名前*/
        , int ObjectCreateFlg/*近くにあるオブジェクト　-1 何もなし, 0　fire, 1 enemy*/)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        //生成するインスタンスをPrefabで取得＆生成オブジェクトの角度を設定
        //壁が燃えている方のPrefab
        Quaternion BigFireRote = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        GameObject BigFirePrefab = (GameObject)Resources.Load("Prefab/BigFire");
        //プレイヤーの反対方向に噴出する方のPrefab
        Quaternion AttackFireRote = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        GameObject AttackFirePrefab = (GameObject)Resources.Load("Prefab/Fire");

        CubeControl obsTriggerNear;//検索対象オブジェクトの<CubeControl>一時的保管場所
        int ObjectCount = 0;
        GameObject TriggerObject = null; //検索結果のオブジェクトを入れるやつ

        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(TagName))
        {
            ObjectCount++;
            obsTriggerNear = obs.GetComponent<CubeControl>();
            if (obsTriggerNear.NearTriggerObject != -1)
            {
                TriggerObject = obs;
            }
        }

        //プレイヤーを参照し、敵に噴出する火の向きを設定する-------------------------------
        float DifferenceX;
        float DifferenceZ;
        //X軸、Z軸のプレイヤーと壁の座標の差を取得
        //差のより大きい方を優先とし向きを設定
        DifferenceX = Player.transform.position.x - thisGameObject.transform.position.x;
        DifferenceZ = Player.transform.position.z - thisGameObject.transform.position.z;
        //負の数を正の数へ変更
        if (DifferenceX < 0)
        {
            DifferenceX *= -1;
        }
        if (DifferenceZ < 0)
        {
            DifferenceZ *= -1;
        }

        if (DifferenceX < DifferenceZ)
        {
            if (Player.transform.position.z > thisGameObject.transform.position.z)
            {
                //180
                AttackFireRote = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
        }
        else if (DifferenceX > DifferenceZ)
        {
            if (Player.transform.position.x > thisGameObject.transform.position.x)
            {
                //270
                AttackFireRote = Quaternion.Euler(0.0f, 270.0f, 0.0f);
            }
            else
            {
                //90
                AttackFireRote = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }
        }
        else
        {
            Debug.Log("差が同じにより初期値０となります。");
        }
        //-----------------------------------------------------------------------------------

        //検索結果が自分のオブジェクトではなく、同じタグのオブジェクトが2つ以上存在しており
        //自身の周りには対象のトリガーオブジェクトがない
        if (thisGameObject != TriggerObject && ObjectCount >= 2 && ObjectCreateFlg == -1)
        {
            GameObject BigFire = (GameObject)Instantiate(BigFirePrefab, thisGameObject.transform.position, BigFireRote);
            GameObject Fire = (GameObject)Instantiate(AttackFirePrefab, thisGameObject.transform.position, AttackFireRote);
            BigFire.transform.parent = transform;
            Fire.transform.parent = transform;
        }
    }

    //当たった壁以外の壁を検索しエネミーをそこへワープさせる
    public void EnemyWarp(GameObject thisGameObject/*自身のオブジェクト*/, string TagName/*タグの名前*/,
        GameObject Enemy)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        CubeControl obsTriggerNear;//検索対象オブジェクトの<CubeControl>一時的保管場所
        GameObject TriggerObject = null; //検索結果のオブジェクトを入れるやつ
        
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(TagName))
        {
            if (obs != thisGameObject)
            {
                TriggerObject = obs;
            }
        }
        //検索結果が自分のオブジェクトではなく、同じタグのオブジェクトが2つ以上存在しており
        //自身の周りには対象のトリガーオブジェクトがない
        if (TriggerObject != null)
        {
            //ワープ先から近いエネミーを検索用変数
            float NearEnemyDistance = 3.0f;
            float tmpDistance = 0.0f;           //距離用一時変数
            float nearDistance = 0.0f;          //最も近いオブジェクトの距離
            GameObject TargetEnemy = null;

            //移動先の壁の周囲のエネミーを検索し、エネミーの位置により移動先を少しずらす（移動先の壁に接触しないように）
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                //自身と取得したオブジェクトの距離を取得
                tmpDistance = Vector3.Distance(obs.transform.position, TriggerObject.transform.position);

                //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                //一時変数に距離を格納
                if (nearDistance == 0.0f || nearDistance > tmpDistance)
                {
                    nearDistance = tmpDistance;
                    TargetEnemy = obs;
                }
            }
            if(TargetEnemy != null && nearDistance != 0.0f && nearDistance <= NearEnemyDistance)
            {
                float DifferenceX;
                float DifferenceZ;
                //壁との接触を避けるため少し移動ポイントをずらす
                float WapePoint = 1.5f;
                //X軸、Z軸のプレイヤーと壁の座標の差を取得
                //差のより大きい方を優先とし向きを設定
                DifferenceX = TargetEnemy.transform.position.x - TriggerObject.transform.position.x;
                DifferenceZ = TargetEnemy.transform.position.z - TriggerObject.transform.position.z;
                //負の数を正の数へ変更
                if (DifferenceX < 0)
                {
                    DifferenceX *= -1;
                }
                if (DifferenceZ < 0)
                {
                    DifferenceZ *= -1;
                }

                if (DifferenceX < DifferenceZ)
                {
                    if (Player.transform.position.z > TriggerObject.transform.position.z)
                    {
                        //Debug.Log("下");
                        //180
                        //Enemy.transform.position = new Vector3(6.0f,1.0f,7.0f);
                        Enemy.transform.position = new Vector3(TriggerObject.transform.position.x,
                            TriggerObject.transform.position.y, TriggerObject.transform.position.z - WapePoint);
                    }
                    else
                    {
                        //Debug.Log("上");
                        //Enemy.transform.position = new Vector3(6.0f, 1.0f, 7.0f);
                        Enemy.transform.position = new Vector3(TriggerObject.transform.position.x,
                            TriggerObject.transform.position.y, TriggerObject.transform.position.z + WapePoint);
                    }
                }
                else if (DifferenceX > DifferenceZ)
                {
                    if (Player.transform.position.x > TriggerObject.transform.position.x)
                    {
                        //Debug.Log("左");
                        //Enemy.transform.position = new Vector3(6.0f, 1.0f, 7.0f);
                        Enemy.transform.position = new Vector3(TriggerObject.transform.position.x - WapePoint,
                            TriggerObject.transform.position.y, TriggerObject.transform.position.z);
                    }
                    else
                    {
                        //Debug.Log("右");
                        //Enemy.transform.position = new Vector3(6.0f, 1.0f, 7.0f);
                        Enemy.transform.position = new Vector3(TriggerObject.transform.position.x + WapePoint,
                            TriggerObject.transform.position.y, TriggerObject.transform.position.z);
                    }
                }
                else
                {
                    Debug.Log("差が同じにより初期値０となります。");
                }
                //わーぷ
                Debug.Log(Enemy.transform.position);
            }
        }
    }
}

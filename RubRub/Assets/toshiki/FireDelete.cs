using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDelete : MonoBehaviour
{
    private ParticleSystem particle;
    private float DeleteTime = 0.0f;
    // Use this for initialization
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NearWallSearch(this.gameObject))
        {
            particle.Stop();
            DeleteTime += Time.deltaTime;
            if(DeleteTime >= 10.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    bool NearWallSearch(GameObject ThisGameObject)
    {
        float ObjectNeardistance = 0.5f;
        float tmpDistance = 0;           //距離用一時変数
        float nearDistance = 0;          //最も近いオブジェクトの距離
        int FireWallCount = 0;
        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag("FireWall"))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDistance = Vector3.Distance(obs.transform.position, ThisGameObject.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDistance == 0 || nearDistance >= tmpDistance)
            {
                nearDistance = tmpDistance;
            }
            FireWallCount++;
        }
        if(FireWallCount < 2 && nearDistance > 0.0f)
        {
            return true;
        }
        return false;
    }
}

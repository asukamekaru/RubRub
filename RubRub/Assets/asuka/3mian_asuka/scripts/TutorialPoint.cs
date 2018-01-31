//=================================================
// GameMainScene <= チュートリアルの画像を回転させるスクリプト
// AsukaMekaru
// 2017/01/22
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoint : MonoBehaviour
{


    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    [Header("回転速度")]
    [SerializeField]
    private float RollSpeed;

    [Header("プレイヤーオブジェクト")]
    [SerializeField]
    private GameObject Player;

    [Header("チュートリアルマネージャー")]
    [SerializeField]
    TutorialMgr tutorialmgr;
    ////////////////////////////////////// 変数 //////////////////////////////////////

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, RollSpeed, 0));//画像の回転

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialmgr.active();//当たればチュートリアルが表示される
        }
    }
}

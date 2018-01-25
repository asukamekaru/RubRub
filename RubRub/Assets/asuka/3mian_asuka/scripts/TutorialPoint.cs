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

    [Header("範囲")]
    [SerializeField]
    private float fRange;

    [Header("チュートリアルマネージャー")]
    [SerializeField]
    TutorialMgr tutorialmgr;
    ////////////////////////////////////// 変数 //////////////////////////////////////

    float fWallXMin, fWallXMax, fWallZMin, fWallZMax;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, RollSpeed, 0));//画像の回転

        if (Player.transform.position.x >= this.transform.position.x - fRange &&
                    Player.transform.position.x <= this.transform.position.x + fRange &&
                    Player.transform.position.y >= this.transform.position.y - fRange &&
                    Player.transform.position.y <= this.transform.position.y + fRange &&
                    Player.transform.position.z >= this.transform.position.z - fRange &&
                    Player.transform.position.z <= this.transform.position.z + fRange) tutorialmgr.active();

    }
}

//=================================================
// ホーム画面を管理するスクリプト
// AsukaMekaru
// 2017/12/25
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class homeManager : MonoBehaviour
{
    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    const int MAXSTAGE = 3; //最大ステージ

    selectUIScript selectUiScript;
    
    [Header("ステージを選ぶボタン")]
    public GameObject[] Stagebtn = new GameObject[MAXSTAGE];

    [Header("中央のボタンの位置")]
    public Vector3 CenterButtonPosi;
    [Header("中央以外のボタンの位置")]
    public Vector3 OtherButtonPosi;
    [Header("中央のボタンの大きさ")]
    public float CenterButtonSize;
    [Header("中央以外のボタンの大きさ")]
    public float OtherButtonSize;

    [Header("ボタンがスクロールする速さ")]
    public float ButtonScrollSpeed;


    ////////////////////////////////////// 変数 //////////////////////////////////////
    public int iNowSelectStage = 0;//ボタンを押したら飛ばされるステージの番号

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < MAXSTAGE; i++)
        {
            Stagebtn[i].gameObject.GetComponent<selectUIScript>().iStageNum = i;//ボタンに飛ぶステージの情報を渡す
            Stagebtn[i].gameObject.GetComponent<selectUIScript>().getMyNum(-i);//飛ばされるステージ番号の初期化
        }

    }

    // Update is called once per frame
    void Update()
    {



    }

    public void getControll(string s)
    {
        if (s == "right" && iNowSelectStage < MAXSTAGE - 1) ++iNowSelectStage;//右ボタンを押し、かつ最大ステージでなければ増やす

        if (s == "left" && iNowSelectStage > 0) --iNowSelectStage;//左ボタンを押し、かつ最大ステージでなければ減らす

        Debug.Log(iNowSelectStage);

        for (int i = 0; i < MAXSTAGE; i++)
        {
            Stagebtn[i].gameObject.GetComponent<selectUIScript>().getMyNum(iNowSelectStage - i);
        }
    }
}

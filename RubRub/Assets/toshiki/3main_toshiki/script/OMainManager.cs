//=================================================
// GameMainScene <= 関数とか状態を管理するスクリプト
// AsukaMekaru
// 2017/11/22
//=================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMainManager : MonoBehaviour
{

    public enum LAST_KEY { _KEY_RIGHT_, _KEY_LEFT_, _KEY_UP_, _KEY_DOWN_ };//ゲームの状態
    public static LAST_KEY LastKey;

    public enum STATUS { _GAME_PLAY_, _GAME_RUB_, _GAME_POSE_, _GAME_CLEAR_ };//ゲームの状態
    public static STATUS NowStatus;

    public static string sNowGround, sNowGroundTag, sCreateGroundName;//今立っている地面 - 今立っている地面のタグ - 作りたい場所の地面

    public static WallCreate CreateGround;
    public static MouseController RubRub;   //追加、撫でるスクリプト

    // Use this for initialization
    void Start()
    {
        ChangeStatus(STATUS._GAME_PLAY_);
    }

    // Update is called once per frame
    void Update()
    {
        switch (NowStatus)
        {
            case STATUS._GAME_PLAY_:
                break;

            case STATUS._GAME_RUB_:
                break;

            case STATUS._GAME_POSE_:
                break;

            case STATUS._GAME_CLEAR_:
                break;
        }
    }


    //============================================
    // 状態を変える時に通過する関数　
    //============================================
    public static void ChangeStatus(STATUS CHANGE)
    {
        switch (CHANGE)
        {
            case STATUS._GAME_PLAY_:
                Time.timeScale = 1;//時間をすすめる
                OMainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_RUB_:
                Time.timeScale = 0;//時間を止める
                OMainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_POSE_:
                Time.timeScale = 0;//時間を止める
                OMainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_CLEAR_:
                Time.timeScale = 1;//時間をすすめる
                OMainManager.NowStatus = CHANGE;
                break;
        }
    }

    //============================================
    // 壁が作れるかを判定する関数
    //============================================
    public static void IFCreateCall()
    {
        if (sNowGround != sCreateGroundName)//立っている地面と作りたい地面が別か？
        {
            Debug.Log("アクティブ");
            switch (LastKey)//最後に入力したキーと立っている地面のタグ（方向）が合理するか？
            {
                case LAST_KEY._KEY_RIGHT_:
                    if (sNowGroundTag == "Ground_Hori")
                    {
                        CreateGround.enabled = true;//壁になーれ
                    }
                    break;

                case LAST_KEY._KEY_LEFT_:
                    if (sNowGroundTag == "Ground_Hori")
                    {
                        CreateGround.enabled = true;//壁になーれ
                    }
                    break;

                case LAST_KEY._KEY_UP_:
                    if (sNowGroundTag == "Ground_Vert")
                    {
                        CreateGround.enabled = true;//壁になーれ
                    }
                    break;

                case LAST_KEY._KEY_DOWN_:
                    if (sNowGroundTag == "Ground_Vert")
                    {
                        CreateGround.enabled = true;//壁になーれ
                    }
                    break;
            }
        }
    }
}

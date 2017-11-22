//=================================================
// GameMainScene <= 関数とか状態を管理するスクリプト
// AsukaMekaru
// 2017/11/22
//=================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    public enum STATUS { _GAME_PLAY_, _GAME_RUB_, _GAME_POSE_, _GAME_CLEAR_ };//ゲームの状態
    public static STATUS NowStatus;

    // Use this for initialization
    void Start () {
        ChangeStatus(STATUS._GAME_PLAY_);
    }
	
	// Update is called once per frame
	void Update () {
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
    // 状態を変える関数　
    //============================================
    public static void ChangeStatus(STATUS CHANGE)
    {
        switch (CHANGE)
        {
            case STATUS._GAME_PLAY_:
                MainManager.NowStatus = CHANGE;
                Time.timeScale = 1;
                break;

            case STATUS._GAME_RUB_:
                Time.timeScale = 0;
                MainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_POSE_:
                Time.timeScale = 0;
                MainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_CLEAR_:
                Time.timeScale = 0;
                MainManager.NowStatus = CHANGE;
                break;
        }
    }
}

//=================================================
// GameMainScene <= 関数とか状態を管理するスクリプト
// AsukaMekaru
// 2017/11/22
//=================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MouseController;

public class MainManager : MonoBehaviour
{
    ////////////////////////////////////// 変数シンボル //////////////////////////////////////

    //時間
    [SerializeField]
    [Header("ゲームが始まるまでのインターバル")]
    private float fSTARTINTERVAL;
    [SerializeField]
    [Header("ゴールしてシーン以降するまでのインターバル")]
    private float fGOALINTERVAL;
    [SerializeField]
    [Header("死んでシーン以降するまでのインターバル")]
    private float fDEADINTERVAL;

    //数字
    [SerializeField]
    [Header("ゴールの範囲")]
    private int iClearPoint = 1;

    //スクリプト
    [Header("カメラスクリプト")]
    public cameraScript camerascript;
    [Header("プレイヤー死亡スクリプト")]
    public PlayerDead playerdead;

    //ポイント
    [SerializeField]
    [Header("スタートのポイント")]
    private GameObject StartPoint;
    [SerializeField]
    [Header("ゴールのポイント")]
    private GameObject GoalPoint;

    //オブジェクト
    [SerializeField]
    [Header("プレイヤーオブジェクト")]
    private GameObject Player;

    ////////////////////////////////////// 変数 //////////////////////////////////////
    public enum LAST_KEY { _KEY_RIGHT_, _KEY_LEFT_, _KEY_UP_, _KEY_DOWN_ };//ゲームの状態
    public static LAST_KEY LastKey;

    public enum STATUS { _GAME_START_, _GAME_PLAY_, _GAME_RUB_, _GAME_POSE_, _GAME_CLEAR_, _GAME_OVER_ };//ゲームの状態
    public static STATUS NowStatus;

    public static string sNowGround, sNowGroundTag, sCreateGroundName;//今立っている地面 - 今立っている地面のタグ - 作りたい場所の地面

    public static CubeControl CreateGround;
    public static EnemyWarp enemyWarp;

    public static float fCount;//停止中のタイマー

    // Use this for initialization
    void Start()
    {
        Player.transform.position = StartPoint.transform.position;
        ChangeStatus(STATUS._GAME_START_);
    }

    // Update is called once per frame
    void Update()
    {
        switch (NowStatus)
        {
            case STATUS._GAME_START_:
                if (++fCount > fSTARTINTERVAL) ChangeStatus(STATUS._GAME_PLAY_);//fSTARTTIMERミリ秒後スタート
                break;

            case STATUS._GAME_PLAY_:
                //ゴール範囲に入ればクリア
                if (Player.transform.position.x >= GoalPoint.transform.position.x - iClearPoint &&
                    Player.transform.position.x <= GoalPoint.transform.position.x + iClearPoint &&
                    Player.transform.position.y >= GoalPoint.transform.position.y - iClearPoint &&
                    Player.transform.position.y <= GoalPoint.transform.position.y + iClearPoint &&
                    Player.transform.position.z >= GoalPoint.transform.position.z - iClearPoint &&
                    Player.transform.position.z <= GoalPoint.transform.position.z + iClearPoint) ChangeStatus(STATUS._GAME_CLEAR_);

                break;

            case STATUS._GAME_RUB_:
                break;

            case STATUS._GAME_POSE_:
                break;

            case STATUS._GAME_CLEAR_:
                if (++fCount > fGOALINTERVAL) ChangeScene("GameClear", 1);
                break;

            case STATUS._GAME_OVER_:

                camerascript.UPCAMERA(1);//カメラズームイン
                if (playerdead.DEAD() && ++fCount > fDEADINTERVAL)//死んだアニメーションが流され、指定の時間に到達した時シーンを以降させる
                {
                    ChangeScene("GameOver", 1);//シーンを変える
                }
                break;
        }
    }

    //===============================================================================================
    // シーンを変える時に通過する関数　(シーン名と時間の流れを指定 1 = 時間を進める 2 = 時間を止める)
    //===============================================================================================
    public void ChangeScene(string SceneName,int time)
    {
        Time.timeScale = time;
        SceneManager.LoadScene(SceneName);
    }

    //============================================
    // 状態を変える時に通過する関数　
    //============================================
    public static void ChangeStatus(STATUS CHANGE)
    {
        switch (CHANGE)
        {
            case STATUS._GAME_START_:
                fCount =
                Time.timeScale = 0;//時間をとめる
                MainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_PLAY_:
                Time.timeScale = 1;//時間をすすめる
                MainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_RUB_:
                Time.timeScale = 0;//時間を止める
                MainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_POSE_:
                Time.timeScale = 0;//時間を止める
                MainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_CLEAR_:
                fCount =
                Time.timeScale = 0;//時間を止める
                MainManager.NowStatus = CHANGE;
                break;

            case STATUS._GAME_OVER_:
                Time.timeScale = 1;//時間を止める
                MainManager.NowStatus = CHANGE;
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
            switch (LastKey)//最後に入力したキーと立っている地面のタグ（方向）が合理するか？
            {
                case LAST_KEY._KEY_RIGHT_:
                    if (sNowGroundTag == "Ground_Hori")
                    {
                        CreateGround.enabled = true;//壁になーれ
                        for (int i = 0; i < 3; i++)
                        {
                            if (MouseController.MouseController.WallType[i] == 1)
                            {
                                CreateGround.WallType = i;
                                enemyWarp.WallType = i;
                            }
                        }
                    }
                    break;

                case LAST_KEY._KEY_LEFT_:
                    if (sNowGroundTag == "Ground_Hori")
                    {
                        CreateGround.enabled = true;//壁になーれ
                        for (int i = 0; i < 3; i++)
                        {
                            if (MouseController.MouseController.WallType[i] == 1)
                            {
                                CreateGround.WallType = i;
                                enemyWarp.WallType = i;
                            }
                        }
                    }
                    break;

                case LAST_KEY._KEY_UP_:
                    if (sNowGroundTag == "Ground_Vert")
                    {
                        CreateGround.enabled = true;//壁になーれ
                        for (int i = 0; i < 3; i++)
                        {
                            if (MouseController.MouseController.WallType[i] == 1)
                            {
                                CreateGround.WallType = i;
                                enemyWarp.WallType = i;
                            }
                        }
                    }
                    break;

                case LAST_KEY._KEY_DOWN_:
                    if (sNowGroundTag == "Ground_Vert")
                    {
                        CreateGround.enabled = true;//壁になーれ
                        for (int i = 0; i < 3; i++)
                        {
                            if (MouseController.MouseController.WallType[i] == 1)
                            {
                                CreateGround.WallType = i;
                                enemyWarp.WallType = i;
                            }
                        }
                    }
                    break;
            }
        }
    }
}

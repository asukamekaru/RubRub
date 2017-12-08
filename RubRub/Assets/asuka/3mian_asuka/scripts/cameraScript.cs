//=================================================
// GameMainScene <= カメラの管理スクリプト
// AsukaMekaru
// 2017/12/07
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{

    public GameObject player;       //プレイヤーゲームオブジェクトへの参照を格納する Public 変数
    private Vector3 offset;         //プレイヤーとカメラ間のオフセット距離を格納する Public 変数

    private const int CAMERA_SCALE_INIT = 60;//カメラの初期の拡大率
    private const int CAMERA_SCALE_DEAD = 20;//カメラのプレイヤーが死んだときの拡大率

    static int iCameraView;    //カメラの拡大の数字

    // イニシャライゼーションに使用ます。
    void Start()
    {
        //プレイヤーとカメラ間の距離を取得してそのオフセット値を計算し、格納します。
        offset = transform.position - player.transform.position;
        //カメラの拡大初期化
        iCameraView = CAMERA_SCALE_INIT;
    }

    public static void UPCAMERA(int i)
    {
        if (iCameraView != CAMERA_SCALE_DEAD)
        {
            iCameraView -= i;
            Camera.main.fieldOfView = iCameraView;
        }
        else
        {
            MainManager.NowStatus = MainManager.STATUS._GAME_OVER_;
        }
        Debug.Log(iCameraView);


    }

    // 各フレームで、Update の後に LateUpdate が呼び出されます。
    void LateUpdate()
    {
        //カメラの transform 位置をプレイヤーのものと等しく設定します。ただし、計算されたオフセット距離によるずれも加えます。
        transform.position = player.transform.position + offset;
    }
}


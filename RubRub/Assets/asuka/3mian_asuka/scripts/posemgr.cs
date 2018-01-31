//=================================================
// GameMainScene <= ポーズの管理スクリプト
// AsukaMekaru
// 2017/01/25
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class posemgr : MonoBehaviour {

    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    [SerializeField]
    private MainManager mainmanager;

    [Header("ポーズの際に表示されるオブジェクトの親")]
    [SerializeField]
    private GameObject posePanel;
    [Header("各ボタン")]
    [SerializeField]
    private Button posebtn;
    [SerializeField]
    private Button poseHomeBtn;
    [SerializeField]
    private Button posePlayBtn;
    [SerializeField]
    private Button poseRetryBbtn;

    soundManager soundmanager;

    ////////////////////////////////////// 変数 //////////////////////////////////////

    // Use this for initialization
    void Start ()
    {
        posePanel.gameObject.SetActive(false);

        posebtn.gameObject.GetComponent<Button>().onClick.AddListener(OnPoseBtn);
        poseHomeBtn.gameObject.GetComponent<Button>().onClick.AddListener(OnPoseHomeBtn);
        posePlayBtn.gameObject.GetComponent<Button>().onClick.AddListener(OnPosePlayBtn);
        poseRetryBbtn.gameObject.GetComponent<Button>().onClick.AddListener(OnPoseRetryBtn);

        soundmanager = GameObject.Find("SoundManager").GetComponent<soundManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // *** 通常プレイ時からポーズに移る際の処理

    //ポーズボタンの処理
    void OnPoseBtn() { 
        MainManager.ChangeStatus(MainManager.STATUS._GAME_POSE_); 
        posePanel.gameObject.SetActive(true); /*ステータスをポーズにする*/
        soundmanager.PlaySound(11, false);//システムサウンドを鳴らす
    }


    // *** 以下ポーズ中の各ボタンの処理

    //ポーズ中のホームボタンの処理
    void OnPoseHomeBtn() { 
        mainmanager.ChangeScene("HomeScene",1); /*ホームシーンへ移動*/
        soundmanager.PlaySound(12, false);//システムキャンセルサウンドを鳴らす
    }
    //ポーズ中のプレイボタンの処理
    void OnPosePlayBtn() { 
        MainManager.ChangeStatus(MainManager.STATUS._GAME_PLAY_); 
        posePanel.gameObject.SetActive(false); /*ステータスをプレイに戻す*/
        soundmanager.PlaySound(12, false);//システムキャンセルサウンドを鳴らす
    }
    //ポーズ中のリトライボタンの処理
    void OnPoseRetryBtn() { 
        mainmanager.ChangeScene("GameMainScene", 1); /*ゲームメインシーンの再読込*/
        soundmanager.PlaySound(12, false);//システムキャンセルサウンドを鳴らす
    }
}

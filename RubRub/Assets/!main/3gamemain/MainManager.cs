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
    [SerializeField]
    [Header("スタートエンドのUIが飛び出す速さ")]
    private float fSEUISPEED;

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
    [SerializeField]
    [Header("ブラックアウトするパネル")]
    private GameObject BlackOutPanel;

    ////////////////////////////////////// 変数 //////////////////////////////////////
    public enum LAST_KEY { _KEY_RIGHT_, _KEY_LEFT_, _KEY_UP_, _KEY_DOWN_ };//ゲームの状態
    public static LAST_KEY LastKey;

    public enum STATUS { _GAME_START_, _GAME_PLAY_, _GAME_RUB_, _GAME_POSE_, _GAME_CLEAR_, _GAME_OVER_ };//ゲームの状態
    public static STATUS NowStatus;

    public enum BLACKOUT_COLOR { _BLACK_, _WHITE_ = 255 };//ブラックアウト時の色

    public static string sNowGround, sNowGroundTag, sCreateGroundName;//今立っている地面 - 今立っている地面のタグ - 作りたい場所の地面

    public static CubeControl2 CreateGround;
    public static EnemyWarp enemyWarp;

    Player_Wall_Ray PWR;
    CubeControl2 cube;
    StartEndUIScript SEUS;
    soundManager soundmanager;

    public static float fCount;//停止中のタイマー

    // Rayが衝突したコライダーの情報を得る
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        soundmanager = GameObject.Find("SoundManager").GetComponent<soundManager>();
        SEUS = GameObject.Find("StartEndUIImg").GetComponent<StartEndUIScript>();
        PWR = GameObject.Find("Yuko_sum_humanoid").GetComponent<Player_Wall_Ray>();
        Player.transform.position = StartPoint.transform.position;
        ChangeStatus(STATUS._GAME_START_);
    }

    // Update is called once per frame
    void Update()
    {
        switch (NowStatus)
        {
            case STATUS._GAME_START_:

                soundmanager.ChangeBgm(4);
                BlackOutPanel.gameObject.SetActive(true);//パネルを出すついでに操作できなくする

                if (BlackOutPanel.gameObject.GetComponent<BlackOut>().GameBlackOut((int)BLACKOUT_COLOR._BLACK_, "start"))
                {
                    if (SEUS.ScaleChange("START", fSTARTINTERVAL, fSEUISPEED, false))
                    {
                        BlackOutPanel.gameObject.SetActive(false);//パネルを消す
                        ChangeStatus(STATUS._GAME_PLAY_);//fSTARTTIMERミリ秒後スタート
                    }
                }
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

                BlackOutPanel.gameObject.SetActive(true);//パネルを出すついでに操作できなくする

                if (SEUS.ScaleChange("GOAL", fGOALINTERVAL, fSEUISPEED, true))
                {
                    if (BlackOutPanel.gameObject.GetComponent<BlackOut>().GameBlackOut((int)BLACKOUT_COLOR._WHITE_, "end")) ChangeScene("GameClear", 1);
                }
                break;

            case STATUS._GAME_OVER_:

                BlackOutPanel.gameObject.SetActive(true);//パネルを出すついでに操作できなくする

                camerascript.UPCAMERA(1);//カメラズームイン

                if (playerdead.DEAD() && SEUS.ScaleChange("OVER", fDEADINTERVAL, fSEUISPEED, true))//死んだアニメーションが流され、指定の時間に到達した時シーンを以降させる
                {
                    if (BlackOutPanel.gameObject.GetComponent<BlackOut>().GameBlackOut((int)BLACKOUT_COLOR._BLACK_, "end")) ChangeScene("GameOver", 1);//シーンを変える
                }
                break;
        }
    }
    
    //===============================================================================================
    // シーンを変える時に通過する関数　(シーン名と時間の流れを指定 1 = 時間を進める 2 = 時間を止める)
    //===============================================================================================
    public void ChangeScene(string SceneName, int time)
    {
        Time.timeScale = time;
        soundmanager.StopBgm();
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
    public void IFCreateCall()
    {
        // Rayが衝突したかどうか
        if (!Physics.Raycast(PWR.ray, out hit, PWR.RayLength))
        {
            //Instantiate( 生成するオブジェクト,  場所, 回転 );  回転はそのままなら↓
            if (MainManager.LastKey == MainManager.LAST_KEY._KEY_UP_ || MainManager.LastKey == MainManager.LAST_KEY._KEY_DOWN_)
            {
                Instantiate(PWR.wall,
                            new Vector3(Mathf.RoundToInt(PWR.Point.transform.position.x),
                                        this.transform.position.y - 2,
                                        PWR.Point.transform.position.z),
                            Quaternion.identity);
            }

            if (MainManager.LastKey == MainManager.LAST_KEY._KEY_LEFT_ || MainManager.LastKey == MainManager.LAST_KEY._KEY_RIGHT_)
            {
                Instantiate(PWR.wall,
                            new Vector3(PWR.Point.transform.position.x,
                                        this.transform.position.y - 2,
                                        Mathf.RoundToInt(PWR.Point.transform.position.z)),
                            Quaternion.identity);
            }
        }

        /*if (sNowGround != sCreateGroundName)//立っている地面と作りたい地面が別か？
        {
            switch (LastKey)//最後に入力したキーと立っている地面のタグ（方向）が合理するか？
            {
                case LAST_KEY._KEY_RIGHT_:
                    if (sNowGroundTag == "Ground_Hori")
                    {
                        CreateGround.enabled = true;//壁になーれ
                        CreateGround.MoveEnd = false;
                        CreateGround.endPosition = new Vector3(CreateGround.transform.position.x, 1, CreateGround.transform.position.z);
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
                        CreateGround.MoveEnd = false;
                        CreateGround.endPosition = new Vector3(CreateGround.transform.position.x, 1, CreateGround.transform.position.z);
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
                        CreateGround.MoveEnd = false;
                        CreateGround.endPosition = new Vector3(CreateGround.transform.position.x, 1, CreateGround.transform.position.z);
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
                        CreateGround.MoveEnd = false;
                        CreateGround.endPosition = new Vector3(CreateGround.transform.position.x, 1, CreateGround.transform.position.z);
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
        }*/
    }

    //============================================
    // 壁が消せるかを判定する関数
    //============================================
    public void IFDeleteCall()
    {
        // Rayが衝突したかどうか
        if (Physics.Raycast(PWR.ray, out hit, PWR.RayLength))
        {
            //衝突してそれがすでに生成された壁なら
            if (Physics.Raycast(PWR.ray, out hit, PWR.RayLength, PWR.visibleLayer))
            {
                //Destroy(hit.collider.gameObject);
                cube = hit.collider.GetComponent<CubeControl2>();
                cube.enabled = true;
                cube.MoveEnd = false;
                cube.WallType = 0;

            }

        }
        /*if (sNowGround != sCreateGroundName)//立っている地面と作りたい地面が別か？
        {
            switch (LastKey)//最後に入力したキーと立っている地面のタグ（方向）が合理するか？
            {
                case LAST_KEY._KEY_RIGHT_:
                    if (sNowGroundTag == "Ground_Hori")
                    {
                        //すべての要素を消す
                        CreateGround.enabled = true;//壁になーれ
                        CreateGround.MoveEnd = false;
                        CreateGround.WallType = 0;
                        enemyWarp.WallType = 0;
                        CreateGround.endPosition = new Vector3(CreateGround.transform.position.x, -1, CreateGround.transform.position.z);
                    }
                    break;

                case LAST_KEY._KEY_LEFT_:
                    if (sNowGroundTag == "Ground_Hori")
                    {
                        //すべての要素を消す
                        CreateGround.enabled = true;//壁になーれ
                        CreateGround.MoveEnd = false;
                        CreateGround.WallType = 0;
                        enemyWarp.WallType = 0;
                        CreateGround.endPosition = new Vector3(CreateGround.transform.position.x, -1, CreateGround.transform.position.z);
                    }
                    break;

                case LAST_KEY._KEY_UP_:
                    if (sNowGroundTag == "Ground_Vert")
                    {
                        //すべての要素を消す
                        CreateGround.enabled = true;//壁になーれ
                        CreateGround.MoveEnd = false;
                        CreateGround.WallType = 0;
                        enemyWarp.WallType = 0;
                        CreateGround.endPosition = new Vector3(CreateGround.transform.position.x, -1, CreateGround.transform.position.z);
                    }
                    break;

                case LAST_KEY._KEY_DOWN_:
                    if (sNowGroundTag == "Ground_Vert")
                    {
                        //すべての要素を消す
                        CreateGround.enabled = true;//壁になーれ
                        CreateGround.MoveEnd = false;
                        CreateGround.WallType = 0;
                        enemyWarp.WallType = 0;
                        CreateGround.endPosition = new Vector3(CreateGround.transform.position.x, -1, CreateGround.transform.position.z);
                    }
                    break;
            }*/
    }
}

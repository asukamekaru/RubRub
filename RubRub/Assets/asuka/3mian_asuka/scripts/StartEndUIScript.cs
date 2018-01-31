//=================================================
// GameMainScene <= スタート、エンド時に表示されるUIを管理するスクリプト
// AsukaMekaru
// 2018/01/29
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEndUIScript : MonoBehaviour
{

    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    private enum SEUI_STATUS { _PANEL_START_, _PANEL_SCALEUP_, _PANEL_FIRST_INTERVAL_, _PANEL_SCALEDOWN_, _PANEL_END_ };//ゲームの状態
    private SEUI_STATUS seuistatus;

    soundManager soundmanager;

    [Header("UI画像START")]
    [SerializeField]
    private Sprite START;
    [Header("UI画像GOAL")]
    [SerializeField]
    private Sprite GOAL;
    [Header("UI画像OVER")]
    [SerializeField]
    private Sprite OVER;

    const int MAXSCALE = 1;//画像の最大値
    const int MINSCALE = 0;//画像の最小値

    ////////////////////////////////////// 変数 //////////////////////////////////////

    float fNowScale;//今のスケール
    int iIntervalTime;//インターバルの時間
    bool bStartFlg, bGoalFlg, bOverFlg;//一度だけ通るようにさせるフラグ

    // Use this for initialization
    void Start()
    {
         bStartFlg = bGoalFlg = bOverFlg = true;
        seuistatus = SEUI_STATUS._PANEL_START_;
        iIntervalTime = 0;//インターバルの時間を初期化
        fNowScale = MINSCALE;//最小値を設定↓
        GetComponent<RectTransform>().localScale = new Vector2(fNowScale, fNowScale);//自身のオブジェクトに当てる
        soundmanager = GameObject.Find("SoundManager").GetComponent<soundManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //UIを表示させる
    public bool ScaleChange(string type, float time, float speed)//使う画像 - インターバル - 速さ
    {
        switch (seuistatus)
        {
            case SEUI_STATUS._PANEL_START_:

                if (type == "START" && bStartFlg)
                {
                    bStartFlg = false;//一度だけ実行させるため
                    soundmanager.PlaySound(8, true);//ゲームスタートSE
                    this.gameObject.GetComponent<Image>().sprite = START;//画像を変える
                    seuistatus = SEUI_STATUS._PANEL_SCALEUP_;//移る
                }
                else if (type == "GOAL" && bGoalFlg)
                {
                    bGoalFlg = false;//一度だけ実行させるため
                    soundmanager.PlaySound(9, true);//ゲームコンプリートSE
                    this.gameObject.GetComponent<Image>().sprite = GOAL;//画像を変える
                    seuistatus = SEUI_STATUS._PANEL_SCALEUP_;//移る
                }
                else if (type == "OVER" && bOverFlg)
                {
                    bOverFlg = false;//一度だけ実行させるため
                    soundmanager.PlaySound(10, true);//ゲームオーバーSE
                    this.gameObject.GetComponent<Image>().sprite = OVER;//画像を変える
                    seuistatus = SEUI_STATUS._PANEL_SCALEUP_;//移る
                }
                else { return true; }
                break;

            case SEUI_STATUS._PANEL_SCALEUP_://拡大させる
                if (fNowScale < MAXSCALE)//最大値以下なら↓
                {
                    fNowScale += speed;//拡大
                }
                else
                {
                    fNowScale = MAXSCALE;//サイズ修正
                    seuistatus = SEUI_STATUS._PANEL_FIRST_INTERVAL_;//インターバルを挟む
                }
                Debug.Log("b");
                break;
            case SEUI_STATUS._PANEL_FIRST_INTERVAL_:

                //スタート以外ならスケールダウンさせない
                if (type == "START")
                {
                    //インターバル中 インターバルの時間を経過すればスケールを縮小させる
                    if (++iIntervalTime >= time) seuistatus = SEUI_STATUS._PANEL_SCALEDOWN_;
                }
                else
                {
                    seuistatus = SEUI_STATUS._PANEL_END_;
                }
                break;

            case SEUI_STATUS._PANEL_SCALEDOWN_://縮小させる
                if (fNowScale > MAXSCALE)//最小値以下なら↓
                {
                    fNowScale -= speed;//縮小
                }
                else//最小値以上になれば↓
                {
                    fNowScale = MINSCALE;//サイズ修正
                    seuistatus = SEUI_STATUS._PANEL_END_;//終わらせる準備に入る
                }
                break;

            case SEUI_STATUS._PANEL_END_:

                seuistatus = SEUI_STATUS._PANEL_START_;//ステータスの初期化
                iIntervalTime = 0;//インターバルの時間の初期化
                return true;//終わり
        }
        GetComponent<RectTransform>().localScale = new Vector2(fNowScale, fNowScale);//自身のオブジェクトに当てる
        return false;
    }
}

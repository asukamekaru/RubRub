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

    // Use this for initialization
    void Start()
    {
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
    public bool ScaleChange(string imagetype, float time, float speed, bool flag)//使う画像 - インターバル - 速さ - 途中で終わるかのフラグ
    {
        switch (seuistatus)
        {
            case SEUI_STATUS._PANEL_START_:
                switch (imagetype)//使う画像を変える
                {
                    case "START":
                        this.gameObject.GetComponent<Image>().sprite = START;
                        soundmanager.PlaySound(8,false);//ゲームスタートSE
                        break;
                    case "GOAL":
                        this.gameObject.GetComponent<Image>().sprite = GOAL;
                        soundmanager.PlaySound(9, false);//ゲームコンプリートSE
                        break;
                    case "OVER":
                        this.gameObject.GetComponent<Image>().sprite = OVER;
                        soundmanager.PlaySound(10, false);//ゲームオーバーSE
                        break;
                }
                seuistatus = SEUI_STATUS._PANEL_SCALEUP_;//移る
                Debug.Log("a");
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

                if (!flag)
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

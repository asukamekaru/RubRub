//=================================================
// GameMainScene <= チュートリアルマネージャー
// AsukaMekaru
// 2017/11/23
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMgr : MonoBehaviour {

    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    const int MAXHINT = 6;

    [Header("各ポイントのオブジェクト")]
    [SerializeField]
    private GameObject[] _HintObject = new GameObject[MAXHINT];
    [Header("各ポイントのヒント画像")]
    public Sprite[] _HintImage = new Sprite[MAXHINT];
    [Header("各ポイントのパネル")]
    [SerializeField]
    private GameObject TutorialPanel;

    ////////////////////////////////////// 変数 //////////////////////////////////////
    [HideInInspector]
    public int iNowHint;//今のヒント番号

    // Use this for initialization
    void Start () {
        //最初のやつだけアクティブにする
        _HintObject[0].gameObject.SetActive(true);
        //それ以外を非アクティブにする
        for(int i = 1;i < MAXHINT;i++) _HintObject[i].gameObject.SetActive(false);

    }

    //範囲に入ればチュートリアルの説明パネルをアクティブにして時間を止める
    public void active(){ TutorialPanel.gameObject.SetActive(true); Time.timeScale = 0; }
    public void inactive() {
        //最大値以下なら次のヒントへ移る
        if (iNowHint < MAXHINT) iNowHint++;
        //チュートリアルパネルを非アクティブにする
        TutorialPanel.gameObject.SetActive(false);
        //今表示されているヒントオブジェクトを非アクティブにする
        _HintObject[iNowHint - 1].gameObject.SetActive(false);
        //最大じゃなければ次のヒントオブジェクトをアクティブにする
        if (iNowHint != MAXHINT)
        {
            _HintObject[iNowHint].gameObject.SetActive(true);
        }
        //最大値ならすべてのヒントオブジェクトを非アクティブにする
        else
        {
            _HintObject[iNowHint - 1].gameObject.SetActive(false);
        }
        //時間をすすめる
        Time.timeScale = 1;
    }
}

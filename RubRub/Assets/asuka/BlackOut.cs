//=================================================
// GameMainScene <= ブラックアウトさせるスクリプト
// AsukaMekaru
// 2017/01/17
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour {

    private GameObject BlackOutImage;


    public float pf_Step;    // alpah増減値(点滅スピード調整)
    public float waittime;    // 完全に黒くなってからの待ち時間

    void Start()
    {
        //オブジェクト読み込み
        this.BlackOutImage = GameObject.Find("BlackOutImage");
    }

    public bool GameBlackOut(int color,string things)
    {
        // 現在のAlpha値を取得
        float f_toColor = this.BlackOutImage.GetComponent<Image>().color.a;

        if (things == "start" && f_toColor >= -waittime)
        {
            pf_Step = pf_Step * 1;
            this.BlackOutImage.GetComponent<Image>().color = new Color(color, color, color, f_toColor - pf_Step);
        }
        else if (things == "end" && f_toColor <= waittime)
        {
            pf_Step = pf_Step * 1;
            this.BlackOutImage.GetComponent<Image>().color = new Color(color, color, color, f_toColor + pf_Step);
        }
        else
        {
            return true;
        }

        Debug.Log(f_toColor);

        return false;
    }
}
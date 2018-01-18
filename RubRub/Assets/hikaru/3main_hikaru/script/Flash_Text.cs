using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
 
// オブジェクトを点滅させる
public class Flash_Text : MonoBehaviour {
 
    private GameObject p_FlashImage;

    private float pf_Step = 0.0045f;    // alpah増減値(点滅スピード調整)
    static float sf_AlpahMax = 1.0f;    // alpahの最大値
    static float sf_AlpahMin = 0.2f;       // alpahの最小値
    
    void Start()
    {
        //オブジェクト読み込み
        this.p_FlashImage = GameObject.Find("Flash_Text");
    }
 
    void Update()
    {
        // 現在のAlpha値を取得
        float f_toColor = this.p_FlashImage.GetComponent<Image>().color.a;
        // Alphaが最小値 または 最大値になったら増減値を反転
        if (f_toColor < sf_AlpahMin || f_toColor > sf_AlpahMax)
        {
            pf_Step = pf_Step * -1;     //*-1で反転
        }
        // Alpha値を増減させてセット
        // 色変更もここで反映
        this.p_FlashImage.GetComponent<Image>().color = new Color(255, 255, 255, f_toColor + pf_Step);
    }
}
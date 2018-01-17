//=================================================
// GameHomeScene <= ステージセレクトボタンのスクリプト
// AsukaMekaru
// 2017/11/23
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectUIScript : MonoBehaviour
{
    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    [SerializeField]
    private homeManager homemanager;

    ////////////////////////////////////// 変数 //////////////////////////////////////
    //bool bBtnAnime;

    private Vector3 getVec;//取ってきた位置を保存するもの
    private float getSize;//取ってきたサイズを保存するもの

    [SerializeField]
    private string nextScene;

    [HideInInspector]
    public int iStageNum;//ステージ番号

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClickBtn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getMyNum(int get)//自分の位置を取得
    {
        //位置を保存
        getVec.x = (get == 0) ? homemanager.CenterButtonPosi.x : -(get * homemanager.OtherButtonPosi.x);
        getVec.y = (get == 0) ? homemanager.CenterButtonPosi.y : homemanager.OtherButtonPosi.y;
        getVec.z = (get == 0) ? homemanager.CenterButtonPosi.z : changeSign((get * homemanager.OtherButtonPosi.z));

        //サイズを保存
        this.transform.GetComponent<RectTransform>().sizeDelta = (get == 0) ?
            new Vector2(homemanager.CenterButtonSize, homemanager.CenterButtonSize) :
            new Vector2(homemanager.OtherButtonSize, homemanager.OtherButtonSize);

        this.transform.localPosition = getVec;
    }

    private void ClickBtn() { if (nextScene == "NULL") return; SceneManager.LoadScene(nextScene); }//ボタンがクリックされたときの処理

    private float changeSign(float f) { if (f < 0) f *= -1.0f; return f; }//強制的に符号をプラスに変える

}

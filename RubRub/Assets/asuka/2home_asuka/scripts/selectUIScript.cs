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

    public Vector3 myVec;//自分の座標
    public float mySize;//自分のサイズ
    private Vector3 getVec;//取ってきた位置を保存するもの
    private float getSize;//取ってきたサイズを保存するもの

    [SerializeField]
    private string nextScene;

    [HideInInspector]
    public int iStageNum;//ステージ番号

    // Use this for initialization
    void Start()
    {
        mySize = this.transform.GetComponent<RectTransform>().sizeDelta.x;
        myVec = this.transform.localPosition;
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClickBtn);
    }

    // Update is called once per frame
    void Update()
    {
        //ボタン移動のアニメーション

        //位置X
        if (myVec.x >= getVec.x)
        {
            myVec.x -= homemanager.ButtonScrollSpeed;
            if (myVec.x <= getVec.x) myVec.x = getVec.x;
        }
        else if (myVec.x <= getVec.x)
        {
            myVec.x += homemanager.ButtonScrollSpeed;
            if (myVec.x >= getVec.x) myVec.x = getVec.x;
        }

        //位置Z
        if (myVec.z >= getVec.z)
        {
            myVec.z -= homemanager.ButtonScrollSpeed;
            if (myVec.z <= getVec.z) myVec.z = getVec.z;
        }
        else if (myVec.z <= getVec.z)
        {
            myVec.z += homemanager.ButtonScrollSpeed;
            if (myVec.z >= getVec.z) myVec.z = getVec.z;
        }

        //サイズ
        if (mySize >= getSize)
        {
            mySize -= homemanager.ButtonScrollSpeed;
            if (mySize <= getSize) mySize = getSize;
        }
        else if (mySize <= getSize)
        {
            mySize += homemanager.ButtonScrollSpeed;
            if (mySize >= getSize) mySize = getSize;
        }

        //自分に当てる
        this.transform.localPosition = myVec;
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(mySize, mySize);
    }

    public void getMyNum(int get)//自分の位置を取得
    {
        //位置を保存
        getVec.x = (get == 0) ? homemanager.CenterButtonPosi.x : -(get * homemanager.OtherButtonPosi.x);
        getVec.y = (get == 0) ? homemanager.CenterButtonPosi.y : homemanager.OtherButtonPosi.y;
        getVec.z = (get == 0) ? homemanager.CenterButtonPosi.z : changeSign((get * homemanager.OtherButtonPosi.z));

        //サイズを保存
        getSize = (get == 0) ? homemanager.CenterButtonSize : homemanager.OtherButtonSize;

    }

    private void ClickBtn() { if (nextScene == "NULL") return; SceneManager.LoadScene(nextScene); }//ボタンがクリックされたときの処理

    private float changeSign(float f) { if (f < 0) f *= -1.0f; return f; }//強制的に符号をプラスに変える

}

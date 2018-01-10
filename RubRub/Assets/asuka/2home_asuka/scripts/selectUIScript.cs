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
    [SerializeField]
    private homeManager homemanager;

    bool bBtnAnime;

    Vector3 setvec;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(ClickBtn);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = setvec;
    }

    private void ClickBtn() { }//ボタンがクリックされたときの処理

    public void getMyNum(int get)//自分の位置を取得
    {
        setvec.x = (get == 0) ? homemanager.CenterButtonPosi.x : homemanager.OtherButtonPosi.x - (get * homemanager.OtherButtonPosi.x);
        setvec.y = (get == 0) ? homemanager.CenterButtonPosi.y : homemanager.OtherButtonPosi.y;
        setvec.z = (get == 0) ? homemanager.CenterButtonPosi.z : homemanager.OtherButtonPosi.z;
    }
}

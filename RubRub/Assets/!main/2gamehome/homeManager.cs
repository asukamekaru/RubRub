using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class homeManager : MonoBehaviour
{
    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    selectUIScript selectUiScript;

    readonly int MAXSTAGE = 3;

    [SerializeField]
    [Header("ステージを選ぶボタン")]
    private List<GameObject> Stagebtn = new List<GameObject>();//ステージ選択のボタンのリスト

    [Header("デフォルトの中央ボタンの位置")]
    public Vector3 DefCenterButtonPosi;//x0 y-120 z0 ボタンの中央の位置

    ////////////////////////////////////// 変数 //////////////////////////////////////
    private int iNowSelectStage = 0;//ボタンを押したら飛ばされるステージの番号

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < MAXSTAGE; i++)
        {
            Stagebtn[i].gameObject.GetComponent<selectUIScript>().getMyNum(i);//飛ばされるステージ番号の初期化
            Stagebtn[i].gameObject.GetComponent<Button>().onClick.AddListener(BTN);//ボタンをクリックしたときの処理
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void BTN() { Debug.Log("a"); }

    public void getControll(string s)
    {
        if (s == "right")
        {
            ++iNowSelectStage;//右ボタンを押し、かつ最大ステージでなければ増やす
        }

        if (s == "left")
        {
            --iNowSelectStage;//左ボタンを押し、かつ最大ステージでなければ減らす
        }

        Debug.Log(iNowSelectStage);

        for (int i = 0; i < MAXSTAGE; i++)
        {
            Stagebtn[i].gameObject.GetComponent<selectUIScript>().getMyNum(iNowSelectStage + i);
        }
    }
}

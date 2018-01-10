//=================================================
// GameHomeScene <= ステージセレクトボタンのスクリプト
// AsukaMekaru
// 2017/11/23
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectUIScript : MonoBehaviour
{
    [SerializeField]
    private homeManager homemanager;

    int iMyNowNum;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = 
            new Vector3(homemanager.DefCenterButtonPosi.x - iMyNowNum * -250, homemanager.DefCenterButtonPosi.y, homemanager.DefCenterButtonPosi.z);
    }

    public void getMyNum(int i)
    {
        iMyNowNum = i;
    }
}

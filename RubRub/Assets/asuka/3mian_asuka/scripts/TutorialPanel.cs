using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour {

    ////////////////////////////////////// 変数シンボル //////////////////////////////////////

    [Header("デフォルトのポジション")]
    [SerializeField]
    private float fDefPosition;
    [Header("移動速度")]
    [SerializeField]
    private float fMoveSpeed;

    [Header("チュートリアルマネージャー")]
    [SerializeField]
    TutorialMgr tutorialmgr;
    ////////////////////////////////////// 変数 //////////////////////////////////////
    bool bReturn = false;

    // Use this for initialization
    void Start () {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!bReturn)Up();

        this.gameObject.GetComponent<Image>().sprite = tutorialmgr._HintImage[tutorialmgr.iNowHint];

        if (Input.GetMouseButtonDown(0)) bReturn = true;//タップしたら一旦パネルを戻す

        if (bReturn && Down()) { tutorialmgr.inactive(); bReturn = false; }//戻れば終了
    }

    void Up()
    {
        if (this.gameObject.transform.localPosition.y > 0)//下に移動
        {
            this.gameObject.transform.localPosition -= new Vector3(0, fMoveSpeed, 0);
        }

        if (this.gameObject.transform.localPosition.y < 0)//過ぎたら修正
        {
            this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    bool Down()
    {
        if (this.gameObject.transform.localPosition.y < fDefPosition)//上に移動
        {
            this.gameObject.transform.localPosition += new Vector3(0, fMoveSpeed, 0);
        }

        if (this.gameObject.transform.localPosition.y > fDefPosition)//過ぎたら修正
        {
            this.gameObject.transform.localPosition = new Vector3(0, fDefPosition, 0);
        }

        if (this.gameObject.transform.localPosition.y == fDefPosition)
        {
            return true;//終わり
        }

        return false;
    }
}

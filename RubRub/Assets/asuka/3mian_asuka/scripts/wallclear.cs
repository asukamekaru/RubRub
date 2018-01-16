using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wallclear : MonoBehaviour {
    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    
    private float fWallRangeX = 0.5f;
    private float fWallRangeZ = 2.0f;

    [Header("プレイヤーオブジェクト")]
    [SerializeField]
    private GameObject Player;

    [Header("透明、非透明のイメージ")]
    [SerializeField]
    public Material[] _material;
    ////////////////////////////////////// 変数 //////////////////////////////////////

    float fWallXMin, fWallXMax, fWallZMin,fWallZMax;

    Image image;

	// Use this for initialization
	void Start () {
        fWallXMin = this.transform.localPosition.x - (this.transform.localScale.x / 2 - fWallRangeX);
        fWallXMax = this.transform.localPosition.x + (this.transform.localScale.x / 2 + fWallRangeX);
        fWallZMin = this.transform.localPosition.z;
        fWallZMax = this.transform.localPosition.z + fWallRangeZ;
        

        this.GetComponent<Renderer>().material = _material[0];
	}
	
	// Update is called once per frame
	void Update () {

        //範囲内なら透明になる
        if (Player.transform.localPosition.x > fWallXMin && Player.transform.localPosition.x < fWallXMax && Player.transform.localPosition.z < fWallZMax && Player.transform.localPosition.z > fWallZMin)
        {
                this.GetComponent<Renderer>().material = _material[0];
        }
        //範囲外なら普通になる
        else
        {
            this.GetComponent<Renderer>().material = _material[1];
        }
	}
}

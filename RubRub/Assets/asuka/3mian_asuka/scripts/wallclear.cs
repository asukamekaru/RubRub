using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallclear : MonoBehaviour {
    ////////////////////////////////////// 変数シンボル //////////////////////////////////////

    
    [Header("検知する範囲")]
    [SerializeField]
    private float fWallRange;

    [Header("プレイヤーオブジェクト")]
    [SerializeField]
    private GameObject Player;

    ////////////////////////////////////// 変数 //////////////////////////////////////

    float fWallXMin,fWallXMax;

	// Use this for initialization
	void Start () {
        fWallXMin = this.transform.localScale.x / 2;
        fWallXMin = fWallXMin * 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.transform.position.x > fWallXMin && Player.transform.position.x < fWallXMin)
        {

        }
	}
}

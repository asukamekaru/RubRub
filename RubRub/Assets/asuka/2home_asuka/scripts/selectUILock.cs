using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectUILock : MonoBehaviour {

    ////////////////////////////////////// 変数シンボル //////////////////////////////////////
    [Header("親のスイッチ")]
    [SerializeField]
    private selectUIScript parentUI;

    [Header("ロック画像")]
    [SerializeField]
    public Sprite[] _sprite;
    

    ////////////////////////////////////// 変数 //////////////////////////////////////

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(parentUI.mySize, parentUI.mySize);

        this.gameObject.GetComponent<Image>().sprite = _sprite [1];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour{

    SpriteRenderer MainSpriteRenderer;
    // publicで宣言し、inspectorで設定可能にする
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;

    private bool SpriteChangeFlg;
    private int SpriteNumber = 0;
    private void Start()
    {
        SpriteNumber = Random.Range(0, 5);
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (SpriteNumber != 0)
        {
            if (!SpriteChangeFlg)
            {
                Change();
                SpriteChangeFlg = true;
            }
        }
    }
    // 何かしらのタイミングで呼ばれる
    void Change()
    {
        switch (SpriteNumber)
        {
            case 1:
                MainSpriteRenderer.sprite = Sprite1;
                break;
            case 2:
                MainSpriteRenderer.sprite = Sprite2;
                break;
            case 3:
                MainSpriteRenderer.sprite = Sprite3;
                break;
            case 4:
                MainSpriteRenderer.sprite = Sprite4;
                break;
        }
    }
}

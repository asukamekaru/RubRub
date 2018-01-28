using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Migration : MonoBehaviour {

    [SerializeField]
    [Header("ブラックアウトするパネル")]
    private GameObject BlackOutPanel;

    bool bPanelFlag = false;//パネルが出てるか否かのフラグ
    GameObject BGMmanager;
    // Use this for initialization
    void Start () {
        BGMmanager = GameObject.Find("SoundManager");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
           
            soundManager SM = BGMmanager.GetComponent<soundManager>();
            SM.PlaySound(0,false);
            BlackOutPanel.gameObject.SetActive(true);//パネルを出すついでに操作できなくする
            bPanelFlag = true;
        }

        if (bPanelFlag && BlackOutPanel.gameObject.GetComponent<BlackOut>().GameBlackOut(0, "end")) SceneManager.LoadScene("HomeScene");
		
	}
}

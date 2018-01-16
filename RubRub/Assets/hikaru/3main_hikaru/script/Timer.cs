using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text p_timerText;

    public float pf_totalTime;
    int i_Seconds;
    int i_Minute;

	// Use this for initialization
	void Start () {
        pf_totalTime += 1;

    }
	
	// Update is called once per frame
	void Update () {
        pf_totalTime -= Time.deltaTime;
        if (pf_totalTime < 0)
        {
            pf_totalTime = 0;
            MainManager.NowStatus = MainManager.STATUS._GAME_OVER_;
            Debug.Log("終了");
        }
        i_Seconds = (int)pf_totalTime / 60;
        i_Minute = (int)pf_totalTime % 60;
        p_timerText.text = i_Seconds.ToString("00") + ":" + i_Minute.ToString("00");
	}
}

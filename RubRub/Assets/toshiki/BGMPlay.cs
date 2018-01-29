using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlay : MonoBehaviour
{

    GameObject BGMmanager = null;
    soundManager SM;
    int NowTask = 0;
    public int PlayBGM_Number = 0;
    // Use this for initialization
    void Start()
    {
        BGMmanager = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (BGMmanager != null)
        {
            switch (NowTask)
            {
                case 0:
                    SM = BGMmanager.GetComponent<soundManager>();
                    SM.StopBgm();
                    NowTask = 1;
                    break;
                case 1:
                    SM.ChangeBgm(PlayBGM_Number);
                    NowTask = 2;
                    break;
                case 2:
                    break;
            }
        }
        
    }
}
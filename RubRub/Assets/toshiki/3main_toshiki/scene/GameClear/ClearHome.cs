using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearHome : MonoBehaviour {
    GameObject SEManager = null;
    void Start()
    {
        SEManager = GameObject.Find("SoundManager");
    }
        public void SceneChangeHome()
    {
        if (SEManager != null)
        {
            soundManager SM = SEManager.GetComponent<soundManager>();
            SM.PlaySound(0, false);
        }
        SceneManager.LoadScene("HomeScene");
    }
}

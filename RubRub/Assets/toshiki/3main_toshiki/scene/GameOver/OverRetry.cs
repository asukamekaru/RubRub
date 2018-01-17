using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverRetry : MonoBehaviour {

    public void SceneChangeRetry()
    {
        SceneManager.LoadScene("GameMainScene");
    }
}

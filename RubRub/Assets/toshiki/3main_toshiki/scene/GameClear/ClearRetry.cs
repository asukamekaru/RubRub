using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearRetry : MonoBehaviour {

    public void SceneChangeRetry()
    {
        SceneManager.LoadScene("GameMainScene");
    }
}

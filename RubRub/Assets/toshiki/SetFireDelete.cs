using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFireDelete : MonoBehaviour {

    public static bool DeleteFlg;
    private ParticleSystem particle;
    private float DeleteTime = 0.0f;
    // Use this for initialization
    void Start () {
        DeleteFlg = false;
        particle = this.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		if(DeleteFlg)
        {
            particle.Stop();
            DeleteTime += Time.deltaTime;
            if (DeleteTime >= 10.0f)
            {
                Destroy(gameObject);
            }
        }
	}
}

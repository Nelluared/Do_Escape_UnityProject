using UnityEngine;
using System.Collections;

public class Effect_Default : MonoBehaviour {
    public float LiveTime;
    float norLiveTime = 0;
	// Use this for initialization
    
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        norLiveTime += Time.deltaTime;
        if (norLiveTime >= LiveTime)
            Destroy(gameObject);

    }
}

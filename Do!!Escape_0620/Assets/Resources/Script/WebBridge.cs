using UnityEngine;
using System.Collections;

public class WebBridge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Rigidbody2D>().isKinematic == false)//거미줄은 고정된다.
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}

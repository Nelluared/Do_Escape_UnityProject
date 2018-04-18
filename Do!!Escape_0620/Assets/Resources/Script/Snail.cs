using UnityEngine;
using System.Collections;

public class Snail : MonoBehaviour {
    bool Hide = false;
    public GameObject ChangeThat;//변신할대상
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {


        if (coll.transform.tag == "Player" && !Hide)
        {

            Hide = true;
            Instantiate(ChangeThat, gameObject.transform.position,gameObject.transform.rotation);
            Destroy(gameObject);
            //StartCoroutine(WebCreating());
        }
    }
}

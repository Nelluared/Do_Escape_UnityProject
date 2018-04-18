using UnityEngine;
using System.Collections;

public class FallDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Block")
        {
            SoundsGenerator.SoundsGeneratorIns().PlaySound("Holefalling", new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0));
            Destroy(coll.gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class ClearZone : MonoBehaviour {
    public GameBase gb;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            gb.GameClear = true;
            SoundsGenerator.SoundsGeneratorIns().PlaySound("ClearSound", new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0));
        }
    }
}

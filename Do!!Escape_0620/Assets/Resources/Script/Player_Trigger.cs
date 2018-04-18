using UnityEngine;
using System.Collections;

public class Player_Trigger : MonoBehaviour {
    public bool Grounded_trigger;
	// Use this for initialization
	void Start () {
        Grounded_trigger = false;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.tag == "Platform" || coll.tag == "Block")
        {
            Grounded_trigger = true;
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {

        if (coll.tag == "Platform" || coll.tag == "Block")
        {
            Grounded_trigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {

        if (coll.tag == "Platform"|| coll.tag == "Block")
        {
            Grounded_trigger = false;
        }
    }
}

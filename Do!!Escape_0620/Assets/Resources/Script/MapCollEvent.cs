using UnityEngine;
using System.Collections;

public class MapCollEvent : MonoBehaviour {
    public float EffectOutDegree;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Player")
        {
            ContactPoint2D contact = collision.contacts[0];
            //Vector2 pos = gameObject.GetComponent<Collision2D>().contacts[0].point;
            Vector2 pos = contact.point;//collision.contacts[0].point;
            Vector2 Dir = new Vector2(collision.transform.position.x, collision.transform.position.y) - pos;
            Vector2 Position = pos + (Dir.normalized * EffectOutDegree);
            //Debug.Log(pos.x + " ff" + pos.y);
            EffectGenerator.EffectGeneratorIns().CreateEffect("Effect_Dust", Position, Vector3.zero, 1);
            SoundsGenerator.SoundsGeneratorIns().PlaySound("CharacterJump", new Vector3(Position.x, Position.y,0));
        }
    }
}

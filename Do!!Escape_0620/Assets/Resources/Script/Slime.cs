using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour
{
    Rigidbody2D myRigidBody;//자주쓰니까 빼놓기
    BoxCollider2D myCollider;//위와 동일
    public PhysicsMaterial2D ChangeMate;//바뀔 재질
                                        // Use this for initialization
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myRigidBody.isKinematic == true)
        {
            myCollider.sharedMaterial = ChangeMate;
        }
        else
            myCollider.sharedMaterial = null;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            SoundsGenerator.SoundsGeneratorIns().PlaySound("SlimeBounce", new Vector3(transform.position.x, transform.position.y, 0));
        }

    }
}

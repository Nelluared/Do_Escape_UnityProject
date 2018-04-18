using UnityEngine;
using System.Collections;

public class GazeEye : MonoBehaviour {
    public Transform Target;
	// Use this for initialization
	void Start () {
	
	}
    void GazeTarget()
    {
        Vector3 vec = Target.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        //q.w = 1;
        q.y = 0;
        q.x = 0;
       ;
        // if (q.z >= 0 && q.z < Mathf.PI*0.5 )
        // {
        //     Debug.Log("1");
        //    // q.z = q.z- Mathf.PI ;
        // }
        //else if (q.z >= Mathf.PI * 0.5 && q.z < Mathf.PI)
        // {
        //     Debug.Log("2");
        //    // q.z = q.z - Mathf.PI;
        // }
        // else if (q.z >= Mathf.PI && q.z < Mathf.PI* 1.5f)
        // {
        //     Debug.Log("3");
        //     //q.z = q.z - Mathf.PI;
        // }
        // else if (q.z >= Mathf.PI * 1.5f && q.z < Mathf.PI*2.0f)
        // {
        //     Debug.Log("4");
        //     //q.z = q.z - Mathf.PI;
        // }
        //Vector3 LookPoint = Target.position;
        //LookPoint.y = transform.position.y;
        ////LookPoint.z = transform.position.z;
        //transform.LookAt(LookPoint);
        //transform.rotation= 
        //transform.rotation = q;
        float angle = 0;
        Vector3 relative = transform.InverseTransformPoint(Target.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle-90);
    }
	// Update is called once per frame
	void Update () {
        GazeTarget();
	}
}

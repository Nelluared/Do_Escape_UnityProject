using UnityEngine;

public class Drag : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;


   public Vector3 GetDragScreenMove()
    {
        Vector3 move = Vector3.zero;
       
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return move;
        }

        if (!Input.GetMouseButton(0)) {
            dragOrigin = Input.mousePosition;
            return move;
        }

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
         move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed,0 );

        
        return move;
    }


}

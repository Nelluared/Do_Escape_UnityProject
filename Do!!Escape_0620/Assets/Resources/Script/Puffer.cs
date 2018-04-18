using UnityEngine;
using System.Collections;

public class Puffer : MonoBehaviour
{
    public float DefaultSize;
    public float BiggerSize;
    public float SmallerSize;
    bool Bigger = false;
    bool Biggered = false;
    Vector3 BaseSize;
    
    public Vector3 DlPoint;
    DialogueEffect Dialog;
    // Use this for initialization
    void Start()
    {
        BaseSize = transform.localScale;
    }
    IEnumerator BigandDead()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].GetComponent<Rigidbody2D>(). isKinematic = false;
        }
        System.Array.Clear(temp, 0, temp.Length);
        yield return null;
        
        for (float i = DefaultSize; i <= BiggerSize;)
        {
       
            i += Mathf.Lerp(DefaultSize, BiggerSize, Time.deltaTime * 2);
            transform.localScale = BaseSize * i;

            //yield return new WaitForSeconds(0.001f);
            Dialog.transform.position = transform.position+ DlPoint * i/4;
            yield return new WaitForSeconds(1.5f);
        }
        GameObject[] temp2 = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < temp2.Length; i++)
        {
            temp2[i].GetComponent<Rigidbody2D>().isKinematic = true;
        }
        System.Array.Clear(temp2, 0, temp2.Length);
        StopCoroutine(BigandDead());
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        //if(Bigger&&!Biggered)



    }
    void OnTriggerEnter2D(Collider2D coll)
    {        
        if (coll.transform.tag == "Player"&&!Bigger)
        {
            SoundsGenerator.SoundsGeneratorIns().PlaySound("MomboBigger", new Vector3(transform.position.x, transform.position.y, 0));
            gameObject.GetComponent<Animator>().SetBool("Blow", true);
            Bigger = true;    
            StartCoroutine(BigandDead());
        }
    }
}

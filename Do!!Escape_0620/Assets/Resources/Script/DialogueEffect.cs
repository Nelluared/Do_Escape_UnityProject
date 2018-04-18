using UnityEngine;
using System.Collections;

public class DialogueEffect : MonoBehaviour {
    public Sprite DialogueImage;
    public float LifeTime;
    bool Alive = true;

    float SurviveTime = 0;//생존한 시간
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = DialogueImage;
        StartCoroutine(DialogueAnimation());
    }
    IEnumerator DialogueAnimation()
    {

        while (SurviveTime < LifeTime)
        {
            SurviveTime += Time.deltaTime;
            yield return null;
        }
        StopCoroutine(DialogueAnimation());
        Alive = false;
         yield return null;
      

    }
	// Update is called once per frame
	void Update () {
        if(!Alive)
        Destroy(gameObject);
    }
}

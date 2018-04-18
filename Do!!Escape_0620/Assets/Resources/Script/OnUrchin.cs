using UnityEngine;
using System.Collections;

public class OnUrchin : MonoBehaviour {

    public Transform DlPoint;
    DialogueEffect Dialogue;
    bool TermPass = true;
    Player Target;
    // Use this for initialization
    void Start () {

    }
    IEnumerator DialogueTerm()
    {
        TermPass = false;
        yield return new WaitForSeconds(3.0f);

        TermPass = true;
        StopCoroutine(DialogueTerm());
        yield return null;
    }
	// Update is called once per frame
	void Update () {

            if (!Dialogue&& TermPass)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        Dialogue = DialogueBoxPlayer.DialogueBoxPlayerIns().
                  CreateDialogue("Dl_UrchinStick", DlPoint.position, 3.0f, 1);
                        break;
                    case 1:
                   
                        break;
                    case 2:
                        break;
                }
            StartCoroutine(DialogueTerm());
        }
            else if(Dialogue){
            Dialogue.transform.position = DlPoint.position;
        }

    }
}

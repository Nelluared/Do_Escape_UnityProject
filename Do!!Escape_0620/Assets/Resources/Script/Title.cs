using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public AudioSource Enter;
    public UnityEngine.UI.Image Toud;
    public UnityEngine.UI.Image Bs;
    // Use this for initializationf
    float Alpha ;
    float fade ;
    void Start()
    {        

    }
    void OnEnable()
    {
        Alpha = 0;
        fade = 1;
        Bs.color = new Color(1, 1, 1, 0);
        Toud.color = new Color(1, 1, 1, 0);
        StartCoroutine(ShowTouch());
    }
   IEnumerator ShowTouch()
    {
        yield return new WaitForSeconds(2f);
        for (; Alpha < 1;)
        {
            Debug.Log(Alpha);

            //Toud.GetComponent<Renderer>().material.color = new Color(1, 1, 1, Alpha);
            Toud.color = new Color(1, 1, 1, Alpha);
            Alpha += Time.deltaTime * (float)1;
            yield return new WaitForSeconds(0.001f);
        }
        StopCoroutine(ShowTouch());
        yield return null;
    }
    IEnumerator ShowNext()
    {

        for (; fade > 0;)
        {
            Debug.Log("asdad");
            fade -= Time.deltaTime * 1f;
            Bs.color = new Color(1,1,1, 1-fade);
            Toud.color = new Color(fade, fade, fade, Alpha);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("Title");
        StopCoroutine(ShowNext());
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
            Application.Quit();
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Alpha = 1;
            Enter.Play();
            StartCoroutine(ShowNext());

        }
    }
}

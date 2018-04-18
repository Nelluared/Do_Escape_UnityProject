using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class LoadOnClick : MonoBehaviour
{
    public string NextSceneName;//원래는 씬이름리스트를 다 모아야 하지만 간이로...
    public GameObject loadingImage;
    void Start()
    {
        //SceneManager.LoadScene(NextSceneName);
        Debug.Log("fadfafsaf");
    }  
    public void LoadScene(int level)
    {
        // loadingImage.SetActive(true);
        Debug.Log("fad1111111111111111sSf");
        SceneManager.LoadScene(NextSceneName);
    }
}
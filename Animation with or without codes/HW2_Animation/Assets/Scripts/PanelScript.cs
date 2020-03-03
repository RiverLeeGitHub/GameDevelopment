using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetSceneByName("Scene2").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Scene2");
        }
        if (SceneManager.GetSceneByName("Scene1").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Scene1");
        }
        //SceneManager.UnloadSceneAsync("Scene1");
        //SceneManager.UnloadSceneAsync("Scene2");
    }

    public void onClick1()
    {
        Debug.Log("onClick1");
        SceneManager.LoadScene("Scene1");
    }

    public void onClick2()
    {
        Debug.Log("onClick2");
        SceneManager.LoadScene("Scene2");
    }

}

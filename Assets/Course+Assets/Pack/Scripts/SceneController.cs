using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static string keyword;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(keyword);
    }

    public void askGoogle(string PassedKeyword)
    {
        keyword = PassedKeyword;
       //StartCoroutine(makeURLReguest());
    }

    public void changeScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

}


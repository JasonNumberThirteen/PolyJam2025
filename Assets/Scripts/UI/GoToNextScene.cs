using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    public string NextSceneName;
    public void GoToScene()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}

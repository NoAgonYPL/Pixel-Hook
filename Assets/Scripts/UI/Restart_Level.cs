using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_Level : MonoBehaviour
{
    [SerializeField] string sceneName;
  

    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}

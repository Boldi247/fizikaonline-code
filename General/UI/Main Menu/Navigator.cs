using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Navigator : MonoBehaviour
{
    public int buildIndex;

    public void NavigateToScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }
}
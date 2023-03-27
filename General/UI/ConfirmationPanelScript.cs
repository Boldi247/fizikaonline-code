using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmationPanelScript : MonoBehaviour
{
    public GameObject confirmationPanel;

    public int levelBuildindex;

    public void ExitPanel()
    {
        confirmationPanel.SetActive(false);
    }
    
    public void SwitchToQuestions()
    {
        SceneManager.LoadScene(levelBuildindex);
    }
}

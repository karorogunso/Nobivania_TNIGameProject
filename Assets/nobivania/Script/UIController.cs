using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public void OnStartClick()
    {
        SceneManager.LoadScene("Act1_Scene1");
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
}

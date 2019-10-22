using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("Game is exiting");
	}

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}

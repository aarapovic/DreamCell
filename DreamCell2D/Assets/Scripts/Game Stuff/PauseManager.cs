using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
	private bool isPaused;
	public GameObject pausePanel;
	public string mainMenu;
    public GameObject uiCanvas;

    
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        //uiCanvas.GetComponent<Canvas>().CompareTag("UI");
        
        isPaused = false;
        uiCanvas.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)

        uiCanvas.SetActive(false);
        if (Input.GetButtonDown("pause"))
		{
            uiCanvas.SetActive(false);
            Nastavi();
		}
    }

	public void Nastavi()
	{

        isPaused = !isPaused;
		if (isPaused)
		{
			pausePanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			pausePanel.SetActive(false);
			Time.timeScale = 1f;
		}
        
        uiCanvas.SetActive(true);
    }

	public void NatragNaMeni()
	{

        SceneManager.LoadScene(mainMenu);
		Time.timeScale = 1f;

	}
}

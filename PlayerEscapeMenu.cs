using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEscapeMenu : MonoBehaviour {

	public bool escapeMenuOn;
	public bool resumed;
	public Canvas escapeMenuCanvas;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape)){

			if (!escapeMenuOn){

				escapeMenuOn = true;
				escapeMenuCanvas.enabled = true;
				GetComponent<PlayerMovement>().enabled = false;
			}else{

				escapeMenuOn = false;
				escapeMenuCanvas.enabled = false;
				GetComponent<PlayerMovement>().enabled = true;
			}
		}
	}

	public void OnResumeButton(){

		escapeMenuCanvas = GameObject.Find("EscapeCanvas").GetComponent<Canvas>();
		escapeMenuOn = false;
		escapeMenuCanvas.enabled = false;
		GetComponent<PlayerMovement>().enabled = true;
	}
	public void OnMainMenuButton(){

		SceneManager.LoadScene("Main Menu");
	}
}

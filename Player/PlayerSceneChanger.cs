using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSceneChanger : MonoBehaviour {

    public Text techOpenText;
	public Text submarineConfirmText;
	public bool waitSubConfirm;

    private void Start()
    {
        GameManager.instance.PlayRandomClip(true);
    }

    void OnTriggerEnter2D(Collider2D collision2D){

		if (collision2D.transform.tag == "SubmarineEnterObject"){

			submarineConfirmText.enabled = true;
			waitSubConfirm = true;
		}
        if(collision2D.transform.tag == "TechObject")
        {
            techOpenText.enabled = true;
        }
	}

	void OnTriggerExit2D(Collider2D collision2D){

		if (collision2D.transform.tag == "SubmarineEnterObject")
        {

			submarineConfirmText.enabled = false;
			waitSubConfirm = false;
		}
        if (collision2D.transform.tag == "TechObject")
        {
            techOpenText.enabled = false;
        }
    }

	void Update(){

		if (waitSubConfirm && Input.GetKeyDown(KeyCode.E)){

			SceneManager.LoadScene("Underwater");
		}
	}
}

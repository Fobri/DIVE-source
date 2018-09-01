using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    private float energy = 100;
    [HideInInspector]
    public float hp = 100;
	private float energyDrainRate = 2.5f;
    public Text energyText;
    public Text HPText;

    public Slider hpSlider;
    public Slider energySlider;
    SubShowDamage subDmgLayers;
    public AcquireResources acquireResources;

	public Text lowEnergyAlert;

    private int maxSafeDepth;
    public int curDepth;

    public float waitForSecondsAfterDeath;
    public Behaviour[] scriptsToDisableOnDeath;
    public GameObject runOutOfEnergyTextObject;
    public GameObject runOutOfHealthTextObject;
    bool died = false;
    public GameObject[] lightLevels;

    void Start ()
    {
        GameManager.instance.PlayRandomClip(false);
        lightLevels[GameManager.instance.baseLightLevel].SetActive(true);
        subDmgLayers = GetComponent<SubShowDamage>();
        maxSafeDepth = GameManager.instance.baseMaxSafeDepth;
        energy = GameManager.instance.baseMaxEnergyAmount;
        hp = GameManager.instance.baseSubHealth;
        energyDrainRate = GameManager.instance.baseEnergyDrainSpeed;
        InvokeRepeating("RemoveEnergy", 1, 1);
        InvokeRepeating("CheckMaxDepth", 10, 1);
        //energyText.text = "Energy: " + energy.ToString();
        //HPText.text = "HP: " + hp.ToString();
		energyText.text = "" + energy.ToString();
		HPText.text = "" + hp.ToString();
	}

    void CheckMaxDepth()
    {
        if(curDepth < maxSafeDepth)
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        HPText.text = "HP: " + hp.ToString();
        hpSlider.value = hp;
        subDmgLayers.hp = hp;
        if (hp <= 0)
            Die(true);
    }

    void RemoveEnergy()
    {
        if (energy > 0)
        {
            energy -= energyDrainRate;
			//energyText.text = "Energy: " + energy.ToString();
			energyText.text = "" + energy.ToString();
			energySlider.value = (int)energy;
            //HPText.text = "HP: " + hp.ToString();
			HPText.text = "" + hp.ToString();
			if (energy < 20) {
				lowEnergyAlert.text = "LOW ENERGY";
			}
            if (energy <= 0) {
				Die(false);
			}      
        }
        else
            Die(false);
    }

    void Die(bool loseEverything)
    {
        if (!died)
        {
            died = true;
            if (!loseEverything)
            {
                runOutOfEnergyTextObject.SetActive(true);
                for (int i = 0; i < acquireResources.currentDiveResources.Count; i++)
                {
                    GameManager.instance.resources[i].amount += acquireResources.currentDiveResources[i].amount;
                }
            }
            else
                runOutOfHealthTextObject.SetActive(true);
            foreach (var script in scriptsToDisableOnDeath)
            {
                script.enabled = false;
            }
            StartCoroutine(WaitLoadSurface());
        }
    }

    IEnumerator WaitLoadSurface()
    {
        yield return new WaitForSeconds(waitForSecondsAfterDeath);
        SceneManager.LoadScene("Surface", LoadSceneMode.Single);
    }
}

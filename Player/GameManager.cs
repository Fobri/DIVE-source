using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public List<Resource> resources = new List<Resource>();
    public List<Upgrades> currentUpgrades = new List<Upgrades>();
    [HideInInspector]
    public bool techOpen;
    [HideInInspector]
    public GameObject upgradeBlocker;

    public int lastDialogueNumber = 0;

    public DialogueSystemSettings globalDialogueSystemSettings;

    [HideInInspector]
    public int surfaceSceneDialogueIndex = -1;
    [HideInInspector]
    public int underWaterSceneDialogueIndex = -1;
    public AudioClip[] surfaceClips;
    public AudioClip[] underwaterClips;
    AudioSource audioSource;

    public void PlayRandomClip(bool surfaceClip)
    {
        int random = Random.Range(0, 2);
        if (surfaceClip)
            audioSource.clip = surfaceClips[random];
        else
            audioSource.clip = underwaterClips[random];

        audioSource.Play();
    }

    //Sub variables
    [Header("Submarine base settings")]
    public int baseMaxSafeDepth = 100;
    public float baseEnergyDrainSpeed = 2.5f;
    public float baseMaxEnergyAmount = 100;
    public float baseSubSpeed = 35;
    public float baseSubReverseSpeed = 25;
    public int baseSubSlowRotSpeed = 40;
    public int baseSubFastRotSpeed = 80;
    public int baseSubHealth = 100;
    public int baseTurboAmount = 0;

    public bool canUseTurbo = false;
    public int baseLightLevel = 0;

    [HideInInspector]
    public bool[] coreTechCanvasIndexes = new bool[8];
    [HideInInspector]
    public bool[] experimentalTechCanvasIndexes = new bool[8];
    public void BlockThisUpgrade(Transform _pos)
    {
        Instantiate(upgradeBlocker, _pos);
    }

    public void HandleUpgrade(Upgrades upgrade)
    {
        bool hasUpgradeHappened = false;
        switch (upgrade.upgradeType)
        {
            case Upgrades.UpgradeType.MaxDepth:
                baseMaxSafeDepth -= upgrade.upgradeAddAmount;
                hasUpgradeHappened = true;
                break;
            case Upgrades.UpgradeType.EnergyDrain:
                baseEnergyDrainSpeed *= upgrade.percentage;
                hasUpgradeHappened = true;
                break;
            case Upgrades.UpgradeType.EnergyCapacity:
                baseMaxEnergyAmount += upgrade.upgradeAddAmount;
                hasUpgradeHappened = true;
                break;
            case Upgrades.UpgradeType.Speed:
                baseSubSpeed += upgrade.upgradeAddAmount;
                baseSubReverseSpeed += upgrade.upgradeAddAmount;
                hasUpgradeHappened = true;
                break;
            case Upgrades.UpgradeType.RotationSpeed:
                baseSubFastRotSpeed += upgrade.upgradeAddAmount;
                baseSubSlowRotSpeed += upgrade.upgradeAddAmount;
                hasUpgradeHappened = true;
                break;
            case Upgrades.UpgradeType.Health:
                baseSubHealth += upgrade.upgradeAddAmount;
                hasUpgradeHappened = true;
                break;
            case Upgrades.UpgradeType.TurboSpeed:
                baseTurboAmount += upgrade.upgradeAddAmount;
                canUseTurbo = true;
                hasUpgradeHappened = true;
                break;
            case Upgrades.UpgradeType.Light:
                baseLightLevel += upgrade.upgradeAddAmount;
                hasUpgradeHappened = true;
                break;
            case Upgrades.UpgradeType.WinGame:
                WinGame();
                break;
                
        }
        if (hasUpgradeHappened)
        {
            currentUpgrades.Add(upgrade);
            resources[upgrade.resourceCostId].amount -= upgrade.resourceCostAmount;
            GameObject.FindObjectOfType<SurfaceResourceAmountDisplayer>().UpdateResourceAmount();
            Debug.Log("Bought an upgrade!");
        }
        else
            Debug.LogError("Failed on adding an upgrade");
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < 4; i++)
        {
            resources.Add(new Resource(i, 0));
        }
    }

    void WinGame()
    {
        Debug.Log("Won game");
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}

[System.Serializable]
public class Upgrades
{
    public enum UpgradeType { MaxDepth, EnergyDrain, Speed, EnergyCapacity, RotationSpeed, Health, TurboSpeed, ScannerModule, Light, WinGame }
    public UpgradeType upgradeType;

    public int upgradeAddAmount;

    public float percentage;
    [Tooltip("0 = green thing, 1 = yellow thing, 2 = red thing, 3 = pink thing")]
    public int resourceCostId;
    public int resourceCostAmount;
}

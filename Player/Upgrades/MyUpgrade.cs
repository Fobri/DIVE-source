using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUpgrade : MonoBehaviour {

    public Upgrades myUpgrade;
    public bool upgradeBought = false;

    public void OnUpgrade()
    {
        if (myUpgrade != null && !upgradeBought && haveEnoughResources)
        {
            GameManager.instance.HandleUpgrade(myUpgrade);

            if (transform.parent.name == "CoreTechCanvas")
                GameManager.instance.coreTechCanvasIndexes[transform.GetSiblingIndex()] = true;
            else
                GameManager.instance.experimentalTechCanvasIndexes[transform.GetSiblingIndex()] = true;
            GameManager.instance.BlockThisUpgrade(transform);

        }
    }
    bool haveEnoughResources
    {
        get
        {
            if (GameManager.instance.resources[myUpgrade.resourceCostId].amount >= myUpgrade.resourceCostAmount)
            {
                return true;
            }
            else
                return false;
        }
    }
}

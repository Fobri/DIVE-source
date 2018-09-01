using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AcquireResources : MonoBehaviour {
    
    public GameObject[] resourceDisplayers;
    public List<Resource> currentDiveResources = new List<Resource>();

    private void Start()
    {
        UpdateResourceCountOnScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Resource")
        {
            ResourceScript resourceScript = collision.transform.GetComponent<ResourceScript>();
            foreach (var resource in currentDiveResources)
            {
                if(resource.resourceId == resourceScript.resource.resourceId)
                {
                    resource.amount += resourceScript.resource.amount;
                    break;
                }
            }


            Destroy(collision.transform.gameObject);
            UpdateResourceCountOnScreen();
        }
    }

    void UpdateResourceCountOnScreen()
    {
        for(int i = 0; i < resourceDisplayers.Length; i++)
        {
            resourceDisplayers[i].transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = currentDiveResources[i].amount.ToString();
        }
    }
}

[System.Serializable]
public class Resource
{
    public int resourceId;
    public int amount;

    public Resource(int resourceId, int amount)
    {
        this.resourceId = resourceId;
        this.amount = amount;
    }
}

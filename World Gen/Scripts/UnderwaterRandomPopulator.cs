using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterRandomPopulator : MonoBehaviour {

    public UnderWaterObject[] underWaterObjects;
    public float maxWidth;
    public Transform parentObject;
    public float minDstToOtherObjects;
    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private void Start()
    {
        PopulateWater();
    }
    
    public void PopulateWater()
    {
        for(int i = 0; i < underWaterObjects.Length; i++)
        {
            for (int s = 0; s < underWaterObjects[i].spawnAmount; s++)
            {
                var change = Random.Range(0, 100);
                if (change < underWaterObjects[i].spawnChange)
                {
                    Vector2 depthArea = underWaterObjects[i].depthArea;
                    Vector2 pos = GetNewPosition(depthArea);
                    if (underWaterObjects[i].spawnPlace != Vector2.zero)
                        pos = underWaterObjects[i].spawnPlace;
                    GameObject shit = Instantiate(underWaterObjects[i].underWaterObject, parentObject);
                    shit.transform.SetParent(parentObject);
                    shit.transform.position = pos;
                    if(underWaterObjects[i].randomRotation)
                        shit.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180, 180));
                    else
                        Quaternion.Euler(0, 0, underWaterObjects[i].zRotation);
                }
            }
        }
    }
    

    Vector2 GetNewPosition(Vector2 depthArea)
    {
        return new Vector2(Random.Range(-maxWidth, maxWidth), Random.Range(depthArea.x, depthArea.y));
    }

    private void OnDisable()
    {
        DestroyObjects();
    }

    public void DestroyObjects()
    {
        foreach(var uwObject in underWaterObjects)
        {
            for(int i = 0; i < instantiatedObjects.Count; i++)
            {
                Destroy(instantiatedObjects[i]);
            }
        }
        instantiatedObjects.Clear();
    }
}


[System.Serializable]
public class UnderWaterObject
{
    public GameObject underWaterObject;
    public Vector2 depthArea;
    public bool randomRotation;
    public float zRotation;
    [Range(0f, 100f)]
    public float spawnChange = 100f;
    public int spawnAmount = 0;
    [HideInInspector]
    public Vector2 spawnPlace;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingScreen : MonoBehaviour
{
    public List<Section> sectionsInOrder = new List<Section>(); //todo: probably switch to procedural generation, so have a list of prefabs.
    public List<GameObject> sectionPrefabs = new List<GameObject>();
    public GameObject bossAreaPrefab;

    private List<GameObject> generatedSections = new List<GameObject>();
    [HideInInspector]
    public int currentZone = 0;
        
    private void Awake()
    {
        WorldManager.Instance.mainGame = this;

        if (WorldManager.Instance.mySections.Count == 0)
        {
            for (int i = 0; i < 10; i++)
                WorldManager.Instance.mySections.Add(UnityEngine.Random.Range(0, sectionPrefabs.Count));
        }
        else
        {
            currentZone = Math.Max(0, WorldManager.Instance.lowestFloor - 2);
        }

        Generate();

        StartCoroutine(WorldManager.Instance.FadeScreen(true, null));
    }

    public void GetNextZone(int upOrDown)
    {
        currentZone += upOrDown;
        RectTransform rt = (generatedSections[currentZone].transform as RectTransform);
        WorldManager.Instance.SetPlayerSectionBounds(rt.position, new Vector2(rt.sizeDelta.x, rt.sizeDelta.y));
    }

    public void Generate()
    {
        Section prev = null;
        int startingZone = Math.Max(0, WorldManager.Instance.lowestFloor - 2);
        UiManager.Instance.UpdateStartMarker(startingZone);
        for (int i = startingZone; i < 10; i++) //todo: change based on how many sections you've "gotten farther than" - and adjust the "difficulty" of the sections accordingly.
        {
            GameObject newSection = Instantiate(sectionPrefabs[WorldManager.Instance.mySections[i]]);
            newSection.transform.SetParent(transform);
            newSection.transform.localScale = new Vector3(1, 1, 1);
            newSection.transform.SetLocalPosition(z: -1);
            newSection.GetComponent<Section>().SetOrder(i, prev == null ? new Vector3(0, 360, 0) : prev.bottom.position, i == startingZone);

            generatedSections.Add(newSection);
            prev = newSection.GetComponent<Section>();

            if (i == 9)
                WorldManager.Instance.lowestYValue = prev.gameObject.transform.position.y - (prev.gameObject.transform as RectTransform).sizeDelta.y * 0.5f;
        }
        var bossSection = Instantiate(bossAreaPrefab);
        bossSection.transform.SetParent(transform);
        bossSection.transform.localScale = new Vector3(1, 1, 1);
        bossSection.transform.SetLocalPosition(z: -1);
        bossSection.GetComponent<Section>().SetOrder(10, prev == null ? new Vector3(0, 360, 0) : prev.bottom.position, false, true);
        generatedSections.Add(bossSection);

        RectTransform rt = (generatedSections[currentZone].transform as RectTransform);
        WorldManager.Instance.SetPlayerSectionBounds(rt.position, new Vector2(rt.sizeDelta.x, rt.sizeDelta.y));
    }
}

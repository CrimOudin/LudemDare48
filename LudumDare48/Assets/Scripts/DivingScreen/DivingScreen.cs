using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingScreen : MonoBehaviour
{
    public List<Section> sectionsInOrder = new List<Section>(); //todo: probably switch to procedural generation, so have a list of prefabs.
    public List<GameObject> sectionPrefabs = new List<GameObject>();

    private List<int> mySections = new List<int>();
    private List<GameObject> generatedSections = new List<GameObject>();
    private int currentZone = 0;

    public void AssignSectionsToMe()
    {
        int count = 0;
        Section prev = null;
        foreach (Section s in sectionsInOrder)
        {
            s.transform.parent = transform;
            if (prev != null)
                s.SetOrder(count, prev.bottom.position);
            else
                s.SetOrder(0, new Vector3(0, 360, 0));

            prev = s;
        }
        RectTransform rt = (sectionsInOrder[currentZone].transform as RectTransform);
        WorldManager.Instance.SetPlayerSectionBounds(rt.position, new Vector2(rt.sizeDelta.x, rt.sizeDelta.y));
    }

    private void Awake()
    {
        WorldManager.Instance.mainGame = this;

        for (int i = 0; i < 10; i++)
            mySections.Add(UnityEngine.Random.Range(0, sectionPrefabs.Count));

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
        for (int i = 0; i < 10; i++) //todo: change based on how many sections you've "gotten farther than" - and adjust the "difficulty" of the sections accordingly.
        {
            GameObject newSection = Instantiate(sectionPrefabs[mySections[i]]);
            newSection.transform.SetParent(transform);
            newSection.transform.localScale = new Vector3(1, 1, 1);
            newSection.transform.SetLocalPosition(z: -1);
            newSection.GetComponent<Section>().SetOrder(i, prev == null ? new Vector3(0, 360, 0) : prev.bottom.position);

            generatedSections.Add(newSection);
            prev = newSection.GetComponent<Section>();
        }

        RectTransform rt = (generatedSections[currentZone].transform as RectTransform);
        WorldManager.Instance.SetPlayerSectionBounds(rt.position, new Vector2(rt.sizeDelta.x, rt.sizeDelta.y));
    }
}

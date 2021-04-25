using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingScreen : MonoBehaviour
{
    public List<Section> sectionsInOrder = new List<Section>(); //todo: probably switch to procedural generation, so have a list of prefabs.

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
        StartCoroutine(WorldManager.Instance.FadeScreen(true, () => { AssignSectionsToMe(); }));
    }

    public void GetNextZone(int upOrDown)
    {
        currentZone += upOrDown;
        RectTransform rt = (sectionsInOrder[currentZone].transform as RectTransform);
        WorldManager.Instance.SetPlayerSectionBounds(rt.position, new Vector2(rt.sizeDelta.x, rt.sizeDelta.y));
    }
}

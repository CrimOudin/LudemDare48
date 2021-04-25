using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingScreen : MonoBehaviour
{
    public List<Section> sectionsInOrder = new List<Section>(); //todo: probably switch to procedural generation, so have a list of prefabs.

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
    }

    private void Awake()
    {
        StartCoroutine(WorldManager.Instance.FadeScreen(true, () => { AssignSectionsToMe(); }));
    }

    public void SetNewZone()
    {

    }
}

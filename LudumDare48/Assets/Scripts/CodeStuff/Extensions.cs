using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions
{
    public static void SetColor(this GameObject go, float? r = null, float? g = null, float? b = null, float? a = null)
    {
        //I hate this.  But for some reason I can't pass a Color in and then set the color.
        if (go.GetComponent<Text>() != null)
            go.GetComponent<Text>().color = new Color(r ?? go.GetComponent<Text>().color.r,
                                                      g ?? go.GetComponent<Text>().color.g,
                                                      b ?? go.GetComponent<Text>().color.b,
                                                      a ?? go.GetComponent<Text>().color.a);

        if (go.GetComponent<Image>() != null)
            go.GetComponent<Image>().color = new Color(r ?? go.GetComponent<Image>().color.r,
                                                       g ?? go.GetComponent<Image>().color.g,
                                                       b ?? go.GetComponent<Image>().color.b,
                                                       a ?? go.GetComponent<Image>().color.a);

        if (go.GetComponent<SpriteRenderer>() != null)
            go.GetComponent<SpriteRenderer>().color = new Color(r ?? go.GetComponent<SpriteRenderer>().color.r,
                                                                g ?? go.GetComponent<SpriteRenderer>().color.g,
                                                                b ?? go.GetComponent<SpriteRenderer>().color.b,
                                                                a ?? go.GetComponent<SpriteRenderer>().color.a);

    }

    public static void SetLocalPosition(this Transform t, float? x = null, float? y = null, float? z = null)
    {
        t.localPosition = new Vector3(x ?? t.localPosition.x, y ?? t.localPosition.y, z ?? t.localPosition.z);
    }

    public static void SetPosition(this Transform t, float? x = null, float? y = null, float? z = null)
    {
        t.position = new Vector3(x ?? t.position.x, y ?? t.position.y, z ?? t.position.z);
    }

    public static void SetSize(this RectTransform t, float? x = null, float? y = null)
    {
        t.sizeDelta = new Vector2(x ?? t.sizeDelta.x, y ?? t.sizeDelta.y);
    }
}

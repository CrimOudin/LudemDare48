using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Section : MonoBehaviour
{
    public Transform top;
    public Transform bottom;

    private int myOrder;

    public void SetOrder(int order, Vector3 topConnection)
    {
        myOrder = order;
        transform.position = topConnection - top.localPosition;
    }
}

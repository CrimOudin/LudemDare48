using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Camera.main != null)
            transform.SetPosition(Camera.main.transform.position.x, Camera.main.transform.position.y);
    }
}

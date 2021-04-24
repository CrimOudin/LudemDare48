using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Button;

public class CustomButton : MonoBehaviour
{
    public ButtonClickedEvent clickEvent;

    private void OnMouseDown()
    {
        clickEvent.Invoke();
    }
}

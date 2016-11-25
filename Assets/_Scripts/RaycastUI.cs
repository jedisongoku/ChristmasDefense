using UnityEngine;
using System.Collections;

public class RaycastUI : MonoBehaviour {

    public void OnMouseOver()
    {
        MouseController.isMouseOnUI = true;
        Invoke("DelayedMouseExit", 0.125f);
    }

    public void OnMouseExit()
    {
        MouseController.isMouseOnUI = false;
    }

    void DelayedMouseExit()
    {
        MouseController.isMouseOnUI = false;
    }
}

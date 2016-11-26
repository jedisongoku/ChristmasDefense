using UnityEngine;
using System.Collections;

public class RaycastUI : MonoBehaviour {

    public void OnMouseOver()
    {
        MouseController.isMouseOnUI = true;

    }

    public void OnMouseExit()
    {
        MouseController.isMouseOnUI = false;

    }

}

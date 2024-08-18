using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SelectFirstButtonInMenu : MonoBehaviour
{
    public EventSystem eventSystem;

    public void SelectButtonInMenu(GameObject button)
    {
        eventSystem.SetSelectedGameObject(button);
    }
}

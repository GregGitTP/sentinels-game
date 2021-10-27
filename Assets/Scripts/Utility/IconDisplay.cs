using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconDisplay : MonoBehaviour
{
    public IconSpawner iconSpawner; //The iconSpawner component that spawned this icon. Automatically set via IconSpawner

    [Header("Mission Details")]
    public PopUpManager.Mission mission; //The mission details, defined inside PopUpManager

    public void PopUp() //Triggered via the alert's button's onclickm
    {
        PopUpManager.Instance.gameObject.SetActive(true); //Enables the Pop Up gameobject after finding the current instance, which also plays its animation
        PopUpManager.Instance.SetDetails(mission); //Updates the pop up's mission details
        CanvasManager.Instance.SetActiveAlerts(false);// Disables all alerts.
    }
}

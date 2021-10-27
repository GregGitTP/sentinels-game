using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconSpawner : MonoBehaviour
{
    public GameObject button; //The UI gameobject that spawns when the game starts
    Canvas canvas; // The canvas gameobject. Retrieved by CanvasManager's singleton function
    GameObject newButton;// The instantiated button spawned once the game starts
    [Header("Mission Details")]
    public PopUpManager.Mission mission; //The mission details, defined inside PopUpManager

    void Start()
    {
        canvas = CanvasManager.Instance.MainCanvas; //Retrieves the scene's canvas via CanvasManager's singleton
        newButton = Instantiate(button, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, canvas.transform) as GameObject; //Spawns the button at the correct position
        newButton.transform.SetAsFirstSibling(); //Sets the button as the first sibiling, so that every other UI element displays over it
        CanvasManager.Instance.UpdateAllAlerts(newButton); //Updates the alerts List inside canvas manager with the spawned alert

        IconDisplay display = newButton.GetComponent<IconDisplay>(); //Gets the IconDisplay script attached to the instantiated button
        display.iconSpawner = this; //Sets the IconDisplay's iconSpawner variable to this script

        //Forwards the mission details
        display.mission.title = mission.title;
        display.mission.description = mission.description;
        display.mission.proceedSceneIndex = mission.proceedSceneIndex;
    }

    private void Update()
    {
        //Moves the button to the correct position every frame
        newButton.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}

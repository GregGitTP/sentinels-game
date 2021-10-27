using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    //Stolen from https://answers.unity.com/questions/1463832/how-to-reference-the-canvas.html
    // singleton static reference
    private static CanvasManager _Instance;
    public static CanvasManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<CanvasManager>();
                if (_Instance == null)
                    Debug.LogError("There is no CanvasManager in the scene!");
            }
            return _Instance;
        }
    }

    // assign this in the inspector
    [SerializeField]
    private Canvas _mainCanvas;
    public Canvas MainCanvas { get { return _mainCanvas; } }
    // _mainCanvas field is private with a public getter property to ensure it is read-only.

    //A list containing all the alerts currently in scene
    public List<GameObject> allAlerts;

    public void UpdateAllAlerts(GameObject alert) //Updates the list of alerts currently in scene
    {
        if (allAlerts.Contains(alert)) //If the list already contains the alert
        {
            allAlerts.Remove(alert); //Remove it
        }
        else //Else
        {
            allAlerts.Add(alert);//Add it to the List
        }
    }

    public void SetActiveAlerts(bool val)//Sets all alerts in the scene to the bool value
    {
        foreach(GameObject alert in allAlerts)//For every alert in the allAlerts List
        {
            alert.SetActive(val);//Set the active value to the bool value
        }
    }
}

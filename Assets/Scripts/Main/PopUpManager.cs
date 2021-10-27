using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PopUpManager : MonoBehaviour
{
    [Serializable]
    public class Mission
    {
        public string title; //The title of the mission
        public string description; //The description of the mission
        public int proceedSceneIndex;//Based off the scene's build index. If set to the current scene, no scene transitions will occur
    }

    //Stolen from https://answers.unity.com/questions/1463832/how-to-reference-the-canvas.html
    // singleton static reference
    private static PopUpManager _Instance;
    public static PopUpManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = Resources.FindObjectsOfTypeAll<PopUpManager>()[0];
                if (_Instance == null)
                {
                    Debug.Log("There is no PopUpManager in the scene!");
                }
            }
            return _Instance;
        }
    }

    public Animator popUpAnimator; //The animator component attached to the Pop Up Gameobject

    public TextMeshProUGUI title; // The textmesh component of the title text
    public TextMeshProUGUI description;// The textmesh component of the description text
    public float popUpDelay = 1f;// a delay that allows the close animation to play before the pop up is disabled

    public Animator transitionAnimator; //The animator component attached the the SceneTransition gameobject
    public float sceneTransitionDelay = 2f;// a delay that allows the close animation to play before the scene changes

    public Mission mission;// the current mission details, updated via IconDisplay
    public void SetDetails(Mission nextMission) //Updates the Pop Up UI with the current mission details
    {
        //Updates the current Mission details
        mission.title = nextMission.title; 
        mission.description = nextMission.description;
        mission.proceedSceneIndex = nextMission.proceedSceneIndex;

        //Updates the pop up UI with the current mission details
        title.text = mission.title;
        description.text = mission.description;
    }
    public void Proceed() //Triggered via the proceed button's button component's onclick
    {
        StartCoroutine(ProceedEnu()); //Starts the ProceedEnu IEnumerator
    }
    IEnumerator ProceedEnu()
    {
        if (SceneManager.GetActiveScene().buildIndex != mission.proceedSceneIndex) //If transitioning to a new scene
        {
            transitionAnimator.SetTrigger("NextScene"); //Trigger the scenetransition close animation
            yield return new WaitForSeconds(sceneTransitionDelay);//wait for it to complete
            SceneManager.LoadScene(mission.proceedSceneIndex); //Load the next scene
        }
        else
        {
            Close(); //Close the popup
        }
    }
    public void Close()
    {
        StartCoroutine(CloseEnu()); //Starts the CloseEnu IEnumerator
    }
    IEnumerator CloseEnu()
    {
        popUpAnimator.SetTrigger("Exit"); //Triggers the pop up's close animation
        yield return new WaitForSeconds(popUpDelay); //waits for it to complete
        CanvasManager.Instance.SetActiveAlerts(true); //re-enables all alerts
        transform.gameObject.SetActive(false); //Disables the pop up
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour
{
    [Header("Canvas Elements")]
    
    public Vector3 startingMainCamRotation;
    public float camRotationSpeed;
    Camera cam;
    public Combat com;
    [Header("Transition to Level Select")]
    public Vector3 transitionPosition;
    public Vector3 transitionRotation;
    public float transitionTime;
    public float transitionDelay;

    [Header("Transition to Greenhouse Level")]
    public Vector3 transitionPosition2;
    public Vector3 transitionRotation2;
    public float transitionTime2;
    public float transitionDelay2;

    bool inMain = true;





    [Header("Canvas Elements")]
    public Canvas canvas;
    [Header("Start Button")]
    public GameObject startButton;
    public Animator startButtonAnimator;
    [Header("Start Header")]
    public GameObject startHeader;
    public Animator startHeaderAnimator;
    [Header("Scene Transition")]
    public GameObject sceneTransition;
    public Animator sceneTransitionAnimator;
    public float sceneTransitionStartDelay;
    public float sceneTransitionEndDelay;
    [Header("Level Select")]
    public GameObject levelSelect;
    public Animator levelSelectAnimator;
    public float levelSelectDelay;

    public void BackToMain()
    {
        inMain = true;
        cam.transform.localPosition = new Vector3(0, 0, -2);
        cam.transform.localEulerAngles = Vector3.zero;
        com.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
        Start();
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        StartCoroutine(StartEnu());
    }

    IEnumerator StartEnu()
    {
        yield return new WaitForSeconds(sceneTransitionStartDelay);
        startButton.SetActive(true);
        startHeader.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (inMain)
        {
            float oldY = startingMainCamRotation.y;
            float newY = (oldY + camRotationSpeed) % 360;
            startingMainCamRotation = new Vector3(startingMainCamRotation.x, newY, 0);
            transform.eulerAngles = startingMainCamRotation;
        }
    }
    public void PressButton()
    {
        startButtonAnimator.SetTrigger("Press");
        startHeaderAnimator.SetTrigger("End");
        MoveCamera(1);
    }
    public void PressLevel()
    {
        StartCoroutine(PressLevelEnu());
    }
    IEnumerator PressLevelEnu()
    {
        levelSelectAnimator.SetTrigger("Press");
        yield return new WaitForSeconds(levelSelectDelay);
        sceneTransitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(sceneTransitionStartDelay);
        MoveCamera(2);
        yield return new WaitForSeconds(sceneTransitionEndDelay);
        canvas.gameObject.SetActive(false);
        com.gameObject.SetActive(true);

    }
    public void MoveCamera(int code)
    {
        if (code == 1)
        {
            StartCoroutine(MoveCameraEnu(transitionPosition, transitionRotation, transitionTime, transitionDelay, 1));
        }
        else if (code == 2)
        {
            StartCoroutine(MoveCameraEnu(transitionPosition2, transitionRotation2, transitionTime2, transitionDelay2, 2));
        }
    }

    IEnumerator MoveCameraEnu(Vector3 pos, Vector3 rot, float t, float delay, int code)
    {
        yield return new WaitForSeconds(delay);
        inMain = false;
        float time = 0f;

        Vector3 startingCamPos = cam.transform.position;
        Quaternion startingCamRot = cam.transform.rotation;
        while (time < t)
        {
            time += Time.deltaTime;
            cam.transform.position = Vector3.Slerp(startingCamPos, pos, time/t);
            cam.transform.rotation = Quaternion.Lerp(startingCamRot, Quaternion.Euler(rot), time/t);
            yield return new WaitForEndOfFrame();
        }

        if (code == 1)
        {
            startButton.SetActive(false);
            startHeader.SetActive(false);
            levelSelect.SetActive(true);
        }
        else if (code == 2)
        {
            levelSelect.SetActive(false);
        }
        
        yield break;
    }
}

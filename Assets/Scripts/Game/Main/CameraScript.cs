using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    

    [Header("Canvas Elements")]
    public Vector3 startingMainCamRotation;
    public float camRotationSpeed;
    public Camera cam;

    public Vector3 transitionPosition;
    public Vector3 transitionRotation;
    public float transitionTime;
    public float transitionDelay;

    
    bool inMain = true;





    [Header("Canvas Elements")]
    
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
    // Start is called before the first frame update
    void Start()
    {
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
    }
    public void PressLevel()
    {
        StartCoroutine(PressLevelEnu());
    }
    IEnumerator PressLevelEnu()
    {
        levelSelectAnimator.SetTrigger("Press");
        yield return new WaitForSeconds(sceneTransitionEndDelay);
        sceneTransitionAnimator.SetTrigger("End");
    }
    public void MoveCamera()
    {
        StartCoroutine(MoveCameraEnu(transitionPosition, transitionRotation, transitionTime, transitionDelay));
    }

    IEnumerator MoveCameraEnu(Vector3 pos, Vector3 rot, float t, float delay)
    {
        yield return new WaitForSeconds(delay);
        inMain = false;
        float time = 0f;

        Vector3 startingCamPos = cam.transform.position;
        Quaternion startingCamRot = cam.transform.rotation;
        while (time < t)
        {
            time += Time.deltaTime;
            cam.transform.position = Vector3.Slerp(startingCamPos, pos, time);
            cam.transform.rotation = Quaternion.Lerp(startingCamRot, Quaternion.Euler(rot), time);
            yield return new WaitForEndOfFrame();
        }
        startButton.SetActive(false);
        startHeader.SetActive(false);
        levelSelect.SetActive(true);
        yield break;
    }
}

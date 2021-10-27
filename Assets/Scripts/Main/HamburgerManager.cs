using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerManager : MonoBehaviour
{
    public GameObject hamburgerMenu; //The Actual hamburger Menu Gameobject
    public Animator hamburgerAnimator;// The animator component attached to the hamburger menu
    public float closeAnimDuration = 1f;// The delay before the component is disabled, to allow for the close animation to play
    public void Close()
    {
        StartCoroutine(CloseEnu()); //Starts the CloseEnu IEnumerator
    }
    //Note: The hamburger menu is opened by the Hamburger Menu Button Component's onClick.
    IEnumerator CloseEnu()
    {
        hamburgerAnimator.SetTrigger("Close"); //Triggers the close animation
        yield return new WaitForSeconds(closeAnimDuration);// Waits for the delay
        hamburgerMenu.SetActive(false);// Disables the hamburger Menu
    }
}

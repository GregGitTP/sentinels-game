using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINavigation : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject eventsPanel;
    public GameObject shopPanel;
    public GameObject gamePanel;
    public GameObject seedStorePanel;

    public void navGoHome(){
        homePanel.SetActive(true);
        eventsPanel.SetActive(false);
        shopPanel.SetActive(false);
        gamePanel.SetActive(false);
        seedStorePanel.SetActive(false);
    }
    public void navGoEvents(){
        homePanel.SetActive(false);
        eventsPanel.SetActive(true);
        shopPanel.SetActive(false);
        gamePanel.SetActive(false);
        seedStorePanel.SetActive(false);
    }
    public void navGoShop(){
        homePanel.SetActive(false);
        eventsPanel.SetActive(false);
        shopPanel.SetActive(true);
        gamePanel.SetActive(false);
        seedStorePanel.SetActive(false);
    }
    public void navGoGame(){
        homePanel.SetActive(false);
        eventsPanel.SetActive(false);
        shopPanel.SetActive(false);
        gamePanel.SetActive(true);
        seedStorePanel.SetActive(false);
    }
    public void openSeedStore(){
        seedStorePanel.SetActive(true);
    }
}

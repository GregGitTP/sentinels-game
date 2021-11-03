using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeManager : MonoBehaviour
{
    public GameObject dQuest;
    public GameObject dQTitle;
    public GameObject dQDescription;
    public GameObject dQTime;
    public GameObject dQCompRed;
    public GameObject dQCompTxt;
    public int dQReward=10;

    TextMeshProUGUI dQTitleText;
    TextMeshProUGUI dQDescriptionText;
    TextMeshProUGUI dQTimeText;
    Button dQBtn;
    int maxTime=5;//24*60*60;
    int currTime;
    int currQuest;
    string[][] quests;

    private void Start(){
        dQTitleText=dQTitle.GetComponent<TextMeshProUGUI>();
        dQDescriptionText=dQDescription.GetComponent<TextMeshProUGUI>();
        dQTimeText=dQTime.GetComponent<TextMeshProUGUI>();
        dQBtn=dQuest.GetComponent<Button>();

        quests=new string[3][];
        quests[0]=new string[]{"This is a test title","This is just a test description for testing"};
        quests[1]=new string[]{"This is another test title","This is the description for the second quest in this list"};
        quests[2]=new string[]{"",""};

        SetupQuest();
    }

    private void SetupQuest(){
        dQTitleText.text=quests[currQuest][0];
        dQDescriptionText.text=quests[currQuest][1];
        dQTimeText.text="Time Left: 24:00:00";
        currTime=maxTime;
        dQCompRed.SetActive(false);
        dQCompTxt.SetActive(false);
        dQBtn.enabled=false;
        StartCoroutine(CountDown());
    }
    private void WaitForCompletion(){
        dQCompRed.SetActive(true);
        dQCompTxt.SetActive(true);
        dQBtn.enabled=true;
    }
    public void CompleteQuest(){
        RewardSeeds(dQReward);
        if(currQuest<quests.Length-1) currQuest++;
        else currQuest=0;
        SetupQuest();
    }
    private void RewardSeeds(int amt){
        //ADD REWARDS HERE
    }
    IEnumerator CountDown(){
        while(currTime>0){
            int _hours=Mathf.FloorToInt(currTime/(60*60));
            int _mins=Mathf.FloorToInt((currTime/60)%60);
            int _secs=Mathf.FloorToInt(currTime%60);
            string hours=_hours<10?"0"+_hours.ToString():_hours.ToString();
            string mins=_mins<10?"0"+_mins.ToString():_mins.ToString();
            string secs=_secs<10?"0"+_secs.ToString():_secs.ToString();
            dQTimeText.text="Time Left: "+hours+":"+mins+":"+secs;
            currTime-=1;
            yield return new WaitForSeconds(1);
        }
        dQTimeText.text="Time Left: 00:00:00";
        WaitForCompletion();
    }
}

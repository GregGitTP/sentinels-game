using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    public Seeds instance;
    int total=0;
    
    private void Awake(){
        if(instance!=null&&instance!=this)Destroy(this.gameObject);
        else instance=this;
    }
    public int GetAmount(){return total;}
    public void Add(int amt){total+=amt;}
    public void Deduct(int amt){total-=amt;}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Criteria", menuName = "Conversation System/Criteria")]
public class Criteria : ScriptableObject
{
    public enum TypeOpt { Story, Item }
    public TypeOpt typeSelect;

    public string info;

    public SetManager SMR;
    public void setSMR()
    {
        SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();
    }

    public bool CriteriaMet(){
        if (typeSelect == TypeOpt.Story){
            if (SMR.GetChapter() == int.Parse(info)){
                return true;
            } else {
                return false;
            }
        } if (typeSelect == TypeOpt.Item) {
            // check items
            if (SMR.hasItem(info)){
                return true;
            } else {
                return false;
            }
        }

        return false;
    }
}

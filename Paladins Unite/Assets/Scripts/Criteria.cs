using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Criteria", menuName = "Conversation System/Criteria")]
public class Criteria : ScriptableObject
{
    public enum TypeOpt { Story, Item, Name, StoryPast }
    public TypeOpt typeSelect;

    public string info;

    public bool reverse = false;

    public bool CriteriaMet(){
        bool temp = CriteriaCheck();
        if (reverse){
            return !temp;
        } else {
            return temp;
        }
    } private bool CriteriaCheck(){
        if (typeSelect == TypeOpt.Story){
            if (SettingsManager.GetChapter() == int.Parse(info)){
                return true;
            } else {
                return false;
            }
        } if (typeSelect == TypeOpt.Item) {
            // check items
            if (SettingsManager.hasItem(info)){
                return true;
            } else {
                return false;
            }
        } if (typeSelect == TypeOpt.Name) {
            // check items
            if (SettingsManager.specificCriteria(info)){
                return true;
            } else {
                return false;
            }
        } if (typeSelect == TypeOpt.StoryPast){
            if (SettingsManager.GetChapter() >= int.Parse(info)){
                return true;
            } else {
                return false;
            }
        }

        return false;
    }
}

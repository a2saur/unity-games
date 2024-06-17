using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndActionManager : MonoBehaviour
{
    public void setSpecificCriteria(string criteriaName){
        SettingsManager.setSpecificCriteria(criteriaName);
    }

    public void Pause(){
        SettingsManager.Pause();
    }

    public void Resume(){
        SettingsManager.Resume();
    }
}

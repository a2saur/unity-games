using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    public enum InteractionType
    {
        Dialogue,
        Pickup,
        NA
    }

    public InteractionType interactionType;
    public bool commandable;
    public Dialogue lines;

    public List<KeyCode> getApplicableKeys(){
        List<KeyCode> applicableKeys = new List<KeyCode>();
        if (interactionType != InteractionType.NA){
            // interactable button is applicable
            applicableKeys.Add(SettingsManager.interactButton);
        }

        if (commandable){
            // command button is applicable
            applicableKeys.Add(SettingsManager.commandButton);
        }

        return applicableKeys;
    }

    public List<string> getApplicableKeyNames(){
        List<string> applicableKeys = new List<string>();
        if (interactionType != InteractionType.NA){
            // interactable button is applicable
            applicableKeys.Add("Interact");
        }

        if (commandable){
            // command button is applicable
            applicableKeys.Add("Command");
        }

        return applicableKeys;
    }

    public string getInteractionType(){
        if (interactionType == InteractionType.Dialogue){
            // interactable button is applicable
            return "Dialogue";
        } else {
            return "?";
        }
    }

    public Dialogue getDialogue(){
        if (interactionType == InteractionType.Dialogue){
            return lines;
        } else {
            return null;
        }
    }
}

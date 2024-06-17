using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Conversation", menuName = "Conversation System/Conversation")]
public class Conversation : ScriptableObject
{
    public Criteria[] criteriaList;
    public UnityEvent endActions;

    public string[] lines;
    public NPC npcAttached;

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CriteriaMet(){
        if (criteriaList.Length == 0){
            return true;
        } else {
            for (int i = 0; i < criteriaList.Length; i++){
                if (criteriaList[i].CriteriaMet()){
                    // continue
                } else {
                    return false;
                }
            }
            return true;
        }
    }

    public string[] getLines(){
        return lines;
    }

    public UnityEvent getEndActions(){
        return endActions;
    }

    public void setNPC(NPC npc){
        npcAttached = npc;
    }

    // public void NPCJump(){
    //     npcAttached.Jump();
    // }
}

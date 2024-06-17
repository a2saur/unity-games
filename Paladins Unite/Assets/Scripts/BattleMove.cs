using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleMove", menuName = "Battles/BattleMove")]
public class BattleMove : ScriptableObject
{
    public enum Targetable { Enemies, Ally, Self, AllEnemies }
    public Targetable targets;
    public string moveName;
    public bool selecting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

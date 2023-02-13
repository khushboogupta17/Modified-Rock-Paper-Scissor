using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    [System.Serializable]
    public enum Strengths
    {
        Cover,
        Crush,
        Cut,
        Vaporize,
        Smash,
        Disprove,
        Eat,
        Poison,
        Decapicitate,
        CantAttack,
        Draw,
        Default
    }
    
    [System.Serializable]
    public enum AttackModes
    {
        Rock,
        Paper,
        Scissors,
        Spock,
        Lizard,
        Default
    }
    
    public List<AttackModeInfo> AttackModesInfo;
    public int LevelResetBreak = 2;
    public static GameDatabase Instance;
    

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
       
    }

    public AttackModeInfo GetAttackModeInfo(AttackModes attackModeName)
    {
      return AttackModesInfo.Find(x => x.Name == attackModeName);
        
    }
}

[System.Serializable]
public class AttackModeInfo
{
    [NotNull]
    public GameDatabase.AttackModes Name;
    public List<OpponentInfo> OpponentInfos = new List<OpponentInfo>();

    [System.Serializable]
    public class OpponentInfo
    {
        public GameDatabase.AttackModes OpponentName;
        public GameDatabase.Strengths AttackedByStrength;
    }


}


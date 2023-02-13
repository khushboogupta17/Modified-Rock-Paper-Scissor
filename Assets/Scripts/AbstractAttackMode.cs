
using DefaultNamespace;
using UnityEngine;

public abstract class AbstractAttackMode: MonoBehaviour
{
    public GameDatabase.AttackModes AttackModeName;
    private int _deltaScore = 1;
    public AttackModeInfo AttackModeInfo;
    
    public void SetAttackModeInfo(AttackModeInfo attackModeInfo)
    {
        AttackModeInfo = attackModeInfo;
        AttackModeName = attackModeInfo.Name;
    }
      

}

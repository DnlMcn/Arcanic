using UnityEngine;

[CreateAssetMenu(fileName = "New Troop", menuName = "Scriptable Objects/Troop")]
public class TroopSO : ScriptableObject
{
    public string troopName;

    public TroopLevelSO levelBeast2;
    public TroopLevelSO levelBeast1;
    public TroopLevelSO levelBase;
    public TroopLevelSO levelMachine1;
    public TroopLevelSO levelMachine2;

    public TroopLevelSO GetTroopLevel(int level)
    {
        switch (level)
        {
            case -2:
                return levelBeast2 == null ? levelBeast2 : levelBase;
            case -1:
                return levelBeast1 == null ? levelBeast1 : levelBase;
            case 0:
                return levelBase;
            case 1:
                return levelMachine1 == null ? levelMachine1 : levelBase;
            case 2:
                return levelMachine2 == null ? levelMachine2 : levelBase;
        }
        return levelBase;
    }
}

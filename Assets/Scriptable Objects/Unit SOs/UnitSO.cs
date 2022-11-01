using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Objects/Unit")]
public class UnitSO : ScriptableObject
{
    public string UnitName;

    public UnitLevelSO levelBeast2;
    public UnitLevelSO levelBeast1;
    public UnitLevelSO levelBase;
    public UnitLevelSO levelMachine1;
    public UnitLevelSO levelMachine2;

    public UnitLevelSO GetUnitLevel(int level)
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

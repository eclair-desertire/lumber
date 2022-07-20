using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AxeScriptableObject", order = 1)]
public class AxeScriptableObject : ScriptableObject
{
    public int maxCapacity = 50;
    public int currentCapacity = 50;
    public int price = 50;
    public int attack = 10;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade : ScriptableObject
{
    public int cost = 100;
    public string description;
    public string title;
    public abstract void ApplyUpgrade(Turret turret);
}

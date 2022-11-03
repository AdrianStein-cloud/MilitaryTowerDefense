using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public int cost = 100;
    public string desctription;
    public string title;

    public abstract void ApplyUpgrade(Turret turret);
}

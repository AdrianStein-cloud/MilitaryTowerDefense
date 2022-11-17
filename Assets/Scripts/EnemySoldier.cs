using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : Enemy
{
    private float _health = 5;
    private float _maxHealth = 5;
    public override int damagePoint => 5;
    public override float speed => 5;
    public override float health{
        get { return _health; }
        set { _health = value; }
    }
    public override float maxHealth{
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
}

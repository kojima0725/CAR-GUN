﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC車両のインターフェース
/// </summary>
public class NCar : MonoBehaviour
{
    protected float hp;
    protected bool dead;
    protected RoadChip currentRoadChip;

    public bool IsDead => dead;
    public void GetDamage(float damage)
    {
        if (dead)
        {
            return;
        }
        hp -= damage;
        if (hp < 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        dead = true;
    }
}

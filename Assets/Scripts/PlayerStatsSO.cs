using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats_SO", menuName = "ScriptableObjects/Player")]
public class PlayerStatsSO : ScriptableObject
{
    public int health;
    public int damage;
    public int level;
    public int exp;
}

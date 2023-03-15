using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Status : MonoBehaviour
{
    public string name_monster;
    public string birthplace;
    public int HP;
    public float energy;
    public int atk;
    [SerializeField]
    private int speed;

    public int GetHP()
    {
        return HP;
    }
    public int GetAttack()
    {
        return atk;
    }

}

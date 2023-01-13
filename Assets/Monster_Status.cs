using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Status : MonoBehaviour
{
    public string name_monster;
    public string birthplace;
    public float HP;
    public float energy;
    [SerializeField]
    private int atk;
    [SerializeField]
    private int speed;
}

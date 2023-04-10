using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Status Effect")]
public class StatusEffectData : ScriptableObject
{
    public Sprite icon;
    public string effect_name;
    public int DOT_Amount;
    public int TickSpeed;
    public int MovementPenalty;
    public int MovementPenaltyPercent;
    public int Lifetime;

    [TextArea]
    public string EffectDetails;
    [TextArea]
    public string description;
}

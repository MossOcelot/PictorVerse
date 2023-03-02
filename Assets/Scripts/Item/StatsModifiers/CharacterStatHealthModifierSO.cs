using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerStatus player = character.gameObject.GetComponent<PlayerStatus>();
        if(player != null)
        {
            player.setHP((int)val);
        }
    }
}

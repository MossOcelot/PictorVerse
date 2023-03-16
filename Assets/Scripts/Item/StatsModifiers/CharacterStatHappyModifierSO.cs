using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHappyModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerStatus player = character.gameObject.GetComponent<PlayerStatus>();

        if(player != null)
        {
            float newStaticHappy = player.getMyStatic().static_happy + val;
            player.setMyStatic(6, newStaticHappy);
        }
    }
}

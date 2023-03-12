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
            if (player.getHP() + (int)val > player.getMaxHP())
            {
                int maxval = player.getMaxHP() - player.getHP();
                player.setHP(maxval);
            } else
            {
                player.setHP((int)val);
            }

            if (player.getEnergy() + (int)val > player.getMaxEnergy())
            {
                int maxval = player.getMaxEnergy() - player.getEnergy();
                player.setEnergy(maxval);
            } else
            {
                player.setEnergy((int)val);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusEffectController : MonoBehaviour
{
    public UIStatusEffectBar statusEffectUI;
    [SerializeField]
    private PlayerStatus playerStatus;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private List<StatusEffectPlayer> statusEffects;
    public GameObject animationHealing;
    int oldCount;
    int oldTime;
    // Start is called before the first frame update
    private void Start()
    {
        UpdateData();
    }

    private void Update()
    {
        int Count = statusEffects.Count;
        if (Count != oldCount)
        {
            UpdateData();
            oldCount = Count;
        }

        int[] time = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>().getDateTime();
        if (oldTime != time[4])
        {
            CheckEffect();
            oldTime = time[4];
        }
    }

    public void CheckEffect()
    {
        if (statusEffects.Count == 0) return;
        List<StatusEffectPlayer> data = statusEffects;
        foreach (StatusEffectPlayer statusEffect in data)
        {
            statusEffect.current_time++;
            if (statusEffect.data.TickSpeed != 0)
            {
                if (statusEffect.current_time % statusEffect.data.TickSpeed == 0)
                {
                    playerStatus.setHP(-statusEffect.data.DOT_Amount);
                }
            }

            if (!statusEffect.StatusSlow)
            {
                float defaultMoveSpeed = playerMovement.getDefaultMoveSpeed();
                float realMoveSpeed = (defaultMoveSpeed - (float)statusEffect.data.MovementPenalty) * ((100f - (float)statusEffect.data.MovementPenaltyPercent) / 100f);

                Debug.Log("Default: " + defaultMoveSpeed + " MoveSpeed " + realMoveSpeed);
                if (realMoveSpeed < 1)
                {
                    playerMovement.setDefaultMoveSpeed(1f);
                }
                else
                {
                    playerMovement.setDefaultMoveSpeed(realMoveSpeed);
                }
                statusEffect.StatusSlow = true;
            }


            if (statusEffect.current_time >= statusEffect.data.Lifetime)
            {
                int index = statusEffects.IndexOf(statusEffect);

                // fix default Speed Max
                playerMovement.setDefaultMoveSpeed(5);
                statusEffects.RemoveAt(index);
            }
        }
    }

    private void UpdateData()
    {
        PreparestatusEffectsUI();

        int len = statusEffects.Count;
        for (int i = 0; i < len; i++)
        {
            StatusEffectPlayer status = statusEffects[i];
            statusEffectUI.UpdateData(i, status);
        }
        oldCount = len;
    }
    private void PreparestatusEffectsUI()
    {
        statusEffectUI.InitializeUIStatusEffect(statusEffects.Count);
        statusEffectUI.OnDescriptionRequested += HandleItemDescriptionRequest;
        statusEffectUI.OnHideDescriptionRequested += HandleHideItemDescriptionRequest;
    }

    private void HandleItemDescriptionRequest(int index)
    {
        StatusEffectPlayer statusEffect = statusEffects[index];

        statusEffectUI.showItemDescriptionAction(index);
        statusEffectUI.AddDescription(statusEffect);
    }

    private void HandleHideItemDescriptionRequest(int index)
    {
        statusEffectUI.AllDestroyDescription();
    }
    public void AddStatus(StatusEffectPlayer statusEffect)
    {
        if (CheckHasStatus(statusEffect)) return;
        statusEffects.Add(statusEffect);
    }

    private bool CheckHasStatus(StatusEffectPlayer statusEffect)
    {
        foreach(StatusEffectPlayer effect in statusEffects)
        {
            if(effect.data.effect_name == statusEffect.data.effect_name)
            {
                effect.current_time = 0;
                return true;
            }
        }
        return false;
    }

    public List<StatusEffectPlayer> GetstatusEffects()
    {
        return statusEffects;
    }

    public void ClearEffects()
    {
        playerMovement.setDefaultMoveSpeed(5);
        statusEffects.Clear();
        StartCoroutine((IEnumerator)LoadAnimationHealer());
            }
    private IEnumerator LoadAnimationHealer()
    {
        animationHealing.SetActive(true);
        yield return new WaitForSeconds(3f);
        animationHealing.SetActive(false);
    }
}

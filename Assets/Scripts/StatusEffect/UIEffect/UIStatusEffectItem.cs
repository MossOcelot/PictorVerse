using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIStatusEffectItem : MonoBehaviour
{
    public StatusEffectData status_effect;
    public Image icon;
    public static UIStatusEffectItem Instance { get; private set; }

    public event Action<UIStatusEffectItem> OnEnterMouseBtn, OnExitMouseBtn;
    int Life_time;
    int current_time;
    public int old_time;
    public void Awake()
    {
        ResetData();
    }

    public void SetData(StatusEffectPlayer status_effect)
    {
        this.status_effect = status_effect.data;
        icon.sprite = status_effect.data.icon;
        float per = GetPercent();
        Life_time = status_effect.data.Lifetime;
        current_time = status_effect.current_time;


        icon.fillAmount = per;
    }

    public void Update()
    {
        int[] times = GameObject.FindGameObjectWithTag("Time").gameObject.GetComponent<Timesystem>().getDateTime();
        if (times[4] != old_time)
        {
            current_time++;
            float per = GetPercent();
            icon.fillAmount = per;
            old_time = times[4];
        }
    }
    private float GetPercent()
    {
        return 1 - ((float)current_time / (float)Life_time);
    }
    public void ResetData()
    {
        status_effect = null;
    }

    public void OnPointerEnter()
    {
        OnEnterMouseBtn?.Invoke(this);
    }

    public void OnPointerExit()
    {
        OnExitMouseBtn?.Invoke(this);
    }
}

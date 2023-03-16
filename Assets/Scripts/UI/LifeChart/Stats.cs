using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public event EventHandler OnStatsChanged;

    public static int STAT_MIN = 0;
    public static int STAT_MAX = 20;

    private SingleStat credibilityStat;
    private SingleStat stabilityStat;
    private SingleStat healthStat;
    private SingleStat happinessStat;
    private SingleStat riskStat;

    public enum Type
    {
        Credibility,
        Stability,
        Health,
        Happiness,
        risk
    }

    public Stats(int credibilityStat, int stabilityStat, int healthStat, int happinessStat, int riskStat)
    {
        this.credibilityStat = new SingleStat(credibilityStat);
        this.stabilityStat = new SingleStat(stabilityStat);
        this.healthStat = new SingleStat(healthStat);
        this.happinessStat = new SingleStat(happinessStat);
        this.riskStat = new SingleStat(riskStat);
    }

    private SingleStat GetSingleStat(Type statType)
    {
        switch (statType)
        {
            default:
            case Type.Stability: return this.stabilityStat;
            case Type.Credibility: return this.credibilityStat;
            case Type.Health: return this.healthStat;
            case Type.Happiness: return this.happinessStat;
            case Type.risk: return this.riskStat;
        }
    }

    public void SetStatAmount(Type statType, int statAmount)
    {
        GetSingleStat(statType).SetStatAmount(statAmount);
        if (OnStatsChanged != null) OnStatsChanged(this, EventArgs.Empty);
    }

    public int GetStatAmount(Type statType)
    {
        return GetSingleStat(statType).GetStatAmount();
    }

    public float GetStatAmountNormalized(Type statType)
    {
        return GetSingleStat(statType).GetStatAmountNormalized();
    }


    private class SingleStat
    {
        private int stat;

        public SingleStat(int statAmount)
        {
            SetStatAmount(statAmount);
        }
        public void SetStatAmount(int statAmount)
        {
            stat = Mathf.Clamp(statAmount, STAT_MIN, STAT_MAX);
        }

        public int GetStatAmount()
        {
            return stat;
        }

        public float GetStatAmountNormalized()
        {
            return (float)stat / STAT_MAX;
        }
    }
}

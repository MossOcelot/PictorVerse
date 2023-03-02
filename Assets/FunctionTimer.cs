using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    private Action action;
    private float timer;
    private float real_timer;

    public FunctionTimer(Action action, float timer)
    {
        this.action = action;
        this.timer = timer;
        this.real_timer = timer;
    }

    public void Update()
    {
        real_timer -= Time.deltaTime;
        if(real_timer < 0)
        {
            action();
            real_timer = timer;
        }
    }
}

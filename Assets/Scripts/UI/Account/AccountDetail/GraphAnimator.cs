using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GraphAnimator : MonoBehaviour
{
    public UILineRenderer[] lines;

    public float time = 1f;

    private void Start()
    {
        // AnimateLines();
    }

    public void AnimateLines()
    {
        foreach(UILineRenderer line in lines)
        {
            AnimateLine(line);
        }
    }

    void AnimateLine(UILineRenderer line)
    {
        Vector2[] points = line.points;

        Animate(line, points);
    }

    public void Animate(UILineRenderer line, Vector2[] points)
    {
        line.points = new Vector2[] { };

        for(int i=0; i<points.Length; i++)
        {
            int index = i;
            AnimatePoint(line, index, new Vector2(0, 4), points[index]);
        }
    }

    void AnimatePoint(UILineRenderer line, int index, Vector2 start, Vector2 end) 
    {
        LeanTween.delayedCall(time * index, () =>
        {
            if (index > 0)
            {
                start = line.points[index - 1];
                line.points[index] = start;
            }
            else
            {
                line.points[index] = start;
            }

            LeanTween.value(gameObject, (value) =>
            {
                line.points[index] = value;
                line.SetVerticesDirty();
            }, start, end, time);
        });
    }
}

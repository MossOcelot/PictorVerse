using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UILineArrowController : MonoBehaviour
{
    [System.Serializable]
    public class LineArrowShow
    {
        public string name;
        public GameObject[] Arrows;
        public int timer;
    }

    public List<LineArrowShow> ArrowShows;

    public bool finish = true;

    public void SetFinish(bool finish)
    {
        this.finish = finish;
    }
    public void Update()
    {
        if (finish)
        {
            StartCoroutine(AutoShowLineArrow());
        }
    }

    IEnumerator AutoShowLineArrow()
    {
        finish = false;
        int count = ArrowShows.Count;

        for(int i = 0; i < count; i++)
        {
            Debug.Log("Round : " + i);
            if( i != 0)
            {
                foreach(GameObject arrow in ArrowShows[i - 1].Arrows)
                {
                    arrow.SetActive(false);
                }
            }
            foreach(GameObject arrow in ArrowShows[i].Arrows)
            {
                arrow.SetActive(true);
            }
            yield return new WaitForSeconds(ArrowShows[i].timer);
        }

        finish = true;
    }
}

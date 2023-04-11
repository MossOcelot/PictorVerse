using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIActiveIcon : MonoBehaviour
{
    public int index_page;
    public Image line;
    float all_page = 3;

    public Image[] icons;
    public Sprite[] icon_active;
    public Sprite[] icon_inactive;

    public void Previous()
    {
        index_page--;
        UpdateActive();
    }

    public void Next()
    {
        index_page++;
        UpdateActive();
    }

    private void UpdateActive()
    {
        float page = (float)index_page;
        float per = page / 2;
        line.fillAmount = per;

        if(page == 0)
        {
            icons[0].sprite = icon_inactive[0];
            icons[1].sprite = icon_inactive[1];
        } else if(page == 1)
        {
            icons[0].sprite = icon_active[0];
            icons[1].sprite = icon_inactive[1];
        } else if(page == 2)
        {
            icons[1].sprite = icon_active[1];
        }
    }
}

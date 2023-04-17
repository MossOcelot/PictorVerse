using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UINotiBox : MonoBehaviour
{
    public Image BG;
    public Image icon;
    public TextMeshProUGUI text;

    public void SetData(string title)
    {
        //this.icon.sprite = icon;
        this.text.text = title;
        StartCoroutine(alertColor());
    }

    private IEnumerator alertColor()
    {
        for(int i = 0; i < 5; i++)
        {
            this.icon.color = Color.red;
            this.text.color = Color.white;
            yield return new WaitForSeconds(1);
            this.icon.color = Color.white;
            this.text.color = Color.black;
            yield return new WaitForSeconds(1);
        }
    }
}

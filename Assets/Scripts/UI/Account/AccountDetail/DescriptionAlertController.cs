using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionAlertController : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus playerStatus;
    public GameObject Alert_template;
    GameObject Alert;

    public GameObject Alert_Description;

    [SerializeField]
    private List<AlertDescription> alert_description;
    public Transform content;
    public Transform description_content;

    int oldCount;
    // Start is called before the first frame update
    private void Start()
    {
        UpdateData();
    }

    private void Update()
    {
        int Count = alert_description.Count;

        if (Count != oldCount)
        {
            UpdateData();
            oldCount = Count;
        }
    }

    private void UpdateData()
    {
        ClearData();
        foreach (AlertDescription alert in alert_description)
        {
            Alert = Instantiate(Alert_template, content);
            UIAlertCircle uiAlert = Alert.GetComponent<UIAlertCircle>();
            uiAlert.SetData(alert);
            Alert.GetComponent<Button>().AddEventListener(alert, OnClick);
        }
    }

    private void ClearData()
    {
        int len = content.childCount;
        for(int i = 0; i < len; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    private void OnClick(AlertDescription i)
    {
        GameObject alertDescription = Instantiate(Alert_Description, description_content);
        UIDescriptionAlertTemplate uiAlertDescription = alertDescription.AddComponent<UIDescriptionAlertTemplate>();
        uiAlertDescription.SetData(i);
    }
}

[System.Serializable]
public class AlertDescription
{
    public enum Alert_type { type1, type2, type3, type4 }
    public Alert_type type;
    public string description;
}
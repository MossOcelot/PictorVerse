using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlanetPage : MonoBehaviour
{
    [SerializeField]
    private UIplanetItem planetPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<UIplanetItem> listOfUIItems = new List<UIplanetItem>();

    public void InitializePlanetUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIplanetItem uiItem = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Navbar : MonoBehaviour
{
    public StockSystem stockSystem;
    public GameObject buttonTemplate;
    public GameObject navbar;
    public GameObject table;
    public GameObject itemPage;

    public GameObject itemTemplate;

    public GameObject UI;
    public GameObject page;

    GameObject CategoryBtn;
    GameObject itemBtn;
    Button CateBtn;
    Button iBtn;
    private void Start()
    {
        int len = stockSystem.stock.Count;
        Debug.Log(len);
        for(int i = 0; i < len; i++)
        {
            Debug.Log(stockSystem.stock[i].item_categories);
            CategoryBtn = Instantiate(buttonTemplate, navbar.transform);

            CategoryBtn.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = stockSystem.stock[i].item_categories;

            CateBtn = CategoryBtn.gameObject.GetComponent<Button>();
            CateBtn.AddEventListener(i, CategoryStock);
        }

    }

    public void CategoryStock(int i)
    {
        List<ItemStock> stock = stockSystem.stock[i].itemStock;
        int len = stock.Count;

        int len_item_table = table.transform.childCount;
        if (len_item_table != 0)
        {
            for(int n = 0; n < len_item_table; n++)
            {
                Destroy(table.transform.GetChild(n).gameObject);
            }
        }

        for (int s = 0; s < len; s++)
        {
            itemBtn = Instantiate(itemTemplate, table.transform);
            itemBtn.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = stock[s].item.icon;
            itemBtn.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = stock[s].item.name;
            itemBtn.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = stock[s].orderBook.marketPrice.ToString("F");
            itemBtn.gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = stock[s].orderBook.quantityItem.ToString();
            iBtn = itemBtn.GetComponent<Button>();
            iBtn.AddEventListener( new int[] { s, i }, OpenitemTable);
        }
    }

    public void OpenitemTable(int[] s)
    {
        UI.SetActive(false);
        page.SetActive(true);
        List<ItemStock> stock = stockSystem.stock[s[1]].itemStock;
        page.gameObject.GetComponent<Page_Manager>().ItemInStock = stock[s[0]];
        page.gameObject.GetComponent<Page_Manager>().setDataInPage();
    }
}

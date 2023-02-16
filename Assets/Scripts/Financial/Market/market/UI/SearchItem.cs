using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchItem : MonoBehaviour
{
    public GameObject Searchbar;
    public GameObject table;
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void Search()
    {
        string searchText = Searchbar.gameObject.GetComponent<InputField>().text;
        int searchTxtlength = searchText.Length;

        int searchElements = 0;
        Debug.Log(searchText);
        int len = table.transform.childCount;
        for (int i = 0 ; i < len; i++)
        {
            searchElements += 1;
            
            if(table.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text.Length >= searchTxtlength)
            {
                if(searchText.ToLower() == table.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text.Substring(0, searchTxtlength).ToLower())
                {
                    table.transform.GetChild(i).gameObject.SetActive(true);
                } else
                {
                    table.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    
}

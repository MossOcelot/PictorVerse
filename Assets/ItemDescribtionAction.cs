using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace inventory.Model
{
    public class ItemDescribtionAction : MonoBehaviour
    {
        [SerializeField]
        private GameObject DescriptionPrefab;
        [SerializeField]
        private GameObject ButtonBar;
        [SerializeField]
        private GameObject buttonPrefab;
        public Transform trans;
        GameObject template;
        public void AddDescription(Sprite icon, string item_name, int quantity, float item_price, string description)
        {
            GameObject descriptionCard = Instantiate(DescriptionPrefab, transform);
            descriptionCard.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = item_name;
            descriptionCard.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = quantity.ToString();
            descriptionCard.gameObject.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = item_price.ToString("F");
            descriptionCard.gameObject.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().text = description;
            descriptionCard.gameObject.transform.GetChild(9).gameObject.GetComponent<Image>().sprite = icon;
            
        }

        public void AddAction(int n,string action_name, Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, trans);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<Text>().text = action_name;
        }

        public void Toggle(bool val)
        {
            RemoveOldDescription();
            gameObject.SetActive(val);
        }

        public void RemoveOldDescription()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }

            foreach (Transform transformChildObjects in trans)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }
    }
}

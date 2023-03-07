using System;
using System.Collections;
using System.Collections.Generic;
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

        GameObject template;
        public void AddDescription(string item_name, int quantity, float item_price, string description)
        {
            
            GameObject descriptionCard = Instantiate(DescriptionPrefab, transform);
            descriptionCard.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = item_name;
            descriptionCard.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = quantity.ToString();
            descriptionCard.gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = item_price.ToString("F");
            descriptionCard.gameObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = description;
            
        }

        public void AddAction(int n,string action_name, Action onClickAction)
        {
            if(n == 0)
            {
                template = Instantiate(ButtonBar, transform);

            }
            Transform trans = template.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform;
            Debug.Log(trans.name);
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
        }
    }
}

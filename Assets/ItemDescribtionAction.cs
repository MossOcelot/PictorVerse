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

        public void AddDescription(string item_name, int quantity, float item_price, string description)
        {
            
            GameObject descriptionCard = Instantiate(DescriptionPrefab, transform);
            descriptionCard.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = item_name;
            descriptionCard.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = quantity.ToString();
            descriptionCard.gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = item_price.ToString("F");
            descriptionCard.gameObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = description;

        }

        public void Toggle(bool val)
        {
            if (val == true)
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

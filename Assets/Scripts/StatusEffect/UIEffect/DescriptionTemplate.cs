using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionTemplate : MonoBehaviour
{
    [SerializeField]
    private GameObject DescriptionPrefab;

    // Start is called before the first frame update
    public void AddDescription(StatusEffectPlayer data)
    {
        GameObject descriptionCard = Instantiate(DescriptionPrefab, transform);
        descriptionCard.gameObject.GetComponent<UIStatusEffectDescription>().SetData(data);
    }
}

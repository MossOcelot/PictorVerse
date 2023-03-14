using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIFillBag : MonoBehaviour
{
    [SerializeField]
    private Image fillbag;
    [SerializeField]
    private TextMeshProUGUI size_status;
    private PlayerMovement player_movement;
    // Start is called before the first frame update
    void Start()
    {
        player_movement = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float weight = player_movement.GetWeight_player();
        float strength = player_movement.GetStrength();
        SetWeight(weight, strength);
        size_status.text = weight.ToString("F") + " / " + strength.ToString("F") + " kg.";
    }

    void SetWeight(float weight, float maxWeight)
    {
        fillbag.fillAmount = weight / maxWeight;
    }
}

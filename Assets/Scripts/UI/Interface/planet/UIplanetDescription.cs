using inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIplanetDescription : MonoBehaviour
{
    [SerializeField]
    private Image planetImage;
    [SerializeField]
    private Image planetSymbol;
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text location;
    [SerializeField]
    private TMP_Text rank;

    [SerializeField]
    private TMP_Text uniqueness;
    [SerializeField]
    private TMP_Text Advantage;
    [SerializeField]
    private TMP_Text Disadvantage;


    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.planetImage.gameObject.SetActive(false);
        this.planetSymbol.gameObject.SetActive(false);
        this.Name.text = "";
        this.location.text = "";
        this.rank.text = "";
        this.uniqueness.text = "";
        this.Advantage.text = "";
        this.Disadvantage.text = "";
    }

    public void SetDescription(Sprite planetImage, Sprite planetSymbol, string planetName,
        string planetLocation, string rank, string unique, 
        string advantage, string disadvantage)
    {
        this.planetImage.gameObject.SetActive(true);
        this.planetImage.sprite = planetImage;
        this.planetSymbol.gameObject.SetActive(true);
        this.planetSymbol.sprite = planetSymbol;
        this.Name.text = planetName;
        this.location.text = planetLocation;
        this.rank.text = rank;
        this.uniqueness.text = unique;
        this.Advantage.text = advantage;
        this.Disadvantage.text = disadvantage;
    }
}
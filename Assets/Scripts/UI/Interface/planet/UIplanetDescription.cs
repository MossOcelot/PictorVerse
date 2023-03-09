using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIplanetDescription : MonoBehaviour
{
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
    [SerializeField]
    private Image resource;


    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.planetSymbol.gameObject.SetActive(false);
        this.Name.text = "";
        this.location.text = "";
        this.rank.text = "";
        this.uniqueness.text = "";
        this.Advantage.text = "";
        this.Disadvantage.text = "";
        this.resource.gameObject.SetActive(false);
    }

    public void SetDescription(Sprite planetSprite, string planetName,
        string planetLocation, string rank, string unique, 
        string advantage, string disadvantage, Sprite resource)
    {
        this.planetSymbol.gameObject.SetActive(true);
        this.planetSymbol.sprite = planetSprite;
        this.Name.text = planetName;
        this.location.text = planetLocation;
        this.rank.text = rank;
        this.uniqueness.text = unique;
        this.Advantage.text = advantage;
        this.Disadvantage.text = disadvantage;
        this.resource.gameObject.SetActive(true);
        this.resource.sprite = resource;
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIplanetItem : MonoBehaviour
{
    [SerializeField]
    private Image planetImage;

    [SerializeField]
    private Image borderImage;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void SetData(Sprite sprite)
    {
        this.planetImage.gameObject.SetActive(true);
        this.planetImage.sprite = sprite;
        this.empty = false;
    }

    public void ResetData()
    {
        this.planetImage.gameObject.SetActive(false);
        this.empty = true;
    }

    public void Deselect()
    {
        this.borderImage.enabled = false;
    }


    public void Select()
    {
        borderImage.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

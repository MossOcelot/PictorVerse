using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus player_status;
    [SerializeField]
    private Image hp_bar;
    [SerializeField]
    private Image stamina_bar;
    // Start is called before the first frame update
    void Start()
    {
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        float per_hp = (float)player_status.getHP() / (float)player_status.getMaxHP();
        float per_stamina = (float)player_status.getEnergy() / (float)player_status.getMaxEnergy();

        hp_bar.fillAmount = per_hp;
        stamina_bar.fillAmount = per_stamina;
    }

}

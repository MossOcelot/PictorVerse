using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TopUpSystem : MonoBehaviour
{
    [SerializeField] private Text myMoney;
    [SerializeField] private GameObject needTopUp;
    [SerializeField] private StockSystem stockSystem;
    [SerializeField] private Button confirmBtn;

    [SerializeField] private Text myMoneyInMarket;
    [SerializeField] private GameObject needGainMoney;
    [SerializeField] private Button confirmBtn2;

    private string section_scene;
    private PlayerStatus player_status;

    private float playerHasMoney;
    private float TopUpCount;

    private float balanceInMarket;
    private float GainMoneyInMarket;
    // Start is called before the first frame update
    private void Start()
    {
        SceneStatus sceneStatus = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>();
        player_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();

        section_scene = sceneStatus.sceneInsection.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        playerHasMoney = player_status.player_accounts.getPocket()[section_scene];
        myMoney.text = "$ " + playerHasMoney.ToString("F");

        balanceInMarket = stockSystem.getBalance();
        Debug.Log(balanceInMarket);
        myMoneyInMarket.text = "$ " + balanceInMarket.ToString("F");

        if (playerHasMoney < TopUpCount )
        {
            confirmBtn.interactable = false;
        } else
        {
            confirmBtn.interactable = true;
        }

        if(balanceInMarket < GainMoneyInMarket)
        {
            confirmBtn2.interactable = false;
        }
        else
        {
            confirmBtn2.interactable = true;
        }
    }

    public void confirm()
    {
        float balance = stockSystem.getBalance();
        float newBalance = balance + TopUpCount;

        float new_player_balance = playerHasMoney - TopUpCount;

        player_status.player_accounts.setPocket(section_scene, new_player_balance);
        stockSystem.setBalance(newBalance);
        clearValue();
    }

    public void confirm2()
    {
        float newBalance = balanceInMarket - GainMoneyInMarket;

        float new_player_balance = playerHasMoney + GainMoneyInMarket;
        Debug.Log("newBalance: " + newBalance + " new_player_balance: " + new_player_balance);
        player_status.player_accounts.setPocket(section_scene, new_player_balance);
        stockSystem.setBalance(newBalance);
        clearValue2();
    }

    public void setValue()
    {
        if (float.TryParse(needTopUp.gameObject.GetComponent<InputField>().text, out TopUpCount))
        {

            TopUpCount = float.Parse(needTopUp.gameObject.GetComponent<InputField>().text);
        }
    }

    public void setValue2()
    {
        if (float.TryParse(needGainMoney.gameObject.GetComponent<InputField>().text, out GainMoneyInMarket))
        {

            GainMoneyInMarket = float.Parse(needGainMoney.gameObject.GetComponent<InputField>().text);
        }
    }

    public void clearValue()
    {
        myMoney.text = "$ 0";
        needTopUp.gameObject.GetComponent<InputField>().text = "$ 0";
        playerHasMoney = 0f;
        TopUpCount = 0f;
    }

    public void clearValue2()
    {
        myMoneyInMarket.text = "$ 0";
        needGainMoney.gameObject.GetComponent<InputField>().text = "$ 0";
        balanceInMarket = 0f;
        GainMoneyInMarket = 0f;
    }
}

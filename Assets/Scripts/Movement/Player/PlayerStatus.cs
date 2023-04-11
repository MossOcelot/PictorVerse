using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inventory.Model;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class PlayerStatus : MonoBehaviour
{
    [System.Serializable]
    public class StaticValue
    {
        public float static_useEnergy;
        public float static_spendVAT;
        public float static_spendBuy;
        public float static_spendSell;
        public float static_distanceWalk;
        public float static_stability; //
        public float static_happy; //
        public float static_credibility; //
        public float static_healthy; //
        public float static_risk; //
    };

    public int player_id => GetInstanceID();
    [SerializeField]
    private string playername;
    public PocketDetails player_accounts;
    public Financial_Details financial_detail;
    [SerializeField]
    private List<AccountsDetail> accountsDetails;
    [SerializeField]
    private StaticValue myStatic;
    [SerializeField]
    private string account_id;

    [SerializeField]
    private int MaxHP;
    [SerializeField]
    private int MaxEnergy;
    [SerializeField]
    private int HP;
    [SerializeField]
    private int energy;
    [SerializeField]
    private AudioSource DeadSFX;

    public bool IsDead = false;
    public bool IsRespawn = false;

    public PlayerTeleport player_teleport;
    private SceneStatus.section section_name;


    private Rigidbody2D rb2d;
    private Vector2 knockbackDirection;
    private bool isColliding = false;
    public Animator animator;
    public Rigidbody2D rb;
    public PlayerMovement movementScript;
    public StatusEffectController effect_controller;
    public ToolController attackScript;

    public LoanPlayerController loanPlayerController;

    public void setPlayerName(string newName)
    {
        this.playername = newName;
    }

    public string getPlayerName()
    {
        return this.playername;
    }

    public void setMaxHP(int MaxHP)
    {
        this.MaxHP += MaxHP;
    }
    public int getMaxHP()
    {
        return this.MaxHP;
    }
    public void setMaxEnergy(int MaxEnergy)
    {
        this.MaxEnergy += MaxEnergy;
    }

    public int getMaxEnergy()
    {
        return this.MaxEnergy;
    }

    public void setHP(int hp)
    {
        this.HP += hp;
    }
    public int getHP()
    {
        return this.HP;
    }
    public void setEnergy(int useEnergy)
    {
        this.energy += useEnergy;
    }

    public int getEnergy()
    {
        return this.energy;
    }

    public StaticValue getMyStaticFromData()
    {
        return this.myStatic;
    }

    public StaticValue getMyStatic()
    {
        return myStatic;
    }

    public void setMyStatic(int command, float value)
    {
        if (command == 0)
        {
            this.myStatic.static_useEnergy = value;
        }
        else if (command == 1)
        {
            this.myStatic.static_spendBuy = value;
        }
        else if (command == 2)
        {
            this.myStatic.static_spendVAT = value;
        } else if (command == 3)
        {
            this.myStatic.static_spendSell = value;
        }
        else if (command == 4)
        {
            this.myStatic.static_distanceWalk = value;
        }
        else if (command == 5)
        {
            this.myStatic.static_stability = value;
        }
        else if (command == 6)
        {
            this.myStatic.static_happy = value;
        }
        else if (command == 7)
        {
            this.myStatic.static_credibility = value;
        }
        else if (command == 8)
        {
            this.myStatic.static_healthy = value;
        }
        else if (command == 9)
        {
            this.myStatic.static_risk = value;
        }
    }

    public List<AccountsDetail> getAccountsDetails()
    {
        return this.accountsDetails;
    }

    public void addAccountsDetails(AccountsDetail account)
    {
        this.accountsDetails.Insert(0, account);
    }

    public void SetAccountID(string account_id)
    {
        this.account_id = account_id;
    }
    public string GetAccountID()
    {
        return this.account_id;
    }

    public void setFinancial_detail(string command, float value)
    {
        if (command == "balance")
        {
            this.financial_detail.balance = value;
        }
        else if (command == "debt")
        {
            this.financial_detail.debt = value;
        }
    }

    public Financial_Details getFinancial_Details()
    {
        return this.financial_detail;
    }

    public float GetFinancial_balance()
    {
        return this.financial_detail.balance;
    }

    public float GetFinancial_debt()
    {
        return this.financial_detail.debt;
    }

    public bool GetIsDead()
    {
        return this.IsDead;
    }

    public void SetIsdead(bool isdead)
    {
        this.IsDead = isdead;
    }
    //private void Start()
    //{
    // Load();
    //}

    public void Awake()
    {
        //Load();
        section_name = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;
    }

    public void newBorn()
    {
        IsDead = false;
        effect_controller.ClearEffects();
        player_teleport.Respawner();
        this.HP = Mathf.RoundToInt((float)MaxHP / 2f);
        this.energy = Mathf.RoundToInt((float)MaxEnergy / 2f);
        movementScript.enabled = true;
        attackScript.enabled = true;
        rb.mass = 50f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetTrigger("Respawn");
        movementScript.enabled = true;
        attackScript.enabled = true;
        rb.mass = 50f;
        IsRespawn = true;
    }

    private void Update()
    {
        if (this.energy <= 0)
        {
            this.energy = 0;
        }
        if (this.HP <= 0)
        {
            DeadSFX.Play();
            IsDead = true;
            
        }
        if (IsDead)
        {
            Debug.Log("IsDead");
            animator.SetTrigger("isDeath");
            movementScript.enabled = false;
            attackScript.enabled = false;
            rb.velocity = new Vector2(0f, 0f);
            rb.angularDrag = 0f;
            rb.mass = 5000f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (IsRespawn )
        {
            StartCoroutine(RespawnAnimation());
        }
    }

    private IEnumerator RespawnAnimation()
    {
        animator.SetTrigger("Respawn");
        yield return new WaitForSeconds(5f);
        IsRespawn = false;

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        UpdateStability();
    }

    private float oldStabilityForFinancial;
    float totalAssets;
    float oldtotalAssets;
    float totalAccountDetail;
    private void UpdateStability()
    {
        GetTotalAssets();
        GetTotalAccountDetail();

        float cash = ((financial_detail.balance - financial_detail.debt) * 0.7f) + (totalAccountDetail * 0.3f);
        float exhangeCashToGold = new ExchangeRate().getExchangeRate((int)section_name, 5) * cash;
        float stability = exhangeCashToGold * 10;
        if (oldStabilityForFinancial != stability)
        {
            float difference = stability - oldStabilityForFinancial;
            float newStability = getMyStatic().static_stability + difference;
            
            setMyStatic(5, newStability);

            oldStabilityForFinancial = stability;

        }

        //reset
        totalAssets = 0;
        totalAccountDetail = 0;
    }

    private void GetTotalAssets()
    {
        Dictionary<string, float> player_account = player_accounts.getPocket();

        int n = 0;
        foreach (string key in player_account.Keys)
        {
            if (key == section_name.ToString())
            {
                totalAssets += player_account[key];
            }
            else
            {
                totalAssets += player_account[key] * new ExchangeRate().getExchangeRate(n, (int)section_name);
            }
            n++;
        }
        if(oldtotalAssets != totalAssets)
        {
            float differenct = totalAssets - oldtotalAssets;
            financial_detail.balance += differenct;
            oldtotalAssets = totalAssets;
        }
    }

    private void GetTotalAccountDetail()
    {
        List<AccountsDetail> player_account_details = getAccountsDetails();

        foreach (AccountsDetail account in player_account_details)
        {
            totalAccountDetail += (account.income - account.expense);
        }
    }
    
    private void OnApplicationQuit()
    {
        // Save();
    }
    public void Save()
    {
        SavePlayerSystem.SavePlayerStatus(this);
    }

    public void Load()
    {
        PlayerStatusData data = SavePlayerSystem.LoadPlayerStatus();

        if (data != null)
        {
            player_accounts = data.player_accounts;
            financial_detail = data.financial_detail;
            accountsDetails = data.accountsDetails;

            myStatic = data.myStatic;

            MaxHP = data.MaxHP;
            HP = data.HP;
            MaxEnergy = data.MaxEnergy;
            energy = data.Energy;
            IsDead = data.IsDead;

            Vector3 position;
            position.x = data.position_player[0];
            position.y = data.position_player[1];
            position.z = data.position_player[2];
            transform.position = position;
        }
    }

    public float PayTaxes(float income)
    {
        GovermentPolicy goverment = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>();
        List<GovermentPolicyData.IndividualRangeTax> invidualRangeTax = goverment.getIndividualTax();
        float incomeAllYear = income;
        float tax = 0;
        float net_income = 0;
        if(incomeAllYear * 0.5f > goverment.govermentPolicy.limitLessExpenses)
        {
            net_income = goverment.govermentPolicy.limitLessExpenses;
        } else
        {
            net_income = incomeAllYear * 0.5f;
        }
        foreach (GovermentPolicyData.IndividualRangeTax IndividualTax in invidualRangeTax)
        { 
            float maxIncome = IndividualTax.maxIncome;
            float minIncome = IndividualTax.minIncome;
            float perTax = IndividualTax.Tax / 100f;

            if (maxIncome == 0)
            {
                tax += ((net_income - minIncome + 1) * perTax);
                break;
            } 

            if (net_income <= maxIncome)
            {
                tax += ((net_income - minIncome) * perTax);
                break;
            }

            tax += ((maxIncome - minIncome + 1) * perTax);
        }

        return tax;
        
    }

    public float GetIncomeAllYear()
    {
        int year = GameObject.FindGameObjectWithTag("TimeSystem").gameObject.GetComponent<Timesystem>().getDateTime()[2];
        int[] date = new int[] { 1, 3, year - 1 };

        float allIncome = 0;
        foreach(AccountsDetail account in accountsDetails) 
        {
            int[] date_account = account.date;
            if (date_account[2] <= date[2])
            {
                if (date_account[1] == date[1])
                {
                    if(date_account[0] <= date[0])
                    {
                        break;
                    }
                }
                else if (date_account[1] < date[1])
                {
                    break;
                }
            }
            allIncome += account.income;
        }
        return allIncome;
    }
}

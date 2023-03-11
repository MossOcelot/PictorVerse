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

    public bool IsDead = false;


    private Rigidbody2D rb2d;
    private Vector2 knockbackDirection;
    private bool isColliding = false;
    public Animator animator;
    public Rigidbody2D rb;
    public PlayerMovement movementScript;
  
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

    public Dictionary<string, float> getMyStatic()
    {
        return new Dictionary<string, float>
        {
            {"static_useEnergy", this.myStatic.static_useEnergy },
            {"static_SpendBuy", this.myStatic.static_spendBuy },
            {"static_SpendVat", this.myStatic.static_spendVAT }
        };
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

    public void setFinancial_detail(string command, int value)
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
    private void Start()
    {
        Animator animator = GetComponent<Animator>();

    }

    //public void Awake()
    //{
      //  Load();
    //}

    private void Update()
    {
        if (this.energy <= 0)
        {
            Debug.Log("Empty Energy");
            this.energy = 0;
        }
        if (this.HP <= 0)
        {
            IsDead = true;

        }
        if (IsDead || this.HP <= 0)
        {
            animator.SetTrigger("isDeath");
            movementScript.enabled = false;
            rb.velocity = new Vector2(0f, 0f);
            rb.angularDrag = 0;
            rb.mass = 5000f;

        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ItemCutD") || other.gameObject.CompareTag("Enemy"))
        {
            this.setHP(-2);
            Vector2 knockbackDirection = (transform.position - other.gameObject.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * 1200, ForceMode2D.Impulse);
            StartCoroutine(FlashRed());

        }
    }
    private IEnumerator FlashRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ItemCutD") || other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    private void OnApplicationQuit()
    {
        Save();
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
}

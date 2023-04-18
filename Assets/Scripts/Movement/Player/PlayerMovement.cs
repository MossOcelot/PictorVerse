﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inventory.Model;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventory_player;
    [SerializeField]
    private InventorySO inventoryMini_player;
    [SerializeField]
    private InventorySO Weapon_player;
    [SerializeField]
    private InventorySO Set_player;

    public Vector2 lastPos;
    // กำหนด LayerMask
    public LayerMask interactableLayer;

    [SerializeField]
    private float defaultMoveSpeed = 5f;
    public float realMoveSpeed;
    [SerializeField]
    public float moveSpeed;
    private bool isMoving;
    public bool isLooking = false;
    public float dashSpeed;

    [SerializeField]
    private PlayerStatus status;
    private Rigidbody2D rb;
    public Animator animator;

    public float walk_distance = 0;
    public bool iswalk;
    public bool isDashButtonDown;

    private Vector3 MoveDir;


    public int energy_for_walk;
    public float strength;
    public float weight_player;
    [SerializeField] private TrailRenderer tr;

    public StatusEffectController player_effects;
    [SerializeField] private StatusEffectPlayer effect_to_work;
    Vector2 playerposition;


    Vector2 movement;

    private void Awake()
    {
       // Load();
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerposition = transform.position;
        realMoveSpeed = defaultMoveSpeed;
    }
    private void Update()
    {
        if(isLooking) { animator.SetBool("isMoving", false); return; }
        Movement();
        if (movement.x != 0 || movement.y != 0)
        {
            iswalk = true;
            isMoving = true;

        }
        else
        {
            iswalk = false;
            isMoving = false;

        }
        count_distance_for_walk();
        //Set การหยุดหรือไม่หยุดของ Animation
        animator.SetBool("isMoving", isMoving);


        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        { 
            lastPos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.LeftShift)))
        {
            isDashButtonDown = true;

        }
       


    }

    public float dashCountdownTimer = 5f;
    [SerializeField]
    private bool canDashSpeed = true;
    float dashCountdownTime = 0f;
    public int maxCountDashSpeed;
    public int countDashSpeed;
    private void FixedUpdate()
    {
        if (!canDashSpeed)
        {
            if(dashCountdownTime <= dashCountdownTimer)
            {
                dashCountdownTime += Time.deltaTime;
                return;
            } else
            {
                dashCountdownTime = 0f;
                countDashSpeed = 0;
                canDashSpeed = true;
            }
        }

        if (isDashButtonDown && canDashSpeed)
        {
            tr.emitting = true;
            dashSpeed = 1.5f;
            rb.MovePosition(transform.position + MoveDir * dashSpeed);
            useEnergy(energy_for_walk * 2);
            isDashButtonDown = false;
            countDashSpeed += 1;
            if(countDashSpeed > maxCountDashSpeed)
            {
                canDashSpeed = false;
            }
        }
        else { tr.emitting = false; }
    }

    private void OnApplicationQuit()
    {
       // Save();
    }

    private void Movement()
    {
        int energy = status.getEnergy();

        checkWeightPerStrength();

        if (energy == 0)
        {
            moveSpeed = 1f;
        }
        else
        {
            moveSpeed = realMoveSpeed;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        //หันหน้า
        if (movement != Vector2.zero)
        {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);

            chanceOfGettingSick();


        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        MoveDir = new Vector3(movement.x, movement.y).normalized;

        AnimationMovement();

    }
    private void AnimationMovement()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Monster"))
        {
            Destroy(target.gameObject);
        }
    }

    private void count_distance_for_walk()
    {
        if (iswalk)
        {
            var distance = Vector2.Distance(transform.position, playerposition);

           if (distance >= 10)
           {
                walk_distance += 10;
                playerposition = transform.position;

                float newDistanceStatic = status.getMyStatic().static_distanceWalk + distance;
                status.setMyStatic(4, newDistanceStatic);
                

                float rand_chance = UnityEngine.Random.Range(0f, 100f);
                if (rand_chance <= 10f)
                {
                    float newHealth = status.getMyStatic().static_healthy + 1;
                    float newHappy = status.getMyStatic().static_happy + 0.5f;
                    status.setMyStatic(8, newHealth);
                    status.setMyStatic(6, newHappy);
                }

                useEnergy(energy_for_walk);
           }
        }
    }

    private void useEnergy(int energy)
    {
        status.setEnergy(-energy);
        float energy_static = status.getMyStatic().static_useEnergy + energy;
        status.setMyStatic(0, energy_static);
    }

    private float getWeightItem()
    {
        float weight = 0;
        Dictionary<int, InventoryItem> myItems = inventory_player.GetCurrentInventoryState();
        foreach (InventoryItem item in myItems.Values)
        {
            weight += (item.item.weight * item.quantity);
        }

        Dictionary<int, InventoryItem> myMiniItem = inventoryMini_player.GetCurrentInventoryState();
        foreach (InventoryItem item in myMiniItem.Values)
        {
            weight += (item.item.weight * item.quantity);
        }

        Dictionary<int, InventoryItem> myWeaponItem = Weapon_player.GetCurrentInventoryState();
        foreach (InventoryItem item in myWeaponItem.Values)
        {
            weight += (item.item.weight * item.quantity);
        }

        Dictionary<int, InventoryItem> mySetItem = Set_player.GetCurrentInventoryState();
        foreach (InventoryItem item in mySetItem.Values)
        {
            weight += (item.item.weight * item.quantity);
        }

        return weight;
    }

    private void checkWeightPerStrength()
    {
        weight_player = getWeightItem();
        float weight_per_strength = (weight_player / strength) * 100; // ระบบ weight player

        if (weight_per_strength > 90f)
        {
            realMoveSpeed = defaultMoveSpeed * 0.1f;
        }
        else if (weight_per_strength > 80f)
        {
            realMoveSpeed = defaultMoveSpeed * 0.25f;
        }
        else if (weight_per_strength > 70f)
        {
            realMoveSpeed = defaultMoveSpeed * 0.35f;
        }
        else if (weight_per_strength > 60f)
        {
            realMoveSpeed = defaultMoveSpeed * 0.45f;
        }
        else if (weight_per_strength > 50f)
        {
            realMoveSpeed = defaultMoveSpeed * 0.55f;
        }
        else
        {
            realMoveSpeed = defaultMoveSpeed;
        }
    }

    public float GetWeight_player()
    {
        return weight_player;
    }

    public float GetStrength()
    {
        return strength;
    }

    public float getDefaultMoveSpeed()
    {
        return defaultMoveSpeed;
    }
    public void setDefaultMoveSpeed(float newDefault)
    {
        defaultMoveSpeed = newDefault; 
    }
    public void SetIsLooking(bool newStatus)
    {
        isLooking = newStatus;
    }

    private void chanceOfGettingSick()
    {
        float rand_chance = UnityEngine.Random.Range(0f, 100f);
        float chance = CalculateChanceSick();
        if (rand_chance <= chance)
        {
            player_effects.AddStatus(effect_to_work);
        }
    }

    private float CalculateChanceSick()
    {
        float healthy = status.getMyStatic().static_healthy;
        float chance = 0.01f;
        if(healthy <= 20)
        {
            chance *= 5f;
        }
        else if(healthy <= 80)
        {
            chance *= 2.5f;
        }
        else if(healthy <= 100)
        {
            chance *= 1;
        }
        else
        {
            chance *= 0.5f;
        }
        return chance;
    }

    // ------------ save and load ------------
    public void Save()
    {
        SavePlayerSystem.SavePlayerMovement(this);
    }

    public void Load()
    {
        PlayerMovementData data = SavePlayerSystem.LoadPlayerMovement();

        if (data != null) { 
            defaultMoveSpeed = data.defaultMoveSpeed;
            walk_distance = data.walk_distance;
            iswalk = data.iswalk;
            isDashButtonDown = data.isDashButtonDown;
            energy_for_walk = data.energy_for_walk;
            strength = data.strength;
            weight_player = data.weight_player;
        }

    }
    
}

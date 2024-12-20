﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour{
    public static GameController instance;

    private static float health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 20;
    private static float moveSpeedInitial = 20;
    private static float fireRate = 0.5f;
    private static float fireRateInitial = 0.5f;
    private static float bulletSize = 0.2f;
    private static float bulletSizeInitial = 0.2f;

    private bool bootCollected = false;
    private bool screwCollected = false;
    public List<string> collectedNames = new List<string>();

    public DeathMenu deathMenu;

    public static float Health{get => health; set => health = value;}
    public static int MaxHealth{get => maxHealth; set => maxHealth = value;}
    public static float MoveSpeed{get => moveSpeed; set => moveSpeed = value;}
    public static float FireRate{get => fireRate; set => fireRate = value;}
    public static float BulletSize{get => bulletSize; set => bulletSize = value;}

    public Text healthText;

    private void Awake(){
        if (instance == null){
            instance = this;
        }
        health = maxHealth;
        moveSpeed = moveSpeedInitial;
        fireRate = fireRateInitial;
        bulletSize = bulletSizeInitial;
    }

    void Update(){
        healthText.text = "Health: " + health;
    }

    public void DamagePlayer(int damage){
        health -= damage;
        if (health <= 0){
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount){
        Health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed){
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate){
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size){
        bulletSize += size;
    }

    private void KillPlayer(){
        FindObjectOfType<AudioManager>().Play("Player Death");
        deathMenu.GameOver();
        health = maxHealth;
        moveSpeed = moveSpeedInitial;
        fireRate = fireRateInitial;
        bulletSize = bulletSizeInitial;
    }

    public void UpdateCollectedItems(CollectionController item){
        collectedNames.Add(item.item.name);

        foreach (string name in collectedNames){
            switch (name){
                case "Boot":
                    bootCollected = true;
                    break;
                case "Screw":
                    screwCollected = true;
                    break;
            }
        }

        if (bootCollected && screwCollected){
            FireRateChange(0.35f);
        }
    }
}

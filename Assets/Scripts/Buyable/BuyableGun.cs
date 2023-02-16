using System;
using System.Globalization;
using Commands;
using Controllers;
using Entities;
using EventQueue;
using Flyweight;
using TMPro;
using UnityEngine;
using Weapons;

namespace Buyable
{
    public class BuyableGun : InteractableItem, IBuyable
    {
        [SerializeField] private GunStats _gunStats;
        [SerializeField] private Player player;
        private GunType type => _gunStats.Type;

        public int AmmoPrice => _gunStats.AmmoPrice;
        public int WeaponPrice => _gunStats.WeaponPrice;

        private bool PlayerOwnsGun => player.OwnsGun(type);


        private void Start()
        {
            onInteract.AddListener(() => EventQueueManager.Instance.AddCommand(new CmdBuyShotgun(this)));
        }

        private void RefreshPrice()
        {
            price = PlayerOwnsGun ? AmmoPrice : WeaponPrice;
        }

        private void RefreshPromptText()
        {
            var finalPrompt = PlayerOwnsGun
                ? $"buy ammo"
                : $"buy {_gunStats.Name}";

            prompt = finalPrompt;
        }

        private void Update()
        {
            RefreshPrice();
            RefreshPromptText();
        }

        public void Buy()
        {
            var finalPrice = PlayerOwnsGun ? AmmoPrice : WeaponPrice;

            if (!player.scoreController.CanSubtract(finalPrice)) return;

            player.scoreController.SubtractScore(finalPrice);

            if (PlayerOwnsGun)
            {
                player.RefillGunAmmo(type);
            }
            else
            {
                player.AddGun(type);
            }
        }
    }
}
using System;
using System.Globalization;
using Commands;
using Managers;
using Controllers;
using Strategy;
using Entities;
using EventQueue;
using Flyweight;
using TMPro;
using UnityEngine;
using Weapons;

namespace Buyable
{
    public class BuyableGunUpgrade : InteractableItem, IBuyable
    {
        [SerializeField] private IGun _gun;
        [SerializeField] private Player player;

        public float _cooldown = 1f;
        private void OnGunChange(){
            _gun = player.GetGun();
            Refresh();
        }
        void Update(){
            base.Update();
            if(_cooldown <= 0) return;
            _cooldown-=Time.deltaTime;
        }
        private void Start()
        {
            EventsManager.Instance.OnGunChange += OnGunChange;
            onInteract.AddListener(() => EventQueueManager.Instance.AddCommand(new CmdBuy(this)));
            _gun = player.GetGun();
        }

        private void RefreshPrice()
        {
            price = (1 +_gun.Level) * _gun.Price;
        }

        private void RefreshPromptText()
        {
            prompt = $"Upgrade {_gun.Name} to lvl {_gun.Level + 1}";
            if(_playerInside){
                promptCanvas.enabled = true;
                promptText.text = $"Press {interactionKey} to {prompt}";
                if (price >= 0)
                {
                    promptText.text += $" [{price}]";
                }
            }
        }

        private void Refresh()
        {
            RefreshPrice();
            RefreshPromptText();

        }   

        public void Buy()
        {
            Debug.Log("Attempting to buy");
            var finalPrice = price;

            if (!player.scoreController.CanSubtract(finalPrice) || _cooldown > 0) {
                  Debug.Log("player cant buy!");
                return;
            }
             base.Buy(); 
            player.scoreController.SubtractScore(finalPrice);
            _cooldown = 1f;
            _gun.Upgrade();
            _gun.DrawGun();
            Refresh();
           
        }
    }
}
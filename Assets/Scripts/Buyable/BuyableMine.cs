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
    public class BuyableMine : InteractableItem, IBuyable
    {
         [SerializeField] private Player player;
        [SerializeField] private MineThrowingController mines;
    
        private int _price;

        private void Start()
        {
            onInteract.AddListener(() => EventQueueManager.Instance.AddCommand(new CmdBuy(this)));
        }

        private void RefreshPrice()
        {
            _price = mines.IsFull() ? 0 : price;
        }

        private void RefreshPromptText()
        {
            var finalPrompt = mines.IsFull()
                ? $"Buy mines [FULL]"
                : $"Buy mines";

            prompt = finalPrompt;
        }

        private void Update()
        {
            RefreshPrice();
            RefreshPromptText();
            base.Update();
        }

        public void Buy()
        {
            Debug.Log("Attempting to buy");

            if (!player.scoreController.CanSubtract(_price) && mines.IsFull()) {
                  Debug.Log("player cant buy!");
                return;
            }
            
            player.scoreController.SubtractScore(_price);

            mines.RefillAll();
            
        }
    }
}
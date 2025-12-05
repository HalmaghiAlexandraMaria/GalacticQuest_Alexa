using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticQuest
{
    internal class Player
    {

        // Fields

        private int _hp;
        private int _attack;
        private int _credits;

        private List<(string Name, int Attack)> _items = new List<(string, int)>();

        // Properties
        public int Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public int Attack
        {
            get => _attack;
            set => _attack = value;
        }

        public int Credits
        {
            get => _credits;
            private set => _credits = value;
        }

        public List<(string Name, int Attack)> Items
        {
            get => _items;
            private set => _items = value;
        }

        internal Player(int hp, int attack)
        {
            Hp = hp;
            Attack = attack;
            Credits = 0;   // default
        }

        // Constructor gol ( player default)
        internal Player()
        {
            Hp = 100;
            Attack = 10;
            Credits = 0;
        }


        // -----------------------
        //  OnDeath + UpdateHp
        // -------------------

        /// Scade sau crește HP-ul și verifică dacă playerul moare.
        internal void UpdateHp(int amount)
        {
            Hp += amount;

            if (Hp <= 0)
            {
                Hp = 0;
                OnDeath();   // cerință tema 4
            }
        }

        /// Afișează mesaj când HP ajunge la 0.
        private void OnDeath()
        {
            Console.WriteLine("Player has fallen... Game Over!");
        }


        // -------------------------
        // Credits + Items logic
        // -------------------------

        /// Actualizează balance-ul de credite.
        internal void UpdateCredits(int amount)
        {
            Credits += amount;
        }

        /// Adaugă un item dacă există destui bani.

        internal void AddItem((string Name, int Attack) newItem, int price)
        {
            if (Credits < price)
            {
                Console.WriteLine("Not enough credits to buy this item!");
                return;
            }

            // scădem banii
            UpdateCredits(-price);

            // adăugăm itemul
            Items.Add(newItem);

            Console.WriteLine($"Item '{newItem.Name}' purchased!");
        }

        /// Vinde un item (îl eliminăm și dăm bani).

        internal void SellItem(int itemIndex, int sellPrice)
        {
            if (itemIndex < 0 || itemIndex >= Items.Count)
            {
                Console.WriteLine("Invalid item index!");
                return;
            }

            var soldItem = Items[itemIndex];
            Items.RemoveAt(itemIndex);

            UpdateCredits(sellPrice);

            Console.WriteLine($"Item '{soldItem.Name}' sold!");
        }


        internal void ShowDetails()
        {
            Console.WriteLine($"Player HP: {Hp}");
            Console.WriteLine($"Player Attack: {Attack}");
            Console.WriteLine($"Credits: {Credits}");

            Console.WriteLine("\nItems:");
            if (Items.Count == 0)
            {
                Console.WriteLine("  No items yet.");
            }
            else
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine($"  [{i}] {Items[i].Name} (+{Items[i].Attack} ATK)");
                }
            }

            Console.WriteLine("----------------------");
        }
    }
}

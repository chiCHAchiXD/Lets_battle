﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsBattleBookCase
{
#region classCreation
    public class Character
    {
#region variables
        protected string name;
        protected int health;
        protected int damage;
        protected int defense;
        protected int who;
        protected int level;
        protected int actionpts;
        Dice dice = new Dice();
#endregion

        public Character() { }

        public Character(int h, int da, int de, string na, int wh)
        {
            health = h;
            damage = da;
            defense = de;
            name = na;
            who = wh;
            level = 1;
            actionpts = 5;
        }

#region getAndSet
        public int Health { get { return health; } set { health = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
        public int Defense { get { return defense; } set { defense = value; } }
        public int Who { get { return who; } set { who = value; } }
        public int Level { get { return level; } set { defense = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int ActionPoint { get { return actionpts; } set { actionpts = value; } }
#endregion

        public int Fight(Character attackedOn, int dmg) { return attackedOn.DamageDealt(attackedOn, dmg); }

        private int DamageDealt(Character attackedOn, int dmg)
        {
            int damageDealt = dice.DiceRoll(dmg) - dice.DiceRoll(attackedOn.Defense);
            if (damageDealt < 0) damageDealt = 0; //nobody wants to add lives, do you ?
            attackedOn.MinusHealth(damageDealt);
            return damageDealt;
        }

        public bool IsAlive() { return (health > 0); }

        protected void MinusHealth(int dmg) { health -= dmg; }

        public void LevelIncrease()
        {
            level++;
            health += 5;
            damage += 5;
            defense += 5;
            actionpts += 2;
        }

        //lets get some stats, 1todo get usefull!!!!!!!
        public void LevelStat(Character attacker) { int livelo = attacker.Level; }
    }

    public abstract class Player : Character
    {
        public Player() { }

        public Player(int h, int da, int de, string na, int wh) : base(h, da, de, na, wh) { }
        
#region virtualMethods
        public virtual int FireBall() { return 0; }
        public virtual int IceBall() { return 0; }

        public virtual int LightAttack(int damageOfItem) { return 0; }
        public virtual int HeavyAttack(int damageOfItem) { return 0; }

        public virtual int LightAttack() { return 0; }
        public virtual int HeavyAttack() { return 0; }
        /*
        //Inventory / Items methods
        public virtual int GetItemDamage(Item item) { return 0; }
        public virtual string GetItemName(Item item) { return ""; } //get item name 
        public virtual int GetSwordSharpness() { return 0; } //get item sharpness
        */
#endregion
    }

    public class Mage : Player
    {
        Dice dice = new Dice();
        public Mage(int h, int da, int de, string na, int wh) : base(h, da, de, na, wh) { }

        public override int FireBall()
        {
            int damage = dice.DiceRoll(Damage) + 1;
            return damage;
        }

        public override int IceBall()
        {
            int damage = dice.DiceRoll(Damage) + 2;
            return damage;
        }
    }

    public class Warrior : Player
    {
        Dice dice = new Dice();
        public Warrior(int h, int da, int de, string na, int wh) : base(h, da, de, na, wh) { }

        public override int HeavyAttack(int damageOfItem)
        {
            int damage = dice.DiceRoll(damageOfItem) + 3;
            return damage;
        }

        public override int LightAttack(int damageOfItem)
        {
            int damage = dice.DiceRoll(damageOfItem) + 1;
            return damage;
        }
    }

    public class Creation
    {
        Dice dice = new Dice();
        public Player InitialyPlayer(int h, int dam, int def, string name, int whoP, int classP)
        {
            Dice dice = new Dice();

            int roll = dice.DiceRoll();

            Player returnPlayer = null;

            switch (classP)
            {
                case 0: // warrior
                        /*
                        if (roll % 2 == 0) //if sude
                            inventoryPlayer = new Sword("Broken Sword", 5, 5);
                        else //if liche
                            inventoryPlayer = new Sword("Straight Sword", 10, 25);

                        inventoryListPlayer.Add(inventoryPlayer); //lets add that generated shit into player inventory
                        */
                    returnPlayer = new Warrior(h, dam, def, name, whoP); //create player as warrior
                    break;
                case 1: //mage
                    returnPlayer = new Mage(h, dam, def, name, whoP);
                    break;
            }

            return returnPlayer;

            /*

            if (roll % 2 == 0) //if sude 
                inventoryEnemy = new Sword("Broken Sword", 5, 5);
            else //if liche
                inventoryEnemy = new Sword("Straight Sword", 10, 25);

            inventoryListEnemy.Add(inventoryEnemy);
            */
        }

        public Player InitialyEnemy(Character player)
        {
            int whoE = 1;
            Player returnEnemy = null;

            returnEnemy = ChooseOfEnemy(whoE, player); // creation of enemy

            return returnEnemy;
        }

        public Player NextEnemy(int whoE, Character player)
        {
            Player returnEnemy = ChooseOfEnemy(whoE, player);
            return returnEnemy;
        }

        protected Player ChooseOfEnemy(int whoE, Character player)
        {
            Player returnEnemy = null;
            int roll = dice.DiceRoll();
            if (roll % 2 == 0) //if sude
                returnEnemy = new Warrior(dice.DiceRoll(player.Health) + 5, dice.DiceRoll(player.Damage) + 6, dice.DiceRoll(player.Defense) + 6, "Ferda", whoE);
            else //if liche 
                returnEnemy = new Mage(dice.DiceRoll(player.Health) + 3, dice.DiceRoll(player.Damage) + 7, dice.DiceRoll(player.Defense) + 2, "Janko", whoE);
            return returnEnemy;
        }
        
    }
#endregion
    
    public class Dice
    {
        Random rnd = new Random();
        public Dice() { }

        public int DiceRoll() { return rnd.Next() + 1; }

        public int DiceRoll(int max) { return rnd.Next(max) + 1; }
    }
}

using System;
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

#region inventory

    public abstract class Inventory
    {
        //public List<Item> inventory = new List<Item>();

        //public List<Item> Invent { get { return inventory; } }

        public string GetInventory(List<Item> inventory)
        {
            string a = "";
            foreach (Item i in inventory) a += i + Environment.NewLine;
            return a;
        }

        public virtual string ItemInfo(List<Item> inventory) { return ""; }

        public virtual Item CreateSword(string name, int damage) { return null; }
        
    }

    public class Item : Inventory
    {
        protected string nameOfItem;
        protected int damageOfItem;
        
        public Item() { }
        
        public Item(string nme, int dmg)
        {
            nameOfItem = nme;
            damageOfItem = dmg;
        }

        public string ItemName { get { return nameOfItem; } set { nameOfItem = value; } }
        public int ItemDamage { get { return damageOfItem; } set { damageOfItem = value; } }

        public override string ItemInfo(List<Item> inventory)
        {
            string a = "";
            foreach (Item it in inventory)
            {
                a += it.ItemName + it.ItemDamage + Environment.NewLine;
            }
            //return Convert.ToString(ItemName + " " + ItemDamage);
            return a;
        }

        public override Item CreateSword(string name, int damage)
        {
            Item item = new Sword(name, damage, 10);
            return item;
        }
    }

#region items

    class Sword : Item
    {
        int sharpness = 0;

        public Sword(string nme, int dmg, int sness) :base(nme,dmg) { sharpness = sness; }

        public int GetSwordSharpness() { return sharpness; }
    }

#endregion

#endregion

    public class GameArena
    {
        public int wIP;
        public int wIE;
        public int arenaSize;
        public char[] gameArena;

        public GameArena(int size, int whereIsPlayer, int whereIsEnemy)
        {
            arenaSize = size;
            wIP = whereIsPlayer;
            wIE = whereIsEnemy;
            gameArena = new char[size];
            gameArena = FillArena();
            gameArena[whereIsPlayer] = 'P';
            gameArena[whereIsEnemy] = 'E';
        }

        public int WIP { get { return wIP; } set { wIP = value; } }
        public int WIE { get { return wIE; } set { wIE = value; } }
        public int Length { get { return arenaSize; } }

        //it returns whole 2D arena
        public string GetGameArenaLook()
        {
            string a = "";
            //a = Convert.ToString(FillArena(gameArena));
            for (int i = 0; i < gameArena.Length; i++)
            {
                a += Convert.ToString(gameArena[i]);
            }
            return a;
        }

        public int GetWhereIsWho(Character a)
        {
            int where = 0;

            switch (a.Who)
            {
                //Player
                case 0:
                    where = wIP;
                    break;
                //Enemy
                case 1:
                    where = wIE;
                    break;
            }

            return where;
        }

        public char[] FillArena()
        {
            //fill whole field with #
            for (int i = 0; i < gameArena.Length; i++)
            {
                gameArena[i] = '#';
            }
            //where if enemy write char E
            gameArena[WIP] = 'E';

            //where if player write char P
            gameArena[WIE] = 'P';
            return gameArena;
        }

        public void Move(Character a, int where)
        {
            switch (a.Who)
            {
                case 0:
                    int p = wIP + where;
                    try
                    {
                        if (wIP > 1 || wIP < Length - 1)
                        {
                            gameArena[WIP] = '#';
                            gameArena[p] = 'P';
                            wIP = p;
                        }
                    }
                    catch
                    {
                        if (wIP == 0)
                        {
                            gameArena[0] = 'P';
                            wIP = 0;
                        }
                        else if (wIP == gameArena.Length - 1)
                        {
                            gameArena[gameArena.Length - 1] = 'P';
                            wIP = gameArena.Length - 1;
                        }
                    }
                    break;
                //enemy
                case 1:
                    int e = wIE + where;
                    try
                    {
                        if (wIE > 1 || wIE < gameArena.Length - 1)
                        {
                            gameArena[GetWhereIsWho(a)] = '#';
                            gameArena[e] = 'E';
                            wIP = e;
                        }
                    }
                    catch
                    {
                        if (wIE == 0)
                        {
                            gameArena[0] = 'E';
                            wIP = 0;
                        }
                        else if (wIE == gameArena.Length - 1)
                        {
                            gameArena[gameArena.Length - 1] = 'E';
                            wIP = gameArena.Length - 1;
                        }
                    }
                    break;
            }
        }
    }

    public class Dice
    {
        Random rnd = new Random();
        public Dice() { }

        public int DiceRoll() { return rnd.Next() + 1; }

        public int DiceRoll(int max) { return rnd.Next(max) + 1; }
    }
}

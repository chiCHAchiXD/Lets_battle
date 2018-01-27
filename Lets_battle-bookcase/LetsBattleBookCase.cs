using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LetsBattleBookCase
{
#region characters

    public class Character
    {

#region variables

        protected string name;
        protected int health;
        protected int damage;
        protected int defense;
        protected int who;
        protected int level;
        protected int actionpt;
        
        protected Dice dice = new Dice();

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
            actionpt = 5;
        }

#region getAndSet

        public int Health { get { return health; } set { health = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
        public int Defense { get { return defense; } set { defense = value; } }
        public int Who { get { return who; } set { who = value; } }
        public int Level { get { return level; } set { defense = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int ActionPoint { get { return actionpt; } set { actionpt = value; } }

#endregion

        public int Fight(Character attackedOn, int dmg) { return attackedOn.DamageDealt(attackedOn, dmg); }

        private int DamageDealt(Character attackedOn, int dmg)
        {

            int damageDealt = (dmg + Damage /*dice.DiceRoll(Damage)*/) - (attackedOn.Defense);//- dice.DiceRoll(3));
            
            if (damageDealt < 0)
                damageDealt = 0; //nobody wants to add lives, do you ?
            
            attackedOn.MinusHealth(damageDealt);
            return damageDealt;

        }

        public bool IsAlive() { return (health > 0); }

        public bool HasActionPts() { return (ActionPoint>0); }

        protected void MinusHealth(int dmg) { health -= dmg; }

        public void LevelIncrease()
        {
            level++;
            health += 5;
            damage += 5;
            defense += 5;
            actionpt += 5;
        }

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

#region classes

    public class Mage : Player
    {
        public Mage(int h, int da, int de, string na, int wh) : base(h, da, de, na, wh) { }
        
    }
    public class Warrior : Player
    {
        public Warrior(int h, int da, int de, string na, int wh) : base(h, da, de, na, wh) { }
    }

    #region skills

    public class MagicalAttack : Player
    {
        public override int FireBall() { return dice.DiceRoll(Damage) + 10; }

        public override int IceBall() { return dice.DiceRoll(Damage) + 20; }

        /*
        public override int FireBall()
        {
            int NeededActionPoints = 1;
            int dmg = 0;
            if (ActionPoint >= NeededActionPoints)
            {
                ActionPoint -= NeededActionPoints;
                dmg = dice.DiceRoll(Damage) + 1;
            }
            else dmg = 0;
            return dmg;
        }

        public override int IceBall()
        {
            int NeededActionPoints = 2;
            int dmg = 0;
            if (ActionPoint >= NeededActionPoints)
            {
                ActionPoint -= NeededActionPoints;
                dmg = dice.DiceRoll(Damage) + 2;
            }
            else dmg = 0;
            return dmg;
        }
        */
    }

    public class PhisicalAttack : Player
    {
        public override int HeavyAttack(int damageOfItem) { return dice.DiceRoll(damageOfItem) + 30; }

        public override int LightAttack(int damageOfItem) { return dice.DiceRoll(damageOfItem) + 10; }

        /*
        public override int HeavyAttack(int damageOfItem)
        {
            int NeededActionPoints = 2;
            int dmg = 0;
            if (ActionPoint >= NeededActionPoints)
            {
                ActionPoint -= NeededActionPoints;
                dmg = dice.DiceRoll(damageOfItem) + 3;
            }
            else dmg = 0;
            return dmg;
        }

        public override int LightAttack(int damageOfItem)
        {
            int NeededActionPoints = 2;
            int dmg = 0;
            if (ActionPoint >= NeededActionPoints)
            {
                ActionPoint -= NeededActionPoints;
                dmg = dice.DiceRoll(damageOfItem) + 1;
            }
            else dmg = 0;
            return dmg;
        }
        */
    }

    #endregion

    #endregion



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

        }

        public Player InitialyEnemy(Character player)
        {

            int whoE = 1;
            Player returnEnemy = ChooseOfEnemy(whoE, player); // creation of enemy
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

        public string GetInventory(List<Weapon> inventory)
        {
            string a = "";
            foreach (Weapon i in inventory) a += i + Environment.NewLine;
            return a;
        }

        public virtual string WeaponInfo(List<Inventory> inventory) { return ""; }
        public virtual Weapon CreateSword(string name, int damage) { return null; }
    }

    public class Weapon : Inventory
    {
        protected string nameOfItem;
        protected int damageOfItem;

        public Weapon() { }
        public Weapon(string nme, int dmg)
        {
            nameOfItem = nme;
            damageOfItem = dmg;
        }

        public string ItemName { get { return nameOfItem; } set { nameOfItem = value; } }
        public int ItemDamage { get { return damageOfItem; } set { damageOfItem = value; } }

        public override string WeaponInfo(List<Inventory> inventory)
        {
            string a = "";
            foreach (Weapon it in inventory)
            {
                a += it.ItemName + it.ItemDamage + Environment.NewLine;
            }
            //return Convert.ToString(ItemName + " " + ItemDamage);
            return a;
        }

        public override Weapon CreateSword(string name, int damage)
        {
            Weapon item = new Sword(name, damage, 10);
            return item;
        }
    }

#region items

    class Sword : Weapon
    {
        int sharpness = 0;
        public Sword(string nme, int dmg, int sness) : base(nme,dmg) { sharpness = sness; }
        public int GetSwordSharpness() { return sharpness; }
    }

#endregion

#endregion

    public class Help
    {

        public string Name { get; set; }
        public int ClassP { get; set;  }
        public int Health { get; set;  }
        public int Damage { get; set;  }
        public int Defense { get; set; }
        public bool Clicked { get; set; }

    }
    
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
       // public int Length { get { return arenaSize; } }
        
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
                    int p = WIP + where;
                    try
                    {
                        /*
                        //if player is before or behind enemy
                        if (WIP == WIE - 1 || WIP == WIE + 1)
                        {
                            //before
                            if (WIP == WIE - 1 && WIE == gameArena[gameArena.Length])
                            {
                                
                                if (WIE + 1 < gameArena[gameArena.Length - 1]) 
                                {
                                    gameArena[WIE - 1] = '#';
                                    gameArena[WIE + 1] = 'P';
                                    WIP = WIE + 1;
                                }
                                else
                                {
                                    gameArena[WIE + 1] = '#';
                                    gameArena[WIE - 1] = 'P';
                                    WIP = WIE - 1;
                                }
                                gameArena[WIE - 1] = 'P';
                                WIP = WIE - 1;
                            }
                            else
                            {
                                gameArena[WIE - 1] = '#';
                                gameArena[WIE + 1] = 'P';
                                WIP = WIE + 1;
                            }
                            //behind
                            if (WIP == WIE + 1 && WIE != gameArena[0])
                            
                                else if (WIP == WIE + 1)
                                {
                                    if (WIE - 1 > gameArena[0])
                                    {
                                        gameArena[WIE + 1] = '#';
                                        gameArena[WIE - 1] = 'P';
                                        WIP = WIE - 1;
                                    }
                                    else
                                    {
                                        gameArena[WIE - 1] = '#';
                                        gameArena[WIE + 1] = 'P';
                                        WIP = WIE + 1;
                                    }
                                gameArena[WIE + 1] = 'P';
                                WIP = WIE + 1;
                            }
                            else
                            {
                                gameArena[WIE + 1] = '#';
                                gameArena[WIE - 1] = 'P';
                                WIP = WIE - 1;
                            }
                        }
                        */
                        //if player wants move before or behind the enemy
                        if (p < WIE || p > WIE)
                        {
                            gameArena[WIP] = '#';
                            gameArena[p] = 'P';
                            WIP = p;
                        }
                    }
                    catch
                    {
                        //if plazer is on right side
                        if (WIP == 0)
                        {
                            gameArena[0] = 'P';
                            WIP = 0;
                        }
                        //if plazer is on left side
                        else if (WIP == gameArena.Length - 1)
                        {
                            gameArena[gameArena.Length - 1] = 'P';
                            WIP = gameArena.Length - 1;
                        }
                    }
                    break;
                //enemy
                case 1:
                    int e = wIE + where;
                    try
                    {
                        //if enemy wants move before or behind the player
                        if (e < WIP || e > WIP)
                        {
                            gameArena[WIE] = '#';
                            gameArena[e] = 'E';
                            WIE = e;
                        }
                    }
                    catch
                    {
                        //if enemy is on right side
                        if (WIE == 0)
                        {
                            gameArena[0] = 'E';
                            WIE = 0;
                        }
                        //if enemy is on left side
                        else if (WIE == gameArena.Length - 1)
                        {
                            gameArena[gameArena.Length - 1] = 'E';
                            WIE = gameArena.Length - 1;
                        }
                    }

                    /*
                    try
                    {
                        if (WIE == WIP - 1 || WIE == WIP + 1)
                        {
                            if (WIE == WIP - 1)
                            {
                                gameArena[WIP - 1] = '#';
                                gameArena[WIP + 1] = 'E';
                                WIE = WIP + 1;

                            }

                            else if (WIE == WIP + 1)
                            {
                                gameArena[WIP + 1] = '#';
                                gameArena[WIP - 1] = 'E';
                                WIP = WIP - 1;
                            }

                        }
                        if (e < WIP || e > WIP)
                        {
                            gameArena[WIP] = '#';
                            gameArena[e] = 'E';
                            WIE = e;
                        }
                    }
                    catch
                    {

                        if (WIE == 0)
                        {
                            gameArena[0] = 'E';
                            WIE = 0;
                        }
                        else if (WIE == gameArena.Length - 1)
                        {
                            gameArena[gameArena.Length - 1] = 'E';
                            WIE = gameArena.Length - 1;
                        }

                    }
                    */
                    break;
            }
        }
    }

    public class LetsSayEngine
    {

        public Player player;
        public Player enemy;
        public Help he = new Help();
        Creation cr = new Creation();
        GameArena gA = new GameArena(10, 1, 9);
        Weapon mec = new Weapon();

        public List<Inventory> inventP = new List<Inventory>();
        public int whoP;
        public int whoE;

        /// <summary>
        /// 0 - warrior
        /// 1 - mage
        /// </summary/>

        public int classP;

        public LetsSayEngine() { }

        public void PlayerEnemyCreation(int h, int dam, int def, string name, int classP)
        {
            whoP = 0;

            player = cr.InitialyPlayer(h, dam, def, name, whoP, classP);

            enemy = cr.InitialyEnemy(player);

            whoE = enemy.Who;

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

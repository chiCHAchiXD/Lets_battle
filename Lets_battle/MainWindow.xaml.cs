using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LetsBattleBookCase;

namespace Lets_battle
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
#region variables
        public Player player;
        public Player enemy;
        Creation cr = new Creation();
        public int whoP;
        public int whoE;
        public int classP;
#endregion

        public MainWindow()
        {
            //it must be on top!!
            InitializeComponent();

            var createPlayer = new CreatePlayer();
            createPlayer.ShowDialog(); //createPlayer Window will show when you run this program
            string name = createPlayer.name;
            classP = createPlayer.classP;
            int h = createPlayer.health;
            int dam = createPlayer.damage;
            int def = createPlayer.defense;
            whoP = 0;
            
            player = cr.InitialyPlayer(h, dam, def, name, whoP, classP);
            enemy = cr.InitialyEnemy(player);
            whoE = enemy.Who;

            GetInformLb(4);
        }

#region infoInTb/Mb
        public void GetInformTb(string who, string withWhat, int howMuch)
        {
            //if player attacked
            if (who == player.Name)
            {
                Tb_get_informed.Text += ">> you attacked on " + enemy.Name + " with " + withWhat + " for " + howMuch + " damage!" + Environment.NewLine;
                Tb_get_informed.Text += "-------------------------------------" + Environment.NewLine;
            }

            //if enemy attacked
            else if (who == enemy.Name)
            {
                Tb_get_informed.Text += "<< " + who + " attacked on " + player.Name + " with " + withWhat + " for " + howMuch + " damage!" + Environment.NewLine;
                Tb_get_informed.Text += "-------------------------------------" + Environment.NewLine;
            }

            Tb_get_informed.ScrollToEnd();
        }

        public void GetInformTb(string whateverYouWant)
        {
            Tb_get_informed.Text += whateverYouWant + Environment.NewLine;
            Tb_get_informed.Text += "-------------------------------------" + Environment.NewLine;
            Tb_get_informed.ScrollToEnd();
        }

        public void GetInformMb(string who, string toWho, string withWhat, int howMuch)
        {
            MessageBox.Show(who + " attacked on " + toWho + " with " + withWhat + " for " + howMuch + " damage!");
        }

        public void GetInformMb(string whateverYouWant)
        {
            MessageBox.Show(whateverYouWant);
        }

        public void GetInformLb(int choice)
        {
            switch (choice)
            {
                case 1:
                    L_playerName.Content = player.Name;
                    L_playerLevel.Content = player.Level;
                    L_playerHealth.Content = player.Health;
                    L_playerDamage.Content = player.Damage;
                    L_playerDefense.Content = player.Defense;
                    break;
                case 2:
                    L_enemyName.Content = enemy.Name;
                    L_enemyHealth.Content = enemy.Health;
                    L_enemyDamage.Content = enemy.Damage;
                    L_enemyDefense.Content = enemy.Defense;
                    break;
                case 3:
                    //L_arena.Content = 
                    break;
                case 4:
                    L_playerName.Content = player.Name;
                    L_playerLevel.Content = player.Level;
                    L_playerHealth.Content = player.Health;
                    L_playerDamage.Content = player.Damage;
                    L_playerDefense.Content = player.Defense;

                    L_enemyName.Content = enemy.Name;
                    L_enemyHealth.Content = enemy.Health;
                    L_enemyDamage.Content = enemy.Damage;
                    L_enemyDefense.Content = enemy.Defense;
                    break;
                case 7:
                    L_playerName.Content = player.Name;
                    L_playerLevel.Content = player.Level;
                    L_playerHealth.Content = player.Health;
                    L_playerDamage.Content = player.Damage;
                    L_playerDefense.Content = player.Defense;

                    L_enemyName.Content = enemy.Name;
                    L_enemyHealth.Content = enemy.Health;
                    L_enemyDamage.Content = enemy.Damage;
                    L_enemyDefense.Content = enemy.Defense;
                    //L_arena.Content = 
                    break;
            }
        }
        #endregion

#region Ai
        public void WhoDied()
        {
            bool a = !player.IsAlive();
            if (a)
            {
                if (player.Health < 0) player.Health = 0;

                GetInformTb("you died" + Environment.NewLine);
                B_fight.IsEnabled = false;
            }

            else
            {
                GetInformTb(enemy.Name + " died" + Environment.NewLine);
                player.LevelIncrease(); // players livelo + 1
                cr.NextEnemy(whoE, player); // add another enemy
                GetInformLb(4);
            }

        }

        public void HowToEnemy()
        {

            string enemyM = Convert.ToString(enemy.Name);
            string playerM = Convert.ToString(player.Name);

            if (enemy is Mage)
            {
                int attack0 = enemy.FireBall(); //make damage value attack of FireBall
                int attack1 = enemy.IceBall(); //make damage value attack of IceBall
                if (attack1 > attack0)
                {
                    int a = enemy.Fight(player, attack1); //get real damage and attack on player
                    GetInformTb(enemyM, "FireBall", a); //get player informed
                } //if one is more damagefull

                else
                {
                    int a = enemy.Fight(player, attack0); //get real damage and attack on player
                    GetInformTb(enemyM, "IceBall", a); //get player informed
                } //if another is more damagefull 
            }

            else if (enemy is Warrior)
            {
                int attack0 = enemy.LightAttack(10);//inventoryEnemy.GetItemDamage((Item)game.inventoryListEnemy[0]));
                int attack1 = enemy.HeavyAttack(10);//inventoryEnemy.GetItemDamage((Item)game.inventoryListEnemy[0]));

                if (attack1 > attack0)
                {
                    int a = enemy.Fight(player, attack1);
                    GetInformTb(enemyM, "Heavy Attack", a);
                }

                else
                {
                    int a = enemy.Fight(player, attack0);
                    GetInformTb(enemyM, "Light Attack", a);
                }
            }
        }

        public void HowToPlayer(int B_number)
        {
            if (player is Mage)
            {
                switch (B_number)
                {
                    case 0:
                        int attack = player.Fight(enemy, player.FireBall());
                        GetInformTb(player.Name, "FireBall", attack);
                        break;
                    case 1:
                        int attack1 = player.Fight(enemy, player.IceBall());
                        GetInformTb(player.Name, "IceBall", attack1);
                        break;
                }
            }

            else if (player is Warrior)
            {
                switch (B_number)
                {
                    //if 0 then attack with Heavy Attack
                    case 0:
                        int attack = player.Fight(enemy, player.HeavyAttack(10));//inventoryPlayer.GetItemDamage((Item)game.inventoryListPlayer[0])));
                        GetInformTb(player.Name, "Heavy Attack", attack);
                        break;
                    //if 1 then attack with Light Attack
                    case 1:
                        int attack1 = player.Fight(enemy, player.LightAttack(10));//inventoryPlayer.GetItemDamage((Item)game.inventoryListPlayer[0])));
                        GetInformTb(player.Name, "Light Attack", attack1);
                        break;
                }
            }
        }

        public void HowShouldEveryoneAttack(int B_number)
        {
            Dice dice = new Dice();
            int whoWillAttackFirst = dice.DiceRoll();
            int whoAttacked;

            //if sude player first attack
            if (whoWillAttackFirst % 2 == 0)
            {
                HowToPlayer(B_number);
                whoAttacked = 0;
            }

            //if sude enemyfirst attack
            else
            {
                HowToEnemy();
                whoAttacked = 1;
            }

            GetInformLb(4);

            //based on whoattacked the other one will be next
            switch (whoAttacked)
            {
                //enemy
                case 0:
                    HowToEnemy();
                    break;
                //player
                case 1:
                    HowToPlayer(B_number);
                    break;
            }

            GetInformLb(4);

            if (!player.IsAlive() || !enemy.IsAlive())
                WhoDied();
        }
#endregion
        private void B_fight_Click(object sender, RoutedEventArgs e)
        {
            //player.Fight(enemy, 10);
            HowShouldEveryoneAttack(1);
            GetInformLb(4);
            /*
            switch (classP)
            {
                case 0:
                    var attackWarior = new AttackWarrior();
                    attackWarior.ShowDialog();
                    break;
                case 1:
                    var attackMage = new AttackMage();
                    attackMage.ShowDialog();
                    break;
            }
            */
        }

        private void B_inventory_Click(object sender, RoutedEventArgs e)
        {
            /*
            var inventory = new PlayerInventory();
            inventory.ShowDialog();
            */
        }

        private void B_transfer_Click(object sender, RoutedEventArgs e)
        {
            /*
            if ((sender as Button) == B_go_left)
            {

            }

            else if ((sender as Button) == B_go_right)
            {
                wm.GetInformedIntoLabels(3, player, enemy);
                game.Move(player, +1);
                wm.GetInformedIntoLabels(3, player, enemy);
            }
            */
        }

        private void Key_pressed(object sender, KeyEventArgs e)
        {
            /*
            if (e.Key == Key.Left)
            {
                wm.GetInformedIntoLabels(3, player, enemy);
                game.Move(player, -1);
                wm.GetInformedIntoLabels(3, player, enemy);
            }
            else if (e.Key == Key.Right)
            {
                wm.GetInformedIntoLabels(3, player, enemy);
                game.Move(player, +1);
                wm.GetInformedIntoLabels(3, player, enemy);
            }
            */
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class LetsBattle : Window
    {

#region variables

        public Player player;
        public Player enemy;

        public LetsSayEngine lse = new LetsSayEngine();
        public Help he = new Help();
        Creation cr = new Creation();
        GameArena gA = new GameArena(10, 1, 9);
        Weapon mec = new Weapon();

        string name;
        int h;
        int dam;
        int def;

        public List<Inventory> inventP = new List<Inventory>();

        public int whoP;
        public int whoE;

        /// <summary>
        /// 0 - warrior
        /// 1 - mage
        /// </summary>
        public int classP;

        #endregion

        public LetsBattle()
        {
            //it must be on top!!
            InitializeComponent();

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            //lse.PlayerEnemyCreation(((MainWindow)Application.Current.MainWindow).help.Health, ((MainWindow)Application.Current.MainWindow).help.Damage, ((MainWindow)Application.Current.MainWindow).help.Defense, ((MainWindow)Application.Current.MainWindow).help.Name, ((MainWindow)Application.Current.MainWindow).help.ClassP,);


            SetStatsToLetsBattle();
            

            inventP.Add(mec.CreateSword("mec", 10));
            inventP.Add(mec.CreateSword("novy mec", 20));
            
            player = cr.InitialyPlayer(h, dam, def, name, whoP, classP);
            enemy = cr.InitialyEnemy(player);

            whoE = enemy.Who;

            Pb_player_health.Maximum = player.Health;
            Pb_player_health.Value = player.Health;

            Pb_enemy_health.Maximum = enemy.Health;
            Pb_enemy_health.Value = enemy.Health;

            Gb_attack_warrior.Visibility = Visibility.Hidden;
            Gb_attack_mage.Visibility = Visibility.Hidden;
            Gb_increase_level.Visibility = Visibility.Hidden;

            GetInformLb(7);
            
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

        public void GetInformMb(string who, string toWho, string withWhat, int howMuch) { MessageBox.Show(who + " attacked on " + toWho + " with " + withWhat + " for " + howMuch + " damage!"); }

        public void GetInformMb(string whateverYouWant) { MessageBox.Show(whateverYouWant); }

        public void GetInformLb(int choice)
        {
            switch (choice)
            {
                case 1:
                    //player
                    L_playerName.Content = player.Name;
                    L_playerLevel.Content = player.Level;
                    L_playerHealth.Content = player.Health;
                    L_playerDamage.Content = player.Damage;
                    L_playerDefense.Content = player.Defense;

                    Pb_player_health.Value = player.Health;
                    break;
                case 2:
                    //enemy
                    L_enemyName.Content = enemy.Name;
                    L_enemyHealth.Content = enemy.Health;
                    L_enemyDamage.Content = enemy.Damage;
                    L_enemyDefense.Content = enemy.Defense;

                    Pb_enemy_health.Value = enemy.Health;
                    break;
                case 3:
                    //arena
                    L_arena.Content = gA.GetGameArenaLook();
                    break;
                case 4:
                    //player
                    L_playerName.Content = player.Name;
                    L_playerLevel.Content = player.Level;
                    L_playerHealth.Content = player.Health;
                    L_playerDamage.Content = player.Damage;
                    L_playerDefense.Content = player.Defense;

                    Pb_player_health.Value = player.Health;
                    //enemy
                    L_enemyName.Content = enemy.Name;
                    L_enemyHealth.Content = enemy.Health;
                    L_enemyDamage.Content = enemy.Damage;
                    L_enemyDefense.Content = enemy.Defense;

                    Pb_enemy_health.Value = enemy.Health;
                    break;
                case 7:
                    //player
                    L_playerName.Content = player.Name;
                    L_playerLevel.Content = player.Level;
                    L_playerHealth.Content = player.Health;
                    L_playerDamage.Content = player.Damage;
                    L_playerDefense.Content = player.Defense;

                    Pb_player_health.Value = player.Health;
                    //enemy
                    L_enemyName.Content = enemy.Name;
                    L_enemyHealth.Content = enemy.Health;
                    L_enemyDamage.Content = enemy.Damage;
                    L_enemyDefense.Content = enemy.Defense;

                    Pb_enemy_health.Value = enemy.Health;
                    //arena
                    L_arena.Content = gA.GetGameArenaLook();
                    //L_action_pts.Content = player.ActionPoint;
                    break;
            }
        }

        #endregion

#region methods
        
        void SetStatsToLetsBattle()
        {

            name = ((MainWindow)Application.Current.MainWindow).help.Name;
            classP = ((MainWindow)Application.Current.MainWindow).help.ClassP;
            h = ((MainWindow)Application.Current.MainWindow).help.Health;
            dam = ((MainWindow)Application.Current.MainWindow).help.Damage;
            def = ((MainWindow)Application.Current.MainWindow).help.Defense;
            whoP = 0;

        }

        void SetStatsToMainWindowOnClose()
        {

            ((MainWindow)Application.Current.MainWindow).help.Health = player.Health;
            ((MainWindow)Application.Current.MainWindow).help.Damage = player.Damage;
            ((MainWindow)Application.Current.MainWindow).help.Defense = player.Defense;
            ((MainWindow)Application.Current.MainWindow).help.Name = player.Name;
            ((MainWindow)Application.Current.MainWindow).help.ClassP = classP;

        }

        void CloseAndOpenParrent()
        {

            SetStatsToMainWindowOnClose();

            Close();

            ((MainWindow)Application.Current.MainWindow).Show();

        }

        void ShowStats()
        {

            Lb_stats.Items.Clear();
            Lb_stats.Items.Add("Health: " + player.Health);
            Lb_stats.Items.Add("Damage: " + player.Damage);
            Lb_stats.Items.Add("Defense: " + player.Defense);

        }

        void IncreaseLevelShow()
        {
            
            if (player.IncreasePoint != 0)
            {

                ShowStats();

                Gb_increase_level.Visibility = Visibility.Visible;

            }
            
        }

        void IncreaseStats()
        {

            if (player.IncreasePoint > 1)
            {

                int selIn = Lb_stats.SelectedIndex;

                player.IncreaseCurrentStat(selIn);

                ShowStats();

                player.IncreasePoint--;

            }

            else Gb_increase_level.Visibility = Visibility.Collapsed;

        }

        void FightMenu()
        {

            switch (classP)
            {

                case 0:

                    Gb_attack_mage.Visibility = Visibility.Hidden;
                    Gb_attack_warrior.Visibility = Visibility.Visible;

                    break;

                case 1:

                    Gb_attack_warrior.Visibility = Visibility.Hidden;
                    Gb_attack_mage.Visibility = Visibility.Visible;

                    break;

            }

        }

        void MoveLeft()
        {

            GetInformLb(3);
            gA.Move(player, -1);
            gA.Move(enemy, 1);
            GetInformLb(3);

        }

        void MoveRight()
        {

            GetInformLb(3);
            gA.Move(player, +1);
            gA.Move(enemy, -1);
            GetInformLb(3);

        }

#region Ai

        public void WhoDied()
        {

            bool a = !player.IsAlive();

            if (a)
            {

                if (player.Health < 0) player.Health = 0;

                GetInformTb("you died" + Environment.NewLine);
                B_fight.IsEnabled = false;
                B_attack_fireball.IsEnabled = false;
                B_attack_iceball.IsEnabled = false;
                B_attack_heavy.IsEnabled = false;
                B_attack_light.IsEnabled = false;

            }

            else
            {

                Gb_attack_warrior.Visibility = Visibility.Hidden;
                Gb_attack_mage.Visibility = Visibility.Hidden;

                GetInformTb(enemy.Name + " died" + Environment.NewLine);

                player.IncreasePoint++;
                enemy = cr.NextEnemy(whoE, player); // add another enemy

                GetInformLb(4);

            }

        }

        public void HowToEnemy()
        {
            string enemyM = Convert.ToString(enemy.Name);
            string playerM = Convert.ToString(player.Name);

            if (enemy is Mage)
            {

                int attack0 = enemy.FireBall();
                int attack1 = enemy.IceBall();

                if (attack1 > attack0)
                {

                    int a = enemy.Fight(player, attack1);

                    GetInformTb(enemyM, "FireBall", a);

                } 

                else
                {

                    int a = enemy.Fight(player, attack0);

                    GetInformTb(enemyM, "IceBall", a);

                }

            }

            else if (enemy is Warrior)
            {

                gA.Move(enemy, 1);

                int attack0 = enemy.LightAttack(10);
                int attack1 = enemy.HeavyAttack(10);

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

                    //if 0 Heavy Attack
                    case 0:

                        int attack = player.Fight(enemy, player.HeavyAttack(10));//inventoryPlayer.GetItemDamage((Item)game.inventoryListPlayer[0])));
                        GetInformTb(player.Name, "Heavy Attack", attack);

                        break;

                    //if 1 Light Attack
                    case 1:

                        int attack1 = player.Fight(enemy, player.LightAttack(10));//inventoryPlayer.GetItemDamage((Item)game.inventoryListPlayer[0])));
                        GetInformTb(player.Name, "Light Attack", attack1);

                        break;

                }

            }
        }

        public void HowShouldEveryoneAttack(int B_number)
        {
            HowToPlayer(B_number);
            HowToEnemy();
            GetInformLb(7);
            /*
            Dice dice = new Dice();
            int whoWillAttackFirst = dice.DiceRoll();
            int whoAttacked;
            */
            /*
            if (player.HasActionPts())
            {
                HowToPlayer(B_number);
            }
            */
            //if sude player first attack
            /*if (whoWillAttackFirst % 2 == 0)
            {
                HowToPlayer(B_number);
                whoAttacked = 0;
            }
            */
            //if sude enemyfirst attack
            /*
            if (enemy.HasActionPts())
            {
                HowToEnemy();
            }
            */
            /*
            else
            {
                HowToEnemy();
                whoAttacked = 1;
            }
            */
            //GetInformLb(7);
            //based on whoattacked the other one will be next
            /*
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
            */
            //GetInformLb(7);

            //player.ActionPoint++;

            if (!player.IsAlive() || !enemy.IsAlive()) WhoDied();

            GetInformLb(7);
        }

        #endregion

        #endregion

#region buttons

        void B_click(object sender, RoutedEventArgs e)
        {

            if ((sender as Button) == B_fight) { FightMenu(); }

            else if ((sender as Button) == B_inventory) { GetInformTb(mec.WeaponInfo(inventP)); }

            else if ((sender as Button) == B_go_left) { MoveLeft(); }

            else if ((sender as Button) == B_go_right) { MoveRight(); }

            else if ((sender as Button) == B_increase_level) { IncreaseLevelShow(); }

            else if ((sender as Button) == B_increase_stat) { IncreaseStats(); }

            else if ((sender as Button) == B_close) { CloseAndOpenParrent(); }

        }

        void Key_pressed(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Left) { MoveLeft(); }

            else if (e.Key == Key.Right) { MoveRight(); }
                
        }

        void B_attack_Click(object sender, RoutedEventArgs e)
        {
            switch (classP)
            {
                case 0:
                    HowShouldEveryoneAttack(0);
                    GetInformLb(4);
                    break;

                case 1:
                    HowShouldEveryoneAttack(1);
                    GetInformLb(4);
                    break;
            }
        }

#endregion

    }

}

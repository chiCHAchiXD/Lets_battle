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
using System.Windows.Shapes;
using LetsBattleBookCase;
using System.IO;

namespace Lets_battle
{
    /// <summary>
    /// Interakční logika pro Start.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string name;
        public int classP;
        public int h;
        public int dam;
        public int def;
        public Help he = new Help();

        public MainWindow()
        {
            InitializeComponent();

            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_load.Visibility = Visibility.Collapsed;
           // Gb_settings.Visibility = Visibility.Collapsed;
        }

        private void B_start_Click(object sender, RoutedEventArgs e)
        {
            var letsBattleWindow = new LetsBattle();
            Close();
            letsBattleWindow.ShowDialog();
        }

        private void B_new_player(object sender, RoutedEventArgs e)
        {
            Gb_create_player.Visibility = Visibility.Visible;

            //Help h = new Help();
            //var createPlayer = new CreatePlayer();

            Width = 638;
            

            //createPlayer.ShowDialog(); //createPlayer Window will show when you run this program
            /*
            h.ClassP = createPlayer.classP;
            h.Health = createPlayer.health;
            h.Name = createPlayer.name;
            h.Defense = createPlayer.defense;
            
            name = createPlayer.he.Name;
            classP = createPlayer.he.ClassP;
            h = createPlayer.he.Health;
            dam = createPlayer.he.Damage;
            def = createPlayer.he.Defense;
            */
        }

        public void B_readInput_Click(object sender, RoutedEventArgs e)
        {
            /*
            he.Name = tb_inputName.Text;
            he.ClassP = cb_Class.SelectedIndex;

            he.Health = Convert.ToInt32(tb_Health.Text);
            he.Damage = Convert.ToInt32(tb_Damage.Text);
            he.Defense = Convert.ToInt32(tb_Defense.Text);

            Close();
            */
            
            name = tb_inputName.Text;
            classP = cb_Class.SelectedIndex;
            h = Convert.ToInt32(tb_Health.Text);
            dam = Convert.ToInt32(tb_Damage.Text);
            def = Convert.ToInt32(tb_Defense.Text);
            Gb_load.Visibility = Visibility.Collapsed;
            Width = 293.826;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            he.ClassP = cb_Class.SelectedIndex;
            Dice dice = new Dice();

            int h = dice.DiceRoll(20);
            int da = dice.DiceRoll(20);
            int de = dice.DiceRoll(20);

            h += dice.DiceRoll(5);
            da += dice.DiceRoll(5);
            de += dice.DiceRoll(5);

            #region IDontKnow

            tb_Health.Text = Convert.ToString(h);
            tb_Damage.Text = Convert.ToString(da);
            tb_Defense.Text = Convert.ToString(de);

            int tbH = Convert.ToInt16(tb_Health.Text);
            int tbDa = Convert.ToInt16(tb_Damage.Text);
            int tbDe = Convert.ToInt16(tb_Defense.Text);

            #endregion

            switch (he.ClassP)
            {
                case 0:
                    if (tbH < 7 || tbDa < 10 || tbDe < 5)
                    {
                        tb_Health.Text = 7.ToString();
                        tb_Damage.Text = 10.ToString();
                        tb_Defense.Text = 5.ToString();
                    }
                    break;
                case 1:
                    if (tbH < 10 || tbDa < 8 || tbDe < 9)
                    {
                        tb_Health.Text = 10.ToString();
                        tb_Damage.Text = 8.ToString();
                        tb_Defense.Text = 9.ToString();
                    }
                    break;
            }
        }
        
        private void B_settings_Click(object sender, RoutedEventArgs e)
        {
            Width = 638;
            //Gb_settings.Visibility = Visibility.Visible;
        }

        private void B_save_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("save.txt");

            //sw.Write(DateTime.Now + "-" + name + "-" + classP + "-" + h + "-" + dam + "-" + def);
            sw.Write(DateTime.Now + Environment.NewLine + name + Environment.NewLine + classP + Environment.NewLine + h + Environment.NewLine + dam + Environment.NewLine + def);

            sw.Close();
        }

        private void B_load_Click(object sender, RoutedEventArgs e)
        {
            Gb_load.Visibility = Visibility.Visible;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Width = 638;
            if ((sender as Button) == B_load)
            {
                int newLineCounter = 0;
                var sr = new StreamReader("save.txt");
                //List<string> line = new List<string>();
                string[] line = new string[1];
                while ((line[0] = sr.ReadLine()) != null)
                {
                    Tb_saved_characters.Text += line[0] + ", "; // + Environment.NewLine;
                    if (newLineCounter == 1)
                    {
                        Cb_choosed_character.Items.Add(line[0]);
                    }
                    newLineCounter++;
                }
                sr.Close();
            }
            else if((sender as Button) == B_load_player)
            {

            }
        }

        private void B_author_Click(object sender, RoutedEventArgs e)
        {

        }

        private void B_close_Click(object sender, RoutedEventArgs e) { Close(); }
    }
}

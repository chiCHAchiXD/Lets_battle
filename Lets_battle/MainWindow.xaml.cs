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
using System.Windows.Threading;

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
            Gb_author.Visibility = Visibility.Collapsed;
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
            for (double i = 296.305; i < 638; i++)
            {
                Width = i;
            }
            Gb_create_player.Visibility = Visibility.Visible;
        }

        public void B_readInput_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                name = tb_inputName.Text;
                classP = cb_Class.SelectedIndex;
                h = Convert.ToInt32(tb_Health.Text);
                dam = Convert.ToInt32(tb_Damage.Text);
                def = Convert.ToInt32(tb_Defense.Text);
            }
            catch { }
            
            tb_inputName.Text = null;
            cb_Class.SelectedValue = -1;
            tb_Health.Text = "";
            tb_Damage.Text = "";
            tb_Defense.Text = "";

            for (double i = 638; i > 296.305; i--)
            {
                Width = i;
            }

            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
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
            for (double i = 296.305; i < 638; i++)
            {
                Width = i;
            }
            //Gb_settings.Visibility = Visibility.Visible;
        }

        private void B_save_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("save.txt", append: true);
            
            sw.Write(DateTime.Now + Environment.NewLine + name + Environment.NewLine + classP + Environment.NewLine + h + Environment.NewLine + dam + Environment.NewLine + def + Environment.NewLine);

            MessageBox.Show("player "+ name + " was saved and you can load him anytime you want in load section");

            sw.Close();
        }

        private void B_load_Click(object sender, RoutedEventArgs e)
        {
            for (double i = 296.305; i < 638; i++)
            {
                Width = i;
            }
            Gb_load.Visibility = Visibility.Visible;
            Gb_create_player.Visibility = Visibility.Collapsed;
            //Width = 638;
            if ((sender as Button) == B_load)
            {
                int whereIsNextName = 1;
                int whereIsNextSave = 5;
                int newLineCounter = 0;
                var sr = new StreamReader("save.txt");
                string[] line = new string[1];
                while ((line[0] = sr.ReadLine()) != null)
                {
                    Tb_saved_characters.Text += line[0] + ", ";

                    if (newLineCounter == whereIsNextName)
                    {
                        Cb_choosed_character.Items.Add(line[0]);
                        whereIsNextName += 6;
                    }

                    if (newLineCounter == whereIsNextSave)
                    {
                        Tb_saved_characters.Text += Environment.NewLine;
                        whereIsNextSave += 6;
                    }

                    newLineCounter++;
                }
                sr.Close();
            }

            else if((sender as Button) == B_load_player)
            {
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                for (double i = 638; i > 296.305; i--)
                {
                    Width = i;
                }
            }
        }

        private void B_author_Click(object sender, RoutedEventArgs e)
        {
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Visible;

            for (double i = 296.305; i < 638; i++)
            {
                Width = i;
            }
        }

        private void B_close_Click(object sender, RoutedEventArgs e) { Close(); }
        
        private void B_delete_Click(object sender, RoutedEventArgs e)
        {
            //int removeEntry = Convert.ToInt32(Cb_choosed_character.SelectedValue.ToString());
            
            string removeEntry = Convert.ToString(Cb_choosed_character.SelectionBoxItem);
            MessageBox.Show(removeEntry);

            
            var file = new List<string>(File.ReadAllLines("save.txt"));

            var sr = new StreamReader("save.txt");
            
            string[] line = new string[1];
            
            while ((line[0] = sr.ReadLine()) != null)
            {
                /*if (line[0] != removeEntry)
                {
                    file += Convert.ToString(line[0]);
                }
                else sw.Write("ahoj");
                sw.Close();
                */
                //File.WriteAllLines("save.txt", file.ToArray());
            }
            
            //file.RemoveAt(removeEntry);
            //File.WriteAllLines("save.txt", file.ToArray());

           
           // var sr = new StreamReader("save.txt");
            var sw = new StreamWriter("save.txt", true);
          //  string[] line = new string[1];

            for (int i = 0; i < Tb_saved_characters.LineCount; i++)
            {
            }
            
        }

        private void B_close_author(object sender, RoutedEventArgs e)
        {
        
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            for (double i = 638; i > 296.305; i--)
            {
                Width = i;
            }
            
        }
    }
}

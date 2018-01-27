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
    public partial class MainWindow : Window
    {

#region variables

        public string name;
        public int classP;
        public int h;
        public int dam;
        public int def;

        public Help help = new Help();
        

        #endregion

        public MainWindow()
        {
            InitializeComponent(); //Must be on TOP
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            // Gb_settings.Visibility = Visibility.Collapsed;
        }

#region methods

        private void Start()
        {

            var letsBattleWindow = new LetsBattle();
            // Hide();
            Close();
            letsBattleWindow.ShowDialog();

        }
        
        private void Save()
        {
            
            StreamWriter sw = new StreamWriter("save.txt", append: true);
            //sw.Write(DateTime.Now + Environment.NewLine + name + Environment.NewLine + classP + Environment.NewLine + h + Environment.NewLine + dam + Environment.NewLine + def + Environment.NewLine);
            sw.Write(DateTime.Now + ", " + name + ", " + classP + ", " + h + ", " + dam + ", " + def + Environment.NewLine);
            MessageBox.Show("player " + name + " was saved and you can load him anytime you want in load section");
            sw.Close();
            
        }

#region player

        private void LoadPlayer()
        {

            B_load.IsEnabled = true;
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Width = 296.305;

            string[] arr = new string[10];

            for (int i = 0; i < Tb_saved_characters.SelectedItems.Count; i++)
            {
                string record = Tb_saved_characters.Items[Tb_saved_characters.SelectedIndex].ToString();
                arr = record.Split(',');
                //MessageBox.Show(record);
            }

            help.Name = arr[1];
            help.ClassP = Convert.ToInt32(arr[2]);
            help.Health = Convert.ToInt32(arr[3]);
            help.Damage = Convert.ToInt32(arr[4]);
            help.Defense = Convert.ToInt32(arr[5]);

            /*
           String text = help.Name + "; " + help.ClassP + "; " + help.Health + "; " + help.Damage + "; " + help.Defense;
           MessageBox.Show(text);
            */
            /*
             name = arr[1];
             classP = Convert.ToInt32(arr[2]);
             h = Convert.ToInt32(arr[3]);
             dam = Convert.ToInt32(arr[4]);
             def = Convert.ToInt32(arr[5]);
             String text = name + "; " + classP + "; " + h + "; " + dam + "; " + def;
             MessageBox.Show(text);
             */

        }

        private void DeletePlayer()
        {

            StreamWriter sw = new StreamWriter("save.txt");

            Tb_saved_characters.Items.RemoveAt(Tb_saved_characters.SelectedIndex);

            string record = "";

            for (int i = 0; i < Tb_saved_characters.Items.Count; i++)
            {

                record += Tb_saved_characters.Items[i].ToString() + Environment.NewLine;

            }

            sw.Write(record);
            sw.Close();

        }

        private void LoadToLb()
        {
            Tb_saved_characters.Items.Clear();
            Width = 638;

            Gb_load.Visibility = Visibility.Visible;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;

            int whereIsNextSave = 5;
            int newLineCounter = 0;

            var sr = new StreamReader("save.txt");

            string[] line = new string[1];

            while ((line[0] = sr.ReadLine()) != null)
            {

                Tb_saved_characters.Items.Add(line[0] + ",");

                if (newLineCounter == whereIsNextSave)
                {

                    Tb_saved_characters.Items.Add(Environment.NewLine);
                    whereIsNextSave += 6;

                }

                newLineCounter++;

            }

            sr.Close();

        }

        private void OpenCreatePlayer()
        {
            // Gb_settings.Visibility = Visibility.Collapsed;

            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Visible;
            Width = 638;

            /*
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Width = 296.305;
            */
        }

        private void ReadInput()
        {
            help.Name = tb_inputName.Text;
            help.ClassP = cb_Class.SelectedIndex;
            help.Health = Convert.ToInt32(tb_Health.Text);
            help.Damage = Convert.ToInt32(tb_Damage.Text);
            help.Defense = Convert.ToInt32(tb_Defense.Text);
            
            tb_inputName.Text = null;
            cb_Class.SelectedValue = -1;
            tb_Health.Text = "";
            tb_Damage.Text = "";
            tb_Defense.Text = "";

            Width = 296.305;
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
        }

        private void RandomStat()
        {
            classP = cb_Class.SelectedIndex;
            Dice dice = new Dice();

            int h = 0;
            int da = 0;
            int de = 0;

            switch (classP)
            {
                case 0:

                    h = dice.DiceRoll(21) + dice.DiceRoll(11);
                    da = dice.DiceRoll(19) + dice.DiceRoll(9);
                    de = dice.DiceRoll(18) + dice.DiceRoll(8);

                    #region if

                    if ( h < 11 ) 
                    {

                        h = 11;
                        
                    }

                    if ( da < 9 )
                    {

                        da = 9;

                    }

                    if ( de < 8 )
                    {

                        de = 8;

                    }

                    #endregion

                    break;

                case 1:

                    h = dice.DiceRoll(18) + dice.DiceRoll(8);
                    da = dice.DiceRoll(22) + dice.DiceRoll(12);
                    de = dice.DiceRoll(15) + dice.DiceRoll(5);

                    #region if

                    if (h < 8)
                    {

                        h = 8; 

                    }

                    if (da < 12)
                    {

                        da = 12;

                    }

                    if (de < 5)
                    {

                        de = 5;

                    }

                    #endregion

                    break;
            }

            tb_Health.Text = h.ToString();
            tb_Damage.Text = da.ToString();
            tb_Defense.Text = de.ToString();
            
        }

        #region author

        private void OpenAuthor()
        {

            Gb_load.Visibility = Visibility.Visible;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Visible;
            Width = 638;

            /*
            Width = 296.305;
            Gb_load.Visibility = Visibility.Visible;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            */

        }
        
        private void CloseAuthor()
        {

            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Width = 296.305;

        }

        #endregion

        private void OpenSettings()
        {

            Width = 638;

            // Gb_settings.Visibility = Visibility.Visible;
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            //Width = 296.305;

        }

        #endregion

        #endregion

        private void B_click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) == B_start) { Start(); }

#region player
            else if ((sender as Button) == B_create_player) { OpenCreatePlayer(); }

            else if ((sender as Button) == B_read_input) { ReadInput(); }
            
            else if ((sender as Button) == B_random_stats) { RandomStat(); }
            
            #endregion

#region load/save_player

            else if ((sender as Button) == B_save) { Save(); }
            
            else if ((sender as Button) == B_load) { LoadToLb(); }
            
            else if ((sender as Button) == B_load_player) { LoadPlayer(); }
            
            else if ((sender as Button) == B_delete_player) { DeletePlayer(); }
           
            #endregion

#region author
            else if ((sender as Button) == B_author) { OpenAuthor(); }
            
            else if ((sender as Button) == B_author_close) { CloseAuthor(); }
            
            #endregion

            else if ((sender as Button) == B_settings) { OpenSettings(); }
            
            else if ((sender as Button) == B_close) { Hide(); /*Close();*/ }
        }
    }
}
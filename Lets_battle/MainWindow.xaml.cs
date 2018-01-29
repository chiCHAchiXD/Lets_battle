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

        /*
        public string name;
        public int classP;
        public int h;
        public int dam;
        public int def;
        */

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

        void Start()
        {

            var letsBattleWindow = new LetsBattle();

            Hide();
            //Close();
            
            letsBattleWindow.ShowDialog();
            
        }
        
        void Save()
        {
            
            StreamWriter sw = new StreamWriter("save.txt", append: true);

            //sw.Write(DateTime.Now + Environment.NewLine + name + Environment.NewLine + classP + Environment.NewLine + h + Environment.NewLine + dam + Environment.NewLine + def + Environment.NewLine);

            sw.Write(DateTime.Now + ", " + help.Name + ", " + help.ClassP + ", " + help.Vitality + ", " + help.Strength + ", " + help.Inteligence + ", " + help.Wisdom + Environment.NewLine);
            MessageBox.Show("player " + help.Name + " was saved and you can load him anytime you want in load section");

            sw.Close();
            
        }

#region player

        void HideComponents()
        {

            //Gb_settings.Visibility = Visibility.Collapsed;
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Width = 296.305;

        }

        void ShowComponent()
        {

        }

        void LoadPlayer()
        {

            B_load.IsEnabled = true;

            HideComponents();

            string[] arr = new string[10];

            for (int i = 0; i < Tb_saved_characters.SelectedItems.Count; i++)
            {
                string record = Tb_saved_characters.Items[Tb_saved_characters.SelectedIndex].ToString();
                arr = record.Split(',');
                //MessageBox.Show(record);
            }

            help.Name = arr[1];
            help.ClassP = Convert.ToInt32(arr[2]);
            help.Vitality = Convert.ToInt32(arr[3]);
            help.Strength = Convert.ToInt32(arr[4]);
            help.Inteligence = Convert.ToInt32(arr[5]);
            help.Wisdom = Convert.ToInt32(arr[6]);

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

        void DeletePlayer()
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

        void LoadToLb()
        {
            Tb_saved_characters.Items.Clear();
            
            HideComponents();

            Gb_load.Visibility = Visibility.Visible;
            Width = 638;

            int whereIsNextSave = 5;
            int newLineCounter = 0;

            try
            {

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

            catch {  }

            

        }

        void OpenCreatePlayer()
        {
            // Gb_settings.Visibility = Visibility.Collapsed;

            Gb_load.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            Gb_create_player.Visibility = Visibility.Visible;
            Width = 638;

            /*
            HideComponents();
            */
        }

        void ReadInput()
        {

            help.Name = Tb_input_name.Text;
            help.ClassP = Cb_class.SelectedIndex;
            help.Vitality = Convert.ToInt32(Tb_vitality.Text);
            help.Strength = Convert.ToInt32(Tb_strength.Text);
            help.Wisdom = Convert.ToInt32(Tb_wisdom.Text);
            help.Inteligence = Convert.ToInt32(Tb_inteligence.Text);

            Tb_input_name.Text = null;
            Cb_class.SelectedValue = -1;
            Tb_vitality.Text = "";
            Tb_strength.Text = "";
            Tb_wisdom.Text = "";
            Tb_inteligence.Text = "";

            HideComponents();

        }

        void RandomStat()
        {
            help.ClassP = Cb_class.SelectedIndex;
            Dice dice = new Dice();

            int vit = 0;
            int str = 0;
            int inte = 0;
            int wis = 0;

            switch (help.ClassP)
            {
                case 0:

                    vit = dice.DiceRoll(21) + dice.DiceRoll(11);
                    str = dice.DiceRoll(19) + dice.DiceRoll(9);
                    inte = dice.DiceRoll(18) + dice.DiceRoll(8);
                    wis = dice.DiceRoll(10);
                    #region if

                    if (vit < 11) 
                    {

                        vit = 11;
                        
                    }

                    if (str < 9)
                    {

                        str = 9;

                    }
                    /*
                    if (inte < 8)
                    {

                        inte = 8;

                    }
                    */
                    #endregion

                    break;

                case 1:

                    vit = dice.DiceRoll(18) + dice.DiceRoll(8);
                    str = dice.DiceRoll(22) + dice.DiceRoll(12);
                    inte = dice.DiceRoll(15) + dice.DiceRoll(5);
                    wis = dice.DiceRoll(20);

                    #region if
                    /*
                    if (vit < 8)
                    {

                        vit = 8; 

                    }

                    if (str < 12)
                    {

                        str = 12;

                    }
                    */
                    if (inte < 10)
                    {

                        inte = 10;

                    }

                    if (wis < 10)
                    {

                        wis = 10;

                    }

                    #endregion

                    break;
            }

            Tb_vitality.Text = vit.ToString();
            Tb_strength.Text = str.ToString();
            Tb_inteligence.Text = inte.ToString();
            Tb_wisdom.Text = wis.ToString();
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

        void OpenSettings()
        {

            Width = 638;

            // Gb_settings.Visibility = Visibility.Visible;

            //HideComponents();

        }

        #endregion

        #endregion

        void B_click(object sender, RoutedEventArgs e)
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
            
            else if ((sender as Button) == B_close) { Close(); }
        }
    }
}
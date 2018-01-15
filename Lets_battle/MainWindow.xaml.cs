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


        public MainWindow()
        {
            InitializeComponent();
        }

        private void B_start_Click(object sender, RoutedEventArgs e)
        {
            var letsBattleWindow = new LetsBattle();
            Close();
            letsBattleWindow.ShowDialog();
        }

        private void B_new_player(object sender, RoutedEventArgs e)
        {
            //Help h = new Help();
            var createPlayer = new CreatePlayer();
            
            createPlayer.ShowDialog(); //createPlayer Window will show when you run this program
            /*
            h.ClassP = createPlayer.classP;
            h.Health = createPlayer.health;
            h.Name = createPlayer.name;
            h.Defense = createPlayer.defense;
            */
            name = createPlayer.he.Name;
            classP = createPlayer.he.ClassP;
            h = createPlayer.he.Health;
            dam = createPlayer.he.Damage;
            def = createPlayer.he.Defense;
        }

        private void B_settings_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void B_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void B_load_Click(object sender, RoutedEventArgs e)
        {

        }

        private void B_author_Click(object sender, RoutedEventArgs e)
        {

        }

        private void B_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}

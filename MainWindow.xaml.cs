using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace MatchingGame_LoukaConstant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        int tempsEcoule = 0;
        int nbPairesTrouvees = 0;
        

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        private void SetUPGame()
        {
            int index;
            string nextEmoji;
            Random nbAlea = new Random();
            tempsEcoule = 0;
            nbPairesTrouvees = 0;
            timer.Start();
            
            List<string> animaleEmoji = new List<string>()
            {
                "🐈","🐈",
                "🐷","🐷",
                "🐐","🐐",
                "🦊","🦊",
                "🐴","🐴",
                "🦨","🦨",
                "🦉","🦉",
                "🐀","🐀",
            }; 
            
            foreach (TextBlock textBlock in grdMain.Children.OfType<TextBlock>())
            {
                textBlock.Visibility = Visibility.Visible;
                index = nbAlea.Next(animaleEmoji.Count);
                //index est de type int
                //nbAlea est un objet de type random()
                nextEmoji = animaleEmoji[index];  //nextEmoji est de type string 
                textBlock.Text = nextEmoji;
                animaleEmoji.RemoveAt(index);  // on retire un animal de la liste pour ne pas l'attribuer à nouveau. 
            }
        }


        TextBlock derniereTBClique;
        bool paires = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlockActif = sender as TextBlock;
            bool trouvePaire = false;

            if (!trouvePaire)
            {
                textBlockActif.Visibility = Visibility.Hidden;
                derniereTBClique = textBlockActif;
                trouvePaire = true;
            }

            else if (textBlockActif.Text == derniereTBClique.Text)
            {
                nbPairesTrouvees++;
                textBlockActif.Visibility = Visibility.Hidden; //caché
                trouvePaire = false;                
            }

            else
            {
                derniereTBClique.Visibility = Visibility.Visible; //visible
                trouvePaire = false;
            }

            if (nbPairesTrouvees == 8)
            {
                SetUPGame();
            }
        }

        private void Timer_Tick(object sander, EventArgs e)
        {
            tempsEcoule++;
            txtTemps.Text = (tempsEcoule / 10F).ToString("0.0s");

            if (nbPairesTrouvees == 8)
            {
                timer.Stop();
                txtTemps.Text = txtTemps.Text + " - Rejouer ? ";
            }
        }

         
    }
}

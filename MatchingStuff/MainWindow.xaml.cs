using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace MatchingStuff
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBlock LastTextblockClicked;
        bool CheckingMatch = false;
        SoundPlayer Correctplayer = new SoundPlayer();
        SoundPlayer Wrongplayer = new SoundPlayer();
        SoundPlayer Oneplayer = new SoundPlayer();
        SoundPlayer Winningplayer = new SoundPlayer();
        int matchMade = 0;
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;


        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            StartUpGame();


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            Timer.Text = "Go!--"+(tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchMade == 8)
            {
                timer.Stop();
                Timer.Text = Timer.Text + " - Play again?";
            }
        }

        private void StartUpGame()
        {
            ReadySongs();
            List<string> icons = new List<string>()
            {
                "🍕","🍕",
                "🍔","🍔",
                "🍟","🍟",
                "🍗","🍗",
                "🥗","🥗",
                "🍤","🍤",
                "🥨","🥨",
                "🥓","🥓",


            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "Timer")
                {

                    int index = random.Next(icons.Count);
                    if (index < icons.Count)
                    {
                        string nextIcon = icons[index];
                        textBlock.Text = nextIcon;
                        icons.RemoveAt(index);
                    }

                }

            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            
        }



       
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock SenderTextBlock = sender as TextBlock;

            if (CheckingMatch == false) //første click
            {
                SenderTextBlock.Visibility = Visibility.Hidden;
                LastTextblockClicked = SenderTextBlock;
                CheckingMatch = true;


            }
            else if (LastTextblockClicked.Text == SenderTextBlock.Text) //andet click og at det er rigtigt
            {
                SenderTextBlock.Visibility = Visibility.Hidden;
                CheckingMatch = false;
                CorrectSoundPlay();
                matchMade++;
                if (matchMade == 7)
                    OneMoreTimeSoundPlay();
                if (matchMade == 8)
                    WinningSoundPlay();
            }
            else
            {
                LastTextblockClicked.Visibility = Visibility.Visible;
                CheckingMatch = false;
                WrongSongPlay();
            }


        }

        private void ReadySongs()
        {
            Correctplayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "correct-2-46134.wav";
            Wrongplayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "negative_beeps-6008.wav";
            Winningplayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Applause.wav";
            Oneplayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "onemore.wav";
        }
        private void WrongSongPlay()
        {

            Wrongplayer.Play();

        }
        private void CorrectSoundPlay()
        {

            Correctplayer.Play();

        }
        private void WinningSoundPlay()
        {

            Winningplayer.Play();

        }
        private void OneMoreTimeSoundPlay()
        {

            Oneplayer.Play();

        }

        private async void Timer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
            Timer.Text = "ready?";
            List<string> icons = new List<string>()
            {
                "🍕","🍕",
                "🍔","🍔",
                "🍟","🍟",
                "🍗","🍗",
                "🥗","🥗",
                "🍤","🍤",
                "🥨","🥨",
                "🥓","🥓",


            };
            matchMade = 0;
            Random random3 = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>().OrderBy(x => random3.Next(icons.Count - 1)))
            {
                if (textBlock.Name != "Timer")
                {
                    int index = random3.Next(icons.Count - 1);
                    if (index < icons.Count)
                    {
                        textBlock.Visibility = Visibility.Visible;
                      
                    }
                }
            }
            
            OneMoreTimeSoundPlay();

            for (int i = 0; i < 50; i++)
            {
                Random random2 = new Random();
                foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>().OrderBy(x => random2.Next(icons.Count - 1)))
                {
                    if (textBlock.Name != "Timer")
                    {
                        int index = random2.Next(icons.Count - 1);
                        if (index < icons.Count)
                        {
                            string nextIcon = icons[index];
                            textBlock.Text = nextIcon;
                            await Task.Delay(random2.Next((20 + i) * (1 / 2)));
                        }
                    }
                }
                await Task.Delay(i * 2);
            }

            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>().OrderBy(x => random.Next(icons.Count - 1)))
            {
                if (textBlock.Name != "Timer")
                {
                    int index = random.Next(icons.Count);
                    if (index < icons.Count)
                    {
                        string nextIcon = icons[index];
                        textBlock.Text = nextIcon;
                        icons.RemoveAt(index);
                        await Task.Delay(random.Next(60));
                    }
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;


        }
    }
}

    
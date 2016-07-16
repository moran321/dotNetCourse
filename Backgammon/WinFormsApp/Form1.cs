using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLib;
using System.Threading;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        Image _player1image = global::WinFormsApp.Properties.Resources.blueChecker;
        Image _player2image = global::WinFormsApp.Properties.Resources.whiteChecker2;
        Image _validMoveimage = global::WinFormsApp.Properties.Resources.whiteChecker;
        string _from, _to;

        GameManager _manager;
        private BackgroundWorker backgroundWorker1;

        public Form1()
        {
            _manager = new GameManager(GameManager.GameType.TwoPlayers, "moshe", "tzvika");
            InitializeComponent();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;

            //start game 
            Start();


        }
        /****************************************/

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        /****************************************/


        public void Start()
        {
            //subscribe to events of the game manager        
            _manager.DisplayEvent += DisplayBoardHandler;
            //_manager.DisplayEvent += InstructionsDisplayHandler;
            _manager.ChangedMovesEvent += DisplayMovesOptionsHandler;
            _manager.MessageEvent += PrintMessageHandler;
            _manager.RollDiceEvent += RollDiceHandler;
            _manager.GameOverEvent += GameOverHandler;


            backgroundWorker1.RunWorkerAsync();
        }
        /****************************************/

        private void EnableDragnDrop()
        {
            //  AddEventsToCheckers();
            foreach (Panel panel in mainpanel.Controls.OfType<Panel>())
            {
                foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
                {
                    pictureBox.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                    pictureBox.DragDrop += new DragEventHandler(pictureBox_DragDrop);
                    pictureBox.MouseDown += new MouseEventHandler(pictureBox_MouseDown);
                    pictureBox.AllowDrop = false;
                }

            }
            foreach (PictureBox pictureBox in outZone.Controls.OfType<PictureBox>())
            {
                pictureBox.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                pictureBox.DragDrop += new DragEventHandler(pictureBox_DragDrop);
                pictureBox.AllowDrop = true;
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                _manager.StartPlaying(Get2Inputs);
                backgroundWorker1.ReportProgress(i);

            }
        }
        /****************************************/


        private void GameOverHandler(Object sender, GameOverEventArgs e)
        {
            if (e.VictoryType == GameManager.Victory.Regular)
            {
                message.Text = string.Format("{0} is the winner!", e.Winner.Name);
            }
            else if (e.VictoryType == GameManager.Victory.Mars)
            {
                message.Text = string.Format("{0} did a Mars!", e.Winner.Name);
            }
            else if (e.VictoryType == GameManager.Victory.TurkishMars)
            {
                message.Text = string.Format("{0} did a Turkish Mars!", e.Winner.Name);
            }
        }
        /****************************************/





        private void InstructionsDisplayHandler(Object sender, DisplayInstuctionsEventArgs e)
        {

            this.Invoke((MethodInvoker)delegate
            {
                message.Text = string.Format("player {0}: {1} is your turn!", _manager._currentPlayer.PlayerNumber, _manager._currentPlayer.Name);
            });

        }
        /****************************************/


        private void PrintMessageHandler(Object sender, DisplayMessageEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                message.Text = e.Message;
            });
        }
        /****************************************/
        private void DisplayMovesOptionsHandler(Object sender, TurnEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                int[] dice = _manager._game.GetDiceResults();
                dice_numbers.Text = string.Format(dice[0].ToString() + "," + dice[1].ToString());
            });
            //mark the valid moves
            foreach (Panel panel in mainpanel.Controls.OfType<Panel>())
            {
                foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        pictureBox.AllowDrop = false;
                        pictureBox.BackColor = Color.Transparent;
                    });

                    foreach (Move move in e.Moves)
                    {
                        //mark the valid checkers
                        if (Convert.ToInt32(panel.Tag) == move.From)
                        {
                            int tag = _manager._game.GetCheckersInLane(move.From);
                            if (Convert.ToInt32(pictureBox.Tag) == tag)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    pictureBox.BackColor = Color.Beige;
                                    pictureBox.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                                    pictureBox.MouseDown += new MouseEventHandler(pictureBox_MouseDown);

                                });

                            }

                        }
                        //mark the target
                        else if (Convert.ToInt32(panel.Tag) == move.To)
                        {
                            int tag = _manager._game.GetCheckersInLane(move.To);

                            if (Convert.ToInt32(pictureBox.Tag) == tag)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    pictureBox.AllowDrop = true;
                                    pictureBox.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                                    pictureBox.DragDrop += new DragEventHandler(pictureBox_DragDrop);
                                });

                            }
                            else if (tag == 0 && Convert.ToInt32(pictureBox.Tag) == 1)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    pictureBox.Image = _validMoveimage;
                                    pictureBox.AllowDrop = true;
                                    pictureBox.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                                    pictureBox.DragDrop += new DragEventHandler(pictureBox_DragDrop);
                                });
                            }


                        }

                    }
                }
            }
        }
        /****************************************/


        private void roll_Click(object sender, EventArgs e)
        {
            int[] dice = _manager.RollDice();
            dice_numbers.Text = string.Format(dice[0].ToString() + "," + dice[1].ToString());
            _manager.isRolled = true;
            rollButton.Enabled = false;
            rollButton.Text = "Play";
        }
        /****************************************/

        public string[] Get2Inputs()
        {
            return (new string[] { _from, _to });
        }
        /****************************************/


        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {

            var parent = ((Panel)((PictureBox)sender).Parent);

            ((PictureBox)sender)?.DoDragDrop(((PictureBox)sender)?.Image, DragDropEffects.Move);
            _from = (string)parent.Tag;

        }
        /****************************************/

        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /****************************************/
        private void pictureBox_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;

            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /****************************************/
        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            var sender_panel = sender as Panel;

            if (sender_panel != null)
            {
                _to = "0";
            }
            else
            {
                var parent = ((Panel)((PictureBox)sender).Parent);
                _to = (string)parent.Tag;
            }

            e.Effect = DragDropEffects.Move;
            ((PictureBox)sender).Image = (Image)e.Data.GetData(DataFormats.Bitmap);

        }
        /****************************************/

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /****************************************/

        private void DisplayBoardHandler(Object sender, DisplayInstuctionsEventArgs e)
        {

            List<int> lanes = new List<int>();
            for (int i = 1; i < 25; i++)
            {
                lanes.Add(_manager._game.GetCheckersInLane(i));
            }

            int[] rows_player1 = _manager._game.GetPlayerRowOccupation(_manager._game.GetPlayerOne());
            int[] rows_player2 = _manager._game.GetPlayerRowOccupation(_manager._game.GetPlayerTwo());

            int lane_index = 0;
            // int picture_index = 1;
            foreach (Panel panel in mainpanel.Controls.OfType<Panel>())
            {
                int tag = Convert.ToInt32(panel.Tag);
                if (rows_player1.Contains(tag))
                {
                    foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
                    {

                        if (Convert.ToInt32(pictureBox.Tag) <= lanes[tag - 1])
                        {
                            try
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    pictureBox.Image = _player1image;
                                });
                            }
                            catch (ObjectDisposedException ex)
                            {
                                Thread.Sleep(10);
                            }
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                pictureBox.AllowDrop = true;
                            });

                        }
                    }
                }
                else if (rows_player2.Contains(tag))
                {
                    foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
                    {
                        // pictureBox.Image;
                        if (Convert.ToInt32(pictureBox.Tag) <= lanes[tag - 1])
                        {
                            pictureBox.Image = _player2image;
                        }
                        else
                        {
                            // pictureBox.AllowDrop = true;
                        }
                    }
                }
                else
                {
                    foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
                    {
                        // pictureBox.AllowDrop = true;
                    }

                }
                lane_index++;
            }
        }
        /****************************************/


        private void RollDiceHandler(Object sender, RollDiceEventArgs e)
        {
            dice_numbers.Enabled = true;
        }
        /****************************************/
    }
}

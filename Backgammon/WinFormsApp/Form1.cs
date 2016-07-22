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

        private List<PictureBox> checkersPlace;
        private List<PictureBox> validPlaces;
        private Dictionary<int, Panel> lanes;
        string _from, _to;

        PictureBox _movedChecker, _newChecker;

        private readonly ManualResetEvent mre = new ManualResetEvent(false);

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

            lanes = new Dictionary<int, Panel>();
            checkersPlace = new List<PictureBox>();
            validPlaces = new List<PictureBox>();
            foreach (Panel panel in mainpanel.Controls.OfType<Panel>())
            {
                lanes.Add(Convert.ToInt32(panel.Tag), panel);
                foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
                {
                    checkersPlace.Add(pictureBox);
                }
            }


            DisplayBoard();
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
            _manager.StartTurnEvent += TurnChangedEventHandler;
            _manager.ChangedMovesEvent += DisplayMovesOptionsHandler;
            _manager.PlayerMovedEvent += PlayerMovedHandler;
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
        /****************************************/

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                _manager.StartPlaying(GetMove);
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


        private void PlayerMovedHandler(Object sender, PlayerMovedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                message.Text = string.Format("{0} moved from {1} to {2}", e.CurrentPlayer.Name, e.Moved.From, e.Moved.To);

                //update the board
                _movedChecker.Image = null;
                _movedChecker.AllowDrop = false;
                _movedChecker.BackColor = Color.Transparent;

            });
        }
        /****************************************/


        private void DisplayMovesOptionsHandler(object sender, TurnEventArgs e)
        {
            int[] dice = _manager._game.GetDiceResults();
            this.Invoke((MethodInvoker)delegate
            {
                //show the dice result
                dice_numbers.Text = string.Format(dice[0].ToString() + "," + dice[1].ToString());
            });

            foreach (Panel panel in lanes.Values)
            {
                foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
                {
                    //reset 
                    this.Invoke((MethodInvoker)delegate
                    {
                        pictureBox.AllowDrop = false;
                        pictureBox.BackColor = Color.Transparent;
                    });
                }
            }

            foreach (Move move in e.Moves)
            {
                if (_manager.GetEatenCheckers()?.Length > 0)
                {
                    foreach (CellContent cell in _manager.GetEatenCheckers())
                    {
                        if ((int)cell == 1)
                        {
                            eaten1.Image = _player1image;
                        }
                        else
                        {
                            eaten2.Image = _player2image;
                        }
                    }
                }
                else
                {
                    eaten1.Image = null;
                    eaten2.Image = null;
                }


                if (move.From == 0)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        eaten1.BackColor = Color.Beige;
                        eaten1.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                        eaten1.MouseDown += new MouseEventHandler(pictureBox_MouseDown);

                    });
                    return;
                }
                else if (move.From == 25)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        eaten2.BackColor = Color.Beige;
                        eaten2.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                        eaten2.MouseDown += new MouseEventHandler(pictureBox_MouseDown);

                    });
                    return;
                }
                foreach (PictureBox pictureBox in lanes[move.From].Controls.OfType<PictureBox>())
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
                    else if (tag == 0 && Convert.ToInt32(pictureBox.Tag) == 1)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            pictureBox.Image = _validMoveimage;
                            pictureBox.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                            pictureBox.DragDrop += new DragEventHandler(pictureBox_DragDrop);
                            pictureBox.AllowDrop = true;
                        });
                    }
                }
            }


        }
        /****************************************/


        public TryMove GetMove()
        {
            return new TryMove(_from, _to);
        }
        /****************************************/

        private void ResetValidMoves()
        {

            foreach (PictureBox box in validPlaces)
            {
                if (box.Image == _validMoveimage)
                {
                    box.Image = null;

                }
                box.AllowDrop = false;
                box.BackColor = Color.Transparent;
                box.DragEnter -= pictureBox_DragEnter;
                box.DragDrop -= pictureBox_DragDrop;
                box.MouseDown -= pictureBox_MouseDown;
            }
            validPlaces.Clear();
        }
        /****************************************/

        private void MarkTargets(Panel from_panel)
        {
            ResetValidMoves();

            Move[] moves = _manager.GetPlayerMoves();
            foreach (Move move in moves)
            {
                if (move.From == Convert.ToInt32(from_panel.Tag))
                {
                    int tag = _manager._game.GetCheckersInLane(move.To);
                    foreach (PictureBox pictureBox in lanes[move.To].Controls.OfType<PictureBox>())
                    {
                        if (!_manager._game.GetPlayerRowOccupation(_manager._currentPlayer).Contains(move.To))
                        {
                            if (Convert.ToInt32(pictureBox.Tag) == 1)
                            {
                                pictureBox.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                                pictureBox.DragDrop += new DragEventHandler(pictureBox_DragDrop);
                                // pictureBox.Image = _validMoveimage;
                                pictureBox.AllowDrop = true;
                                validPlaces.Add(pictureBox);
                            }
                        }
                        if (Convert.ToInt32(pictureBox.Tag) == tag + 1)
                        {
                            pictureBox.DragEnter += new DragEventHandler(pictureBox_DragEnter);
                            pictureBox.DragDrop += new DragEventHandler(pictureBox_DragDrop);
                            pictureBox.Image = _validMoveimage;
                            pictureBox.AllowDrop = true;
                            validPlaces.Add(pictureBox);
                        }

                    }
                }
            }
        }
        /****************************************/

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {

            var parent = ((Panel)((PictureBox)sender).Parent);
            MarkTargets(parent);
            if (((PictureBox)sender)?.Image == null)
            {
                return;
            }
           ((PictureBox)sender)?.DoDragDrop(((PictureBox)sender)?.Image, DragDropEffects.Move);

            _from = (string)parent.Tag;
            _movedChecker = (PictureBox)sender;


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

            _newChecker = (PictureBox)sender;

        }
        /****************************************/

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        /****************************************/

        private void TurnChangedEventHandler(Object sender, TurnChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                rollButton.Enabled = true;
                rollButton.Text = "Roll";
                message.Text = string.Format("player {0}: {1} is your turn!", _manager._currentPlayer.PlayerNumber, _manager._currentPlayer.Name);
            });
        }


        //initialize board
        private void DisplayBoard()
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
                            pictureBox.Image = _player1image;
                        }
                        else
                        {
                            pictureBox.AllowDrop = true;
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
            //wait until this thread get a signal
            //the logic thread waits until roll button click
            mre.WaitOne();
        }
        /****************************************/


        //invoked when the user click on "roll" button
        private void roll_Click(object sender, EventArgs e)
        {
            int[] dice = _manager.RollDice();
            dice_numbers.Text = string.Format(dice[0].ToString() + "," + dice[1].ToString());
            _manager.isRolled = true;
            rollButton.Enabled = false;
            rollButton.Text = "Play";
            mre.Set(); //release the logic thread
        }
        /****************************************/

    }
}

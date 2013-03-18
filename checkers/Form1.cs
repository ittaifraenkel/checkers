using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace checkers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CheckBut[,] bu;
        bool turn = true;
        bool firclick = true;
        AddPics pictures;
        int[,] board = new int[,]
        {
            {2,-3,2,-3,2,-3,2,-3},
            {-3,2,-3,2,-3,2,-3,2},
            {2,-3,2,-3,2,-3,2,-3},
            {-3,0,-3,0,-3,0,-3,0},
            {0,-3,0,-3,0,-3,0,-3},
            {-3,1,-3,1,-3,1,-3,1},
            {1,-3,1,-3,1,-3,1,-3},
            {-3,1,-3,1,-3,1,-3,1}
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(800, 800);
            panel1.Size = new Size(this.Width, this.Height);
            bu = new CheckBut[8, 8];
            for (int i = 0; i < bu.GetLength(0); i++)
            {
                for (int j = 0; j < bu.GetLength(1); j++)
                {
                    bu[i, j] = new CheckBut(i, j);
                    bu[i, j].Size = new Size((panel1.Width - 15) / 8, (panel1.Height - 36) / 8);
                    bu[i, j].Location = new Point(panel1.Location.X + (j * ((panel1.Width - 15) / 8)), panel1.Location.Y + (i * ((panel1.Height - 36) / 8)));
                    bu[i, j].FlatStyle = FlatStyle.Flat;
                    bu[i, j].FlatAppearance.BorderSize = 0;
                    bu[i, j].Click += new EventHandler(bu_Click);
                    bu[i, j].Tag = new int[3]{i, j, 0 };
                    if ((i + j) % 2 == 1)
                    {
                        bu[i, j].BackColor = Color.FromArgb(250, 0, 0);
                        bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 0, 0);
                    }
                    else
                    {
                        bu[i, j].BackColor = Color.FromArgb(250, 250, 250);
                        bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
                        if (i > 4)
                        {
                            pictures = new AddPics(bu[i, j].Size);
                            bu[i, j].Image = pictures.GetX();
                            bu[i, j].Tag = new int[3] { i, j, 1 };
                        }
                        if (i < 3)
                        {
                            pictures = new AddPics(bu[i, j].Size);
                            bu[i, j].Image = pictures.GetO();
                            bu[i, j].Tag = new int[3] { i, j, 2 };
                        }
                    }
                    panel1.Controls.Add(bu[i, j]);
                }
            }
        }
        int oldx = 0, oldy = 0, tag = 0;
        void bu_Click(object sender, EventArgs e)
        {
            int[] but = (int[])(((Button)(sender)).Tag);
            int x = but[0], y = but[1];
            if (turn)
            {
                if (firclick)
                {
                    if (board[x, y] == 1)
                    {
                        if (x > 0 && y < board.GetLength(1) - 1 && board[x - 1, y + 1] == 0)
                        {
                            bu[x - 1, y + 1].BackColor = Color.Green;
                        }
                        if (x > 0 && y > 0 && board[x - 1, y - 1] == 0)
                        {
                            bu[x - 1, y - 1].BackColor = Color.Green;
                        }
                        if (x > 1 && y > 1 && (board[x - 1, y - 1] == 2 || board[x - 1, y - 1] == 22) && board[x - 2, y - 2] == 0)
                        {
                            bu[x - 2, y - 2].BackColor = Color.Green;
                        }
                        if (x > 1 && y < board.GetLength(1) - 2 && (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 22) && board[x - 2, y + 2] == 0)
                        {
                            bu[x - 2, y + 2].BackColor = Color.Green;
                        }
                        oldx = x;
                        oldy = y;
                        firclick = !firclick;
                    }
                }
                else
                {
                    if (board[x, y] == 1)
                    {
                        if (x > 0 && y < board.GetLength(1) - 1 && board[x - 1, y + 1] == 0)
                        {
                            bu[x - 1, y + 1].BackColor = Color.White;
                        }
                        if (x > 0 && y > 0 && board[x - 1, y - 1] == 0)
                        {
                            bu[x - 1, y - 1].BackColor = Color.White;
                        }
                        if (x > 1 && y > 1 && (board[x - 1, y - 1] == 2 || board[x - 1, y - 1] == 22) && board[x - 2, y - 2] == 0)
                        {
                            bu[x - 2, y - 2].BackColor = Color.White;
                        }
                        if (x > 1 && y < board.GetLength(1) - 2 && (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 22) && board[x - 2, y + 2] == 0)
                        {
                            bu[x - 2, y + 2].BackColor = Color.Green;
                        }
                        firclick = !firclick;
                    }
                    if (bu[x, y].BackColor == Color.Green)
                    {
                        if (oldx > 0 && oldy < board.GetLength(1) - 1 && board[oldx - 1, oldy + 1] == 0)
                        {
                            bu[oldx - 1, oldy + 1].BackColor = Color.White;
                        }
                        if (oldx > 0 && oldy > 0 && board[oldx - 1, oldy - 1] == 0)
                        {
                            bu[oldx - 1, oldy - 1].BackColor = Color.White;
                        }
                        if (oldx > 1 && oldy > 1 && (board[oldx - 1, oldy - 1] == 2 || board[oldx - 1, oldy - 1] == 22) && board[oldx - 2, oldy - 2] == 0)
                        {
                            bu[oldx - 2, oldy - 2].BackColor = Color.White;
                        }
                        if (oldx > 1 && oldy < board.GetLength(1) - 2 && (board[oldx - 1, oldy + 1] == 2 || board[oldx - 1, oldy + 1] == 22) && board[oldx - 2, oldy + 2] == 0)
                        {
                            bu[oldx - 2, oldy + 2].BackColor = Color.White;
                        }
                        tag = board[oldx, oldy];
                        if (oldx - 2 == x)
                        {
                            if (oldy - 2 == y)
                            {
                                bu[oldx - 1, oldy - 1].RemIm();
                                board[oldx - 1, oldy - 1] = 0;
                            }
                            else
                            {
                                bu[oldx - 1, oldy + 1].RemIm();
                                board[oldx - 1, oldy + 1] = 0;
                            }
                        }
                        bu[x, y].AddImX();
                        board[x, y] = tag;
                        bu[oldx, oldy].RemIm();
                        board[oldx, oldy] = 0;
                        turn = !turn;
                        firclick = !firclick;
                    }
                }
            }
            else
            {
                if (firclick)
                {
                    if (board[x, y] == 2)
                    {
                        if (x < board.GetLength(0) - 1 && y < board.GetLength(1) - 1 && board[x + 1, y + 1] == 0)
                        {
                            bu[x + 1, y + 1].BackColor = Color.Green;
                        }
                        if (x < board.GetLength(0) - 1 && y > 0 && board[x + 1, y - 1] == 0)
                        {
                            bu[x + 1, y - 1].BackColor = Color.Green;
                        }
                        if (x < board.GetLength(0) - 2 && y > 1 && (board[x + 1, y - 1] == 1 || board[x + 1, y - 1] == 11) && board[x + 2, y - 2] == 0)
                        {
                            bu[x + 2, y - 2].BackColor = Color.Green;
                        }
                        if (x < board.GetLength(0) - 2 && y < board.GetLength(1) - 2 && (board[x + 1, y + 1] == 1 || board[x + 1, y + 1] == 11) && board[x + 2, y + 2] == 0)
                        {
                            bu[x + 2, y + 2].BackColor = Color.Green;
                        }
                        oldx = x;
                        oldy = y;
                        firclick = !firclick;
                    }
                }
                else
                {
                    if (board[x, y] == 2)
                    {
                        if (x < board.GetLength(0) - 1 && y < board.GetLength(1) - 1 && board[x + 1, y + 1] == 0)
                        {
                            bu[x + 1, y + 1].BackColor = Color.White;
                        }
                        if (x < board.GetLength(0) - 1 && y > 0 && board[x + 1, y - 1] == 0)
                        {
                            bu[x + 1, y - 1].BackColor = Color.White;
                        }
                        if (x < board.GetLength(0) - 2 && y > 1 && (board[x - 1, y - 1] == 1 || board[x + 1, y - 1] == 11) && board[x + 2, y - 2] == 0)
                        {
                            bu[x + 2, y - 2].BackColor = Color.White;
                        }
                        if (x < board.GetLength(0) - 2 && y < board.GetLength(1) - 2 && (board[x + 1, y + 1] == 1 || board[x + 1, y + 1] == 11) && board[x + 2, y + 2] == 0)
                        {
                            bu[x + 2, y + 2].BackColor = Color.Green;
                        }
                        firclick = !firclick;
                    }
                    if (bu[x, y].BackColor == Color.Green)
                    {
                        if (oldx < board.GetLength(0) - 1 && oldy < board.GetLength(1) - 1 && board[oldx + 1, oldy + 1] == 0)
                        {
                            bu[oldx + 1, oldy + 1].BackColor = Color.White;
                        }
                        if (oldx < board.GetLength(0) - 1 && oldy > 0 && board[oldx + 1, oldy - 1] == 0)
                        {
                            bu[oldx + 1, oldy - 1].BackColor = Color.White;
                        }
                        if (oldx < board.GetLength(0) - 2 && oldy > 1 && (board[oldx + 1, oldy - 1] == 1 || board[oldx + 1, oldy - 1] == 11) && board[oldx + 2, oldy - 2] == 0)
                        {
                            bu[oldx + 2, oldy - 2].BackColor = Color.White;
                        }
                        if (oldx < board.GetLength(0) - 2 && oldy < board.GetLength(1) - 2 && (board[oldx + 1, oldy + 1] == 1 || board[oldx + 1, oldy + 1] == 11) && board[oldx + 2, oldy + 2] == 0)
                        {
                            bu[oldx + 2, oldy + 2].BackColor = Color.White;
                        }
                        tag = board[oldx, oldy];
                        if (oldx + 2 == x)
                        {
                            if (oldy - 2 == y)
                            {
                                bu[oldx + 1, oldy - 1].RemIm();
                                board[oldx + 1, oldy - 1] = 0;
                            }
                            else
                            {
                                bu[oldx + 1, oldy + 1].RemIm();
                                board[oldx + 1, oldy + 1] = 0;
                            }
                        }
                        bu[x, y].AddImO();
                        board[x, y] = tag;
                        bu[oldx, oldy].RemIm();
                        board[oldx, oldy] = 0;
                        turn = !turn;
                        firclick = !firclick;
                    }
                }
            }
        }
    }
}


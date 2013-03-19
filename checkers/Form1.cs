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
                        if (x > 0 && y < board.GetLength(1) - 1 && board[x - 1, y + 1] == 0) // move reg
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
                    if (board[x, y] == 11) //move queen
                    {
                        for (int i = x + 1, j = y + 1; i < board.GetLength(0) && j < board.GetLength(1); i++, j++) // down left
                        {
                            if (board[i, j] == 1 || board[i, j] == 11)
                            {
                                break;
                            }
                            if (board[i, j] == 2 || board[i, j] == 22)
                            {
                                if (j < board.GetLength(1) - 1 && i < board.GetLength(0) - 1 && board[i + 1, j + 1] == 0)
                                {
                                    bu[i + 1, j + 1].BackColor = Color.Green;
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                        }
                        for (int i = x + 1, j = y - 1; i < board.GetLength(0) && j > -1; i++, j--) // down right
                        {
                            if (board[i, j] == 1 || board[i, j] == 11)
                            {
                                break;
                            }
                            if ((board[i, j] == 2 || board[i, j] == 22))
                            {
                                if (j > 0 && i < board.GetLength(0) - 1 && board[i + 1, j - 1] == 0)
                                {
                                    bu[i + 1, j - 1].BackColor = Color.Green;
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                        }
                        for (int i = x - 1, j = y + 1; i > -1 && j < board.GetLength(1); i--, j++) // up left
                        {
                            if (board[i, j] == 1 || board[i, j] == 11)
                            {
                                break;
                            }
                            if (board[i, j] == 2 || board[i, j] == 22)
                            {
                                if (j < board.GetLength(1) - 1 && i > 0 && board[i - 1, j + 1] == 0)
                                {
                                    bu[i - 1, j + 1].BackColor = Color.Green;
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                        }
                        for (int i = x - 1, j = y - 1; i > -1 && j > 0; i++, j--) // up right
                        {
                            if (board[i, j] == 1 || board[i, j] == 11)
                            {
                                break;
                            }
                            if (board[i, j] == 2 || board[i, j] == 22)
                            {
                                if (j > 0 && i > 0 && board[i - 1, j - 1] == 0)
                                {
                                    bu[i - 1, j - 1].BackColor = Color.Green;
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                        }
                        oldx = x;
                        oldy = y;
                        firclick = !firclick;
                    }
                }
                else
                {
                    if (board[x, y] == 1 || board[x, y] == 11)
                    {
                        for (int i = 0; i < board.GetLength(0); i++)
                        {
                            for (int j = 0; j < board.GetLength(1); j++)
                            {
                                if (bu[i, j].BackColor == Color.Green)
                                {
                                    bu[i, j].BackColor = Color.White;
                                }
                            }
                        }
                        firclick = !firclick;
                    }
                    if (bu[x, y].BackColor == Color.Green)
                    {
                        for (int i = 0; i < board.GetLength(0); i++)
                        {
                            for (int j = 0; j < board.GetLength(1); j++)
                            {
                                if (bu[i, j].BackColor == Color.Green)
                                {
                                    bu[i, j].BackColor = Color.White;
                                }
                            }
                        }
                        tag = board[oldx, oldy];
                        if (tag == 1) //eating reg
                        {
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
                        }
                        if (tag == 11) //eating queen
                        {
                            int k;
                            int i = oldx, j = oldy;
                            if (x < i && y < j)
                                for (k = 1; x + k < i && y + k < j; k++)
                                    if (board[x + k, y + k] == 2 || board[x + k, y + k] == 22)
                                    {
                                        board[x + k, y + k] = 0;
                                        bu[x + k, y + k].BackColor = Color.White;
                                        bu[x + k, y + k].RemIm();
                                        break;
                                    }
                            if (x < i && y > j)
                                for (k = 1; x + k < i && y - k > j; k++)
                                    if (board[x + k, y - k] == 2 || board[x + k, y - k] == 22)
                                    {
                                        board[x + k, y - k] = 0;
                                        bu[x + k, y - k].BackColor = Color.White;
                                        bu[x + k, y - k].RemIm();
                                        break;
                                    }
                            if (x > i && y < j)
                                for (k = 1; x - k > i && y + k < j; k++)
                                    if (board[x - k, y + k] == 2 || board[x - k, y + k] == 22)
                                    {
                                        board[x - k, y + k] = 0;
                                        bu[x - k, y + k].BackColor = Color.White;
                                        bu[x - k, y + k].RemIm();
                                        break;
                                    }
                            if (x > i && y > j)
                                for (k = 1; x - k > i && y - k > j; k++)
                                    if (board[x - k, y - k] == 2 || board[x - k, y - k] == 22)
                                    {
                                        board[x - k, y - k] = 0;
                                        bu[x - k, y - k].BackColor = Color.White;
                                        bu[x - k, y - k].RemIm();
                                        break;
                                    }
                        }
                        if (x == 0) //moving the piece and erasing the old one
                        {
                            board[x, y] = 11;
                            bu[x, y].AddImQX();
                        }
                        else
                        {
                            if (tag == 1)
                            {
                                bu[x, y].AddImX();
                                board[x, y] = 1;
                            }
                            if (tag == 11)
                            {
                                bu[x, y].AddImQX();
                                board[x, y] = 11;
                            }
                        }
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
                    if (board[x, y] == 22) //move queen
                    {
                        for (int i = x + 1, j = y + 1; i < board.GetLength(0) && j < board.GetLength(1); i++, j++) // down left
                        {
                            if (board[i, j] == 2 || board[i, j] == 22)
                            {
                                break;
                            }
                            if (board[i, j] == 1 || board[i, j] == 11)
                            {
                                if (j < board.GetLength(1) - 1 && i < board.GetLength(0) - 1 && board[i + 1, j + 1] == 0)
                                {
                                    bu[i + 1, j + 1].BackColor = Color.Green;
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                        }
                        for (int i = x + 1, j = y - 1; i < board.GetLength(0) && j > -1; i++, j--) // down right
                        {
                            if (board[i, j] == 2 || board[i, j] == 22)
                            {
                                break;
                            }
                            if ((board[i, j] == 1 || board[i, j] == 11))
                            {
                                if (j > 0 && i < board.GetLength(0) - 1 && board[i + 1, j - 1] == 0)
                                {
                                    bu[i + 1, j - 1].BackColor = Color.Green;
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                        }
                        for (int i = x - 1, j = y + 1; i > -1 && j < board.GetLength(1); i--, j++) // up left
                        {
                            if (board[i, j] == 2 || board[i, j] == 22)
                            {
                                break;
                            }
                            if (board[i, j] == 1 || board[i, j] == 11)
                            {
                                if (j < board.GetLength(1) - 1 && i > 0 && board[i - 1, j + 1] == 0)
                                {
                                    bu[i - 1, j + 1].BackColor = Color.Green;
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                        }
                        for (int i = x - 1, j = y - 1; i > -1 && j > 0; i++, j--) // up right
                        {
                            if (board[i, j] == 2 || board[i, j] == 22)
                            {
                                break;
                            }
                            if (board[i, j] == 1 || board[i, j] == 11)
                            {
                                if (j > 0 && i > 0 && board[i - 1, j - 1] == 0)
                                {
                                    bu[i - 1, j - 1].BackColor = Color.Green;
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                        }
                        oldx = x;
                        oldy = y;
                        firclick = !firclick;
                    }
                }
                else
                {
                    if (board[x, y] == 2 || board[x, y] == 22)
                    {
                        for (int i = 0; i < board.GetLength(0); i++)
                        {
                            for (int j = 0; j < board.GetLength(1); j++)
                            {
                                if (bu[i, j].BackColor == Color.Green)
                                {
                                    bu[i, j].BackColor = Color.White;
                                }
                            }
                        }
                        firclick = !firclick;
                    }
                    if (bu[x, y].BackColor == Color.Green)
                    {
                        for (int i = 0; i < board.GetLength(0); i++)
                        {
                            for (int j = 0; j < board.GetLength(1); j++)
                            {
                                if (bu[i, j].BackColor == Color.Green)
                                {
                                    bu[i, j].BackColor = Color.White;
                                }
                            }
                        }
                        tag = board[oldx, oldy];
                        if (tag == 2) //eating reg
                        {
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
                        }
                        if (tag == 22) //eating queen
                        {
                            int k;
                            int i = oldx, j = oldy;
                            if (x < i && y < j)
                                for (k = 1; x + k < i && y + k < j; k++)
                                    if (board[x + k, y + k] == 1 || board[x + k, y + k] == 11)
                                    {
                                        board[x + k, y + k] = 0;
                                        bu[x + k, y + k].BackColor = Color.White;
                                        bu[x + k, y + k].RemIm();
                                        break;
                                    }
                            if (x < i && y > j)
                                for (k = 1; x + k < i && y - k > j; k++)
                                    if (board[x + k, y - k] == 1 || board[x + k, y - k] == 11)
                                    {
                                        board[x + k, y - k] = 0;
                                        bu[x + k, y - k].BackColor = Color.White;
                                        bu[x + k, y - k].RemIm();
                                        break;
                                    }
                            if (x > i && y < j)
                                for (k = 1; x - k > i && y + k < j; k++)
                                    if (board[x - k, y + k] == 1 || board[x - k, y + k] == 11)
                                    {
                                        board[x - k, y + k] = 0;
                                        bu[x - k, y + k].BackColor = Color.White;
                                        bu[x - k, y + k].RemIm();
                                        break;
                                    }
                            if (x > i && y > j)
                                for (k = 1; x - k > i && y - k > j; k++)
                                    if (board[x - k, y - k] == 1 || board[x - k, y - k] == 11)
                                    {
                                        board[x - k, y - k] = 0;
                                        bu[x - k, y - k].BackColor = Color.White;
                                        bu[x - k, y - k].RemIm();
                                        break;
                                    }
                        }
                        if (x == 7) //moving the piece and erasing the old one
                        {
                            board[x, y] = 22;
                            bu[x, y].AddImQO();
                        }
                        else
                        {
                            if (tag == 2)
                            {
                                bu[x, y].AddImO();
                                board[x, y] = 2;
                            }
                            if (tag == 22)
                            {
                                bu[x, y].AddImQO();
                                board[x, y] = 22;
                            }
                        }
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


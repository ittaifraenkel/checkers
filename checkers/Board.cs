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
    class Board : Form
    {
        Panel pa;
        CheckBut[,] bu;
        bool turn = true;
        bool firclick = true;
        AddPics pictures;
        string player1 = "red", player2 = "black";
        bool chainallow;
        public Board(string pl1, string pl2, bool chain)
        {
            chainallow = chain;
            if (pl1 != "")
            {
                player1 = pl1;
            }
            if (pl2 != "")
            {
                player2 = pl2;
            }
            this.Text = player1 + " vs. " + player2;
            this.Size = new Size(1000, 1000);
            pa = new Panel();
            pa.Size = new Size(this.Width, this.Height);
            pa.Location = new Point(0, 0);
            pa.BorderStyle = BorderStyle.Fixed3D;
            this.Size = new Size(800, 800);
            pa.Size = new Size(this.Width, this.Height);
            this.Controls.Add(pa);
            bu = new CheckBut[8, 8];
            for (int i = 0; i < bu.GetLength(0); i++)
            {
                for (int j = 0; j < bu.GetLength(1); j++)
                {
                    bu[i, j] = new CheckBut(i, j);
                    bu[i, j].Size = new Size((pa.Width - 15) / 8, (pa.Height - 36) / 8);
                    bu[i, j].Location = new Point(pa.Location.X + (j * ((pa.Width - 15) / 8)), pa.Location.Y + (i * ((pa.Height - 36) / 8)));
                    bu[i, j].FlatStyle = FlatStyle.Flat;
                    bu[i, j].FlatAppearance.BorderSize = 0;
                    bu[i, j].Click += new EventHandler(bu_Click);
                    bu[i, j].Tag = new int[3] { i, j, 0 };
                    if ((i + j) % 2 == 1)
                    {
                        bu[i, j].BackColor = Color.FromArgb(250, 0, 0);
                        bu[i, j].Enabled = false;
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
                    pa.Controls.Add(bu[i, j]);
                }
            }
        }
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

        private void Board_Load(object sender, EventArgs e)
        {
            
        }
        int oldx = 0, oldy = 0, tag = 0;
        bool chechain = false, yes = true;
        void bu_Click(object sender, EventArgs e)
        {
            int[] but = (int[])(((Button)(sender)).Tag);
            int x = but[0], y = but[1];
            if (turn)
            {
                if (firclick)
                {
                    if (board[x, y] == 1) //move reg
                    {
                        if (x > 0 && y < board.GetLength(1) - 1 && board[x - 1, y + 1] == 0) // move reg
                        {
                            bu[x - 1, y + 1].BackColor = Color.Green;
                            bu[x - 1, y + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x > 0 && y > 0 && board[x - 1, y - 1] == 0)
                        {
                            bu[x - 1, y - 1].BackColor = Color.Green;
                            bu[x - 1, y - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x > 1 && y > 1 && (board[x - 1, y - 1] == 2 || board[x - 1, y - 1] == 22) && board[x - 2, y - 2] == 0)
                        {
                            bu[x - 2, y - 2].BackColor = Color.Green;
                            bu[x - 2, y - 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x > 1 && y < board.GetLength(1) - 2 && (board[x - 1, y + 1] == 2 || board[x - 1, y + 1] == 22) && board[x - 2, y + 2] == 0)
                        {
                            bu[x - 2, y + 2].BackColor = Color.Green;
                            bu[x - 2, y + 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
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
                                    bu[i + 1, j + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
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
                                    bu[i + 1, j - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
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
                                    bu[i - 1, j + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        for (int i = x - 1, j = y - 1; i > -1 && j > -1; i--, j--) // up right
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
                                    bu[i - 1, j - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        oldx = x;
                        oldy = y;
                        firclick = !firclick;
                    }
                }
                else
                {
                    if (yes && (board[x, y] == 1 || board[x, y] == 11))
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
                                    bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
                                }
                            }
                        }
                        tag = board[oldx, oldy];
                        if (tag == 1 && Math.Abs(oldx - x) == 2) // eating reg
                        {
                            board[(oldx + x) / 2, (oldy + y) / 2] = 0;
                            bu[(oldx + x) / 2, (oldy + y) / 2].RemIm();
                            bu[(oldx + x) / 2, (oldy + y) / 2].BackColor = Color.White;
                            chechain = true;
                        }
                        if (tag == 11) //eating queen
                        {
                            int k = 1;
                            int i = oldx, j = oldy;
                            if (x < i && y < j)
                            {
                                if (board[x + k, y + k] == 2 || board[x + k, y + k] == 22)
                                {
                                    board[x + k, y + k] = 0;
                                    bu[x + k, y + k].BackColor = Color.White;
                                    bu[x + k, y + k].RemIm();
                                    chechain = true;
                                }
                            }
                            if (x < i && y > j)
                                    if (board[x + k, y - k] == 2 || board[x + k, y - k] == 22)
                                    {
                                        board[x + k, y - k] = 0;
                                        bu[x + k, y - k].BackColor = Color.White;
                                        bu[x + k, y - k].RemIm();
                                        chechain = true;
                                    }
                            if (x > i && y < j)
                                    if (board[x - k, y + k] == 2 || board[x - k, y + k] == 22)
                                    {
                                        board[x - k, y + k] = 0;
                                        bu[x - k, y + k].BackColor = Color.White;
                                        bu[x - k, y + k].RemIm();
                                        chechain = true;
                                    }
                            if (x > i && y > j)
                                    if (board[x - k, y - k] == 2 || board[x - k, y - k] == 22)
                                    {
                                        board[x - k, y - k] = 0;
                                        bu[x - k, y - k].BackColor = Color.White;
                                        bu[x - k, y - k].RemIm();
                                        chechain = true;
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
                        CheckWin(2);
                        if (chechain && chainallow) // chain eating
                        {
                            bool chain = FunChain(x, y, 1);
                            if (chain)
                            {
                                turn = !turn;
                                firclick = !firclick;
                                oldx = x;
                                oldy = y;
                                chechain = false;
                                yes = false;
                            }
                            else
                            {
                                chechain = false;
                            }
                        }
                        else
                        {
                            yes = true;
                            chechain = false;
                        }
                    }
                }
            }
            else //black turn
            {
                if (firclick)
                {
                    if (board[x, y] == 2)
                    {
                        if (x < board.GetLength(0) - 1 && y < board.GetLength(1) - 1 && board[x + 1, y + 1] == 0)
                        {
                            bu[x + 1, y + 1].BackColor = Color.Green;
                            bu[x + 1, y + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x < board.GetLength(0) - 1 && y > 0 && board[x + 1, y - 1] == 0)
                        {
                            bu[x + 1, y - 1].BackColor = Color.Green;
                            bu[x + 1, y - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x < board.GetLength(0) - 2 && y > 1 && (board[x + 1, y - 1] == 1 || board[x + 1, y - 1] == 11) && board[x + 2, y - 2] == 0)
                        {
                            bu[x + 2, y - 2].BackColor = Color.Green;
                            bu[x + 2, y - 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        if (x < board.GetLength(0) - 2 && y < board.GetLength(1) - 2 && (board[x + 1, y + 1] == 1 || board[x + 1, y + 1] == 11) && board[x + 2, y + 2] == 0)
                        {
                            bu[x + 2, y + 2].BackColor = Color.Green;
                            bu[x + 2, y + 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
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
                                    bu[i + 1, j + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
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
                                    bu[i + 1, j - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
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
                                    bu[i - 1, j + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        for (int i = x - 1, j = y - 1; i > -1 && j > -1; i--, j--) // up right
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
                                    bu[i - 1, j - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                }
                                break;
                            }
                            bu[i, j].BackColor = Color.Green;
                            bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        }
                        oldx = x;
                        oldy = y;
                        firclick = !firclick;
                    }
                }
                else
                {
                    if (yes && (board[x, y] == 2 || board[x, y] == 22))
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
                                    bu[i, j].FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
                                }
                            }
                        }
                        tag = board[oldx, oldy];
                        if (tag == 2 && Math.Abs(oldx - x) == 2) // eating reg
                        {
                            board[(oldx + x) / 2, (oldy + y) / 2] = 0;
                            bu[(oldx + x) / 2, (oldy + y) / 2].RemIm();
                            bu[(oldx + x) / 2, (oldy + y) / 2].BackColor = Color.White;
                            chechain = true;
                        }
                        if (tag == 22) //eating queen
                        {
                            int k = 1;
                            int i = oldx, j = oldy;
                            if (x < i && y < j)
                                    if (board[x + k, y + k] == 1 || board[x + k, y + k] == 11)
                                    {
                                        board[x + k, y + k] = 0;
                                        bu[x + k, y + k].BackColor = Color.White;
                                        bu[x + k, y + k].RemIm();
                                        chechain = true;
                                    }
                            if (x < i && y > j)
                                    if (board[x + k, y - k] == 1 || board[x + k, y - k] == 11)
                                    {
                                        board[x + k, y - k] = 0;
                                        bu[x + k, y - k].BackColor = Color.White;
                                        bu[x + k, y - k].RemIm();
                                        chechain = true;
                                    }
                            if (x > i && y < j)
                                    if (board[x - k, y + k] == 1 || board[x - k, y + k] == 11)
                                    {
                                        board[x - k, y + k] = 0;
                                        bu[x - k, y + k].BackColor = Color.White;
                                        bu[x - k, y + k].RemIm();
                                        chechain = true;
                                    }
                            if (x > i && y > j)
                                    if (board[x - k, y - k] == 1 || board[x - k, y - k] == 11)
                                    {
                                        board[x - k, y - k] = 0;
                                        bu[x - k, y - k].BackColor = Color.White;
                                        bu[x - k, y - k].RemIm();
                                        chechain = true;
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
                        CheckWin(1);
                        if (chechain && chainallow) //chain eating
                        {
                            bool chain = FunChain(x, y, 2);
                            if (chain)
                            {
                                turn = !turn;
                                firclick = !firclick;
                                oldx = x;
                                oldy = y;
                                chechain = false;
                                yes = false;
                            }
                            else
                            {
                                chechain = false;
                            }
                        }
                        else
                        {
                            yes = true;
                            chechain = false;
                        }
                    }
                }
            }
        }

        public void CheckWin(int other)
        {
            int i = 1, j = 1;
            bool t = false;
            for (i = 0; i < board.GetLength(0); i++)
            {
                for(j=  0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == other || board[i, j] == other * 11)
                    {
                        if (other == 1)
                        {
                            if (i > 0 && j < board.GetLength(1) - 1 && board[i - 1, j + 1] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i > 0 && j > 0 && board[i - 1, j - 1] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i > 1 && j > 1 && (board[i - 1, j - 1] == other || board[i - 1, j - 1] == other * 11) && board[i - 2, j - 2] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i > 1 && j < board.GetLength(1) - 2 && (board[i - 1, j + 1] == other || board[i - 1, j + 1] == other * 11) && board[i - 2, j + 2] == 0)
                            {
                                t = true;
                                break;
                            }
                        }
                        if (other == 2)
                        {
                            if (i < board.GetLength(0) - 1 && j < board.GetLength(1) - 1 && board[i + 1, j + 1] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i < board.GetLength(0) - 1 && j > 0 && board[i + 1, j - 1] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i < board.GetLength(0) - 2 && j > 1 && (board[i + 1, j - 1] == 1 || board[i + 1, j - 1] == 11) && board[i + 2, j - 2] == 0)
                            {
                                t = true;
                                break;
                            }
                            if (i < board.GetLength(0) - 2 && j < board.GetLength(1) - 2 && (board[i + 1, j + 1] == 1 || board[i + 1, j + 1] == 11) && board[i + 2, j + 2] == 0)
                            {
                                t = true;
                                break;
                            }
                        }
                    }
                }
                if (t)
                {
                    break;
                }
            }
            if (other == 1 && i == 8 && j == 8)
            {
                MessageBox.Show(player2 + " won the game");
                this.Close();
            }
            if (other == 2 && i == 8 && j == 8)
            {
                MessageBox.Show(player1 + " won the game");
                this.Close();
            }
        }

        public bool FunChain(int row, int col, int turn)
        {
            bool devour = false;
            int other;
            if (turn == 1)
            {
                other = 2;
            }
            else
            {
                other = 1;
            }
            if (board[row, col] == turn)
            {
                if (row < board.GetLength(0) - 2 && col < board.GetLength(1) - 2)
                {
                    if (board[row + 2, col + 2] == 0 && (board[row + 1, col + 1] == other || board[row + 1, col + 1] == other * 11))
                    {
                        bu[row + 2, col + 2].BackColor = Color.Green;
                        bu[row + 2, col + 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        devour = true;
                    }
                }
                if (row < board.GetLength(0) - 2 && col > 1)
                {
                    if (board[row + 2, col - 2] == 0 && (board[row + 1, col - 1] == other || board[row + 1, col - 1] == other * 11))
                    {
                        bu[row + 2, col - 2].BackColor = Color.Green;
                        bu[row + 2, col - 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        devour = true;
                    }
                }
                if (row > 1 && col < board.GetLength(1) - 2)
                {
                    if (board[row - 2, col + 2] == 0 && (board[row - 1, col + 1] == other || board[row - 1, col + 1] == other * 11))
                    {
                        bu[row - 2, col + 2].BackColor = Color.Green;
                        bu[row - 2, col + 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        devour = true;
                    }
                }
                if (row > 1 && col > 1)
                {
                    if (board[row - 2, col - 2] == 0 && (board[row - 1, col - 1] == other || board[row - 1, col - 1] == other * 11))
                    {
                        bu[row - 2, col - 2].BackColor = Color.Green;
                        bu[row - 2, col - 2].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                        devour = true;
                    }
                }
            }
            if (board[row, col] == turn * 11)
            {
                int k;
                for (k = 1; row + k < board.GetLength(0) - 1 && col + k < board.GetLength(1) - 1; k++)
                {
                    if (board[row + k, col + k] == other || board[row + k, col + k] == other * 11)
                    {
                        if (board[row + k + 1, col + k + 1] == 0)
                        {
                            bu[row + k + 1, col + k + 1].BackColor = Color.Green;
                            bu[row + k + 1, col + k + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                            devour = true;
                            break;
                        }
                    }
                    if (board[row + k, col + k] == turn || board[row + k, col + k] == turn * 11)
                        break;
                }
                for (k = 1; row + k < board.GetLength(0) - 1 && col - k > 0; k++)
                {
                    if (board[row + k, col - k] == other || board[row + k, col - k] == other * 11)
                    {
                            if (board[row + k + 1, col - k - 1] == 0)
                            {
                                bu[row + k + 1, col - k - 1].BackColor = Color.Green;
                                bu[row + k + 1, col - k - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                devour = true;
                                break;
                            }
                    }
                    if (board[row + k, col - k] == turn || board[row + k, col - k] == turn * 11)
                        break;
                }
                for (k = 1; row - k > 0 && col + k < board.GetLength(1) - 1; k++)
                {
                    if (board[row - k, col + k] == other || board[row - k, col + k] == other * 11)
                    {
                            if (board[row - k - 1, col + k + 1] == 0)
                            {
                                bu[row - k - 1, col + k + 1].BackColor = Color.Green;
                                bu[row - k - 1, col + k + 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                devour = true;
                                break;
                            }
                    }
                    if (board[row - k, col + k] == turn || board[row - k, col + k] == turn * 11)
                        break;
                }
                for (k = 1; row - k > 0 && col - k > 0; k++)
                {
                    if (board[row - k, col - k] == other || board[row - k, col - k] == other * 11)
                    {
                            if (board[row - k - 1, col - k - 1] == 3)
                            {
                                bu[row - k - 1, col - k - 1].BackColor = Color.Green;
                                bu[row - k - 1, col - k - 1].FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 250, 0);
                                devour = true;
                                break;
                            }
                    }
                    if (board[row - k, col - k] == turn || board[row - k, col - k] == turn * 11)
                        break;
                }
            }
            return devour;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Board
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Board";
            this.Load += new System.EventHandler(this.Board_Load_1);
            this.ResumeLayout(false);

        }

        private void Board_Load_1(object sender, EventArgs e)
        {

        }
    }
}

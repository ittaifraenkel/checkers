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
    class CheckBut : Button
    {
        private int x, y;
        AddPics pictures;
        public CheckBut(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int GetX() 
        {
            return x;
        }

        public int GetY() 
        {
            return y;
        }

        public void AddImO()
        {
            pictures = new AddPics(this.Size);
            this.Image = pictures.GetO();
        }
        
        public void AddImQO()
        {
            pictures = new AddPics(this.Size);
            this.Image = pictures.GetQO();
        }
        
        public void AddImQX()
        {
            pictures = new AddPics(this.Size);
            this.Image = pictures.GetQX();
        }

        public void AddImX()
        {
            pictures = new AddPics(this.Size);
            this.Image = pictures.GetX();
        }

        public void RemIm()
        {
            this.Image = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class SketchBook : Form
    {
        Graphics graphics;
        Boolean cursorMoving = false;
        public Pen cursorPen;
        int cursorX = -1;
        int cursorY = -1;
        Bitmap surface;

        Class1 obj = new Class1();



        public SketchBook()
        {
            InitializeComponent();
            GenerateVaribles();

        }

        void GenerateVaribles()
        {
            graphics = canvas.CreateGraphics();
            CreateCanvas();
        }

        void CreateCanvas()
        {
            surface = new Bitmap(canvas.Width, canvas.Height);
            graphics = Graphics.FromImage(surface);
            canvas.BackgroundImage = surface;
            canvas.BackgroundImageLayout = ImageLayout.None;
            cursorPen = new Pen(Color.Black, 7);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            cursorPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            cursorPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        
        private void blackBox_Click(object sender, EventArgs e)
        {
            PictureBox color = (PictureBox)sender;
            cursorPen.Color = color.BackColor;
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            cursorMoving = true;
            cursorX = e.X;
            cursorY = e.Y;
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            cursorMoving = false;
            cursorX = -1;
            cursorY = -1;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (cursorX != -1 && cursorY != -1 & cursorMoving == true)
            {
                graphics.DrawLine(cursorPen, new Point(cursorX, cursorY), e.Location);
                cursorX = e.X;
                cursorY = e.Y;
                canvas.Invalidate();
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *jpeg; *gif; *bmp; *png)|*.jpg; *jpeg; *.gif; *.bmp; *.png";

                if (open.ShowDialog() == DialogResult.OK)
                {

                    canvas.BackgroundImage = new Bitmap(open.FileName);

                    //cannot edit image after loading - convert back into image map 

                }

            }

            catch (Exception)
            {
                obj.LogWrite("Failed to Load");
                throw new ApplicationException("Failed loading image");
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Png Files (*.png)|*.png";
                sfd.DefaultExt = "png";
                sfd.AddExtension = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    surface.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("Saved File");
                }


            }
            
             catch (Exception)
            {
                obj.LogWrite("Failed to save");
                MessageBox.Show("There is a problem saving the file." +
                    "Check the file permissions.");
            }


        }


        private void newButton_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to clear your canvas?", "New Canvas?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                CreateCanvas();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("You can continue drawing.", "New Canvas?");

            }

        }




        private void errorButton_Click(object sender, EventArgs e)
        {
            // button populates txt file that shows  possible errors

            Class1 obj = new Class1();

            try
            {
                string path = "D://ErrorLog.txt";
                if (System.IO.File.Exists(path))
                {
                    Process.Start("notepad.exe", "D://ErrorLog.txt");
                }
             
                obj.LogWrite("errorButton Clicked");

            }

            catch (Exception ex)
            {
                MessageBox.Show("failed to write text file");
                obj.LogWrite("Exception " + ex.ToString());
            }

        }
    }
}
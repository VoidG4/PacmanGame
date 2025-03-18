using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Windows.Forms;

namespace pacman_1st_attempt
{
    public partial class Form1 : Form
    {
        int count, randomChoice, randomChoice2, randomChoice3, second, minute;
        Point startPoint, endPoint;
        List<Panel> panels = new List<Panel>();
        List<PictureBox> dots = new List<PictureBox>();
        List<PictureBox> monsters = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainGameTimer.Enabled = true;
            monster_timer.Enabled = true;
            startPoint = pictureBox1.Location;
            endPoint = pictureBox1.Location;
            endPoint.X += this.Width;

            foreach (Control x in this.Controls)
            {
                if (x is Panel)
                {
                    panels.Add((Panel)x);
                }
                else if (x is PictureBox && x != pictureBox1 && x!= monster && x != monster2 && x!= monster3)
                {
                    dots.Add((PictureBox)x);
                }
            }

            monsters.AddRange(new PictureBox[]
            {
                monster, monster2, monster3
            });

            foreach(PictureBox coin in dots)
            {
                coin.Image = pictureBox2.Image;
            }
        }
        private void MainGameTimer_Tick(object sender, EventArgs e)
        {
            second++;
            if (second == 60)
            {
                second = 0;
                minute++;
            }
            if (second > 9 && minute > 9)
            {
                label4.Text = minute + ":" + second;
            }
            else if (second > 9)
            {
                label4.Text = "0" + minute + ":"  + second;
            }
            else if (minute > 9)
            {
                label4.Text =  minute + ":" + "0" + second;
            }
            else
            {
                label4.Text = "0" + minute + ":" + "0" + second;
            }
        }

        private void eat_dot(Rectangle pictureBoxBounds)
        {
            foreach(PictureBox pic in dots)
            {
                Rectangle picBounds = pic.Bounds;
                if (pictureBoxBounds.IntersectsWith(picBounds))
                {
                    if (pic.Visible)
                    {
                        count++;
                        label2.Text = count.ToString();
                    }
                    pic.Visible = false;
                }
            }
        }

        private void check_win(Timer timer)
        {
            if (count == dots.Count)
            {
                MainGameTimer.Enabled = false;
                monster_timer.Enabled = monster_Up.Enabled = monster_Down.Enabled = monster_Left.Enabled = monster_Right.Enabled = false;
                timer_Up.Enabled = timer_Down.Enabled = timer_Left.Enabled = timer_Right.Enabled = false;
                monster.Visible = monster2.Visible = monster3.Visible = false;
                Form newForm = new Form();
                newForm.FormBorderStyle = FormBorderStyle.None;
                newForm.BackColor = Color.Black;
                newForm.StartPosition = FormStartPosition.CenterScreen;
                newForm.Size = new Size(700, 250);
                newForm.Show();

                PictureBox GameOver = new PictureBox();
                GameOver.Size = newForm.Size;
                GameOver.SizeMode = PictureBoxSizeMode.StretchImage;
                GameOver.Image = Image.FromFile(@"C:\Users\GAT\MyProjects\repos\pacman_1st_attempt\pacman_1st_attempt\Resources\game_over.jpg");
                newForm.Controls.Add(GameOver);
            }
        }

        private void check_lost(Timer timer)
        { 
            if (pictureBox1.Bounds.IntersectsWith(monster.Bounds) || pictureBox1.Bounds.IntersectsWith(monster2.Bounds) || pictureBox1.Bounds.IntersectsWith(monster3.Bounds))
            {
                MainGameTimer.Enabled = false;
                timer.Enabled = false;
                monster_timer.Enabled = monster_Up.Enabled = monster_Down.Enabled = monster_Left.Enabled = monster_Right.Enabled = false;
                timer_Up.Enabled = timer_Down.Enabled = timer_Left.Enabled = timer_Right.Enabled = false;

                Form newForm = new Form();
                newForm.FormBorderStyle = FormBorderStyle.None;
                newForm.BackColor = Color.Black;
                newForm.StartPosition = FormStartPosition.CenterScreen;
                newForm.Size = new Size(700, 250);
                newForm.Show();

                PictureBox Lost = new PictureBox();
                Lost.Size = newForm.Size;
                Lost.SizeMode = PictureBoxSizeMode.StretchImage;
                Lost.Image = Image.FromFile(@"C:\Users\GAT\MyProjects\repos\pacman_1st_attempt\pacman_1st_attempt\Resources\lost.jpg");
                newForm.Controls.Add(Lost);
            }
        }
        private void timer_Up_Tick(object sender, EventArgs e)
        {
            if (timer_Up.Enabled)
            {
                Point location = pictureBox1.Location;
                location.Y -= 10;
                pictureBox1.Location = location;
            }

            foreach (Panel p in panels)
            {
                if (pictureBox1.Bounds.IntersectsWith(p.Bounds))
                {
                    Point location = pictureBox1.Location;
                    location.Y += 10;
                    pictureBox1.Location = location; 
                    timer_Up.Enabled = false;
                    break;
                }
            }
            eat_dot(pictureBox1.Bounds);
            check_lost(timer_Up);
            check_win(timer_Up);
        }

        private void timer_Down_Tick(object sender, EventArgs e)
        {
            if (timer_Down.Enabled)
            {
                Point location = pictureBox1.Location;
                location.Y += 10;
                pictureBox1.Location = location;
            }

            foreach (Panel p in panels)
            {
                if (pictureBox1.Bounds.IntersectsWith(p.Bounds))
                {
                    Point location = pictureBox1.Location;
                    location.Y -= 10;
                    pictureBox1.Location = location;
                    timer_Down.Enabled = false;
                    break;
                }
            }
            eat_dot(pictureBox1.Bounds);
            check_lost(timer_Down);
            check_win(timer_Down);
        }

        private void timer_Left_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Location.X < 0)
            {
                pictureBox1.Location = endPoint;
            }
              
            if (timer_Left.Enabled)
            {
                Point location = pictureBox1.Location;
                location.X -= 10;
                pictureBox1.Location = location;
            } 
      
            foreach (Panel p in panels)
            {
                if (pictureBox1.Bounds.IntersectsWith(p.Bounds))
                {
                    Point location = pictureBox1.Location;
                    location.X += 10;
                    pictureBox1.Location = location;
                    timer_Left.Enabled = false;
                    break;
                }
            }
            eat_dot(pictureBox1.Bounds);
            check_lost(timer_Left);
            check_win(timer_Left);
        }

        private void timer_Right_Tick(object sender, EventArgs e)
        {
            if (this.Width - pictureBox1.Location.X - pictureBox1.Width <= 0)
            {
                pictureBox1.Location = startPoint;
            }

            if (timer_Right.Enabled)
            {
                Point location = pictureBox1.Location;
                location.X += 10;
                pictureBox1.Location = location;
            }

            foreach (Panel p in panels)
            {
                if (pictureBox1.Bounds.IntersectsWith(p.Bounds))
                {
                    Point location = pictureBox1.Location;
                    location.X -= 10;
                    pictureBox1.Location = location;
                    timer_Right.Enabled = false;
                    break;
                }
            }
            eat_dot(pictureBox1.Bounds);
            check_lost(timer_Right);
            check_win(timer_Right);
        }

        private void monster_timer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            randomChoice = random.Next(1, 5);
            randomChoice2 = random.Next(1, 5);
            randomChoice3 = random.Next(1, 5);
            
            if (randomChoice == 1 || randomChoice2 == 1 || randomChoice3 == 1)
            {
                monster_Down.Enabled = monster_Left.Enabled = monster_Right.Enabled = false;
                monster_Up.Enabled = true;
            }
            else if (randomChoice == 2 || randomChoice2 == 2 || randomChoice3 == 2)
            {
                monster_Up.Enabled = monster_Left.Enabled = monster_Right.Enabled = false;
                monster_Down.Enabled = true;
            }
            else if (randomChoice == 3 || randomChoice2 == 3 || randomChoice3 == 3)
            {
                monster_Up.Enabled = monster_Down.Enabled = monster_Right.Enabled = false;
                monster_Left.Enabled = true;
            }
            else if (randomChoice == 4 || randomChoice2 == 4 || randomChoice3 == 4)
            {
                monster_Up.Enabled = monster_Down.Enabled = monster_Left.Enabled = false;
                monster_Right.Enabled = true;
            }
        }

        private void monster_Up_Tick(object sender, EventArgs e)
        {
            if (randomChoice == randomChoice2 && randomChoice == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (monster_Up.Enabled)
                    {
                        Point location = pic.Location;
                        location.Y -= 10;
                        pic.Location = location;
                    }

                    foreach (Panel p in panels)
                    {
                        if (pic.Bounds.IntersectsWith(p.Bounds))
                        {
                            Point location = pic.Location;
                            location.Y += 10;
                            pic.Location = location;
                            monster_Up.Enabled = false;
                            break;
                        }
                    }
                }
            }
            else if (randomChoice == randomChoice2)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster3)
                    {
                        if (monster_Up.Enabled)
                        {
                            Point location = pic.Location;
                            location.Y -= 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.Y += 10;
                                pic.Location = location;
                                monster_Up.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster2)
                    {
                        if (monster_Up.Enabled)
                        {
                            Point location = pic.Location;
                            location.Y -= 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.Y += 10;
                                pic.Location = location;
                                monster_Up.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice2 == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster)
                    {
                        if (monster_Up.Enabled)
                        {
                            Point location = pic.Location;
                            location.Y -= 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.Y += 10;
                                pic.Location = location;
                                monster_Up.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice == 1)
            {
                if (monster_Up.Enabled)
                {
                    Point location = monster.Location;
                    location.Y -= 10;
                    monster.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster.Location;
                        location.Y += 10;
                        monster.Location = location;
                        monster_Up.Enabled = false;
                        break;
                    }
                }
            }
            else if (randomChoice2 == 1)
            {
                if (monster_Up.Enabled)
                {
                    Point location = monster2.Location;
                    location.Y -= 10;
                    monster2.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster2.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster2.Location;
                        location.Y += 10;
                        monster2.Location = location;
                        monster_Up.Enabled = false;
                        break;
                    }
                }
            }
            else if (randomChoice3 == 1)
            {
                if (monster_Up.Enabled)
                {
                    Point location = monster3.Location;
                    location.Y -= 10;
                    monster3.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster3.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster3.Location;
                        location.Y += 10;
                        monster3.Location = location;
                        monster_Up.Enabled = false;
                        break;
                    }
                }
            }
            check_lost(monster_Up);
        }

        private void monster_Down_Tick(object sender, EventArgs e)
        {
            if (randomChoice == randomChoice2 && randomChoice == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (monster_Down.Enabled)
                    {
                        Point location = pic.Location;
                        location.Y += 10;
                        pic.Location = location;
                    }

                    foreach (Panel p in panels)
                    {
                        if (pic.Bounds.IntersectsWith(p.Bounds))
                        {
                            Point location = pic.Location;
                            location.Y -= 10;
                            pic.Location = location;
                            monster_Down.Enabled = false;
                            break;
                        }
                    }
                }
            }
            else if (randomChoice == randomChoice2)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster3)
                    {
                        if (monster_Down.Enabled)
                        {
                            Point location = pic.Location;
                            location.Y += 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.Y -= 10;
                                pic.Location = location;
                                monster_Down.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster2)
                    {
                        if (monster_Down.Enabled)
                        {
                            Point location = pic.Location;
                            location.Y += 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.Y -= 10;
                                pic.Location = location;
                                monster_Down.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice2 == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster)
                    {
                        if (monster_Down.Enabled)
                        {
                            Point location = pic.Location;
                            location.Y += 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.Y -= 10;
                                pic.Location = location;
                                monster_Down.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice == 2)
            {
                if (monster_Down.Enabled)
                {
                    Point location = monster.Location;
                    location.Y += 10;
                    monster.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster.Location;
                        location.Y -= 10;
                        monster.Location = location;
                        monster_Down.Enabled = false;
                        break;
                    }
                }
            }
            else if (randomChoice2 == 2)
            {
                if (monster_Down.Enabled)
                {
                    Point location = monster2.Location;
                    location.Y += 10;
                    monster2.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster2.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster2.Location;
                        location.Y -= 10;
                        monster2.Location = location;
                        monster_Down.Enabled = false;
                        break;
                    }
                }
            }
            else if (randomChoice3 == 2)
            {
                if (monster_Down.Enabled)
                {
                    Point location = monster3.Location;
                    location.Y += 10;
                    monster3.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster3.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster3.Location;
                        location.Y -= 10;
                        monster3.Location = location;
                        monster_Down.Enabled = false;
                        break;
                    }
                }
            }
            check_lost(monster_Down);
        }

        private void monster_Left_Tick(object sender, EventArgs e)
        {
            if (randomChoice == randomChoice2 && randomChoice == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic.Location.X < 0)
                    {
                        pic.Location = endPoint;
                    }

                    if (monster_Left.Enabled)
                    {
                        Point location = pic.Location;
                        location.X -= 10;
                        pic.Location = location;
                    }
                 
                    foreach (Panel p in panels)
                    {
                        if (pic.Bounds.IntersectsWith(p.Bounds))
                        {
                            Point location = pic.Location;
                            location.X += 10;
                            pic.Location = location;
                            monster_Left.Enabled = false;
                            break;
                        }
                    }
                }
            }
            else if (randomChoice == randomChoice2)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster3)
                    {
                        if (pic.Location.X < 0)
                        {
                            pic.Location = endPoint;
                        }

                        if (monster_Left.Enabled)
                        {
                            Point location = pic.Location;
                            location.X -= 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.X += 10;
                                pic.Location = location;
                                monster_Left.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster2)
                    {
                        if (pic.Location.X < 0)
                        {
                            pic.Location = endPoint;
                        }

                        if (monster_Left.Enabled)
                        {
                            Point location = pic.Location;
                            location.X -= 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.X += 10;
                                pic.Location = location;
                                monster_Left.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice2 == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster)
                    {
                        if (pic.Location.X < 0)
                        {
                            pic.Location = endPoint;
                        }

                        if (monster_Left.Enabled)
                        {
                            Point location = pic.Location;
                            location.X -= 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.X += 10;
                                pic.Location = location;
                                monster_Left.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice == 3)
            {
                if (monster.Location.X < 0)
                {
                    monster.Location = endPoint;
                }

                if (monster_Left.Enabled)
                {
                    Point location = monster.Location;
                    location.X -= 10;
                    monster.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster.Location;
                        location.X += 10;
                        monster.Location = location;
                        monster_Left.Enabled = false;
                        break;
                    }
                }
            }
            else if (randomChoice2 == 3)
            {
                if (monster2.Location.X < 0)
                {
                    monster2.Location = endPoint;
                }

                if (monster_Left.Enabled)
                {
                    Point location = monster2.Location;
                    location.X -= 10;
                    monster2.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster2.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster2.Location;
                        location.X += 10;
                        monster2.Location = location;
                        monster_Left.Enabled = false;
                        break;
                    }
                }
            }
            else if (randomChoice3 == 3)
            {
                if (monster3.Location.X < 0)
                {
                    monster3.Location = endPoint;
                }

                if (monster_Left.Enabled)
                {
                    Point location = monster3.Location;
                    location.X -= 10;
                    monster3.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster3.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster3.Location;
                        location.X += 10;
                        monster3.Location = location;
                        monster_Left.Enabled = false;
                        break;
                    }
                }
            }
            check_lost(monster_Left);
        }

        private void monter_Right_Tick(object sender, EventArgs e)
        {
            if (randomChoice == randomChoice2 && randomChoice == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (this.Width - pic.Location.X - pic.Width <= 0)
                    {
                        pic.Location = startPoint;
                    }

                    if (monster_Right.Enabled)
                    {
                        Point location = pic.Location;
                        location.X += 10;
                        pic.Location = location;
                    }

                    foreach (Panel p in panels)
                    {
                        if (pic.Bounds.IntersectsWith(p.Bounds))
                        {
                            Point location = pic.Location;
                            location.X -= 10;
                            pic.Location = location;
                            monster_Right.Enabled = false;
                            break;
                        }
                    }
                }
            }
            else if (randomChoice == randomChoice2)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster3)
                    {
                        if (this.Width - pic.Location.X - pic.Width <= 0)
                        {
                            pic.Location = startPoint;
                        }

                        if (monster_Right.Enabled)
                        {
                            Point location = pic.Location;
                            location.X += 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.X -= 10;
                                pic.Location = location;
                                monster_Right.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster2)
                    {
                        if (this.Width - pic.Location.X - pic.Width <= 0)
                        {
                            pic.Location = startPoint;
                        }

                        if (monster_Right.Enabled)
                        {
                            Point location = pic.Location;
                            location.X += 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.X -= 10;
                                pic.Location = location;
                                monster_Right.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice2 == randomChoice3)
            {
                foreach (PictureBox pic in monsters)
                {
                    if (pic != monster)
                    {
                        if (this.Width - pic.Location.X - pic.Width <= 0)
                        {
                            pic.Location = startPoint;
                        }

                        if (monster_Right.Enabled)
                        {
                            Point location = pic.Location;
                            location.X += 10;
                            pic.Location = location;
                        }

                        foreach (Panel p in panels)
                        {
                            if (pic.Bounds.IntersectsWith(p.Bounds))
                            {
                                Point location = pic.Location;
                                location.X -= 10;
                                pic.Location = location;
                                monster_Right.Enabled = false;
                                break;
                            }
                        }
                    }
                }
            }
            else if (randomChoice == 4)
            {
                if (this.Width - monster.Location.X - monster.Width <= 0)
                {
                    monster.Location = startPoint;
                }

                if (monster_Right.Enabled)
                {
                    Point location = monster.Location;
                    location.X += 10;
                    monster.Location = location;
                } 

                foreach (Panel p in panels)
                {
                    if (monster.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster.Location;
                        location.X -= 10;
                        monster.Location = location;
                        monster_Right.Enabled = false;
                        break;
                    }
                }
            }
            else if (randomChoice2 == 4)
            {
                if (this.Width - monster2.Location.X - monster2.Width <= 0)
                {
                    monster2.Location = startPoint;
                }

                if (monster_Right.Enabled)
                {
                    Point location = monster2.Location;
                    location.X += 10;
                    monster2.Location = location;
                }
            
                foreach (Panel p in panels)
                {
                    if (monster2.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster2.Location;
                        location.X -= 10;
                        monster2.Location = location;
                        monster_Right.Enabled = false;
                        break;
                    }
                }
            }
            else if (randomChoice3 == 4)
            {

                if (this.Width - monster3.Location.X - monster3.Width <= 0)
                {
                    monster3.Location = startPoint;
                }

                if (monster_Right.Enabled)
                {
                    Point location = monster3.Location;
                    location.X += 10;
                    monster3.Location = location;
                }

                foreach (Panel p in panels)
                {
                    if (monster3.Bounds.IntersectsWith(p.Bounds))
                    {
                        Point location = monster3.Location;
                        location.X -= 10;
                        monster3.Location = location;
                        monster_Right.Enabled = false;
                        break;
                    }
                }
            }
            check_lost(monster_Right);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\GAT\MyProjects\repos\pacman_1st_attempt\pacman_1st_attempt\Resources\pac_up.gif");
                timer_Down.Enabled = timer_Left.Enabled = timer_Right.Enabled = false;
                timer_Up.Enabled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\GAT\MyProjects\repos\pacman_1st_attempt\pacman_1st_attempt\Resources\pac_down.gif");
                timer_Up.Enabled = timer_Left.Enabled = timer_Right.Enabled = false;
                timer_Down.Enabled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\GAT\MyProjects\repos\pacman_1st_attempt\pacman_1st_attempt\Resources\pac_left.gif");
                timer_Up.Enabled = timer_Down.Enabled = timer_Right.Enabled = false;
                timer_Left.Enabled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\GAT\MyProjects\repos\pacman_1st_attempt\pacman_1st_attempt\Resources\pac_right.gif");
                timer_Up.Enabled = timer_Down.Enabled = timer_Left.Enabled = false;
                timer_Right.Enabled = true;
            }
        }
    }
}

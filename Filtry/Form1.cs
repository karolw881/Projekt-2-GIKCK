using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filtry
{
    public partial class Form1 : Form
    {
        public String path;
        public Bitmap resize_entry_bmp, entry_bmp, entry_bmp2, temp_bmp, @bmp_new, @bmp_blue, @bmp_green, bmp_new2, save_bmp;
        public bool if_save;
        public int[,] red, green, blue;
        public int[,] maska;
        public Form1()
        {
            InitializeComponent();    
        }

        //save file 
        private void button11_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                pictureBox2.Image.Save(file);
            }
        }
        
        //pierwszy search
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                entry_bmp = new Bitmap(path);
                resize_entry_bmp = new Bitmap(entry_bmp, new Size(200, 180));
                entry_bmp = resize_entry_bmp;
                bmp_new2 = new Bitmap(entry_bmp , new Size(200 , 180));
                temp_bmp = new Bitmap(entry_bmp );
                pictureBox1.Image = entry_bmp;
            }
        }

        //Drugi Search , 3rdpicturebox startowy do mieszania 
        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                entry_bmp2 = new Bitmap(path);
                resize_entry_bmp = new Bitmap(entry_bmp2, new Size(200, 180));
                entry_bmp2 = resize_entry_bmp;
                pictureBox3.Image = entry_bmp2;
            }
        }

        // Neutral Image
        private void button3_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < entry_bmp.Width; i++)
            {
                for (int j = 0; j < entry_bmp.Height; j++)
                {
                    Color p = entry_bmp.GetPixel(i, j);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;
                    a = sprawdz_rgb(a);
                    r = sprawdz_rgb(r);
                    g = sprawdz_rgb(g);
                    b = sprawdz_rgb(b);
                    bmp_new2.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                }
                pictureBox2.Image = bmp_new2;
            }
        }

        //Jasniej ciemniej
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < entry_bmp.Width; i++)
            {
                for (int j = 0; j < entry_bmp.Height; j++)
                {
                    Color p = entry_bmp.GetPixel(i, j);
                    int a = p.A + trackBar1.Value;
                    int r = p.R + trackBar1.Value;
                    int g = p.G + trackBar1.Value;
                    int b = p.B + trackBar1.Value;
                    a = sprawdz_rgb(a);
                    r = sprawdz_rgb(r);
                    g = sprawdz_rgb(g);
                    b = sprawdz_rgb(b);
                    bmp_new2.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                }
            }
            pictureBox2.Image = bmp_new2;
        }

        //Negatyw
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < entry_bmp.Width; i++)
            {
                for (int j = 0; j < entry_bmp.Height; j++)
                {
                    Color p = entry_bmp.GetPixel(i, j);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    a = sprawdz_rgb(a);
                    r = sprawdz_rgb(r);
                    a = 255 - r;
                    g = sprawdz_rgb(g);
                    a = 255 - g;
                    b = sprawdz_rgb(b);
                    b = 255 - b;
                    bmp_new2.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                }
            }
            pictureBox2.Image = bmp_new2;

        }

        //Transformacja potegowa 
        private void trackBar3_Scroll_1(object sender, EventArgs e)
        {
            for (int i = 0; i < entry_bmp.Width; i++)
            {
                for (int j = 0; j < entry_bmp.Height; j++)
                {
                    Color p = entry_bmp.GetPixel(i, j);
                    int a = p.A * (trackBar3.Value * trackBar3.Value) / 20;
                    int r = p.R * (trackBar3.Value * trackBar3.Value) / 20;
                    int g = p.G * (trackBar3.Value * trackBar3.Value) / 20;
                    int b = p.B * (trackBar3.Value * trackBar3.Value) / 20;
                    a = sprawdz_rgb(a);
                    r = sprawdz_rgb(r);
                    g = sprawdz_rgb(g);
                    b = sprawdz_rgb(b);
                    bmp_new2.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                }

            }
            pictureBox2.Image = bmp_new2;

        }


        //przezroczystosc
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);
                        int a = sprawdz_rgb(sprawdz_rgb(1 - ( trackBar5.Value /100 ) )  * p1.A +  sprawdz_rgb((trackBar5.Value ) * p.A  ) ) ;
                        int r = sprawdz_rgb(sprawdz_rgb(1 - (trackBar5.Value /100) )* p1.R + sprawdz_rgb((trackBar5.Value  ) * p.R));
                        int g = sprawdz_rgb(sprawdz_rgb(1 - ( trackBar5.Value/100 ) ) * p1.G + sprawdz_rgb((trackBar5.Value ) * p.G));
                        int b = sprawdz_rgb(sprawdz_rgb(1 - ( trackBar5.Value /100))  * p1.B + sprawdz_rgb((trackBar5.Value )* p.B));
                        a = sprawdz_rgb(a);
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);
                        bmp_new2.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                    }

                }
                pictureBox2.Image = bmp_new2;
            }
        }



        //Suma
        private void button5_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);
                        int a = sprawdz_rgb(p.A + p1.A);
                        int r = sprawdz_rgb(p.R + p1.R);
                        int g = sprawdz_rgb(p.B + p1.G); ;
                        int b = sprawdz_rgb(p.B + p1.B); ;
                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);

                    }
                }
                pictureBox2.Image = temp_bmp;
            }

        }

        //Odejmowanie
        private void button7_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                {

                    for (int i = 0; i < entry_bmp.Width; i++)
                    {
                        for (int j = 0; j < entry_bmp.Height; j++)
                        {
                            Color p = entry_bmp.GetPixel(i, j);
                            Color p1 = entry_bmp2.GetPixel(i, j);
                            int a = sprawdz_rgb(p.A + p1.A - 255);
                            int r = sprawdz_rgb(p.R + p1.R - 255);
                            int g = sprawdz_rgb(p.B + p1.G - 255); ;
                            int b = sprawdz_rgb(p.B + p1.B - 255); ;
                            temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                            r = sprawdz_rgb(r);
                            g = sprawdz_rgb(g);
                            b = sprawdz_rgb(b);
                        }
                    }
                    pictureBox2.Image = temp_bmp;

                }
            }

        }

        //Roznica
        private void button8_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);
                        int a = sprawdz_rgb(p.A);
                        int r = sprawdz_rgb(Math.Abs(p.R - p1.R));
                        int g = sprawdz_rgb(Math.Abs(p.B - p1.G)); ;
                        int b = sprawdz_rgb(Math.Abs(p.B - p1.B)); ;
                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));

                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);

                    }
                }
                pictureBox2.Image = temp_bmp;

            }
        }

        //Mnozenie

        private void button9_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);
                        int a = sprawdz_rgb(Math.Abs(p.A * p1.A));
                        int r = sprawdz_rgb(Math.Abs(p.R * p1.R));
                        int g = sprawdz_rgb(Math.Abs(p.B * p1.G)); ;
                        int b = sprawdz_rgb(Math.Abs(p.B * p1.B)); ;
                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));





                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);


                    }
                }
                pictureBox2.Image = temp_bmp;
            }
        }


        //Odwrotnosc
        private void button22_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);
                        int a = sprawdz_rgb(255 - sprawdz_rgb(255 - p.A) * sprawdz_rgb(255 - p1.A));
                        int r = sprawdz_rgb(255 - sprawdz_rgb(255 - p.R) * sprawdz_rgb(255 - p1.R));
                        int g = sprawdz_rgb(255 - sprawdz_rgb(255 - p.G) * sprawdz_rgb(255 - p1.G));
                        int b = sprawdz_rgb(255 - sprawdz_rgb(255 - p.B) * sprawdz_rgb(255 - p1.B));
                        a = sprawdz_rgb(a);
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);
                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                    }
                }
                pictureBox2.Image = temp_bmp;
            }
        }

        //Negacja
        private void button23_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);
                        int a = sprawdz_rgb(255 - Math.Abs(sprawdz_rgb((255 - p.A - p1.A))));
                        int r = sprawdz_rgb(255 - Math.Abs(sprawdz_rgb((255 - p.R - p1.R))));
                        int g = sprawdz_rgb(255 - Math.Abs(sprawdz_rgb((255 - p.G - p1.G))));
                        int b = sprawdz_rgb(255 - Math.Abs(sprawdz_rgb((255 - p.B - p1.B))));
                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));

                        //normowanie pixeli !!!!!

                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);

                    }
                }
                pictureBox2.Image = temp_bmp;

            }
        }

        //Ciemniejsze 
        private void button24_Click(object sender, EventArgs e)
        {
            

                    if (entry_bmp == null || entry_bmp2 == null) {  }
                    else
                    {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {

                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);

                        int a, r, g, b;
                        if (p.A < p1.A) a = p.A; else { a = p1.A; }
                        if (p.R < p1.R) r = p.R; else { r = p1.R; }
                        if (p.G < p1.G) g = p.G; else { g = p1.G; }
                        if (p.B < p1.B) b = p.B; else { b = p1.B; }
                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);
                    }
                }
                pictureBox2.Image = temp_bmp;
            }
          
        }

        //Jasniejszy
        private void button25_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);

                        int a, r, g, b;
                        if (p.A > p1.A) a = p.A; else { a = p1.A; }
                        if (p.R > p1.R) r = p.R; else { r = p1.R; }
                        if (p.G > p1.G) g = p.G; else { g = p1.G; }
                        if (p.B > p1.B) b = p.B; else { b = p1.B; }
                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);
                    }
                }
                pictureBox2.Image = temp_bmp;

            }
        }


        //zmniejsz kontrast ,  zmnijeszenie kontrastu drugi wzor 

        private void trackBar4_Scroll_1(object sender, EventArgs e)
        {


            Bitmap Kontrast_bitmap = new Bitmap(entry_bmp);
            int wH = entry_bmp.Height;
            int wW = entry_bmp.Width;

        

            for (int y = 0; y < wH; y++)
            {
                for (int x = 0; x < wW; x++)
                {
                    Color p = entry_bmp.GetPixel(x, y);
                    int a , r , g , b ;
                    a = p.A; 

                    //Skladowa R do zmniejsz kontrast
                    if(p.R < 127 + trackBar4.Value)
                    {
                        r = (127 / sprawdz_rgb(127 + trackBar4.Value)) * p.R; 
                    }
                    else if (p.R > 127 - trackBar4.Value) {
                        r =  ((127 * p.R) + (255 * trackBar4.Value)) / sprawdz_rgb(127 + trackBar4.Value) ;
                    }
                    else  r = 127; 
                    

                    //Skladowa G do zmniejsz kontrast
                    if (p.G < 127 + trackBar4.Value)
                    {
                        g = (127 / sprawdz_rgb(127 + trackBar4.Value)) * p.G;
                    }
                    else if (p.G > 127 - trackBar4.Value)  {
                        g = ((127 * p.G) + (255 * trackBar4.Value)) / sprawdz_rgb(127 + trackBar4.Value); 
                    }
                    else  g = 127; 

                    //Skladowa B do zmniejsz kontrast
                    if (p.B < 127 + trackBar4.Value)
                    {
                        b = (127 / sprawdz_rgb(127 + trackBar4.Value)) * p.B;
                    }
                    else if (p.B > 127 - trackBar4.Value) {
                        b = ((127 * p.B) + (255 * trackBar4.Value)) / sprawdz_rgb(127 + trackBar4.Value); 
                    }
                    else  b = 127; 

                    //ustawienie pixela
                    temp_bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            pictureBox2.Image = temp_bmp;

        }

       

        //Kontrast  Glowny , zwiekszenie kontrastu ze wzoru pierwszego 
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Bitmap Kontrast_bitmap = new Bitmap(entry_bmp);
            int wH = entry_bmp.Height;
            int wW = entry_bmp.Width;

            // a 
            for (int i = 0; i < wH; i++)
            {
                for (int j = 0; j < wW; j++)
                {
                    //Wariant number 2 

                    Color kont = entry_bmp.GetPixel(j, i);
                    int a = kont.A;
                    int r = sprawdz_rgb((sprawdz_rgb(127 / sprawdz_rgb(127 - trackBar2.Value))) * (kont.R - trackBar2.Value));
                    int g = sprawdz_rgb((sprawdz_rgb(127 / sprawdz_rgb(127 - trackBar2.Value))) * (kont.G - trackBar2.Value));
                    int b = sprawdz_rgb((sprawdz_rgb(127 / sprawdz_rgb(127 - trackBar2.Value))) * (kont.B - trackBar2.Value));
                    Kontrast_bitmap.SetPixel(j, i, Color.FromArgb(a, r, g, b));



                    /*
                  
                        Color kont = entry_bmp.GetPixel(j, i);
                        int a = kont.A;
                        int r = sprawdz_rgb((sprawdz_rgb  (127 - trackBar2.Value) / 127) * (kont.R - trackBar2.Value));
                        int g = sprawdz_rgb((sprawdz_rgb (127 - trackBar2.Value) / 127  ) * (kont.G - trackBar2.Value));
                        int b = sprawdz_rgb((sprawdz_rgb(127 - trackBar2.Value) / 127) * (kont.B - trackBar2.Value));
                        Kontrast_bitmap.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                    */





                    /*
                    Color kont = entry_bmp.GetPixel(j, i);
                    int a = kont.A;
                    int r = sprawdz_rgb((127 / sprawdz_rgb(127 - trackBar2.Value)) * (kont.R - trackBar2.Value));
                    int g = sprawdz_rgb((127 / sprawdz_rgb(127 - trackBar2.Value)) * (kont.G - trackBar2.Value));
                    int b = sprawdz_rgb((127 / sprawdz_rgb(127 - trackBar2.Value)) * (kont.B - trackBar2.Value));
                    Kontrast_bitmap.SetPixel(j, i, Color.FromArgb(a, r, g, b));


                    

                    int a = kont.A;
                    int r = sprawdz_rgb((sprawdz_rgb(127 + trackBar2.Value) / 127) * (kont.R - trackBar2.Value));
                    int g = sprawdz_rgb((sprawdz_rgb(127 + trackBar2.Value) / 127) * (kont.G - trackBar2.Value));
                    int b = sprawdz_rgb((sprawdz_rgb(127 + trackBar2.Value) / 127) * (kont.B - trackBar2.Value));
                   
                   */

                    //   int g = sprawdz_rgb((127 / sprawdz_rgb(127 - trackBar1.Value)) * kont.G - trackBar1.Value));
                    //   int b = sprawdz_rgb((127 / sprawdz_rgb(127 - trackBar1.Value)) * (kont.B - trackBar1.Value));
                    //  Kontrast_bitmap.SetPixel(j, i, Color.FromArgb(a, r, g, b));




                    //   int g = sprawdz_rgb((127 / sprawdz_rgb(127 - trackBar1.Value)) * kont.G - trackBar1.Value));
                    //   int b = sprawdz_rgb((127 / sprawdz_rgb(127 - trackBar1.Value)) * (kont.B - trackBar1.Value));

                    /*
             int a = kont.A;
             int r = sprawdz_rgb((sprawdz_rgb(127 + trackBar3.Value) / 127) * (kont.R - trackBar3.Value));
             int g = sprawdz_rgb((sprawdz_rgb(127 + trackBar3.Value) / 127) * (kont.G - trackBar3.Value));
             int b = sprawdz_rgb((sprawdz_rgb(127 + trackBar3.Value) / 127) * (kont.B - trackBar3.Value));
             Kontrast_bitmap.SetPixel(j, i, Color.FromArgb(a, r, g, b));
             */

                }
            }

            pictureBox2.Image = Kontrast_bitmap;
        }




        //Lagondiejszy do poprawy
        private void button29_Click(object sender, EventArgs e)
        {

            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {

                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);


                        int a = sprawdz_rgb(255 - (sprawdz_rgb(255 - p.A) / sprawdz_rgb(p1.A)));
                        int r = sprawdz_rgb(255 - (sprawdz_rgb(255 - p.R) / sprawdz_rgb(p1.R)));
                        int g = sprawdz_rgb(255 - (sprawdz_rgb(255 - p.G) / sprawdz_rgb(p1.G)));
                        int b = sprawdz_rgb(255 - (sprawdz_rgb(255 - p.B) / sprawdz_rgb(p1.B)));

                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);
                    }
                }
                pictureBox2.Image = temp_bmp;

            }
        }

        //Nakladka
        private void button26_Click(object sender, EventArgs e)
        {

            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);



                        int a, r, g, b;

                        a = 255;
                        if (p.R < 255 * 0.5)
                            r = sprawdz_rgb((((510 * p.R * p1.R))));
                        else
                            r = sprawdz_rgb(((255 - 510 * (sprawdz_rgb(255 - p.R) * sprawdz_rgb(255 - p1.R)))));

                        if (p.G < 255 * 0.5)
                            g = sprawdz_rgb((((510 * p.G * p1.G))));
                        else
                            g = sprawdz_rgb(((255 - 510 * (sprawdz_rgb(255 - p.G) * sprawdz_rgb(255 - p1.G)))));


                        if (p.B < 255 * 0.5)
                            b = sprawdz_rgb((((510 * p.B * p1.B))));
                        else
                            b = sprawdz_rgb(((255 - 510 * (sprawdz_rgb(255 - p.B) * sprawdz_rgb(255 - p1.B)))));


                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);

                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));

                    }

                }
                pictureBox2.Image = temp_bmp;
            }

        }

        //Wylaczenie
        private void button27_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);
                        int a, r, g, b;

                        a = 255;
                        r = sprawdz_rgb(p.R + p1.R - sprawdz_rgb(510 * p.R * p1.R));
                        g = sprawdz_rgb(p.G + p1.G - sprawdz_rgb(510 * p.G * p1.G));
                        b = sprawdz_rgb(p.B + p1.B - sprawdz_rgb(510 * p.B * p1.B));



                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);

                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));


                    }

                }
                pictureBox2.Image = temp_bmp;
            }


        }



        //Ostre swiatlo 
        private void button28_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);
                        int a, r, g, b;

                        a = 255;
                        if (p1.R < 255 * 0.5)
                            r = sprawdz_rgb((((510 * p.R * p1.R))));
                        else
                            r = sprawdz_rgb(((255 - 510 * (sprawdz_rgb(255 - p.R) * sprawdz_rgb(255 - p1.R)))));

                        if (p1.G < 255 * 0.5)
                            g = sprawdz_rgb((((510 * p.G * p1.G))));
                        else
                            g = sprawdz_rgb(((255 - 510 * (sprawdz_rgb(255 - p.G) * sprawdz_rgb(255 - p1.G)))));


                        if (p1.B < 255 * 0.5)
                            b = sprawdz_rgb((((510 * p.B * p1.B))));
                        else
                            b = sprawdz_rgb(((255 - 510 * (sprawdz_rgb(255 - p.B) * sprawdz_rgb(255 - p1.B)))));


                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);

                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                    }
                }
                pictureBox2.Image = temp_bmp;
            }
        }

        //rozcienczenie
        private void button30_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);


                        int a = sprawdz_rgb(p.A / sprawdz_rgb(255 - p1.A));
                        int r = sprawdz_rgb(p.R / sprawdz_rgb(255 - p1.R));
                        int g = sprawdz_rgb(p.G / sprawdz_rgb(255 - p1.G));
                        int b = sprawdz_rgb(p.B / sprawdz_rgb(255 - p1.B));

                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);
                    }
                }
                pictureBox2.Image = temp_bmp;


            }
        }

        //wypalenie
        private void button31_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);


                        int a = sprawdz_rgb(255 - (sprawdz_rgb(255 - p.A) / sprawdz_rgb(p1.A)));
                        int r = sprawdz_rgb(255 - (sprawdz_rgb(255 - p.R) / sprawdz_rgb(p1.R)));
                        int g = sprawdz_rgb(255 - (sprawdz_rgb(255 - p.G) / sprawdz_rgb(p1.G)));
                        int b = sprawdz_rgb(255 - (sprawdz_rgb(255 - p.B) / sprawdz_rgb(p1.B)));

                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);
                    }
                }
                pictureBox2.Image = temp_bmp;

            }
        }

        //Reflect Mode 
        private void button32_Click(object sender, EventArgs e)
        {
            if (entry_bmp == null || entry_bmp2 == null) { }
            else
            {
                for (int i = 0; i < entry_bmp.Width; i++)
                {
                    for (int j = 0; j < entry_bmp.Height; j++)
                    {
                        Color p = entry_bmp.GetPixel(i, j);
                        Color p1 = entry_bmp2.GetPixel(i, j);


                        int a = sprawdz_rgb(p.A * p.A / sprawdz_rgb((255 - p1.A)));
                        int r = sprawdz_rgb(p.R * p.R / sprawdz_rgb((255 - p1.R)));
                        int g = sprawdz_rgb(p.G * p.G / sprawdz_rgb((255 - p1.G))); ;
                        int b = sprawdz_rgb(p.B * p.B / sprawdz_rgb((255 - p1.B))); ;

                        temp_bmp.SetPixel(i, j, Color.FromArgb(a, r, g, b));
                        r = sprawdz_rgb(r);
                        g = sprawdz_rgb(g);
                        b = sprawdz_rgb(b);
                    }
                }
                pictureBox2.Image = temp_bmp;

            }
        }

        //Prewitt 1 
        private void button10_Click(object sender, EventArgs e)
        {
            maska = new int[3, 3] { { 1, 1, 1 },
                                    { 0, 0, 0 },
                                    { -1, -1, -1 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0;
                    pomoc_g = 0;
                    pomoc_b = 0;

                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];

                            a_wy = a;

                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;

                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;

                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;

                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));

                }
            }
            pictureBox2.Image = temp_bmp;

        }


        //Prewitt 2
        private void button34_Click(object sender, EventArgs e)
        {

            maska = new int[3, 3] { { 1, 0, -1 },
                                    { 1, 0, -1 },
                                    { 1, 0, -1 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0;
                    pomoc_g = 0;
                    pomoc_b = 0;

                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];

                            a_wy = a;

                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;

                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;

                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;

                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));

                }
            }
            pictureBox2.Image = temp_bmp;

        }






        //Roberts 1
        private void button12_Click(object sender, EventArgs e)
        {

            maska = new int[3, 3] { { 0, 0, 0 },
                                    { 0, 1, -1 },
                                    { 0, 0, 0 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0; pomoc_g = 0; pomoc_b = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];
                            a_wy = a;
                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;
                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;
                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;
                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));
                }
            }
            pictureBox2.Image = temp_bmp;
        }


        //roberts 2 
        private void button13_Click(object sender, EventArgs e)
        {
            maska = new int[3, 3] { { 0, 0, 0 },
                                    { 0, 1, 0 },
                                    { 0, -1, 0 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0; pomoc_g = 0; pomoc_b = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];
                            a_wy = a;
                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;
                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;
                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;
                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));
                }
            }
            pictureBox2.Image = temp_bmp;

        }

        //Sobel 1 
        private void button14_Click(object sender, EventArgs e)
        {
            maska = new int[3, 3] { { 1, 2, 1 },
                                    { 0, 0, 0 },
                                    { -1, -2 , -1 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0; pomoc_g = 0; pomoc_b = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];
                            a_wy = a;
                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;
                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;
                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;
                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));
                }
            }
            pictureBox2.Image = temp_bmp;

        }

        //Sobel 2 
        private void button15_Click(object sender, EventArgs e)
        {
            maska = new int[3, 3] { { 1, 0, -1 },
                                    { 2, 0, -2 },
                                    { 1, 0 , -1 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0; pomoc_g = 0; pomoc_b = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];
                            a_wy = a;
                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;
                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;
                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;
                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));
                }
            }
            pictureBox2.Image = temp_bmp;


        }



        //Laplace 1
        private void button18_Click(object sender, EventArgs e)
        {
            maska = new int[3, 3] { { 0 , -1, 0 },
                                    { -1, 4, -1 },
                                    { 0, -1 , 0 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0; pomoc_g = 0; pomoc_b = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];
                            a_wy = a;
                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;
                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;
                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;
                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));
                }
            }
            pictureBox2.Image = temp_bmp;

        }

        //Laplace 2 
        private void button17_Click(object sender, EventArgs e)
        {
            maska = new int[3, 3] { { -1, -1, -1 },
                                    { -1,  8 , -1 },
                                    { -1, -1 , -1 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0; pomoc_g = 0; pomoc_b = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];
                            a_wy = a;
                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;
                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;
                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;
                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));
                }
            }
            pictureBox2.Image = temp_bmp;

        }

        //Laplace 3
        private void button16_Click(object sender, EventArgs e)
        {
            maska = new int[3, 3] { { -2, 1, -2 },
                                    { 1, 4, 1 },
                                    { -2, 1 , -2 } };
            int pomoc_r, pomoc_g, pomoc_b;
            int red_wy = 0, green_wy = 0, blue_wy = 0, a_wy = 0;
            for (int i = 1; i < entry_bmp.Height - 1; i++)
            {
                for (int j = 1; j < entry_bmp.Width - 1; j++)
                {
                    pomoc_r = 0; pomoc_g = 0; pomoc_b = 0;
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            Color c = entry_bmp.GetPixel(j + k, i + l);
                            int a = c.A;
                            int red = c.R;
                            int green = c.G;
                            int blue = c.B;

                            pomoc_r += red * maska[k + 1, l + 1];
                            pomoc_g += green * maska[k + 1, l + 1];
                            pomoc_b += blue * maska[k + 1, l + 1];
                            a_wy = a;
                        }
                    }
                    if (pomoc_r >= 0 && pomoc_r <= 255) red_wy = pomoc_r;
                    else red_wy = 0;
                    if (pomoc_g >= 0 && pomoc_g <= 255) green_wy = pomoc_g;
                    else green_wy = 0;
                    if (pomoc_b >= 0 && pomoc_b <= 255) blue_wy = pomoc_b;
                    else blue_wy = 0;
                    temp_bmp.SetPixel(j, i, Color.FromArgb(a_wy, red_wy, green_wy, blue_wy));
                }
            }
            pictureBox2.Image = temp_bmp;

        }

        // Minimum
        private void button19_Click(object sender, EventArgs e)
        {

            for (int ii = 0; ii < entry_bmp.Height; ii++)
            {
                Color[,] k = new Color[3, 3];
                int r = 0, g = 0, b = 0;
                int[] red = new int[9];
                int[] green = new int[9];
                int[] blue = new int[9];
                int indeks;


                if (ii == 0 || ii == entry_bmp.Width - 1)
                {
                    continue;
                }
                for (int jj = 0; jj < entry_bmp.Height; jj++)
                {
                    if (jj == 0 || jj == entry_bmp.Height - 1)
                    {
                        continue;
                    }
                    indeks = 0;
                    for (int i = -1; i < 2; i++)
                    {

                        for (int j = -1; j < 2; j++)
                        {

                            k[i + 1, j + 1] = entry_bmp.GetPixel(ii + i, jj + j);

                            red[indeks] = k[i + 1, j + 1].R;
                            green[indeks] = k[i + 1, j + 1].G;
                            blue[indeks] = k[i + 1, j + 1].B;
                            indeks++;
                        }
                    }
                    r = red.Min();
                    g = green.Min();
                    b = blue.Min();
                    temp_bmp.SetPixel(ii, jj, Color.FromArgb(r, g, b));

                }
            }
            pictureBox2.Image = temp_bmp;

        }

        //Max
        private void button20_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < entry_bmp.Height; ii++)
            {
                Color[,] k = new Color[3, 3];

                int r = 0, g = 0, b = 0;
                int[] red = new int[9];
                int[] green = new int[9];
                int[] blue = new int[9];
                int indeks;


                if (ii == 0 || ii == entry_bmp.Width - 1)
                {
                    continue;
                }
                for (int jj = 0; jj < entry_bmp.Height; jj++)
                {
                    if (jj == 0 || jj == entry_bmp.Height - 1)
                    {
                        continue;
                        
                    }
                    indeks = 0;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            k[i + 1, j + 1] = entry_bmp.GetPixel(ii + i, jj + j);
                            red[indeks] = k[i + 1, j + 1].R;
                            green[indeks] = k[i + 1, j + 1].G;
                            blue[indeks] = k[i + 1, j + 1].B;
                            indeks++;
                        }
                    }
                    r = red.Max();
                    g = green.Max();
                    b = blue.Max();
                    temp_bmp.SetPixel(ii, jj, Color.FromArgb(r, g, b));
                }
            }
            pictureBox2.Image = temp_bmp;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }




        //Mediana 
        private void button21_Click(object sender, EventArgs e)
        {
            Color[,] k = new Color[3, 3];
            int r = 0, g = 0, b = 0;
            int[] red = new int[9];
            int[] green = new int[9];
            int[] blue = new int[9];
            int indeks;
            for (int ii = 0; ii < entry_bmp.Height; ii++)
            {



                if (ii == 0 || ii == entry_bmp.Width - 1)
                {
                    continue;
                }
                for (int jj = 0; jj < entry_bmp.Height - 1; jj++)
                {
                    if (jj == 0 || jj == entry_bmp.Height - 1)
                    {
                        continue;
                    }
                    indeks = 0;
                    r = 0; g = 0; b = 0;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            k[i + 1, j + 1] = entry_bmp.GetPixel(ii + i, jj + j);

                            red[indeks] = k[i + 1, j + 1].R;
                            green[indeks] = k[i + 1, j + 1].G;
                            blue[indeks] = k[i + 1, j + 1].B;
                            indeks++;
                        }
                    }
                    Array.Sort(red);
                    Array.Sort(green);
                    Array.Sort(blue);

                    r = red[4];
                    g = green[4];
                    b = blue[4];
                    temp_bmp.SetPixel(ii, jj, Color.FromArgb(r, g, b));
                }
            }
            pictureBox2.Image = temp_bmp;
        }

        //histogram 

        private void draw_hist(Bitmap entry_bmp)
        {
            int[] czerw = new int[256];
            int[] ziel = new int[256];
            int[] nieb = new int[256];
           

            chart1.Series["red"].Color = Color.Red;
            chart1.Series["green"].Color = Color.Green;
            chart1.Series["blue"].Color = Color.Blue;
             


            for (int ii = 0; ii < entry_bmp.Width; ii++)
            {
                for (int jj = 0; jj < entry_bmp.Height; jj++)
                {
                    Color pixel = entry_bmp.GetPixel(ii, jj);
                    czerw[pixel.R]++;
                    ziel[pixel.G]++;
                    nieb[pixel.B]++;
                }
            }


            chart1.Series["red"].Points.Clear();
            chart1.Series["green"].Points.Clear();
            chart1.Series["blue"].Points.Clear();




            for (int i = 0; i < 256; i++)
            {
                chart1.Series["red"].Points.AddXY(i, czerw[i] );
                chart1.Series["green"].Points.AddXY(i + 256, ziel[i]);
                chart1.Series["blue"].Points.AddXY(i + 510, nieb[i]);
            }
            chart1.Invalidate();

        }

        // Rysowanie histogramu
        private void button33_Click(object sender, EventArgs e)
        {
            draw_hist(entry_bmp);
        }

        //zamnkiecie aplikacji
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //sprawdza czy wartosc jest w skali rgb
        public int sprawdz_rgb(int value)
        {
            if (value >= 255) return 254;
            if (value <= 0) return 1;
            else return value;
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }



     

      


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private string operation { get; set; }
        public Form1()
        {
            operation = "";
            InitializeComponent();
            textBox1.Text = "0";
            foreach(var item in  this.Controls)
                if (item is Button)
                {
                    ((Button)item).Click += buttonsPressed;
                }
            
            EventHandler ev = new EventHandler(buttonsPressed);
        }


        void buttonsPressed(object sender, EventArgs e)
        {
            string s=((Button)sender).Text;
            if (s[0] >= '0' && s[0] <= '9')
            {
                if (textBox1.Text == "0")
                    textBox1.Text = ((Button)sender).Text;
                else textBox1.Text += ((Button)sender).Text;
            }
            else if (s[0] == 'C')
                textBox1.Text = "0";
            else if (s[0] == '=')
            {
               textBox1.Text=Method();
            }
            else if (s == "cos")
            {
                if (operation == "")
                    textBox1.Text = Math.Round(Math.Cos((Double.Parse(textBox1.Text) * Math.PI) / 180),3).ToString();
                else
                {
                    textBox1.Text = Math.Round(Math.Cos((Double.Parse(Method()) * Math.PI) / 180),3).ToString();
                    operation = "";
                }
            }
            else if (s == "sin")
            {
                if (operation == "")
                    textBox1.Text = Math.Round(Math.Sin((Double.Parse(textBox1.Text) * Math.PI )/ 180),3).ToString();
                else
                {
                    textBox1.Text = Math.Round(Math.Sin((Double.Parse(Method()) * Math.PI )/ 180),3).ToString();
                    operation = "";
                }
            }
            else
            {
                if (operation == "")
                {
                    operation = s;
                    textBox1.Text += s;
                }
                else
                {
                    textBox1.Text=Method();
                    operation = s;
                    textBox1.Text += s;
                }
            }
        }


        private Double ABcount(string a, string b)
        {
            if (operation == "+")
            {
                return Double.Parse(a) + Double.Parse(b);
            }
            else
                if (operation == "-")
                {
                    return Double.Parse(a) - Double.Parse(b);
                }
                else
                    if (operation == "*")
                    {
                        return Double.Parse(a) * Double.Parse(b);
                    }
                    else
                        if (operation == "/")
                        {
                            return Double.Parse(a) / Double.Parse(b);
                        }
                        else throw new Exception("something wrong");

        }
        private string Method()
        {
            string[] ab = textBox1.Text.Split(operation.ToCharArray());
            try
            {
                return ABcount(ab[0], ab[1]).ToString();
                operation = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "ERR";
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

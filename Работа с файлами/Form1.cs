using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Работа_с_файлами
{
    public partial class Form1 : Form
    {
        String MyFileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog MyOpenFileDialog = new OpenFileDialog();
            MyOpenFileDialog.Filter = "Text|*.txt";
            if (MyOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                MyFileName = MyOpenFileDialog.FileName;
                StreamReader MyStreamReader = new StreamReader(MyFileName,System.Text.Encoding.Default);
                textBox1.Text = MyStreamReader.ReadToEnd();
                MyStreamReader.Close();
                button2.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader MyStreamReader = new StreamReader(MyFileName, System.Text.Encoding.Default);
            String s,Glas,specsymbols;
            Byte i,k_wordsglas = 0;
            Boolean State = true;
            s = MyStreamReader.ReadLine();
            Glas = "уеыаоэяию";
            specsymbols = " /?.>,<'\"]}[{;:\\|=+-_)(*&^%$#@!~`№";
            /*
                у автомата 2 стадии "слов"-true и "не слов"-false.
                изначально проверяется строка начинается со слова или нет
                если проверяется 2 первых буквы слова в не зависимости от результата переходим в стадию не слово
                в стадии не слово происходит проверка на соответствие текущего символа набору спец символов
                если текущий сивол равен спец символу то происходит переход к стадии слово 
            */
            while(s!=null)
            {
                State = true;
                if (specsymbols.Contains(Convert.ToString(s[0])))
                    State = false;
                for (i = 0; i < s.Length-1; i++)
                {
                    if (State==true)
                    {
                        if (Glas.Contains(Convert.ToString(s[i])))
                            if (Glas.Contains(Convert.ToString(s[i + 1])))
                                k_wordsglas++;
                        if (!specsymbols.Contains(Convert.ToString(s[i])))
                            State = false;
                    }
                    else
                        if (specsymbols.Contains(Convert.ToString(s[i])))
                            State = true;
                    MessageBox.Show(s[i] + " " + State + " " + k_wordsglas);
                }
                s = MyStreamReader.ReadLine();
            }
            MyStreamReader.Close();
            label1.Text = Convert.ToString(k_wordsglas);
        }
    }
}
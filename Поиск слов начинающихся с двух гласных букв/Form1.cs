using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        String MyFileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//процедура выбора файла
        {
            OpenFileDialog MyOpenFileDialog = new OpenFileDialog();
            MyOpenFileDialog.Filter = "Text|*.txt";
            if (MyOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                MyFileName = MyOpenFileDialog.FileName;
                StreamReader MyStreamReader = new StreamReader(MyFileName, System.Text.Encoding.Default);
                textBox1.Text = MyStreamReader.ReadToEnd();
                MyStreamReader.Close();
                button2.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)//процедура подсчета количества слов
        {
            Byte k_wordsglas = 0;//счетчики
            Boolean State = true;//переменная состояний
            String Glas = "уеыаоэяию";//список русских гласный строчных букв
            String specsymbols = " /?.>,<'\"]}[{;:\\|=+-_)(*&^%$#@!~`№0123456789";//список "не букв"
            /*
                у автомата 2 стадии "слов"-true и "не слов"-false.
                изначально проверяется строка начинается со слова или нет
                если проверяется 2 первых буквы слова в не зависимости от результата переходим в стадию не слово
                в стадии не слово происходит проверка на соответствие текущего символа набору спец символов
                если текущий сивол равен спец символу то происходит переход к стадии слово 
            */
            StreamReader MyStreamReader = new StreamReader(MyFileName, System.Text.Encoding.Default);//начало чтения файла
            String s = MyStreamReader.ReadLine();
            while (s != null)//перебор строк
            {
                State = true;//состояние word
                if (specsymbols.Contains(Convert.ToString(s[0])))//если первый символ не буква (первая проверка для состояния base)
                    State = false;//базовое состояние
                for (Byte i = 0; i < s.Length - 1; i++)//перебор символов
                {
                    if (State == true)//если состояние word
                    {
                        if (Glas.Contains(Convert.ToString(s[i])))//если текущий символ - гласная строчная буква
                            if (Glas.Contains(Convert.ToString(s[i + 1])))//если следующий символ - гласная строчная буква
                                k_wordsglas++;//увеличение счетчика
                        State = false;//переход в базовое состояние
                    }
                    else
                        if (specsymbols.Contains(Convert.ToString(s[i])))//если текущий символ не буква
                            State = true;//переход в состояние word
                }
                s = MyStreamReader.ReadLine();//чтение следующий строки
            }
            MyStreamReader.Close();//завершение чтения файла
            label1.Text = "Количество слов начинающихся двух русских строчных гласных букв: " + Convert.ToString(k_wordsglas);//вывод количества на форму
        }
    }
}

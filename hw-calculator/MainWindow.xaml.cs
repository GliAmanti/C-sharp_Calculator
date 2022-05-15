using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using static hw_calculator.MySqlClass;

namespace hw_calculator
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        string temp;
        MySqlClass mySql = new MySqlClass();


        public MainWindow()
        {
            InitializeComponent();

            String query_select = "select * from record";
            mySql.DBConnection1(query_select);
        }

        private void Button_7_Click(object sender, RoutedEventArgs e)

        {
            Button btn = (Button)sender;

            console.Text = console.Text + "7";
        }
        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "8";
        }
        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "9";
        }
        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "4";
        }
        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "5";
        }
        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "6";
        }
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "1";
        }
        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "2";
        }
        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "3";
        }
        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "0";
        }
        private void Button_plus_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "+";
        }
        private void Button_minus_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "-";
        }
        private void Button_multiply_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "*";
        }
        private void Button_divide_Click(object sender, RoutedEventArgs e)
        {
            console.Text = console.Text + "/";
        }
        private void Button_deleteOne_Click(object sender, RoutedEventArgs e)
        {
            temp = console.Text;

            if(temp!="")
            {
                temp = temp.Substring(0, temp.Length - 1);
                console.Text = temp;
            }
        }
        private void Button_deleteAll_Click(object sender, RoutedEventArgs e)
        {
            console.Text = "";
        }
        private void Button_enter_Click(object sender, RoutedEventArgs e)
        {
            string input = console.Text;

            getPostPreAns(input);
        }
        private void Button_insert_Click(object sender, RoutedEventArgs e)
        {
            string expression = console.Text, preorder = preAns.Text, postorder = postAns.Text, deci = decAns.Text, bin = binAns.Text;
            string query_check = "SELECT expression FROM record WHERE expression = '"+expression+"'";
            string query_insert = "insert into record (expression, preorder, postorder, deci, bin) values ('"+expression+"', '"+preorder+"', '"+postorder+"', '"+deci+"', '"+bin+"')";
            int check=mySql.DBCheck(query_check,expression);

           if (check == 0)
            {
                mySql.DBInsert(query_insert);
            }  
        }
        private void Button_query_Click(object sender, RoutedEventArgs e)
        {
            DBtable dbtable = new DBtable();
            this.Hide();
            dbtable.Show();
        }

        private int ReadToken(char token)
        {
            if (token == '(')
                return 0;
            else if (token == ')')
                return 1;
            else if (token == '+')
                return 2;
            else if (token == '-')
                return 3;
            else if (token == '*')
                return 4;
            else if (token == '/')
                return 5;
            else if (token == '%')
                return 6;
            else if (token == ' ')
                return 7;
            else
                return 8;
        }
        private void getPostPreAns(string str)
        {
            string resultPost = "", resultPre = "", resultPreReverse = "";
            Stack<char> temp = new Stack<char>();
            temp.Push(' ');
            char[] tokens = new char[200];
            int[] isp = new int[8] { 0, 19, 12, 12, 13, 13, 13, 0 };
            int[] icp = new int[8] { 20, 19, 12, 12, 13, 13, 13, 0 };
            int num = 0;

            string numTemp = "";
            Queue<string> cutNum = new Queue<string>(); 

            for (int i = 0; i < str.Length; i++)
            {
                tokens[i] = str[i];
                num++;
            }
            for (int i = 0; i < num; i++)   //Infix to Postfix
            {
                if (ReadToken(tokens[i]) == 8)
                {
                    resultPost = resultPost + tokens[i];

                    numTemp = numTemp + tokens[i];
                    if(i==num-1)
                    {
                        cutNum.Enqueue(numTemp);
                        numTemp = "";
                    }
                }
                else
                {
                    cutNum.Enqueue(numTemp);
                    numTemp = "";

                    while (isp[ReadToken(temp.Peek())] >= icp[ReadToken(tokens[i])])
                    {
                        resultPost = resultPost + temp.Peek();
                        cutNum.Enqueue(temp.Peek().ToString());
                        temp.Pop();
                    }
                    temp.Push(tokens[i]);
                }
            }
            while (ReadToken(temp.Peek()) != 7)
            {
                resultPost = resultPost + temp.Peek();
                cutNum.Enqueue(temp.Peek().ToString());
                temp.Pop();
            }
            getDecAns(cutNum);  //由Postfix計算答案
            postAns.Text = resultPost;

            for (int i = num-1; i >= 0; i--)    //Infix to Prefix
            {
                if (ReadToken(tokens[i]) == 8)
                {
                    resultPre = resultPre + tokens[i];
                }
                else
                {
                    while (isp[ReadToken(temp.Peek())] > icp[ReadToken(tokens[i])])
                    {
                        resultPre = resultPre + temp.Peek();
                        temp.Pop();
                    }
                    temp.Push(tokens[i]);
                }
            }
            while (ReadToken(temp.Peek()) != 7)
            {
                resultPre = resultPre + temp.Peek();
                temp.Pop();
            }
            for (int i = (resultPre.Length)-1; i >= 0; i--)
            {
                resultPreReverse = resultPreReverse + resultPre[i];
            }
            preAns.Text = resultPreReverse;
        }
         private void getDecAns(Queue<string> q)
        {
            string temp = "";
            int op1, op2,number,finalAns;
            Stack<int> calculation = new Stack<int>();

            while(q.Count!=0)
            {
                temp = q.Dequeue();

                if (temp == "+" || temp == "-" || temp == "*" || temp == "/")
                {
                    op2 = calculation.Pop();
                    op1 = calculation.Pop();

                    if (temp == "+")
                        calculation.Push(op1 + op2);
                    else if(temp == "-")
                        calculation.Push(op1 - op2);
                    else if (temp == "*")
                        calculation.Push(op1 * op2);
                    else if (temp == "/")
                        calculation.Push(op1 / op2);
                }
                else
                {
                    number = int.Parse(temp);
                    calculation.Push(number);
                }
            }
            finalAns = calculation.Pop();
            decAns.Text = (finalAns).ToString();
            toBinary(finalAns);
        }
        private void toBinary(int dec)
        {
            Stack<int> binStack = new Stack<int>();
            string binStr = "";

            if(dec==0)
            {
                binStr = "0";
            }
            else
            {
                while (dec > 0)
                {
                    binStack.Push(dec % 2);
                    dec = dec / 2;
                }
                while (binStack.Count != 0)
                {
                    binStr = binStr + binStack.Pop();
                }
            }
            binAns.Text = binStr;
        }
    }
}
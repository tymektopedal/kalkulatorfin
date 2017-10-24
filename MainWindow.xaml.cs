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


namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    enum Action
    {
        Add,
        Substract,
        Multiply,
        Divide,
        None
    }

    public partial class MainWindow : Window
    {

        private double baseNumber = 0;
        private double operationNumber = 0;
        private Action currentAction = Action.None;
        private bool resetString = true;
        private bool firstAction = true;
        private bool justCalculated = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void changeTitle()
        {
            this.Title = "Calculator (" + baseNumber.ToString() + ")";
        }

        private void checkSize()
        {
            if (NumberBlock.Text.Length > 20)
            { 
                NumberBlock.FontSize = 8;
            } else if (NumberBlock.Text.Length > 10)
            {
                NumberBlock.FontSize = 14;
            } else
            {
                NumberBlock.FontSize = 26;
            }
        }

        private void Nullify(bool full = false)
        {
            NumberBlock.Text = "0";
            resetString = true;
            if (full)
            {
                currentAction = Action.None;
                baseNumber = 0;
                operationNumber = 0;
                firstAction = true;
                OperatorBlock.Text = " ";
            }
                
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string x = ((Button) sender).Content.ToString();
            if (!(x == "0" && NumberBlock.Text == "0"))
            {
                if (resetString)
                {
                    NumberBlock.Text = x;
                    resetString = false;
                }
                else
                {
                    NumberBlock.Text += x;
                }
            }
            checkSize();
            changeTitle();
        }
        
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            string x = ((Button) sender).Content.ToString();

            switch (x)
            {
                case "+":
                    currentAction = Action.Add;
                    break;
                case "-":
                    currentAction = Action.Substract;
                    break;
                case "*":
                    currentAction = Action.Multiply;
                    break;
                case "/":
                    currentAction = Action.Divide;
                    break;
            }

            OperatorBlock.Text = x;

            if (firstAction)
            {
                baseNumber = double.Parse(NumberBlock.Text);
                firstAction = false;
                Nullify();
            } else
            {
                if (justCalculated)
                    Nullify();
                operationNumber = double.Parse(NumberBlock.Text);
            }
            changeTitle();
        }

        private void Calculate(object sender = null, RoutedEventArgs e = null)
        {

            if (operationNumber == 0)
            {
                operationNumber = double.Parse(NumberBlock.Text);
            }
            switch (currentAction)
            {
                case Action.Add:
                    baseNumber += operationNumber;
                    break;
                case Action.Substract:
                    baseNumber -= operationNumber;
                    break;
                case Action.Multiply:
                    baseNumber *= operationNumber;
                    break;
                case Action.Divide:
                    baseNumber /= operationNumber;
                    break;
            }
            NumberBlock.Text = baseNumber.ToString();
            resetString = true;
            justCalculated = true;
            checkSize();
            changeTitle();
        }

        private void Special_Click(object sender, RoutedEventArgs e)
        {
            string x = ((Button)sender).Content.ToString();

            if (x == "←")
            {
                if (NumberBlock.Text.Length > 1)
                    NumberBlock.Text = NumberBlock.Text.Remove(NumberBlock.Text.Length - 1, 1);
                else
                {
                    Nullify();
                }
            } else if (x == "CE") {
                Nullify();
            } else if (x == "C")
            {
                Nullify(true);
            } else if (x == "±")
            {
                double num = double.Parse(NumberBlock.Text);
                num = -num;
                NumberBlock.Text = num.ToString();
            } else if (x == "√")
            {
                NumberBlock.Text = ((Math.Sqrt(double.Parse(NumberBlock.Text)))).ToString();
                resetString = true;
            }
            changeTitle();
        }
    }
}

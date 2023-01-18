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
using System.Windows.Shapes;

namespace ST10127438_FINAL_POE
{
    /// <summary>
    /// Interaction logic for HOME_LOAN.xaml
    /// </summary>
    public partial class HOME_LOAN : Window
    {
        Double HMLRentProp = .0, HMLRentMonthlyRepay = .0;
        List<Double> MyRent = new List<Double>(2); //Generic Collection
        Boolean rdbR = false, rdbB = false;
                                            //Variable Is To Be Used To Hold The Passed Value From MainWindow Window/ Class.
        public double dblGI;
        public HOME_LOAN()
        {
            //After The Appearance Of The Window, This Makes The Textbox And Label Of The Rent Prompts To Be Invisible When The Property Window appears.
            InitializeComponent();
            txtRent.Visibility = Visibility.Hidden;
            grbRent.Visibility = Visibility.Hidden;
            var lbDT = DateTime.Now;
            lbCT.Content = lbDT.ToString();
        }
        private void btnShowStatement(object sender, RoutedEventArgs e) => tbView();
        private void btnDelete(object sender, RoutedEventArgs e) => tbClear();
        private void btnBack(object sender, RoutedEventArgs e) => PreviousPage(); 
        private void rdbRent_Checked(object sender, RoutedEventArgs e) => rdbRentOpt();
        private void rdbBuy_Checked(object sender, RoutedEventArgs e) => rdbBuyOpt(); 
        private void btnPropertyProcess(object sender, RoutedEventArgs e) => HomeLoanI();
        public void PreviousPage()
        {
            this.Hide();
            var dblMW = new MainWindow();
            dblMW.ShowDialog();
        }
        public void HomeLoanI()
        {
            try
            {   
                if (rdbBuy.IsChecked.Equals(true))
                {
                    if (rdbRent.IsChecked.Equals(false))
                    {
                        rdbBuyOpt();
                    }
                    this.Hide();
                    var PropertyI = new BUY_PROPERTY(); 
                    PropertyI.dblInherGrossIncome = dblGI;
                    PropertyI.ShowDialog();
                }
              
                else if (rdbBuy.IsChecked.Equals(false))
                {
                    if (rdbRent.IsChecked.Equals(true))
                    {
                        if (!(txtRent.Text.Trim().Equals("")))
                        {
                            RentI();
                        }
                        else
                        {  
                            lblErr.Content = "'Please enter Rent Amount.'";
                        }
                    }
                }
            }
            //To Catch Exception
            catch (OutOfMemoryException) { lblErr.Content = "'NOT Enough Memory collected To Run Application.'"; }
            catch (ArgumentException) { lblErr.Content = "'No entries made .'"; }
            catch (FormatException) { lblErr.Content = "'Input Type Is Not In A Correct Format.'"; }
        }
        public void RentI()
        {
            
            if (!(Double.TryParse(txtRent.Text.Trim(), out HMLRentProp) == false))
            {
                MyRent.Add(HMLRentProp); MyRent.Add(HMLRentMonthlyRepay);
                MessageBoxResult strMSG = System.Windows.MessageBox.Show(
                    "Do You Want To purchase A Vehicle?", "Vehicle Window", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (!(strMSG.Equals(MessageBoxResult.No)))
                {
                    this.Hide();
                    var dblV = new VEHICLE();
                    dblV.ShowDialog();
                }
                else
                {
                    this.Hide();
                    var dblDS = new SAVINGS();
                    dblDS.ShowDialog();
                }
            }
            else
            {
                System.Windows.MessageBox.Show(
                    "'String entry Is Not allowed. Only Numeric values are allowed.'", "Invalid value entered!", MessageBoxButton.OK);
            }
        }
     
        public void rdbRentOpt()
        {
            txtRent.Clear();
            txtRent.Visibility = Visibility.Visible;
            grbRent.Visibility = Visibility.Visible;
            rdbR = true;
        }
        public void tbClear() { txtRent.Clear(); }
        public Double RentCalc()
        {
            HMLRentMonthlyRepay += dblGI - HMLRentProp;
            return HMLRentMonthlyRepay / 12;
        }
        public void tbView()
        {
            RentI();
            try
            {
                System.Windows.MessageBox.Show(
                    "Rent Amount: R " + HMLRentProp +
                    "\nMonthly Repayment: R" + RentCalc(), "'Successfully Saved!'");
            }
            catch (ArgumentOutOfRangeException) { lblErr.Content = "'Index Is Out Of Range. Please Provide Values.'"; }
        }
        public void rdbBuyOpt()
        {
            txtRent.Visibility = Visibility.Hidden;
            grbRent.Visibility = Visibility.Hidden;
            rdbB = true;
        }
    }
}



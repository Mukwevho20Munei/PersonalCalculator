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
    /// Interaction logic for BUY_PROPERTY.xaml
    /// </summary>
    public partial class BUY_PROPERTY : Window
    {
        MainWindow Inheritance = new MainWindow();

        Double A = .0, a, p, r, n, dblPPurPrice = .0, dblPTotalDep = .0, dblPInterest = .0, dblPMonthlyAmount = 0, dblPMonthsRepay = 0;
        const Double MaxYear = 360, MinYear = 240;
        public double dblInherGrossIncome;
        //(7): Is To Specifying The Array Size Of Values To Be Stored Up.
        List<Double> MyProperty = new List<Double>(5);

        public BUY_PROPERTY()
        {
            InitializeComponent();

            var lbDT = DateTime.Now;
            lbCT.Content = lbDT.ToString();
        }
        private void btnDelete(object sender, RoutedEventArgs e) => tbClear();
        private void btnShowStatement(object sender, RoutedEventArgs e) => getPSavedValues();
        private void btnBack(object sender, RoutedEventArgs e) => PreviousPage();
        private void btnNext(object sender, RoutedEventArgs e) => PropertyI();

        //Directs User To The Previous Window.
        public void PreviousPage()
        {
            this.Hide();
            var dblHL = new HOME_LOAN();
            dblHL.ShowDialog();
        }
        public void PropertyI()
        {
            try
            {
                //To Check Condition Of The Inputs.
                if (!((txtPTotalDept.Text.Trim().Equals(""))
                    || (txtPurPrice.Text.Trim().Equals(""))
                    || (txtPInterest.Text.Trim().Equals(""))
                    || (txtPMonthsRepay.Text.Trim().Equals(""))))
                {
                    tbPropertyValues();
                    //Storing The Values In The Generic Collecton
                    //If User Decides To Purchase A Vehicle, The Vehicle Window Is To Show Hence The ShowDialog() Function Is Implemented.
                    //Unless The User Chooses Otherwise Then, The Results Window Is Gonna Show. 
                    MyProperty.Add(dblPTotalDep); MyProperty.Add(dblPInterest); MyProperty.Add(dblPPurPrice); MyProperty.Add(dblPMonthsRepay); MyProperty.Add(dblPMonthlyAmount);
                    MessageBoxResult dblMSG = System.Windows.MessageBox.Show(
                        "Do You Want To Buy A Vehicle?", "Vehicle Window", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (!(dblMSG.Equals(MessageBoxResult.No)))
                    {
                        this.Hide();
                        var dblV = new VEHICLE(); //Open The Vehicle Window.
                        dblV.ShowDialog();
                    }
                    else
                    {
                        this.Hide();
                        var dblDS = new SAVINGS(); //Otherwise Open The Detail Window.
                        dblDS.ShowDialog();
                    }
                }
                else
                {
                    //Message Is To Pop Up If Any Of The Fields Is Not Complete.
                    System.Windows.MessageBox.Show(
                        "'There Are Some Important Value Fields Required Incomplete", "Incomplete Response!", MessageBoxButton.OK);
                }
            }
            catch (OutOfMemoryException) { lblErr.Content = "'Insufficient Memory Accumulated To Run Application.'"; }
            catch (ArgumentException) { lblErr.Content = "'No Values Were Recieved.'"; }
            catch (FormatException) { lblErr.Content = "'Input Type Is Not In A Correct Format.'"; }
        }
        public void tbPropertyValues()
        {
            try
            {
                //Is To Loop Through A Condition, By Checking If
                //All Values To Be Entered By User Are Of Numeric Type. 
                if (Double.TryParse(txtPurPrice.Text.Trim(), out dblPPurPrice) == false ||
                    Double.TryParse(txtPInterest.Text.Trim(), out dblPInterest) == false ||
                    Double.TryParse(txtPTotalDept.Text.Trim(), out dblPTotalDep) == false ||
                    Double.TryParse(txtPMonthsRepay.Text.Trim(), out dblPMonthsRepay) == false)
                {
                    //Message Is To Display If Any Of The Inputs
                    //To Be Entered By User Are Not Of Numeric Type And One String Type.
                    System.Windows.MessageBox.Show(
                        "'Only Numeric Type Is Allowed.'", "Invalid Type Response!", MessageBoxButton.OK);
                }
                else { PMonthlyPay(); } //Invoke A Method.
            }
            catch (ArgumentException) { lblErr.Content = "'No Values Were Recieved.'"; }
        }
        //By Chance User Enters Months Not
        //Corresponding To The Speccified Ones.
        public void PMonthlyPay()
        {
            if ((dblPMonthsRepay < MinYear))
            {
                System.Windows.MessageBox.Show(
                         "'Month Entered Is Either Greater Than 360 Or Below 240. Please Stick To The Specified Months.'", "Months Alert!");
            }
            else if ((dblPMonthsRepay > MaxYear))
            {
                System.Windows.MessageBox.Show(
                          "'Month Entered Is Greater Than 360. Please Stick To The Specified Months.'", "Months Alert!");
            }
            else
            {
                //Do Some Math The Property Repayment.
                p = (dblPPurPrice - dblPTotalDep);
                r = dblPInterest / 100;
                n = dblPMonthlyAmount / 12;
                A = p * (1 + (r * n));
                dblPMonthlyAmount = A / 12;

                //Checking If Homeloan Is Greater Or
                //Than User's Gross Income 
                if (dblPMonthlyAmount > (dblInherGrossIncome / 3))
                {
                    System.Windows.MessageBox.Show(
                        "Based On The Information You Have Provided, You Do Not Qualify For A Loan.", "Loan Notification");
                }
            }
        }
        public void tbClear()
        {
            txtPInterest.Clear();
            txtPMonthsRepay.Clear();
            txtPTotalDept.Clear();
            txtPurPrice.Clear();
            lblErr.Content = "";
        }
        public void getPSavedValues()
        {
            tbPropertyValues();
            try
            {
                System.Windows.MessageBox.Show(
                    "---------------------------------------------------------------------" +
                "\n\tOUTCOME AFTER CALCULATIONS" +
                    "\n---------------------------------------------------------------------" +
                   "\nProperty Price: R " + dblPPurPrice +
                     "\nTotal Deposit: R " + dblPTotalDep +
                    "\nInterest Rate(%): " + dblPInterest +
                  "\nNo' Of Months To Repay(Between 240 & 360): " + dblPMonthsRepay +
                "\nMonthly Home Loan Repayment: R " + dblPMonthlyAmount +
                "\n---------------------------------------------------------------------"
                    , "'Successfully Saved!'");
            }
            catch (ArgumentOutOfRangeException) { lblErr.Content = "'Index Is Out Of Range. Please Provide Values.'"; }
        }
    }
}

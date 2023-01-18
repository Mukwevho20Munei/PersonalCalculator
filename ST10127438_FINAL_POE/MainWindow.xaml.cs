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

namespace ST10127438_FINAL_POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Double> ExpMyMonthlyExpenses = new List<Double>(5); 
        Double ExpGrossIncome = .0, ExpMonthTax = .0, ExpGroceries = .0, ExpWaterAndLights = .0,
          ExpTravel = .0, ExpcellPhones = .0, ExpOtherExp = .0, ExpMonthlyNetAmount = .0;
        public MainWindow()
        {
            InitializeComponent();
            var lbDT = DateTime.Now;
            lbCT.Content = lbDT.ToString();
        }
        private void btnNext(object sender, RoutedEventArgs e) => getMonthlyValues(); 
        private void btnDELETE(object sender, RoutedEventArgs e) => tbClear();

         private void btnShowStatement(object sender, RoutedEventArgs e) => SavedValues();
        public void getMonthlyValues()
        {
            try
            {
                //To Check Condition Of The Inputs If Textboxes
                //They're Empty Or With Values.
                if (!((txtGrossIncomeExp.Text.Trim().Equals("")) ||
                    (txtGroceriesExp.Text.Trim().Equals("")) ||
                    (txtOtherExp.Text.Trim().Equals("")) ||
                    (txtOtherExp.Text.Trim().Equals("")) ||
                    (txtMonthlyTaxExp.Text.Trim() == ("")) ||
                    (txtTravelCostExp.Text.Trim().Equals("")) ||
                    (txtcellPhonesExp.Text.Trim().Equals("")) ||
                    (txtOtherExp.Text.Trim().Equals(""))))
                {
                    MonthlyValValidation();
                   
                    this.Hide();
                    var dblHL = new HOME_LOAN(); 
                    dblHL.dblGI = ExpGrossIncome;
                    
                    dblHL.ShowDialog();
                }
                else
                {
                    //Message Is To Pop Up If Any Of The Fields Is Not Complete.
                    MessageBox.Show(
                        "'All textbox requires complulsory entery", "Enter all values!", MessageBoxButton.OK);
                }
            }
            catch (OutOfMemoryException) { lblErr.Content = "'NOT Enough Memory collected To Run Application..'"; }
            catch (ArgumentException) { lblErr.Content = "'No Values Were Recieved.'"; }
            catch (FormatException) { lblErr.Content = "'Input Type Is Not In A Correct Format.'"; }
        }
        public void MonthlyValValidation()
        {
            try
            {
                //Is To Loop Through A Condition, By Checking If
                //All Values To Be Entered By User Are Of Numeric Type. 
                if (Double.TryParse(txtGrossIncomeExp.Text.Trim(), out ExpGrossIncome) == false ||
                    Double.TryParse(txtMonthlyTaxExp.Text.Trim(), out ExpMonthTax) == false ||
                    Double.TryParse(txtGroceriesExp.Text.Trim(), out ExpGroceries) == false ||
                    Double.TryParse(txtcellPhonesExp.Text.Trim(), out ExpcellPhones) == false ||
                    Double.TryParse(txtTravelCostExp.Text.Trim(), out ExpTravel) == false ||
                    Double.TryParse(txtWaterAndLightsExp.Text.Trim(), out ExpWaterAndLights) == false ||
                    Double.TryParse(txtOtherExp.Text.Trim(), out ExpOtherExp) == false)
                {
                    //Message Is To Display If Any Of The Inputs
                    //To Be Entered By User Are Not Of Numeric Type.
                    System.Windows.MessageBox.Show(
                        "'Only Numeric Types Are Allowed.'", "Invalid Type Response!", MessageBoxButton.OK);
                }
                else
                {
                    //At The Same Time Getting And Storing The Expense
                    //Values In The Generic Collection Instead Of An Array.
                    ExpMyMonthlyExpenses.Add(ExpGroceries); ExpMyMonthlyExpenses.Add(ExpcellPhones); ExpMyMonthlyExpenses.Add(ExpTravel);
                    ExpMyMonthlyExpenses.Add(ExpWaterAndLights); ExpMyMonthlyExpenses.Add(ExpOtherExp);

                    //this.Hide();
                    //var dblHL = new Home_Loan(); //Invoke The Next Window.
                    //dblHL.dblGI = dblGrossIncome;
                    //dblHL.ShowDialog();
                }
            }
            catch (ArgumentException) { lblErr.Content = "'No Values Were ENTERED.'"; }
        }
       // Generic-Collection In Descendiing Order.
        public void DescOrder()
        {
            ExpMyMonthlyExpenses.Reverse();
        }
        public void tbClear()
        {
           
            txtGrossIncomeExp.Clear();
            txtMonthlyTaxExp.Clear();
            txtGroceriesExp.Clear();
            txtWaterAndLightsExp.Clear();
            txtTravelCostExp.Clear();
            txtcellPhonesExp.Clear();
            txtOtherExp.Clear();
        }
        public void SavedValues()
        {
            try
            {
                MonthlyValValidation();
                
                System.Windows.MessageBox.Show(
                    "****************************************************************************" +
                    "\tOUTCOME AFTER CALCULATIONS" +
                    "\n****************************************************************************" +
                    "\nGross Income: R " + ExpGrossIncome +
                    "\nGroceries: R " + ExpGroceries +
                    "\nMonthly Tax: " + ExpMonthTax +
                    "\nWater & Electricity: R " + ExpWaterAndLights +

                   "\nTravel: R " + ExpTravel +
                    "\nPhones: R " + ExpcellPhones +
                    "\nOtherEx: R " + ExpOtherExp +
                    "\nAvailable Monthly Amount: R " + getAfterDeducts() +
                    "\n****************************************************************************" +
                    "\n\tEXPENDITURES IN DESCENDING ORDER" +
                    "\n****************************************************************************", "'Successfully Saved!'");
            }
            catch (ArgumentOutOfRangeException) { lblErr.Content = "'Index Is Out Of Range. Please Provide Values.'"; }
        }
        //Computing The Amount That Is To Be Left
        //After Deductions Are Made And Specified.
        public Double getAfterDeducts() { return ExpMonthlyNetAmount += ExpGrossIncome - (ExpMonthTax + ExpGroceries + ExpOtherExp + ExpcellPhones + ExpTravel + ExpWaterAndLights); }
    }
}

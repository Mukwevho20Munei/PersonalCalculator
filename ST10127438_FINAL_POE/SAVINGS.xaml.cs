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
    /// Interaction logic for SAVINGS.xaml
    /// </summary>
    public partial class SAVINGS : Window
    {
        Double SavAmount = .0, SavMonthly = .0, SavRate = .0, SavTotalMonthlyCost = .0, SavInterest = .0;
        int SavPeriodOfTime;
        String SavPurpose = "";
        List<Double> MyS = new List<Double>();
      
        public SAVINGS()
        {
            InitializeComponent();
            var lbDT = DateTime.Now;
            lbCT.Content = lbDT.ToString();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void btnPropertyProcess(object sender, RoutedEventArgs e) => getMonthlyValues();
        private void btnBack(object sender, RoutedEventArgs e) => PreviousPage();
        private void btnShowStatement(object sender, RoutedEventArgs e) => getValues();
        private void btnDelete(object sender, RoutedEventArgs e) => tbClear();
        public void PreviousPage()
        {
            this.Hide();
            var dblV = new VEHICLE();
            dblV.ShowDialog();
        }
        public void getMonthlyValues()
        {
            try
            {
                
                if (!((txtPeriod.Text.Trim().Equals("")) ||
                    (txtAmount.Text.Trim().Equals("")) ||
                    (txtReason.Text.Trim().Equals("")) ||
                    (txtInterest.Text.Trim().Equals(""))))
                {
                    Validation();
             
                    MessageBox.Show(
                        "'All textbox requires complulsory entery", "Enter all values!", MessageBoxButton.OK);

                }
                else
                {
                    MessageBoxResult dblMSG = System.Windows.MessageBox.Show(
                        "PROGAM ENDS:)", "Complete:)", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    if (dblMSG.Equals(MessageBoxResult.OK)) { Environment.Exit(0); }
                }
            }
            catch (OutOfMemoryException) { lblErr.Content = "'NOT Enough Memory collected To Run Application..'"; }
            catch (ArgumentException) { lblErr.Content = "'No Values Were Recieved.'"; }
            catch (FormatException) { lblErr.Content = "'Input Type Is Not In A Correct Format.'"; }
        }
        public void Validation()
        {
            try
            {
                // values To Be Entered must be in Numeric Type.
                SavPurpose += Convert.ToString(txtReason.Text.Trim());
                if (Double.TryParse(txtAmount.Text.Trim(), out SavAmount) == false ||
                    Double.TryParse(txtInterest.Text.Trim(), out SavInterest) == false ||
                    int.TryParse(txtPeriod.Text.Trim(), out SavPeriodOfTime) == false)
                {
                    
                    // values To Be Entered must be in Numeric Type.
                    System.Windows.MessageBox.Show(
                        "'Only Numeric Types Are Allowed.'", "Invalid Type Response!", MessageBoxButton.OK);
                }
                else
                {
                    
                    //Generic Collection 
                    MyS.Add(SavPeriodOfTime); MyS.Add(SavAmount); MyS.Add(SavMonthly);
                    getMonthlyCalc();
                }
            }
            catch (ArgumentException) { lblErr.Content = "'No Values Were ENTERED.'"; }
        }
        public void tbClear()
        {
            
            txtAmount.Clear();
            txtPeriod.Clear();
            txtReason.Clear();
        }
        public Double getMonthlyCalc()
        {
            SavInterest += SavAmount * SavPeriodOfTime * SavRate / 100;
            return SavTotalMonthlyCost += SavAmount + SavInterest;
        }
        
        private void tbInter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void getValues()
        {
            Validation();
            try
            {
               
              
               
                
                System.Windows.MessageBox.Show(
                    "**********************************************************************" +
                    "\tOUTCOME AFTER CALCULATIONS" +
                    "\n**********************************************************************" +
                    "\nAmount Saved: R " + SavAmount +
                    "\nYears: " + SavPeriodOfTime +
                    "\nPurpose For Saving: " + SavPurpose +
                    "\nInterest Rate(%): " + SavRate +
                    "\nHave To Pay Monthly: R " + getMonthlyCalc() +
                    "\n**********************************************************************", "'Successfully Saved!'");
            }
            catch (ArgumentOutOfRangeException) { lblErr.Content = "'Index Is Out Of Range. Please Provide Values.'"; }
        }
    }
}

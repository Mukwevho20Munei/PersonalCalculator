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
    /// Interaction logic for VEHICLE.xaml
    /// </summary>
    public partial class VEHICLE : Window
    {
        //Declarations 
        
        Double VEHVPrice = .0, VEHVTotal = .0, VEHVInsurance = .0, VEHVMonthlypay = .0, VEHVRate = .0;
        String strModel = "";
        List<Double> MyVehicleExp = new List<Double>(5);
        public VEHICLE()
        {
            InitializeComponent();
            var lbDT = DateTime.Now;
            lbCT.Content = lbDT.ToString();
        }
        private void btnDelete(object sender, RoutedEventArgs e) => tbClear();
        private void btnBack(object sender, RoutedEventArgs e) => PreviousPage(); 
        private void VbtnProcess_Click_1(object sender, RoutedEventArgs e) => VehicleI(); 
        private void btnShowStatement(object sender, RoutedEventArgs e) => getVSavedValues();
        public void PreviousPage()
        {
            this.Hide();
            var dblP = new BUY_PROPERTY();
            dblP.ShowDialog();
        }
        public void VehicleI()
        {
            try
            {
                
                if (!((txtModel.Text.Trim().Equals(""))
                    || (txtInsurance.Text.Trim().Equals(""))
                    || (txtRate.Text.Trim().Equals(""))
                    || (txtPrice.Text.Trim().Trim().Equals(""))
                    || (txtTotal.Text.Trim().Equals(""))))
                {
                    tbVehicleValues();
                    this.Hide();
                    var dblS = new SAVINGS();
                    dblS.ShowDialog();
                }
                else
                {   
                    System.Windows.MessageBox.Show(
                        "'All textbox requires complulsory entery", "Enter all values!", MessageBoxButton.OK);
                }
            }
            //To Catch Exception 
            catch (OutOfMemoryException) { lblErr.Content = "'NOT Enough Memory collected To Run Application.'"; }
            catch (ArgumentException) { lblErr.Content = "'No entries made.'"; }
            catch (FormatException) { lblErr.Content = "'Input Type Is Not In A Correct Format.'"; }
        }
        public void tbVehicleValues()
        {
            try
            {
                
                //All Values mUST be in Numeric Type.
                strModel = Convert.ToString(txtModel.Text.Trim());
                if (Double.TryParse(txtPrice.Text.Trim(), out VEHVPrice) == false ||
                    Double.TryParse(txtRate.Text.Trim(), out VEHVRate) == false ||
                    Double.TryParse(txtTotal.Text.Trim(), out VEHVTotal) == false ||
                    Double.TryParse(txtInsurance.Text.Trim(), out VEHVInsurance) == false)
                {
                   
                    System.Windows.MessageBox.Show(
                        "'Only Numeric values Is Allowed.'", "Invalid value entered!", MessageBoxButton.OK);
                }
                else
                {
                    
                    //Generic Collecton.
                    MyVehicleExp.Add(VEHVPrice); MyVehicleExp.Add(VEHVTotal); MyVehicleExp.Add(VEHVRate); MyVehicleExp.Add(VEHVInsurance); MyVehicleExp.Add(VEHVMonthlypay);
                    tbCalculations();
                }
            }
            catch (ArgumentException) { lblErr.Content = "'No Values Were Recieved.'"; }
            catch (Exception) { lblErr.Content = "'Invalid Type In Model & Make.'"; }
        }
        public void tbClear()
        {
            txtModel.Clear();
            txtPrice.Clear();
            txtTotal.Clear();
            txtRate.Clear();
            txtInsurance.Clear();
        }
        public void getVSavedValues()
        {
            tbVehicleValues();
            try
            {
                
                System.Windows.MessageBox.Show(
                    "**********************************************************************" +
                    "\n\tAMOUNT AFTER CALCULATIONS" +
                    "\n *********************************************************************" +
                    "\nModel And Make: " + strModel +
                    "\nPurchase Price: R " + VEHVPrice +
                    "\nTotal Deposit: R " + VEHVTotal +
                    "\nInterest Rate(%): " + VEHVRate +
                    "\nInsurance: R " + VEHVInsurance +
                    "\nTotal Monthly Pay: R " + tbCalculations() +
                    "\n*********************************************************************", "' values are Successfully captured!'");
            }
            catch (ArgumentOutOfRangeException) { lblErr.Content = "'no values entered. Please Enter Values.'"; }
        }
        //returns Vehicle Monthly payments
        public Double tbCalculations() { return VEHVMonthlypay += VEHVPrice * (1 + (VEHVRate * 5)); }
    }
}

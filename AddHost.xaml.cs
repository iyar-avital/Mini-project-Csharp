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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddHost.xaml
    /// </summary>
    public partial class AddHost : Window
    {
        BE.Host host;
        IBL myBL = FactoryBl.GetBl();
        List<BankBranch> banks = new List<BankBranch>();
        List<BankBranch> branch = new List<BankBranch>();
        BankBranch bankHost = new BankBranch();
        public AddHost(/*BE.Host myHoster*/)  //התחרטנו, אין לנו צורך בזהץ אם הוא מחובר אין לו גישה לעמוד הזה ואם לא אז אין טעם:)
        {
            InitializeComponent();
            //  host = myHoster;
            host = new Host();
            banks = myBL.GroupByBankNumberOfBranch().OneObjectFromAllKey();
            grid5.DataContext = banks;
            grid3.DataContext = host;
            grid4.DataContext = host;
        }

        private void AddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HostIdMask.Tag.Equals("false") || PrivateNameMask.Tag.Equals("false") || FamilyNameMask.Tag.Equals("false") || PhoneNumberMask.Tag.Equals("false") || MailAddressMask.Tag.Equals("false") ||
                    LanguageMask.Tag.Equals("false") || BankAccountNumberUpDown.Tag.Equals("false") || branchNumberComboBox.Tag.Equals("false"))
                    MessageBox.Show("please check you filled all the details correctly", "failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    host.BankBranchDetails = new BankBranch();
                    host.BankBranchDetails.BankName = bankHost.BankName;
                    host.BankBranchDetails.BankNumber = bankHost.BankNumber;
                    host.BankBranchDetails.BranchNumber = bankHost.BranchNumber;
                    host.BankBranchDetails.BranchAddress = bankHost.BranchAddress;
                    host.BankBranchDetails.BranchCity = bankHost.BranchCity;

                    myBL.addHost(host);
                    MessageBoxResult result = MessageBox.Show("The Hoster " + host.PrivateName + " " + host.FamilyName + " was successfully added:)", "add", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                        Close();
                }
            }
            catch (HostingException h)
            {
                MessageBox.Show(h.Message, "faild", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void MaskedTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (HostIdMask.Text == "_________" || !BE.Validation.IsId(HostIdMask.Text) || HostIdMask.Text.Contains("_"))
            {
                HostIdMask.BorderBrush = Brushes.Red;
                idError.Visibility = Visibility.Visible;
                HostIdMask.Tag = "false";
            }
            else
            {
                HostIdMask.BorderBrush = Brushes.LightGreen;
                idError.Visibility = Visibility.Collapsed;
                HostIdMask.Tag = "true";
            }
        }

        private void privateNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PrivateNameMask.Text == "" || !BE.Validation.IsName(PrivateNameMask.Text))
            {
                PrivateNameMask.BorderBrush = Brushes.Red;
                privateNameError.Visibility = Visibility.Visible;
                PrivateNameMask.Tag = "false";
            }
            else
            {
                PrivateNameMask.BorderBrush = Brushes.LightGreen;
                privateNameError.Visibility = Visibility.Collapsed;
                PrivateNameMask.Tag = "true";
            }
        }

        private void familyNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FamilyNameMask.Text == "" || !BE.Validation.IsName(FamilyNameMask.Text))
            {
                FamilyNameMask.BorderBrush = Brushes.Red;
                familyNameError.Visibility = Visibility.Visible;
                FamilyNameMask.Tag = "false";
            }
            else
            {
                FamilyNameMask.BorderBrush = Brushes.LightGreen;
                familyNameError.Visibility = Visibility.Collapsed;
                FamilyNameMask.Tag = "true";
            }
        }

        private void phoneNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PhoneNumberMask.Text == "___-_______" || !BE.Validation.IsPhoneNumber(PhoneNumberMask.Text))
            {
                PhoneNumberMask.BorderBrush = Brushes.Red;
                phoneNumberError.Visibility = Visibility.Visible;
                PhoneNumberMask.Tag = "false";
            }
            else
            {
                PhoneNumberMask.BorderBrush = Brushes.LightGreen;
                phoneNumberError.Visibility = Visibility.Collapsed;
                PhoneNumberMask.Tag = "true";
            }
        }

        private void mailAdressTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MailAddressMask.Text == "" || !BE.Validation.IsEmail(MailAddressMask.Text))
            {
                MailAddressMask.BorderBrush = Brushes.Red;
                mailAdressError.Visibility = Visibility.Visible;
                MailAddressMask.Tag = "false";
            }
            else
            {
                MailAddressMask.BorderBrush = Brushes.LightGreen;
                mailAdressError.Visibility = Visibility.Collapsed;
                MailAddressMask.Tag = "true";
            }
        }

        private void languagesTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LanguageMask.Text == "" || !BE.Validation.IsName(LanguageMask.Text))
            {
                LanguageMask.BorderBrush = Brushes.Red;
                languageError.Visibility = Visibility.Visible;
                LanguageMask.Tag = "false";
            }
            else
            {
                LanguageMask.BorderBrush = Brushes.LightGreen;
                languageError.Visibility = Visibility.Collapsed;
                LanguageMask.Tag = "true";
            }
        }

        private void bankAccountNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BankAccountNumberUpDown.Text == "")
            {
                BankAccountNumberUpDown.BorderBrush = Brushes.Red;
                bankAccountNamberError.Visibility = Visibility.Visible;
                BankAccountNumberUpDown.Tag = "false";
            }
            else
            {
                BankAccountNumberUpDown.BorderBrush = Brushes.LightGreen;
                bankAccountNamberError.Visibility = Visibility.Collapsed;
                BankAccountNumberUpDown.Tag = "true";
            }
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bankNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BankBranch b = (BankBranch)bankNumberComboBox.SelectedItem;
            branch = myBL.GroupByBankNumberOfBranch().AllElementsForKey(b.BankNumber).OrderBy(item => item.BranchNumber).ToList();
            branchNumberComboBox.SelectedItem = null;
            branchNumberComboBox.Tag = "false";
            branchNumberComboBox.DataContext = branch;
            branchAddressLabel.DataContext = "";
            branchCityLabel.DataContext = "";
            branchNumberComboBox.IsEnabled = true;
        }

        private void branchNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bankHost = (BankBranch)branchNumberComboBox.SelectedItem;
            if(bankHost != null)
            {
                branchAddressLabel.DataContext = bankHost.BranchAddress;
                branchCityLabel.DataContext = bankHost.BranchCity;
                branchNumberComboBox.Tag = "true";
            } 
        }
    }
}

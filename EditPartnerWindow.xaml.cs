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
using MasterPol.Models;

namespace MasterPol
{

    public partial class EditPartnerWindow : Window
    {
        private readonly MasterPolMaluginContext _context;
        private readonly Partner _partner;
        public EditPartnerWindow(Partner partner)
        {
            InitializeComponent();
            _context = new MasterPolMaluginContext();
            _partner = _context.Partners.Find(partner.IdPartner);
            this.Closed += (s, e) => _context.Dispose();
            LoadPartnerData();
        }


        private void LoadPartnerData()
        {
            NamePartnerTextBox.Text = _partner.PartnerName;
            DirectorNameTextBox.Text = _partner.Director;
            PhoneNumberTextBox.Text = $"+7 {_partner.PhoneNumber}";
            EmailPartnerTextBox.Text = _partner.Email;
            AddressPartnerTextBox.Text = _partner.LegalAddress;
            RatingTextBox.Text = _partner.Rating.ToString();
            TinTextBox.Text = _partner.Tin.ToString();

            foreach (ComboBoxItem item in PartnerTypeComboBox.Items)
            {
                if (item.Content.ToString() == _partner.TypePartner)
                {
                    PartnerTypeComboBox.SelectedItem = item;
                    break;
                }
            }
            

        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(NamePartnerTextBox.Text) ||
                string.IsNullOrWhiteSpace(DirectorNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailPartnerTextBox.Text) ||
                string.IsNullOrWhiteSpace(AddressPartnerTextBox.Text) ||
                string.IsNullOrWhiteSpace(RatingTextBox.Text) ||
                string.IsNullOrWhiteSpace(TinTextBox.Text))
            {
                MessageBox.Show("Заполните все обязательные поля!", "Пустые поля", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (byte.Parse(RatingTextBox.Text) > 10)
            {
                MessageBox.Show("Рейтинг должен быть в промежутке от 0 до 10!", "Рейтинг", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
                

            return true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                if (!long.TryParse(TinTextBox.Text, out long tin))
                {
                    MessageBox.Show("Введите корректный ИНН", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _partner.PartnerName = NamePartnerTextBox.Text.Trim();
                _partner.TypePartner = (PartnerTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                _partner.Director = DirectorNameTextBox.Text.Trim();
                _partner.PhoneNumber = PhoneNumberTextBox.Text.Replace("+7", "").Trim();
                _partner.Email = EmailPartnerTextBox.Text.Trim();
                _partner.LegalAddress = AddressPartnerTextBox.Text.Trim();
                _partner.Rating = byte.TryParse(RatingTextBox.Text, out var rating) ? rating : (byte)0;
                _partner.Tin = tin;

                _context.SaveChanges();
                MessageBox.Show("Данные партнера успешно обновлены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DigitalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0) && e.Text != "+";
        }
    }
}

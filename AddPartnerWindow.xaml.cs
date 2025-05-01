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

    public partial class AddPartnerWindow : Window
    {
        private readonly MasterPolMaluginContext _context;
        public AddPartnerWindow()
        {
            InitializeComponent();
            _context = new MasterPolMaluginContext();
            this.Closed += (s, e) => _context.Dispose();
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
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

                var newPartner = new Partner
                {
                    PartnerName = NamePartnerTextBox.Text.Trim(),
                    TypePartner = (PartnerTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Director = DirectorNameTextBox.Text.Trim(),
                    PhoneNumber = PhoneNumberTextBox.Text.Replace("+7", "").Trim(),
                    Email = EmailPartnerTextBox.Text.Trim(),
                    LegalAddress = AddressPartnerTextBox.Text.Trim(),
                    Rating = byte.TryParse(RatingTextBox.Text, out var rating) ? rating : (byte)0,
                    Tin = tin
                };

                _context.Partners.Add(newPartner);
                _context.SaveChanges();

                MessageBox.Show("Партнер успешно добавлен!", "Добавление партнера", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Произошла ошибка при добавлении партнера: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

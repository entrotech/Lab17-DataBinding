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

using Talent.Domain;
using Talent.DataAccess.Ado;


namespace WpfClient
{
    /// <summary>
    /// Interaction logic for BruteForceWindow.xaml
    /// </summary>
    public partial class BruteForceWindow : Window
    {
        MpaaRatingRepository mpaaRatingRepository;
        MpaaRating mpaaRating;

        public BruteForceWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mpaaRatingRepository = new MpaaRatingRepository();
            mpaaRating = mpaaRatingRepository.Fetch().First();
            CopyValuesToControls();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CopyValuesToDomainObject())
            {
                // For now, we need to explicitly set the domain object dirty
                mpaaRating.IsDirty = true;
                mpaaRatingRepository.Persist(mpaaRating);
                DialogResult = true;
            }

        }

        #region Private Methods

        private void CopyValuesToControls()
        {
            MpaaRatingIdTextBlock.Text = mpaaRating == null
                ? "" : mpaaRating.Id.ToString();
            CodeTextBox.Text = mpaaRating == null ? "" : mpaaRating.Code;
            NameTextBox.Text = mpaaRating == null ? "" : mpaaRating.Name;
            DescriptionTextBox.Text = mpaaRating == null
                ? "" : mpaaRating.Description;
            IsInactiveCheckBox.IsChecked = mpaaRating == null
               ? false : mpaaRating.IsInactive;
            DisplayOrderTextBox.Text = mpaaRating == null
                ? "" : mpaaRating.DisplayOrder.ToString();
        }

        private bool CopyValuesToDomainObject()
        {
            bool success = true;
            mpaaRating.Code = CodeTextBox.Text;
            mpaaRating.Name = NameTextBox.Text;
            mpaaRating.Description = DescriptionTextBox.Text;
            mpaaRating.IsInactive = IsInactiveCheckBox.IsChecked ?? false;
            int displayOrder = 0;
            if (Int32.TryParse(DisplayOrderTextBox.Text, out displayOrder))
            {
                mpaaRating.DisplayOrder = displayOrder;
            }
            else
            {
                MessageBox.Show("Display Order must be an integer.", "Validation Error");
                success = false;
            }
            return success;
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class SimpleBindingWindow : Window
    {
        MpaaRatingRepository mpaaRatingRepository;
        MpaaRating mpaaRating;

        public SimpleBindingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mpaaRatingRepository = new MpaaRatingRepository();
            mpaaRating = mpaaRatingRepository.Fetch().First();
            this.DataContext = mpaaRating;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // For now, we need to explicitly set the domain object dirty
                mpaaRating.IsDirty = true;
                mpaaRatingRepository.Persist(mpaaRating);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Failed");
                return;
            }
        }

    }
}


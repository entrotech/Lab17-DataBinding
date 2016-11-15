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

using System.Collections.ObjectModel;
using Talent.Domain;
using Talent.DataAccess.Ado;

namespace WpfClient
{
    public partial class ComplexBindingWindow : Window
    {
        MpaaRatingRepository mpaaRatingRepository;
        ObservableCollection<MpaaRating> mpaaRatings;

        public ComplexBindingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mpaaRatingRepository = new MpaaRatingRepository();
            var ratings = mpaaRatingRepository
                .Fetch(null).OrderBy(d => d.DisplayOrder).ThenBy(d => d.Name);
            mpaaRatings = new ObservableCollection<MpaaRating>(ratings);
            this.DataContext = mpaaRatings;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MpaaRating selectedMpaaRating = MpaaRatingListBox.SelectedItem as MpaaRating;
                if (selectedMpaaRating != null)
                {
                    selectedMpaaRating.IsDirty = true;
                    MpaaRatingListBox.SelectedItem = mpaaRatingRepository
                        .Persist(selectedMpaaRating);
                    MessageBox.Show("Changes Saved", "Changes Saved", 
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Failed",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // To just cancel, we could simply not call Upsert, but we
            // want to update the data in the UI without losing which item
            // has focus in the list.
            MpaaRating selectedMpaaRating =
                MpaaRatingListBox.SelectedItem as MpaaRating;
            if (selectedMpaaRating != null)
            {
                // the Fetch method returns a new instance of the MpaaRating
                MpaaRating updatedMpaaRating = mpaaRatingRepository
                    .Fetch(selectedMpaaRating.Id).FirstOrDefault();

                // Replace the selectedMpaaRating in the mpaaRatings collection
                // with the refreshed instance.
                int index = mpaaRatings.IndexOf(selectedMpaaRating);
                mpaaRatings.Insert(index, updatedMpaaRating);
                mpaaRatings.Remove(selectedMpaaRating);
                MpaaRatingListBox.SelectedItem = updatedMpaaRating;
            }
            MessageBox.Show("By your request", "Changes Cancelled",
                    MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}


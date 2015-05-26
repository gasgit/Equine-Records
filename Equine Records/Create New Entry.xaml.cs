using Equine_Records.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.NumberFormatting;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Equine_Records
{



    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Create_New_Entry : Page
    {
        //public image file byte array
        public byte[] fileBytes = null;
        

        public Create_New_Entry()
        {
            this.InitializeComponent();
        }


       

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            hideAll();
            canvas1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtDate.Text = DateTime.Now.ToString();
            //tableCheck();           

        }

        #region Canvas   region

        private void setAllZIndexValues()
        {
            foreach (Canvas child in BaseCanvas.Children)
            {
                child.SetValue(Canvas.ZIndexProperty, 0);
            }
        }

        private void hideAll()
        {
            foreach (Canvas child in BaseCanvas.Children)
            {
                if (child is Canvas)
                {
                    child.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
        #region back/next input page/canvas

        // validation on next button first entry page/canvas
        // show/hide canvas 
        // repeated code next and back needed the long way to display errors for each message
         
        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                
                errBlock1.Text = "Please enter Rider Name";
            }

            else if (string.IsNullOrWhiteSpace(txtVenue.Text))
            {
                errBlock1.Text = "Please enter Venue";
            }
            else if (string.IsNullOrWhiteSpace(txtEvent.Text))
            {
                errBlock1.Text = "Please enter Event";
            }
           
            else
            {
                errBlock1.Text = "";

                hideAll();
                setAllZIndexValues();
                canvas2.Visibility = Windows.UI.Xaml.Visibility.Visible;
                canvas2.SetValue(Canvas.ZIndexProperty, 9);

                setAllZIndexValues();
                canvas2.SetValue(Canvas.ZIndexProperty, 9);
                backButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

        }

        private void btnBack1_Click(object sender, RoutedEventArgs e)
        {

            hideAll();
            setAllZIndexValues();
            canvas1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            canvas1.SetValue(Canvas.ZIndexProperty, 9);

            setAllZIndexValues();
            canvas1.SetValue(Canvas.ZIndexProperty, 9);
            backButton.Visibility = Windows.UI.Xaml.Visibility.Visible;


        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtHorseName.Text))
            {
                
                errBlock2.Text = "Please enter Horse Name";
            }
            else if(cmbHands.SelectedIndex == -1  )
            {
                errBlock2.Text = "Please select Horse Height Hands";
            }
            else if(cmbInches.SelectedIndex == -1)
            {
                errBlock2.Text = "Please select Horse Height Inches";
            }
            else if(cmbAge.SelectedIndex == -1)
            {
                errBlock2.Text = "Please select Horse Age";
            }
            else if(cmbHorseBreed.SelectedIndex == -1)
            {
                errBlock2.Text = "Please select Horse Breed";
            }
            else if (cmbHorseSex.SelectedIndex == -1)
            {
                errBlock2.Text = "Please select Horse Sex";
            }
            else
            {
                errBlock2.Text = "";

                hideAll();
                setAllZIndexValues();
                canvas3.Visibility = Windows.UI.Xaml.Visibility.Visible;
                canvas3.SetValue(Canvas.ZIndexProperty, 9);

                setAllZIndexValues();
                canvas3.SetValue(Canvas.ZIndexProperty, 9);
            }
        }

        private void btnBack3_Click(object sender, RoutedEventArgs e)
        {

            hideAll();
            setAllZIndexValues();
            canvas2.Visibility = Windows.UI.Xaml.Visibility.Visible;
            canvas2.SetValue(Canvas.ZIndexProperty, 9);

            setAllZIndexValues();
            canvas2.SetValue(Canvas.ZIndexProperty, 9);
        }

        private void btnNext3_Click(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrWhiteSpace(txtEntryCost.Text))
            {
                errBlock3.Text = "Please enter Entry Cost";
            }
            else if (string.IsNullOrWhiteSpace(txtRound.Text))
            {
                errBlock3.Text = "Please enter Round";
            }
            else if (string.IsNullOrWhiteSpace(txtFinishTime.Text))
            {
                errBlock3.Text = "Please enter Finish Time";
            }
            else if (cmbPosition.SelectedIndex == -1)
            {
                errBlock3.Text = "Please select Position";
            }
            else
            {
            errBlock3.Text = "";

            hideAll();
            setAllZIndexValues();
            canvas4.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnSave.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            canvas4.SetValue(Canvas.ZIndexProperty, 9);

            setAllZIndexValues();
            canvas4.SetValue(Canvas.ZIndexProperty, 9);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            hideAll();
            setAllZIndexValues();
            canvas3.Visibility = Windows.UI.Xaml.Visibility.Visible;
            canvas3.SetValue(Canvas.ZIndexProperty, 9);

            setAllZIndexValues();
            canvas3.SetValue(Canvas.ZIndexProperty, 9);

        }

        
        // validation on next before save button appears
        private void btnNextSave_Click(object sender, RoutedEventArgs e)
        {
            

            if (string.IsNullOrWhiteSpace(txtFenceHeight.Text))
            {
                errBlock4.Text = "Please enter Fence Height";       
            }
            else if (cmbClears.SelectedIndex == -1)
            {
                errBlock4.Text = "Please select Clears";
            }
            else if (string.IsNullOrWhiteSpace(txtFaults.Text))
            {
                errBlock4.Text = "Please enter Faults";
            }
            else if (string.IsNullOrWhiteSpace(txtPoints.Text))
            {
                errBlock4.Text = "Please enter Points";
            }
            else if (string.IsNullOrWhiteSpace(txtComments.Text))
            {
                errBlock4.Text = "Please enter Comments";
            }
            else if (imgCreate.Source == null)
            {
                errBlock4.Text = "Please Choose Image";
            }

            else
            {
               
                btnSave.Visibility = Windows.UI.Xaml.Visibility.Visible;

            }
        }

        #endregion  back/next input page/canvas

        // nav back to mainPage
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));

        }



        #endregion canvas

        #region Database

      

        // connection to db
        SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");


        // check if db exists
        public async Task<bool> DoesDbExist(string DatabaseName)
        {
            bool dbexist = true;
            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(DatabaseName);

            }
            catch
            {
                dbexist = false;
            }

            return dbexist;
        }




       


        // save entry to the db
        private async void addEntry()
        {
            // connection string to db
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");

            // new object to insert to db
            var Entry = new Entry()
            {


                RiderName = txtName.Text,
                Venue = txtVenue.Text,
                Event = txtEvent.Text,
                Date = txtDate.Text.ToString(),

                Horse = txtHorseName.Text,
                HorseHeight = ((ComboBoxItem)cmbHands.SelectedItem).Content.ToString() + "\t"
                                + ((ComboBoxItem)cmbInches.SelectedItem).Content.ToString(),

                HorseAge = ((ComboBoxItem)cmbAge.SelectedItem).Content.ToString(),

                HorseSex = ((ComboBoxItem)cmbHorseSex.SelectedItem).Content.ToString(),

                HorseBreed = ((ComboBoxItem)cmbHorseBreed.SelectedItem).Content.ToString(),

                Position = ((ComboBoxItem)cmbPosition.SelectedItem).Content.ToString(),

                EntryCost =  Convert.ToDouble(txtEntryCost.Text),

                Round = Convert.ToInt32(txtRound.Text),

                FinishTime = txtFinishTime.Text,

                FenceHeight = Convert.ToDouble(txtFenceHeight.Text),

                Clears = ((ComboBoxItem)cmbClears.SelectedItem).Content.ToString(),

                Faults = Convert.ToInt32(txtFaults.Text),

                Points = Convert.ToInt32(txtPoints.Text),

                Comments = txtComments.Text,

                Image = fileBytes,

            };
            await connection.InsertAsync(Entry);



        }

       

        // save new record button navs back to record list 
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

                addEntry();
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(Records));
                }
            
            
        }

        // temp button to create table for testing
        private void btnTemp_Click(object sender, RoutedEventArgs e)
        {
           // CreateTable();
        }

        



        #endregion Database
        // image picker
        private async void chooseImage_Click(object sender, RoutedEventArgs e)
        {

            
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            // Open the file picker.
            Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

            // file is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                Windows.Storage.Streams.IRandomAccessStream fileStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                // Set the image source to the selected bitmap.
                Windows.UI.Xaml.Media.Imaging.BitmapImage myBitmapImage =
                    new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                myBitmapImage.SetSource(fileStream);
                imgCreate.Source = myBitmapImage;
                this.DataContext = file;

                if (file != null)
                {
                    using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
                    {
                        fileBytes = new byte[stream.Size];
                        using (DataReader reader = new DataReader(stream))
                        {
                            await reader.LoadAsync((uint)stream.Size);
                            reader.ReadBytes(fileBytes);
                           
                        }
                    }
                }
                
            }
        }

        // was a temp button to delete table for testing

        //private async void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");
        //    await connection.DropTableAsync<Entry>();
        //}

        #region inputTypes

        // allow numeric only
        private void txtPoints_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            char c = Convert.ToChar(e.Key);
            e.Handled = !Char.IsDigit(c);
          

        }

       
        // allow numeric and period, plus etc 
        private void txtEntryCost_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
                e.Handled = true;
          

        }
        // allow numeric only

        private void txtRound_KeyDown(object sender, KeyRoutedEventArgs e)
        {

            char c = Convert.ToChar(e.Key);
            e.Handled = !Char.IsDigit(c);
          
           
        }
        // allow numeric and period, plus etc 

        private void txtFinishTime_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
                e.Handled = true;
        }
        // allow numeric only

        private void txtFaults_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            char c = Convert.ToChar(e.Key);
            e.Handled = !Char.IsDigit(c);
          
        }
        // allow numeric only

        private void txtFenceHeight_KeyDown_1(object sender, KeyRoutedEventArgs e)
        {
            char c = Convert.ToChar(e.Key);
            e.Handled = !Char.IsDigit(c);
        }


        #endregion inputTypes



    }
}

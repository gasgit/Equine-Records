using Equine_Records.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Equine_Records
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Records : Page
    {


        #region public declarations


        DataTransferManager myDataManager = DataTransferManager.GetForCurrentView();

        public int rid;
        App myApp;

        public BitmapImage image = new BitmapImage();

        public Entry objectForImage = new Entry();




        #endregion end declarations

        #region navigation

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();


        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }


        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public Records()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
           // retrieveData();
           // displaySelected();
            myApp = Application.Current as App;


            this.Loaded += RecordsList_Loaded;
            this.Unloaded += RecordsList_Unloaded;
        }









        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }


        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (myApp._myEntry == null)
            {
                myApp._myEntry = new List<Entry>();
            }


            navigationHelper.OnNavigatedTo(e);

            retrieveData();


            myDataManager.DataRequested += myDataManager_DataRequested;


            // object passed from search result grid item selected
            Entry res = e.Parameter as Entry;


            // if res object is present display
            if (res != null)
            {

                txtRiderOut.Text = res.RiderName;
                txtVenueOut.Text = res.Venue;
                txtEventOut.Text = res.Event;
                txtDateOut.Text = res.Date;
                txtHorseOut.Text = res.Horse;
                txtHorseHeightOut.Text = res.HorseHeight;
                txtHorseAgeOut.Text = res.HorseAge;
                txtHorseBreedOut.Text = res.HorseBreed;
                txtHorseSexOut.Text = res.HorseSex;

                txtEntryCostOut.Text = res.EntryCost.ToString();
                txtRoundOut.Text = res.Round.ToString();
                txtFinishTime.Text = res.FinishTime;
                txtPositionOut.Text = res.Position;


                txtFenceHeightOut.Text = res.FenceHeight.ToString();
                txtClearsOut.Text = res.Clears.ToString();
                txtFaultsOut.Text = res.Faults.ToString();
                txtPointsout.Text = res.Points.ToString();
                txtCommentsOut.Text = res.Comments;

                using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
                {

                    using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                    {
                        writer.WriteBytes((byte[])res.Image);
                        writer.StoreAsync().GetResults();

                    }



                    image.SetSource(ms);
                }
                myImage.Source = image;

                    
                
            }


        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            myDataManager.DataRequested -= myDataManager_DataRequested;

        }

        void  myDataManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {

            DataPackage myData = args.Request.Data;
            myData.Properties.Title = "Jumping Results";
            myData.Properties.Description = "";

           

            String myTextToShare = (("Date: " + txtDateOut.Text) + ("\nName: " + txtRiderOut.Text) + ("\nVenue: " + txtVenueOut.Text) +
                                    ("\nPosition: " + txtPositionOut.Text) + ("\nEvent: " + txtEventOut.Text) + ("\nDate: " + txtDateOut.Text) + ("\nHorse: " + txtHorseOut.Text)
                                     + ("\nHorse Height: " + txtHorseHeightOut.Text) + ("\nHorse Age: " + txtHorseAgeOut.Text) + ("\nHorse Breed: " + txtHorseBreedOut.Text)
                                     + ("\nHorse Sex: " + txtHorseSexOut.Text) + ("\nEntry Cost: " + txtEntryCostOut.Text) + ("\nRound: " + txtRoundOut.Text)
                                     + ("\nFinish Time: " + txtFinishTime.Text) + ("\nPosition: " + txtPositionOut.Text)
                                     + ("\nFence Height: " + txtFenceHeightOut.Text) + ("\nClears: " + txtClearsOut.Text) + ("\nFaults: " + txtFaultsOut.Text)
                                     + ("\nPoints: " + txtPointsout.Text) + ("\nComments: " + txtCommentsOut.Text));


           
            myData.SetText(myTextToShare);
            myData.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(myTextToShare.Replace("\n", "<BR/>")));
            
        }




        #endregion

        #endregion navigation


        #region Visual State


        private void RecordsList_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            determineVisualState();
        }

        private void RecordsList_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Current_SizeChanged;
        }

        private void determineVisualState()
        {
            var state = string.Empty;
            var applicationView = ApplicationView.GetForCurrentView();
            var size = Window.Current.Bounds;

            if (applicationView.IsFullScreen)
            {
                if (applicationView.Orientation == ApplicationViewOrientation.Landscape)
                    state = "FullScreenLandscape";
                else
                    state = "FullScreenPortrait";
            }
            else
            {
                if (size.Width == 320)
                    state = "Snapped";
                else if (size.Width <= 500)
                    state = "Narrow";
                else
                    state = "Filled";
            }

            VisualStateManager.GoToState(this, state, true);
        }

        #endregion


        #region Records



        //public async void updateFromDb()
        //{
        //    // connect to db
        //    SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");
           
        //    // load all entries to var collection
        //    var result = await connection.QueryAsync<Entry>("Select * FROM Entries ");

        //    // itterate over result collection
        //    foreach (var item in result)
        //    {

        //        // add each item to myEntry list
        //        myApp._myEntry.Add(item);

        //    }
        //}


        // displays selected form myListView
        public async void displaySelected()
        {
            // connect to db
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");
            // used to select record at position on list
            int found = myListView.SelectedIndex;
            // load all entries to var collection
            var result = await connection.QueryAsync<Entry>("Select * FROM Entries ");

            // itterate over result collection
            foreach (var item in result)
            {
       
                // add each item to myEntry list
                myApp._myEntry.Add(item);

            }

            // itterate over myEntry list/ find index in list equal to selectedIndex from listBox
            foreach (var item in myApp._myEntry)
            {
                if (myApp._myEntry.IndexOf(item) == found)
                {
                    // display attributes of object to display fields
                    txtRiderOut.Text = item.RiderName;
                    txtVenueOut.Text = item.Venue;
                    txtEventOut.Text = item.Event;
                    txtDateOut.Text = item.Date;

                    txtHorseOut.Text = item.Horse;
                    txtHorseHeightOut.Text = item.HorseHeight;
                    txtHorseAgeOut.Text = item.HorseAge;
                    txtHorseBreedOut.Text = item.HorseBreed;
                    txtHorseSexOut.Text = item.HorseSex;

                    txtEntryCostOut.Text = item.EntryCost.ToString();
                    txtRoundOut.Text = item.Round.ToString();
                    txtFinishTime.Text = item.FinishTime;
                    txtPositionOut.Text = item.Position;


                    txtFenceHeightOut.Text = item.FenceHeight.ToString();
                    txtClearsOut.Text = item.Clears.ToString();
                    txtFaultsOut.Text = item.Faults.ToString();
                    txtPointsout.Text = item.Points.ToString();
                    txtCommentsOut.Text = item.Comments;

                    using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
                    {

                        using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                        {
                            writer.WriteBytes((byte[])item.Image);
                            writer.StoreAsync().GetResults();
                        }



                        image.SetSource(ms);
                    }
                    myImage.Source = image;
    
                }

            }

        }


        // method to fill list view
        private async void retrieveData()
        {
            // clears the listbox items
            myListView.Items.Clear();
            // open the connection to db
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");

            // sql with parameters
            // var result = await connection.QueryAsync<Student>("Select Name FROM Students WHERE EnrolledCourse = ?", new object[] { "CSE 4203" });
            var result = await connection.QueryAsync<Entry>("Select * FROM Entries ");
            //var result = await connection.QueryAsync<Entry>("Select Id FROM Entries Where Id = 4");

            // itterate over the result collection and add to the listbox
            foreach (var item in result)
            {


                myListView.Items.Add("ID: " + item.Id
                         + "\nName: " + item.RiderName
                         + "\nVenue: " + item.Venue
                         + "\nEvent: " + item.Event
                         + "\nDate: " + item.Date);

            }

        }


        // display tapped selection from list call
        private void myLs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            displaySelected();
        }




        // delete entry selected from the list
        private async void delete()
        {

            // open connection to the database
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");
            // store place from listbox selected index
            int s = myListView.SelectedIndex;

            // iterate over the myEntry list (declared globally)
            foreach (var item in myApp._myEntry)
            {      // match up mtEntry position with selectedIndex in listbox
                if (myApp._myEntry.IndexOf(item) == s)
                {

                    rid = item.Id;

                }

            }

            var Entry = await connection.Table<Entry>().Where(x => x.Id.Equals(rid)).FirstOrDefaultAsync();

            // check first for Entry ......... must finish with some sort of ex
            if (Entry != null)
            {
                await connection.DeleteAsync(Entry);

            }
            myListView.Items.Clear();
            myApp._myEntry.RemoveAt(s);

            txtRiderOut.Text = "";
            txtVenueOut.Text = "";
            txtEventOut.Text = "";
            txtDateOut.Text = "";

            txtHorseOut.Text = "";
            txtHorseHeightOut.Text = "";
            txtHorseAgeOut.Text = "";
            txtHorseBreedOut.Text = "";
            txtHorseSexOut.Text = "";

            txtEntryCostOut.Text = "";
            txtRoundOut.Text = "";
            txtFinishTime.Text = "";
            txtPositionOut.Text = "";


            txtFenceHeightOut.Text = "";
            txtClearsOut.Text = "";
            txtFaultsOut.Text = "";
            txtPointsout.Text = "";
            txtCommentsOut.Text = "";
            myImage.Source = null;
            retrieveData();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        // flyout to advise user of deletion
        private void DeleteConfirmation_Click(object sender, RoutedEventArgs e)
        {


            delete();
            retrieveData();
            btnDelete.Flyout.Hide();



        }

     

        #endregion Records

        private void myRecordsSearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            this.Frame.Navigate(typeof(SearchResultsPage), args.QueryText);


        }

       

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {

            
            
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);  

        }

        //private void backButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(MainPage));

        //}


    }
}

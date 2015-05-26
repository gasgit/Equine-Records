using Equine_Records.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Equine_Records{

    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class NewsLinks : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public NewsLinks()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected  async override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            await InitialiseListOfLinks();
            loadListView();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        // read json file and parse to templist
        private async Task InitialiseListOfLinks()
        {
            var file = await Package.Current.InstalledLocation.GetFileAsync("Data\\JsonFile.txt");
            var result = await FileIO.ReadTextAsync(file);
            try
            {
                // parse the json file text to a json array
                var tempList = JsonArray.Parse(result);
                convertArrayToList(tempList);

              

            }
            catch (Exception e)
            {
                string trouble = e.Message;
            }


        }


        // create newList
        List<Links> newList = new List<Links>();


        


        // method to convert JsonArray templist to newList
        private void convertArrayToList(JsonArray tempList)
        {  
            // iterate templist
            foreach (var item in tempList)
            {
                // create collection obj
                var obj = item.GetObject();
                // create links object from links cls
                Links links = new Links();
                // get key values 
                foreach (var key in obj.Keys)
                {
                    IJsonValue value;
                    if (!obj.TryGetValue(key, out value))
                        continue;

                    // add values to links object
                    if(key.Equals("strLink"))
                    {
                        links.strLink = value.GetString();
                    }
                    if(key.Equals("strName"))
                    {
                        links.strName = value.GetString();
                    }
          
                   

                }   // add links objects to newlist
                    newList.Add(links);

            }

        }
        // load listview
        private void loadListView()
        {   // iterate newList and add to listview
            foreach (var item in newList)
            {
                listView.Items.Add(item.strName);

            }


        }

        // select from list to open link in webview
        public void displaySelected()
        {
            // get index of selected from listview
            int found = listView.SelectedIndex;
            // iterate newList
            foreach (var item in newList)
            {   
                // if found get link and display webview
                if (newList.IndexOf(item) == found)
                {
                    String link = item.strLink;


                    Uri targetUri = new Uri(@link);

                    webView.Navigate(targetUri);


                }
            }

        }

        private void listView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            displaySelected();
        }

       

       
        // my navigation previous button for webview
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack && webView.CanGoBack)
            {
                webView.GoBack();
            }

        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    Frame rootFrame = Window.Current.Content as Frame;
        //    if (rootFrame != null && rootFrame.CanGoForward && webView.CanGoForward)
        //    {
        //        webView.GoForward();
        //    }


        //}

        

       


        
    }
}

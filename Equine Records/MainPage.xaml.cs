using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Equine_Records
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            tableCheck();

        }

        // check if table exists, having only 1 table, count and if result 0 call create table
        public async void tableCheck()
        {
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");



            var result = await connection.QueryAsync<Entry>("SELECT name FROM sqlite_master WHERE type='table' AND name='table_name'");
            if (result.Count == 0)
            {

                CreateTable();
            }

        }
        // create table Entries from Entry.cs
        public async void CreateTable()
        {
            SQLiteAsyncConnection connection = new SQLiteAsyncConnection("Entry.db");
            await connection.CreateTableAsync<Entry>();




        }
        // nav button to create new entry 
        private void btnNewEntry_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Create_New_Entry));
            }
        }
         //nav button to records, button is named but not for event??
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Records));
            }
        }
        // nav button to  
        private void btnLinks_Click(object sender, RoutedEventArgs e)
        {

            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(NewsLinks));
            }

        }

    }
}

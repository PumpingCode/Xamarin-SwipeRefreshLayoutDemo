using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Support.V7.App;

namespace SwipeRefreshLayoutDemo
{
    [Activity(Label = "Swipe Refresh Layout Demo", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity
    {
        SwipeRefreshLayout swipeContainer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar));

            // Create list with demo content
            var list = new List<string> { "Lorem ipsum dolor", "Sit amet, consetetur", "Sadipscing elitr", 
                "Lorem ipsum dolor", "Sit amet, consetetur", "Sadipscing elitr", "Lorem ipsum dolor", "Sit amet, consetetur", "Sadipscing elitr",
                "Lorem ipsum dolor", "Sit amet, consetetur", "Sadipscing elitr", "Lorem ipsum dolor", "Sit amet, consetetur", "Sadipscing elitr"
            };
                    
            FindViewById<ListView>(Resource.Id.listView).Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, list);

            // Set colors for swipe container and attach a refresh listener
            swipeContainer = FindViewById<SwipeRefreshLayout>(Resource.Id.slSwipeContainer);
            swipeContainer.SetColorSchemeResources(Android.Resource.Color.HoloBlueLight, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloOrangeLight, Android.Resource.Color.HoloRedLight);
            swipeContainer.Refresh += SwipeContainer_Refresh;

            // Manually show refreshing indicator on button press
            FindViewById<Button>(Resource.Id.button).Click += async delegate(object sender, EventArgs e)
            {
                swipeContainer.Refreshing = true;
                await Task.Delay(5000);
                swipeContainer.Refreshing = false;
            };
        }

        // React on swipe to refresh
        async void SwipeContainer_Refresh (object sender, EventArgs e)
        {
            await Task.Delay(5000);
            (sender as SwipeRefreshLayout).Refreshing = false;
        }  
    }
}
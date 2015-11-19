# Xamarin Swipe Refresh Layout Demo
Most apps that deal with online data have implemented a refresh mechanism. If you want to enable your users to trigger this on their own, providing a “swipe to refresh” implementation is a common and intuitive design pattern. Users can pull down from the top of a page to trigger the refresh. This has the best effect when it is added to a scrollable object like `ListView` or `ScrollView`.

![](/images/logo.png)

## Backwards compatibility
First make sure that our app is backward compatible. To do so, add the Android Support Library v4 and v7 to your project and make sure that your activity inherits from `AppCompatActivity`. Additionally you need to use a style that inherits from a `Theme.AppCompat` version like the `Theme.AppCompat.Light.DarkActionBar` theme.

## Prepare your layout
You do not need much to make your app ready for a swipe to refresh implementation. Just surround your layout with a `SwipeRefreshLayout` widget that you can find inside the Android Support Library v4. If you use a toolbar make sure to exclude it from the `SwipeRefreshLayout` to achieve a better optical effect.
```xml
<android.support.v4.widget.SwipeRefreshLayout
    android:id="@+id/swipeContainer"
    android:layout_width="match_parent"
    android:layout_height="match_parent">

</android.support.v4.widget.SwipeRefreshLayout>
```
After inflating the layout in your code, you have the chance to adjust some things like the colors of the loading indicator. By default it is simply black but you can set upto four colors to cycle through.
```c#
var swipeContainer = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeContainer);
swipeContainer.SetColorSchemeResources(Android.Resource.Color.HoloBlueLight, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloOrangeLight, Android.Resource.Color.HoloRedLight);
swipeContainer.Refresh += SwipeContainer_Refresh;
```
You should also add an event listener here to be able to react on the refresh command. As soon as a user pulles the indicator down, the layout fires the `Refresh` event that you can subscribe to.

## Implement your refresh logic
What you want to do when the Refresh event is fired is up to you. Make sure to hide the loading indicator when you are done by setting the `Refreshing` property of the layout to `false`.
```c#
async void SwipeContainer_Refresh (object sender, EventArgs e)
{ 
    // Insert your update logic here
    (sender as SwipeRefreshLayout).Refreshing = false;
}
```
If you want to use the layout to show the user that your application is currently loading, you also have the opportunity to show the loading indicator without having the user pull it down by setting the `Refreshing` property manually to `true`.
```c#
// Show the loading indicator
FindViewById<SwipeRefreshLayout>(Resource.Id.swipeContainer).Refreshing = true;
```
Hint: Please consider that this can only be set when the `SwipeRefreshLayout` has been completely loaded. If you want to show the indicator when your app starts, you need to wait until the layout is loaded and activate the refreshing afterwards to make it work. Use the `Post()` method to delay the command until the control has been loaded.
```c#
// Showing the loading indicator from the beginning has to be delayed until 
// the swipeContainer has been completely loaded to take effect
swipeContainer.Post(() => { swipeContainer.Refreshing = true; });
```

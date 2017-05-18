using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Xamarin.Forms;

namespace SpareTime
{
	public partial class NewItemPage : ContentPage
	{
		public Item Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();

			Item = new Item
			{
				Text = "Item name",
				Description = "This is a nice description"
			};

			BindingContext = this;

            buyBtn.Clicked += async(s,e) => { await IAP(); };
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "AddItem", Item);
			await Navigation.PopToRootAsync();
		}

		public async Task IAP()
		{
			try
			{
                string consume5cedits = "com.zcop.sparetime.testing.consume5credits";
                string consume10cedits = "com.zcop.sparetime.testing.consume10credits";
                string consume15cedits = "com.zcop.sparetime.testing.consume15credits";

                string[] productIds = new string[] { consume5cedits, consume10cedits, consume15cedits };
				var connected = await CrossInAppBilling.Current.ConnectAsync();

				if (!connected)
				{
					//Couldn't connect to billing, could be offline, alert user
					return;
				}

				//try to purchase item
				var products = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.InAppPurchase, productIds);
				if (products == null)
				{
					//error
				}
				else
				{
                    //details about item
                    foreach(var product in products)
                    {
						Item = new Item
						{
                            Text = product.Name,
                            Description = product.Description
						};
                        MessagingCenter.Send(this, "AddItem", Item);
                    }
					
				}
			}
			catch (Exception ex)
			{
				//Something bad has occurred, alert user
			}
			finally
			{
				//Disconnect, it is okay if we never connected
				await CrossInAppBilling.Current.DisconnectAsync();
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MasterDetailApp.Data
{

    public class LessonPage
    {
        public string name { get; set; }
    }

    // object we are going to parse?
    public class LessonObject 
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<LessonPage> pages { get; set; }
    }

    public class ItemsDataSource
    {
        
        async public void fetchPages()
        {
            using (Windows.Web.Http.HttpClient wc = new Windows.Web.Http.HttpClient())
            {
                var headers = wc.DefaultRequestHeaders;
                Uri requestUri = new Uri("https://sessions.io/api/3EFGTLHza");

                //Send the GET request asynchronously and retrieve the response as a string.
                Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
                string httpResponseBody = "";

                try
                {
                    //Send the GET request
                    httpResponse = await wc.GetAsync(requestUri);
                    httpResponse.EnsureSuccessStatusCode();
                    httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                    //parse JSON from the server
                    var result = JsonConvert.DeserializeObject<LessonObject>(httpResponseBody);
                    Debug.WriteLine(httpResponseBody);

                    // then what can we do here?


                }
                catch (Exception ex)
                {
                    httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                }

            }

        }

        public ItemsDataSource()
        {

            fetchPages();

        }

        private static List<Item> _items = new List<Item>()
        {
            new Item()
            {
                Id = 0,
                DateCreated = DateTime.Now,
                Title = "Item 1",
                Text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer id facilisis lectus. Cras nec convallis ante, quis pulvinar tellus. Integer dictum accumsan pulvinar. Pellentesque eget enim sodales sapien vestibulum consequat.

Maecenas eu sapien ac urna aliquam dictum.

Nullam eget mattis metus. Donec pharetra, tellus in mattis tincidunt, magna ipsum gravida nibh, vitae lobortis ante odio vel quam."
            },
            new Item()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                Title = "Item 2",
                Text = @"Quisque accumsan pretium ligula in faucibus. Mauris sollicitudin augue vitae lorem cursus condimentum quis ac mauris. Pellentesque quis turpis non nunc pretium sagittis. Nulla facilisi. Maecenas eu lectus ante. Proin eleifend vel lectus non tincidunt. Fusce condimentum luctus nisi, in elementum ante tincidunt nec.

Aenean in nisl at elit venenatis blandit ut vitae lectus. Praesent in sollicitudin nunc. Pellentesque justo augue, pretium at sem lacinia, scelerisque semper erat. Ut cursus tortor at metus lacinia dapibus."
            },
            new Item()
            {
                Id = 2,
                DateCreated = DateTime.Now,
                Title = "Item 3",
                Text = @"Ut consequat magna luctus justo egestas vehicula. Integer pharetra risus libero, et posuere justo mattis et.

Proin malesuada, libero vitae aliquam venenatis, diam est faucibus felis, vitae efficitur erat nunc non mauris. Suspendisse at sodales erat.
Aenean vulputate, turpis non tincidunt ornare, metus est sagittis erat, id lobortis orci odio eget quam. Suspendisse ex purus, lobortis quis suscipit a, volutpat vitae turpis."
            },
            new Item()
            {
                Id = 3,
                DateCreated = DateTime.Now,
                Title = "Item 4",
                Text = @"Duis facilisis, quam ut laoreet commodo, elit ex aliquet massa, non varius tellus lectus et nunc. Donec vitae risus ut ante pretium semper. Phasellus consectetur volutpat orci, eu dapibus turpis. Fusce varius sapien eu mattis pharetra.

Nam vulputate eu erat ornare blandit. Proin eget lacinia erat. Praesent nisl lectus, pretium eget leo et, dapibus dapibus velit. Integer at bibendum mi, et fringilla sem."
            }
        };

        public static IList<Item> GetAllItems()
        {
            return _items;
        }

        public static Item GetItemById(int id)
        {
            return _items[id];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using RestSharp;

namespace Wisielec.API
{
    public class APICommunicator
    {
        private WordAPI word;
        private int fetchWordFromAPIProgress=0;
        private Vector2 windowSize;

        public APICommunicator(Game1 game)
        {
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
        }

        public WordAPI GetWord()
        {
            var client = new RestClient("https://wordsapiv1.p.rapidapi.com/words/?random=true");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "wordsapiv1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "456382e8d4msh04e537e61f0f24fp1a2670jsn8bf005196cc2");
            while (true)
            {
                //informowanie użytkownika o progresie pobierania słowa z api
                if(fetchWordFromAPIProgress<=90)
                    fetchWordFromAPIProgress += 15;
                IRestResponse response = client.Execute(request);
                this.word = Newtonsoft.Json.JsonConvert.DeserializeObject<WordAPI>(response.Content);
                if (word.Results != null)
                {
                    if (word.Results[0].Definition != null)
                    {
                        if (word.Results[0].Definition.Length < (int)(2 * windowSize.X / 27))
                        {
                            break;
                        }
                    }
                }
            }
            fetchWordFromAPIProgress = 100;
            return word;
        }

        #region
        public int GetFetchProgress()
        {
            return fetchWordFromAPIProgress;
        }
        #endregion
    }
}
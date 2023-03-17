using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Text_To_Speech.Assets
{
    internal class sampleCode
    {
        static async Task Main(string[] args)
        {
            string subscriptionKey = "<your-subscription-key>";
            string endpoint = "<your-endpoint>";

            string ssml = "<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>" +
                "<voice name='en-US-JessaRUS'>" +
                "<prosody rate='x-fast'>Hello world</prosody>" +
                "</voice>" +
                "</speak>";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                client.DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", "audio-16khz-32kbitrate-mono-mp3");

                using (var content = new StringContent(ssml, Encoding.UTF8, "application/ssml+xml"))
                {
                    var response = await client.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Process the audio stream
                        // ...
                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.ReasonPhrase);
                    }
                }
            }
        }

    }
}



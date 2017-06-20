using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HttpClientSample
{
    /****************************************************
     * This class allows the XML POST request to be     *
     * serialized into utf-8 encoding. The default      *
     * StringWriter class will always return utf-16.    *
     ****************************************************/
    public sealed class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return encoding;
            }
        }
    }
    class Program
    {
        //Create HTTP cleint
        static HttpClient client = new HttpClient();
        //Second client for IATA API call
        static HttpClient client2 = new HttpClient();

        static void Main()
        {
            RunAsync().Wait();
        }

        /****************************************************************************************************************
         * Function to implement POST request                                                                           *
         * AUTHOR: Jared Nesbit                                                                                         *
         * PARAMETERS:                                                                                                  *
         *          StringContent     xmlToSend: to hold the XML to send to the server.                                 *
         *          String            format: to specify the format of the output (JSON or XML)                         *
         * RETURNS:                                                                                                     *
         *          String            response: the server's repsonse to the POST request (hopefully content)           *
         ****************************************************************************************************************/
        static async Task<String> BasicPost(StringContent xmlToSend, String format)
        {
            //Set up http client
            client.BaseAddress = new Uri("https://apigateway.pp.tvlport.com:443/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Add("Authorization", "Basic VW5pdmVyc2FsIEFQSS9VQVBJNjI5MzU1NTcyMDpaIXJjMG5mODg4ZDRiYS1mNzlhLTQ2MmMtOGMzYi0xY2Y3MmQ=");
            client.DefaultRequestHeaders.Add("Accept-Version", "3");
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.Add("Accept", $"application/{format}");
            
            //xmlToSend.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            
            //Make POST request
            String response = await client.PostAsync("connect/dev/AirSearch/offers/", xmlToSend).Result.Content.ReadAsStringAsync();            

            return response; ;
        }

        /**************************
         *Deserialize XML string  *
         **************************/
        static Offers parseXml(String xmlToParse)
        {
            Offers result;
            var serializer = new XmlSerializer(typeof(Offers));
            using (var reader = new StringReader(xmlToParse))
            {
                result = (Offers)serializer.Deserialize(reader);
            }

            return result;
        }
        /**************************
         *Deserialize JSON string *
         **************************/
        static Wrapper parseJson(String jsonToParse)
        {
            Wrapper o = JsonConvert.DeserializeObject<Wrapper>(jsonToParse);
            return o;
        }
        //Fill the POST request object in order to recieve the desired response from the API
        static void populate(ref OffersSearchQuery request)
        {
            request.xmlns = "http://www.travelport.com/schema/catalog/air/v4";
            request.maxNumberOfOffersToReturn = 50;
            request.PseudoCityInfo = "UP9";
            request.ReturnBrandedFares = false;

            request.SearchModifiersAir.Add(new SearchModifiersAir());
            request.SearchModifiersAir[0].prohibitChangeOfAirportInd = true;

            request.PassengerCriteria.Add(new PassengerCriteria());
            request.PassengerCriteria[0].number = 2;
            request.PassengerCriteria[0].text = "ADT";

            request.PassengerCriteria.Add(new PassengerCriteria());
            request.PassengerCriteria[1].number = 1;
            request.PassengerCriteria[1].text = "INF";

            request.SearchCriteriaSegment.Add(new SearchCriteriaSegment());
            request.SearchCriteriaSegment[0].departureDate = "2017-12-25";
            Console.Write("Depart from: ");
            request.SearchCriteriaSegment[0].From = Console.ReadLine();
            Console.Write("Travel to: ");
            request.SearchCriteriaSegment[0].To = Console.ReadLine();

            request.PricingModifiersAir.Add(new PricingModifiersAir());
            request.PricingModifiersAir[0].TicketingInfo.currencyType = "USD";
            request.PricingModifiersAir[0].FareSelection.fareType = "PublicFaresOnly";            
        }
        //Accept the formatted strings from the JSON object and convert them to valid DateTime objects
        static DateTime convertDateTime(String date, String time)
        {
            int year = Convert.ToInt32(date.Substring(0, 4));
            int month = Convert.ToInt32(date.Substring(5, 2));
            int day = Convert.ToInt32(date.Substring(8, 2));
            int hour = Convert.ToInt32(time.Substring(0, 2));
            int minute = Convert.ToInt32(time.Substring(3, 2));
            int second = Convert.ToInt32(time.Substring(6, 2));
            DateTime returnedDateTime = new DateTime(year, month, day, hour, minute, second);
            return returnedDateTime;
        }
       
        //Set up the request object and call the function to POST
        static async Task RunAsync()
        {
            //Get desired output
            Console.Write("Enter 1 for JSON, 2 for XML: ");
            int input = Convert.ToInt16(Console.ReadLine());
            String format = (input == 1?"json":"xml");
            try
            {
                // Populate the object to send
                OffersSearchQuery request = new OffersSearchQuery();
                populate(ref request);

                //Convert class into XML
                var serializer = new XmlSerializer(request.GetType());
                StringWriterWithEncoding sw = new StringWriterWithEncoding(Encoding.UTF8);
                //Create txt document to hold xml
                using (var writer = XmlWriter.Create(sw))
                {
                    XmlSerializerNamespaces ns1 = new XmlSerializerNamespaces();
                    
                    ns1.Add("", "http://www.travelport.com/schema/catalog/air/v4");
                    serializer.Serialize(writer, request);
                }
               
                //Store XML in StringContent variable
                StringContent xmlToSend = new StringContent(sw.ToString(), System.Text.Encoding.UTF8, "application/xml");
                //Create String to hold XML
                String requestString = await xmlToSend.ReadAsStringAsync();               
                
                //Make POST request
                String content = await BasicPost(xmlToSend, format);

                //Call the IATA web API to get the full name of airlines based on their IATA code
                //Alternatively, the same JSON is stored in IATAcode.json in the debug folder.
                client2.BaseAddress = new Uri("https://iatacodes.org/api/v6/");
                client2.DefaultRequestHeaders.Accept.Clear();
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                String response = await client2.GetAsync($"airlines?" +
                        $"api_key=e491aad1-a1ab-4bf9-91f3-39741bb4ac73").Result.Content.ReadAsStringAsync();
                IataResponse iataResponse = JsonConvert.DeserializeObject<IataResponse>(response);
                /****************
                * Received JSON *
                *****************/
                if (input == 1)
                {
                    //Traverse through the parsed JSON object and display the data
                    Wrapper OffersObject = parseJson(content);
                    foreach (Wrapper.Offer o in OffersObject.Offers.Offer)
                    {
                        //Find the IATA code in the JSON object
                        int airlineRefNum = Convert.ToInt32(o.Products[0].Product[0].Segment[0].@ref[0].Remove(0, 1));
                        String name = "ERROR: Airline name not found in IATA database";
                        foreach (Response r in iataResponse.response)
                        {
                            if (OffersObject.Offers.ReferenceList[0].Segment[airlineRefNum].TransportFlight.Flight.carrier == r.code)
                                name = r.name;
                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\tOffer {Convert.ToInt32(o.id.Remove(0, 1)) + 1}:\t  {o.Products[0].Product[0].CabinClass[0].value}  \t " +
                            $"${decimal.Round(o.PriceAir.AirPrice[0].Amount.Total.value, 2, MidpointRounding.AwayFromZero)}\t {name}");
                        int optionCounter = 1;
                        //Flight options
                        foreach (Wrapper.Product p in o.Products[0].Product)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\tOption {optionCounter++}");
                            //Will be used to calculate the total travel time
                            DateTime initialDeparture = new DateTime();
                            DateTime finalArrival = new DateTime();
                            bool isInitialDepartureTime = true;
                            //Time options
                            foreach (string r in p.Segment[0].@ref)
                            {
                                //Access the correct slot in the JSON Segment data
                                int refNum = Convert.ToInt32(r.Remove(0, 1));
                                if (isInitialDepartureTime)
                                {
                                    initialDeparture = convertDateTime(OffersObject.Offers.ReferenceList[0].Segment[refNum].Departure.date,
                                        OffersObject.Offers.ReferenceList[0].Segment[refNum].Departure.time);
                                    isInitialDepartureTime = false;
                                }
                                finalArrival = convertDateTime(OffersObject.Offers.ReferenceList[0].Segment[refNum].Arrival.date,
                                    OffersObject.Offers.ReferenceList[0].Segment[refNum].Arrival.time);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\tDeparture: {OffersObject.Offers.ReferenceList[0].Segment[refNum].Departure.date} - {OffersObject.Offers.ReferenceList[0].Segment[refNum].Departure.time}" +
                                    $"\t\t From: {OffersObject.Offers.ReferenceList[0].Segment[refNum].Departure.location}\t\t Arrival: {OffersObject.Offers.ReferenceList[0].Segment[refNum].Arrival.date}" +
                                    $" - {OffersObject.Offers.ReferenceList[0].Segment[refNum].Arrival.time}\t\t At: {OffersObject.Offers.ReferenceList[0].Segment[refNum].Arrival.location}");
                            }
                            //Calculate and "Nicely" display the total travel time
                            TimeSpan duration = finalArrival.Subtract(initialDeparture);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"\tTotal projected travel time: ");
                            if (duration.Days > 0)
                            {
                                if (duration.Days > 1)
                                    Console.Write($"{duration.Days} Days ");
                                else
                                    Console.Write($"{duration.Days} Day ");
                            }
                            if (duration.Hours > 0)
                            {
                                Console.Write($"{duration.Hours} Hours ");
                            }
                            if (duration.Minutes > 0)
                            {
                                Console.Write($"{duration.Minutes} Minutes");
                            }
                            Console.WriteLine($"");
                            Console.WriteLine($"");
                        }

                    }
                }
                /****************
                * Received XML  *
                *****************/
                else if (input == 2)
                {
                    //Traverse through the parsed XML object and display the data
                    Offers XmlObject = parseXml(content);
                    foreach (Offer o in XmlObject.Offer)
                    {
                        //Find the IATA code in the XML object
                        int airlineRefNum = Convert.ToInt32(o.Products.ProductTransport[0].SegmentID.Ref.Split(' ')[0].Remove(0, 1));
                        String name = "ERROR: Airline name not found in IATA database";
                        foreach (Response r in iataResponse.response)
                        {
                            if (XmlObject.ReferenceListSegment.Segment[airlineRefNum].TransportFlight.Flight.Carrier == r.code)
                                name = r.name;
                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\tOffer {Convert.ToInt32(o.Id.Remove(0, 1)) + 1}:\t  {o.Products.ProductTransport[0].CabinClass.Text}  \t " +
                            $"${decimal.Round(o.PriceAir.AirPrice.Amount.Total, 2, MidpointRounding.AwayFromZero)}\t {name}");
                        int optionCounter = 1;
                        //Flight options
                        foreach (ProductTransport p in o.Products.ProductTransport)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\tOption {optionCounter++}");
                            //Will be used to calculate the total travel time
                            DateTime initialDeparture = new DateTime();
                            DateTime finalArrival = new DateTime();
                            bool isInitialDepartureTime = true;
                            //Time options
                            foreach (string r in p.SegmentID.Ref.Split(' '))
                            {
                                //Access the correct slot in the XML Segment data
                                int refNum = Convert.ToInt32(r.Remove(0, 1));
                                if (isInitialDepartureTime)
                                {
                                    initialDeparture = convertDateTime(XmlObject.ReferenceListSegment.Segment[refNum].Departure.Date,
                                       XmlObject.ReferenceListSegment.Segment[refNum].Departure.Time);
                                    isInitialDepartureTime = false;
                                }
                                finalArrival = convertDateTime(XmlObject.ReferenceListSegment.Segment[refNum].Arrival.Date,
                                    XmlObject.ReferenceListSegment.Segment[refNum].Arrival.Time);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\tDeparture: {XmlObject.ReferenceListSegment.Segment[refNum].Departure.Date} - {XmlObject.ReferenceListSegment.Segment[refNum].Departure.Time}" +
                                    $"\t\t From: {XmlObject.ReferenceListSegment.Segment[refNum].Departure.Location}\t\t Arrival: {XmlObject.ReferenceListSegment.Segment[refNum].Arrival.Date}" +
                                    $" - {XmlObject.ReferenceListSegment.Segment[refNum].Arrival.Time}\t\t At: {XmlObject.ReferenceListSegment.Segment[refNum].Arrival.Location}");
                            }
                            //Calculate and "Nicely" display the total travel time
                            TimeSpan duration = finalArrival.Subtract(initialDeparture);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"\tTotal projected travel time: ");
                            if (duration.Days > 0)
                            {
                                if (duration.Days > 1)
                                    Console.Write($"{duration.Days} Days ");
                                else
                                    Console.Write($"{duration.Days} Day ");
                            }
                            if (duration.Hours > 0)
                            {
                                Console.Write($"{duration.Hours} Hours ");
                            }
                            if (duration.Minutes > 0)
                            {
                                Console.Write($"{duration.Minutes} Minutes");
                            }
                            Console.WriteLine($"");
                            Console.WriteLine($"");
                        }

                    }
                }
            }
            //In case of exceptions
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

    }
}

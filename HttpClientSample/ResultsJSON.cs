using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class Wrapper
    {
        public OffersMain Offers = new Wrapper.OffersMain();
        public class Identifier
        {
            public string value { get; set; }
        }

        public class DefaultCurrency
        {
            public string code { get; set; }
            public int minorUnit { get; set; }
        }

        public class Connection
        {
            public string @type { get; set; }
            public string inboundRef { get; set; }
            public string outboundRef { get; set; }
            public string duration { get; set; }
        }

        public class Segment
        {
            public string @type { get; set; }
            public List<string> @ref = new List<string>();
        }

        public class CabinClass
        {
            public List<string> segmentRef = new List<string>();
            public string classOfService { get; set; }
            public string value { get; set; }
        }

        public class Product
        {
            public string @type { get; set; }
            public string duration { get; set; }
            public string id { get; set; }
            public List<Connection> Connection = new List<Connection>();
            public List<Segment> Segment = new List<Segment>();
            public List<CabinClass> CabinClass = new List<CabinClass>();
        }

        public class Products
        {
            public List<Product> Product = new List<Product>();
        }

        public class Base
        {
            public double value { get; set; }
        }

        public class Tax
        {
            public string taxCode { get; set; }
            public double value { get; set; }
        }

        public class Taxes
        {
            public double totalTaxes { get; set; }
            public List<Tax> Tax = new List<Tax>();
        }

        public class Fees
        {
            public double totalFees { get; set; }
        }

        public class Total
        {
            public decimal value { get; set; }
        }

        public class Amount
        {
            public Base Base = new Base();
            public Taxes Taxes = new Taxes();
            public Fees Fees = new Fees();
            public Total Total = new Total();
        }

        public class AirPrice
        {
            public string @type { get; set; }
            public List<string> segmentRef = new List<string>();
            public string ptc { get; set; }
            public int quantity { get; set; }
            public string requestedPTC { get; set; }
            public Amount Amount = new Amount();
        }

        public class PriceAir
        {
            public List<AirPrice> AirPrice = new List<AirPrice>();
        }

        public class BaggageAllowance
        {
            public List<string> segmentRef = new List<string>();
            public int maxPieces { get; set; }
        }

        public class TermsAndConditions
        {
            public string @type { get; set; }
            public List<BaggageAllowance> BaggageAllowance = new List<BaggageAllowance>();
        }

        public class Offer
        {
            public string @type { get; set; }
            public string id { get; set; }
            public List<Products> Products = new List<Products>();
            public PriceAir PriceAir = new PriceAir();
            public List<TermsAndConditions> TermsAndConditions = new List<TermsAndConditions>();
        }

        public class Flight
        {
            public string carrier { get; set; }
            public string number { get; set; }
            public string equipment { get; set; }
        }

        public class TransportFlight
        {
            public Flight Flight = new Flight();
        }

        public class Departure
        {
            public string location { get; set; }
            public string date { get; set; }
            public string time { get; set; }
        }

        public class Arrival
        {
            public string location { get; set; }
            public string date { get; set; }
            public string time { get; set; }
        }

        public class SegmentDB
        {
            public string @type { get; set; }
            public int distance { get; set; }
            public string duration { get; set; }
            public string id { get; set; }
            public TransportFlight TransportFlight = new TransportFlight();
            public Departure Departure = new Departure();
            public Arrival Arrival = new Arrival();
        }

        public class ReferenceList
        {
            public string @type { get; set; }
            public List<SegmentDB> Segment = new List<SegmentDB>();
        }

        public class OffersMain
        {
            public Identifier Identifier = new Identifier();
            public DefaultCurrency DefaultCurrency = new DefaultCurrency();
            public List<Offer> Offer = new List<Offer>();
            public List<ReferenceList> ReferenceList = new List<ReferenceList>();
        }
    }
}


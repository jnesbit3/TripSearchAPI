using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample2
{
    class ResultsJSONConverted
    {
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
        public List<string> @ref { get; set; }
        }

        public class CabinClass
        {
            public List<string> segmentRef { get; set; }
            public string classOfService { get; set; }
            public string value { get; set; }
        }

        public class Product2
        {
            public string @type { get; set; }
        public string duration { get; set; }
        public string id { get; set; }
        public List<Connection> Connection { get; set; }
        public List<Segment> Segment { get; set; }
        public List<CabinClass> CabinClass { get; set; }
        }

        public class ProductWrapper
        {
            public List<Product2> Product { get; set; }
        }

        public class Base
        {
            public int value { get; set; }
        }

        public class Tax
        {
            public string taxCode { get; set; }
            public double value { get; set; }
        }

        public class Taxes
        {
            public double totalTaxes { get; set; }
            public List<Tax> Tax { get; set; }
        }

        public class Fees
        {
            public int totalFees { get; set; }
        }

        public class Total
        {
            public double value { get; set; }
        }

        public class Amount
        {
            public Base Base { get; set; }
            public Taxes Taxes { get; set; }
            public Fees Fees { get; set; }
            public Total Total { get; set; }
        }

        public class Brand
        {
            public string id { get; set; }
        }

        public class AirPrice
        {
            public string @type { get; set; }
            public List<object> segmentRef { get; set; }
            public string ptc { get; set; }
            public int quantity { get; set; }
            public string requestedPTC { get; set; }
            public Amount Amount { get; set; }
            public Brand Brand { get; set; }
        }

        public class PriceAir
        {
            public List<AirPrice> AirPrice { get; set; }
        }

        public class BaggageAllowance
        {
            public List<string> segmentRef { get; set; }
            public int maxPieces { get; set; }
        }

        public class TermsAndCondition
        {
            public string @type { get; set; }
            public List<BaggageAllowance> BaggageAllowance { get; set; }
        }

        public class Offer
        {
            public string @type { get; set; }
            public string id { get; set; }
            public List<ProductWrapper> Products { get; set; }
            public PriceAir PriceAir { get; set; }
            public List<TermsAndCondition> TermsAndConditions { get; set; }
        }

        public class Flight
        {
            public string carrier { get; set; }
            public string number { get; set; }
            public string equipment { get; set; }
        }

        public class TransportFlight
        {
            public Flight Flight { get; set; }
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

        public class IntermediateStop
        {
            public string value { get; set; }
        }

        public class Segment2
        {
            public string @type { get; set; }
            public int distance { get; set; }
            public string duration { get; set; }
            public string id { get; set; }
            public TransportFlight TransportFlight { get; set; }
            public Departure Departure { get; set; }
            public Arrival Arrival { get; set; }
            public List<IntermediateStop> IntermediateStop { get; set; }
        }

        public class ReferenceList
        {
            public string @type { get; set; }
            public List<Segment2> Segment { get; set; }
        }

        public class Offers
        {
            public Identifier Identifier { get; set; }
            public DefaultCurrency DefaultCurrency { get; set; }
            public List<Offer> Offer { get; set; }
            public List<ReferenceList> ReferenceList { get; set; }
        }

        public class RootObject
        {
            public Offers Offers { get; set; }
        }
    }
}

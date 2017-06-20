using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HttpClientSample
{
        [XmlRoot(ElementName = "DefaultCurrency", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class DefaultCurrency
        {
            [XmlAttribute(AttributeName = "code")]
            public string Code { get; set; }
            [XmlAttribute(AttributeName = "minorUnit")]
            public string MinorUnit { get; set; }
        }

        [XmlRoot(ElementName = "Connection", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class Connection
        {
            [XmlAttribute(AttributeName = "inboundRef")]
            public string InboundRef { get; set; }
            [XmlAttribute(AttributeName = "outboundRef")]
            public string OutboundRef { get; set; }
            [XmlAttribute(AttributeName = "duration")]
            public string Duration { get; set; }
        }

        [XmlRoot(ElementName = "SegmentID", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class SegmentID
        {
            [XmlAttribute(AttributeName = "ref")]
            public string Ref { get; set; }
        }

        [XmlRoot(ElementName = "CabinClass", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class CabinClass
        {
            [XmlAttribute(AttributeName = "segmentRef")]
            public string SegmentRef { get; set; }
            [XmlAttribute(AttributeName = "classOfService")]
            public string ClassOfService { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "ProductTransport", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class ProductTransport
        {
            [XmlElement(ElementName = "Connection", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public Connection Connection { get; set; }
            [XmlElement(ElementName = "SegmentID", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public SegmentID SegmentID { get; set; }
            [XmlElement(ElementName = "CabinClass", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public CabinClass CabinClass { get; set; }
            [XmlAttribute(AttributeName = "duration")]
            public string Duration { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
        }

        [XmlRoot(ElementName = "Products", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class Products
        {
            [XmlElement(ElementName = "ProductTransport", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public List<ProductTransport> ProductTransport { get; set; }
        }

        [XmlRoot(ElementName = "Tax", Namespace = "http://www.travelport.com/schema/finance/v4")]
        public class Tax
        {
            [XmlAttribute(AttributeName = "taxCode")]
            public string TaxCode { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "Taxes", Namespace = "http://www.travelport.com/schema/finance/v4")]
        public class Taxes
        {
            [XmlElement(ElementName = "Tax", Namespace = "http://www.travelport.com/schema/finance/v4")]
            public List<Tax> Tax { get; set; }
            [XmlAttribute(AttributeName = "totalTaxes")]
            public string TotalTaxes { get; set; }
        }

        [XmlRoot(ElementName = "Fees", Namespace = "http://www.travelport.com/schema/finance/v4")]
        public class Fees
        {
            [XmlAttribute(AttributeName = "totalFees")]
            public string TotalFees { get; set; }
        }

        [XmlRoot(ElementName = "Amount", Namespace = "http://www.travelport.com/schema/finance/v4")]
        public class Amount
        {
            [XmlElement(ElementName = "Base", Namespace = "http://www.travelport.com/schema/finance/v4")]
            public string Base { get; set; }
            [XmlElement(ElementName = "Taxes", Namespace = "http://www.travelport.com/schema/finance/v4")]
            public Taxes Taxes { get; set; }
            [XmlElement(ElementName = "Fees", Namespace = "http://www.travelport.com/schema/finance/v4")]
            public Fees Fees { get; set; }
            [XmlElement(ElementName = "Total", Namespace = "http://www.travelport.com/schema/finance/v4")]
            public decimal Total { get; set; }
        }

        [XmlRoot(ElementName = "AirPrice", Namespace = "http://www.travelport.com/schema/catalog/air/v3")]
        public class AirPrice
        {
            [XmlElement(ElementName = "Amount", Namespace = "http://www.travelport.com/schema/finance/v4")]
            public Amount Amount { get; set; }
            [XmlAttribute(AttributeName = "segmentRef")]
            public string SegmentRef { get; set; }
            [XmlAttribute(AttributeName = "ptc")]
            public string Ptc { get; set; }
            [XmlAttribute(AttributeName = "quantity")]
            public string Quantity { get; set; }
            [XmlAttribute(AttributeName = "requestedPTC")]
            public string RequestedPTC { get; set; }
            [XmlElement(ElementName = "Brand", Namespace = "http://www.travelport.com/schema/catalog/air/v3")]
            public Brand Brand { get; set; }
        }

        [XmlRoot(ElementName = "PriceAir", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class PriceAir
        {
            [XmlElement(ElementName = "AirPrice", Namespace = "http://www.travelport.com/schema/catalog/air/v3")]
            public AirPrice AirPrice { get; set; }
        }

        [XmlRoot(ElementName = "BaggageAllowance", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class BaggageAllowance
        {
            [XmlAttribute(AttributeName = "segmentRef")]
            public string SegmentRef { get; set; }
            [XmlAttribute(AttributeName = "maxPieces")]
            public string MaxPieces { get; set; }
        }

        [XmlRoot(ElementName = "TermsAndConditionsTransport", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class TermsAndConditionsTransport
        {
            [XmlElement(ElementName = "BaggageAllowance", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public BaggageAllowance BaggageAllowance { get; set; }
        }

        [XmlRoot(ElementName = "Offer", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class Offer
        {
            [XmlElement(ElementName = "Products", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public Products Products { get; set; }
            [XmlElement(ElementName = "PriceAir", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public PriceAir PriceAir { get; set; }
            [XmlElement(ElementName = "TermsAndConditionsTransport", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public TermsAndConditionsTransport TermsAndConditionsTransport { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
        }

        [XmlRoot(ElementName = "Brand", Namespace = "http://www.travelport.com/schema/catalog/air/v3")]
        public class Brand
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
        }

        [XmlRoot(ElementName = "Flight", Namespace = "http://www.travelport.com/schema/catalog/air/v3")]
        public class Flight
        {
            [XmlAttribute(AttributeName = "carrier")]
            public string Carrier { get; set; }
            [XmlAttribute(AttributeName = "number")]
            public string Number { get; set; }
            [XmlAttribute(AttributeName = "equipment")]
            public string Equipment { get; set; }
        }

        [XmlRoot(ElementName = "TransportFlight", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class TransportFlight
        {
            [XmlElement(ElementName = "Flight", Namespace = "http://www.travelport.com/schema/catalog/air/v3")]
            public Flight Flight { get; set; }
        }

        [XmlRoot(ElementName = "Departure", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class Departure
        {
            [XmlAttribute(AttributeName = "location")]
            public string Location { get; set; }
            [XmlAttribute(AttributeName = "date")]
            public string Date { get; set; }
            [XmlAttribute(AttributeName = "time")]
            public string Time { get; set; }
        }

        [XmlRoot(ElementName = "Arrival", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class Arrival
        {
            [XmlAttribute(AttributeName = "location")]
            public string Location { get; set; }
            [XmlAttribute(AttributeName = "date")]
            public string Date { get; set; }
            [XmlAttribute(AttributeName = "time")]
            public string Time { get; set; }
        }

        [XmlRoot(ElementName = "Segment", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class Segment
        {
            [XmlElement(ElementName = "TransportFlight", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public TransportFlight TransportFlight { get; set; }
            [XmlElement(ElementName = "Departure", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public Departure Departure { get; set; }
            [XmlElement(ElementName = "Arrival", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public Arrival Arrival { get; set; }
            [XmlAttribute(AttributeName = "distance")]
            public string Distance { get; set; }
            [XmlAttribute(AttributeName = "duration")]
            public string Duration { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlElement(ElementName = "IntermediateStop", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public List<string> IntermediateStop { get; set; }
        }

        [XmlRoot(ElementName = "ReferenceListSegment", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class ReferenceListSegment
        {
            [XmlElement(ElementName = "Segment", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public List<Segment> Segment { get; set; }
        }

        [XmlRoot(ElementName = "Offers", Namespace = "http://www.travelport.com/schema/catalog/v3")]
        public class Offers
        {
            [XmlElement(ElementName = "Identifier", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public string Identifier { get; set; }
            [XmlElement(ElementName = "DefaultCurrency", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public DefaultCurrency DefaultCurrency { get; set; }
            [XmlElement(ElementName = "Offer", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public List<Offer> Offer { get; set; }
            [XmlElement(ElementName = "ReferenceListSegment", Namespace = "http://www.travelport.com/schema/catalog/v3")]
            public ReferenceListSegment ReferenceListSegment { get; set; }
            [XmlAttribute(AttributeName = "fin-0400", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Fin0400 { get; set; }
            [XmlAttribute(AttributeName = "fin-0500", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Fin0500 { get; set; }
            [XmlAttribute(AttributeName = "fin-0600", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Fin0600 { get; set; }
            [XmlAttribute(AttributeName = "c-0200", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string C0200 { get; set; }
            [XmlAttribute(AttributeName = "c-0300", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string C0300 { get; set; }
            [XmlAttribute(AttributeName = "c-0201", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string C0201 { get; set; }
            [XmlAttribute(AttributeName = "per-0400", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Per0400 { get; set; }
            [XmlAttribute(AttributeName = "cthp-0400", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Cthp0400 { get; set; }
            [XmlAttribute(AttributeName = "cthp-0200", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Cthp0200 { get; set; }
            [XmlAttribute(AttributeName = "cthp-0300", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Cthp0300 { get; set; }
            [XmlAttribute(AttributeName = "prfc-0200", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Prfc0200 { get; set; }
            [XmlAttribute(AttributeName = "org-0300", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Org0300 { get; set; }
            [XmlAttribute(AttributeName = "org-0400", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Org0400 { get; set; }
            [XmlAttribute(AttributeName = "prfl-0200", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Prfl0200 { get; set; }
            [XmlAttribute(AttributeName = "ctan-0200", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctan0200 { get; set; }
            [XmlAttribute(AttributeName = "ctan-0300", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctan0300 { get; set; }
            [XmlAttribute(AttributeName = "ctrl-0200", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctrl0200 { get; set; }
            [XmlAttribute(AttributeName = "ctrl-0300", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctrl0300 { get; set; }
            [XmlAttribute(AttributeName = "rtm", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Rtm { get; set; }
            [XmlAttribute(AttributeName = "ctvh-0200", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctvh0200 { get; set; }
            [XmlAttribute(AttributeName = "ctvh-0300", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctvh0300 { get; set; }
            [XmlAttribute(AttributeName = "ctar-0300", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctar0300 { get; set; }
            [XmlAttribute(AttributeName = "ctar-0400", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctar0400 { get; set; }
            [XmlAttribute(AttributeName = "ctar-0500", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctar0500 { get; set; }
            [XmlAttribute(AttributeName = "ctar-0402", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctar0402 { get; set; }
            [XmlAttribute(AttributeName = "ctar-0401", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctar0401 { get; set; }
            [XmlAttribute(AttributeName = "ctlg-0300", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctlg0300 { get; set; }
            [XmlAttribute(AttributeName = "ctlg-0400", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctlg0400 { get; set; }
            [XmlAttribute(AttributeName = "ctlg-0600", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ctlg0600 { get; set; }
            [XmlAttribute(AttributeName = "c-0202", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string C0202 { get; set; }
            [XmlAttribute(AttributeName = "c-0301", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string C0301 { get; set; }
        }

    
}

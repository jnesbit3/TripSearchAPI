using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HttpClientSample2
{
    [XmlRoot(Namespace = "http://www.travelport.com/schema/catalog/v3")]
    public class Offers
    {
        public String Identifier { get; set; }
        public DefaultCurrency DefaultCurrency = new DefaultCurrency();
        [XmlElement]
        public List<Offer> Offer = new List<Offer>();

    }
    public class DefaultCurrency
    {
        [XmlAttribute("code")]
        public String code { get; set; }
        [XmlAttribute("minorUnit")]
        public int minorUnit { get; set; }
    }
    public class Offer
    {
        [XmlAttribute("id")]
        public String id { get; set; }
        public Products Products = new Products();
        public PriceAir PriceAir = new PriceAir();
        public TermsAndConditionsTransport TermsAndConditionsTransport = new TermsAndConditionsTransport();
    }
    public class Products
    {
        public ProductTransport ProductTransport = new ProductTransport();
    }
    public class ProductTransport
    {
        [XmlAttribute("id")]
        public String id { get; set; }
        [XmlAttribute("duration")]
        public String duration { get; set; }
        public SegmentID SegmentID = new SegmentID();
        public CabinClass CabinClass = new CabinClass();
    }
    public class SegmentID
    {
        [XmlAttribute("refs")]
        public String refs { get; set; }
    }
    public class CabinClass
    { 
        [XmlAttribute("segmentRef")]
        public String segmentRef { get; set; }
        [XmlAttribute("classOfService")]
        public String classOfService { get; set; }
        [XmlText]
        public String text { get; set; }
    }
    public class PriceAir
    {
        public AirPrice AirPrice = new AirPrice();
    }
    public class AirPrice
    {
        [XmlAttribute("segmentRef")]
        public String segmentRef { get; set; }
        [XmlAttribute("ptc")]
        public String ptc { get; set; }
        [XmlAttribute("quantity")]
        public int quantity { get; set; }
        [XmlAttribute("requestedPTC")]
        public String requestedPTC { get; set; }
        public Amount Amount = new Amount();
    }
    public class Amount
    {
        public Double Base { get; set; }
        public Taxes Taxes = new Taxes();
        public Fees Fees = new Fees();
        public Double Total { get; set; }
    }
    public class Taxes
    {
        [XmlAttribute("totalTaxes")]
        public Double totalTaxes { get; set; }
        [XmlElement]
        public List<Tax> Tax = new List<Tax>();
    }
    public class Tax
    {
        [XmlAttribute("taxCode")]
        public String taxCode { get; set; }
        [XmlText]
        public Double text { get; set; }
    }
    public class Fees
    {
        [XmlAttribute("totalFees")]
        public Double totalFees { get; set; }
    }
    public class TermsAndConditionsTransport
    {
        public BaggageAllowance BaggageAllowance = new BaggageAllowance();
    }
    public class BaggageAllowance
    {
        [XmlAttribute("segmentRef")]
        public String segmentRef { get; set; }
        [XmlAttribute("maxPieces")]
        public int maxPieces { get; set; }
    }

}

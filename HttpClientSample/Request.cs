/********************************************************************************
 * REQUEST.CS                                                                   *
 * DESCRIPTION:                                                                 *
 *          This file holds the class definitions for the POST requests         *
 *          being made to the Trip Search web API. These classes are to         *
 *          be serialized into XML or JSON in order to be posted to             *
 *          https://apigateway.pp.tvlport.com:443/connect/dev/AirSearch/offers. *
 * AUTHOR:                                                                      *
 *          Jared Nesbit                                                        *
 *          WWID: 736159                                                        *
 * DECLARATIONS:                                                                *
 *          OffersSearchQuery                                                   *
 *              PassengerCriteria                                               *
 *              SearchCriteriaSegment                                           *
 *              PricingModifiersAir                                             *
 *                  TicketingInfo                                               *
 *                  FareSelection                                               *
 *                  FareSelectionDetail                                         *
 *              SearchModifiersAir                                              *
 *                  GeneralSearchModifiersAir                                   *
 *                      FlightType                                              *
 *                      CabinPreference                                         *
 *                      CarrierPreference                                       *
 *              ReturnBrandedFares                                              *
 *              PsudoCityInfo                                                   *
 *                                                                              *
 ********************************************************************************/             

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HttpClientSample
{
    
    public class PassengerCriteria
    {
        [XmlAttribute("number")]
        public int number { get; set; }
        [XmlText]
        public String text { get; set; }
    }
    
    public class SearchCriteriaSegment
    {
        [XmlAttribute("departureDate")]
        public String departureDate { get; set; }
        [XmlAttribute("departureTime")]
        public String departureTime { get; set; }

        public string From { get; set; }
        public string To { get; set; }
    }

    public class PricingModifiersAir
    {
        [XmlElement]
        public TicketingInfo TicketingInfo = new TicketingInfo();
        [XmlElement]
        public FareSelection FareSelection = new FareSelection();
        FareSelectionDetail FareSelectionDetail = new FareSelectionDetail();
    }
    //Subclass of PricingModifiersAir
    public class TicketingInfo
    {
        [XmlAttribute("currencyType")]
        public String currencyType { get; set; }
    }
    //Subclass of PricingModifiersAir
    public class FareSelection
    {
        [XmlAttribute("fareType")]
        public String fareType { get; set; }
    }
    //Subclass of PricingModifiersAir
    public class FareSelectionDetail
    {
        [XmlAttribute("prohibitAdvancePurchaseFaresInd")]
        public bool prohibitAdvancePurchaseFaresInd { get; set; }
        [XmlAttribute("prohibitMaxStayFaresInd")]
        public bool prohibitMaxStayFaresInd { get; set; }
        [XmlAttribute("prohibitMinStayFaresInd")]
        public bool prohibitMinStayFaresInd { get; set; }
        [XmlAttribute("refundableOnlyInd")]
        public bool refundableOnlyInd { get; set; }
    }
   
    public class SearchModifiersAir
    {
        [XmlAttribute("prohibitChangeOfAirportInd")]
        public bool prohibitChangeOfAirportInd;
        public GeneralSearchModifiersAir GeneralSearchModifiersAir = new GeneralSearchModifiersAir();

    }
    //Subclass of SearchModifiersAir
    public class GeneralSearchModifiersAir
    {
        public FlightType FlightType = new FlightType();
        public CabinPreference CabinPreference = new CabinPreference();
        public CarrierPreference CarrierPreference = new CarrierPreference();
    }
    //Subclass of SearchModifiersAir
    public class FlightType
    {
        [XmlAttribute("connectionType")]
        public String connectionType { get; set; }
        [XmlAttribute("excludeInterlineConnectionsInd")]
        public bool excludeInterlineConnectionsInd { get; set; }
    }
    //Subclass of SearchModifiersAir
    public class CabinPreference
    {
        [XmlAttribute("cabins")]
        public String cabins { get; set; }
        [XmlAttribute("type")]
        public String type { get; set; }
    }
    //Subclass of SearchModifiersAir
    public class CarrierPreference
    {
        [XmlAttribute("carriers")]
        public String cabins { get; set; }
        [XmlAttribute("type")]
        public String type { get; set; }
    }

    //Base class
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.travelport.com/schema/catalog/air/v4", IsNullable = true)]
    public class OffersSearchQuery
    {
        [XmlAttribute("xmlns")]
        public string xmlns { get; set; }
        [XmlAttribute("maxNumberOfOffersToReturn")]
        public int maxNumberOfOffersToReturn { get; set; }

        [XmlElement]
        public List<PassengerCriteria> PassengerCriteria = new List<PassengerCriteria>();
        [XmlElement]
        public List<SearchCriteriaSegment> SearchCriteriaSegment = new List<SearchCriteriaSegment>();
        [XmlElement]
        public List<PricingModifiersAir> PricingModifiersAir = new List<PricingModifiersAir>();
        public List<SearchModifiersAir> SearchModifiersAir = new List<SearchModifiersAir>();
        public bool ReturnBrandedFares { get; set; }
        public String PseudoCityInfo { get; set; }
        
    }
    
}


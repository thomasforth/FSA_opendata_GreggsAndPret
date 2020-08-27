using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GreggsAndPret
{
    class Program
    {
        static void Main(string[] args)
        {
            // You can download FSA data from https://ratings.food.gov.uk/open-data/en-gb
            // Put them all in the same folder (you'll probably want to use a download manager like DownThemAll! for Firefox) and then point to it below.
            string PathToFSAXMLs = @"../../../../../FSA data Jan 2020";

            // Get all xmls
            int success = 0;
            int fail = 0;

            List<Place> AllPlaces = new List<Place>();
            List<Place> SelectedPlaces = new List<Place>();
            foreach (string filePath in Directory.EnumerateFiles(PathToFSAXMLs, "*.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(FHRSEstablishment));
                try
                {
                    // Create a TextReader to read the file. 
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
                    TextReader reader = new StreamReader(fs);
                    FHRSEstablishment root = (FHRSEstablishment)serializer.Deserialize(reader);

                    foreach (FHRSEstablishmentEstablishmentDetail detail in root.EstablishmentCollection)
                    {
                        Place place = new Place();

                        place.BusinessType = detail.BusinessType;
                        place.FullName = detail.BusinessName;
                        place.Postcode = detail.PostCode;
                        place.LocalAuthority = detail.LocalAuthorityName;
                        place.Longitude = detail.Geocode.Longitude;
                        place.Latitude = detail.Geocode.Latitude;
                        AllPlaces.Add(place);

                        if (detail.BusinessName.Contains("Greggs", StringComparison.InvariantCultureIgnoreCase))
                        {
                            place.Name = "Greggs";
                            place.BusinessType = detail.BusinessType;
                            place.FullName = detail.BusinessName;
                            place.Postcode = detail.PostCode;
                            place.LocalAuthority = detail.LocalAuthorityName;
                            place.Longitude = detail.Geocode.Longitude;
                            place.Latitude = detail.Geocode.Latitude;
                            SelectedPlaces.Add(place);
                        }
                        else if (detail.BusinessName.Contains("Pret a Manger", StringComparison.InvariantCultureIgnoreCase))
                        {
                            place.Name = "Pret";
                            place.BusinessType = detail.BusinessType;
                            place.FullName = detail.BusinessName;
                            place.Postcode = detail.PostCode;
                            place.LocalAuthority = detail.LocalAuthorityName;
                            place.Longitude = detail.Geocode.Longitude;
                            place.Latitude = detail.Geocode.Latitude;
                            SelectedPlaces.Add(place);
                        }
                        else if (detail.BusinessName.Contains("Fried Chicken", StringComparison.InvariantCultureIgnoreCase))
                        {
                            place.Name = "Fried Chicken";
                            place.BusinessType = detail.BusinessType;
                            place.FullName = detail.BusinessName;
                            place.Postcode = detail.PostCode;
                            place.LocalAuthority = detail.LocalAuthorityName;
                            place.Longitude = detail.Geocode.Longitude;
                            place.Latitude = detail.Geocode.Latitude;
                            SelectedPlaces.Add(place);
                        }
                    }
                    success++;
                }
                catch
                {
                    Console.WriteLine($"Failed to read file at {filePath}.");
                    fail++;
                }
            }

            using (TextWriter writer = new StreamWriter(@"GreggsAndPretAndFriedChicken.csv", false, System.Text.Encoding.UTF8))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(SelectedPlaces);
            }
            
            using (TextWriter writer = new StreamWriter(@"AllFSAPlacesFlat.csv", false, System.Text.Encoding.UTF8))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(AllPlaces);
            }

            Console.WriteLine("Success: " + success + " . Fail: " + fail);
        }
    }

    public class Place
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Postcode { get; set; }
        public string LocalAuthority { get; set; }
        public string City { get; set; }
        public string BusinessType { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class FHRSEstablishment
    {

        private FHRSEstablishmentHeader headerField;

        private FHRSEstablishmentEstablishmentDetail[] establishmentCollectionField;

        /// <remarks/>
        public FHRSEstablishmentHeader Header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EstablishmentDetail", IsNullable = false)]
        public FHRSEstablishmentEstablishmentDetail[] EstablishmentCollection
        {
            get
            {
                return this.establishmentCollectionField;
            }
            set
            {
                this.establishmentCollectionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FHRSEstablishmentHeader
    {

        private System.DateTime extractDateField;

        private ushort itemCountField;

        private string returnCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime ExtractDate
        {
            get
            {
                return this.extractDateField;
            }
            set
            {
                this.extractDateField = value;
            }
        }

        /// <remarks/>
        public ushort ItemCount
        {
            get
            {
                return this.itemCountField;
            }
            set
            {
                this.itemCountField = value;
            }
        }

        /// <remarks/>
        public string ReturnCode
        {
            get
            {
                return this.returnCodeField;
            }
            set
            {
                this.returnCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FHRSEstablishmentEstablishmentDetail
    {

        private uint fHRSIDField;

        private string localAuthorityBusinessIDField;

        private string businessNameField;

        private string businessTypeField;

        private ushort businessTypeIDField;

        private string addressLine1Field;

        private string addressLine2Field;

        private string addressLine3Field;

        private string addressLine4Field;

        private string postCodeField;

        private string ratingValueField;

        private string ratingKeyField;

        private string ratingDateField;

        private ushort localAuthorityCodeField;

        private string localAuthorityNameField;

        private string localAuthorityWebSiteField;

        private string localAuthorityEmailAddressField;

        private FHRSEstablishmentEstablishmentDetailScores scoresField;

        private string schemeTypeField;

        private string newRatingPendingField;

        private FHRSEstablishmentEstablishmentDetailGeocode geocodeField;

        /// <remarks/>
        public uint FHRSID
        {
            get
            {
                return this.fHRSIDField;
            }
            set
            {
                this.fHRSIDField = value;
            }
        }

        /// <remarks/>
        public string LocalAuthorityBusinessID
        {
            get
            {
                return this.localAuthorityBusinessIDField;
            }
            set
            {
                this.localAuthorityBusinessIDField = value;
            }
        }

        /// <remarks/>
        public string BusinessName
        {
            get
            {
                return this.businessNameField;
            }
            set
            {
                this.businessNameField = value;
            }
        }

        /// <remarks/>
        public string BusinessType
        {
            get
            {
                return this.businessTypeField;
            }
            set
            {
                this.businessTypeField = value;
            }
        }

        /// <remarks/>
        public ushort BusinessTypeID
        {
            get
            {
                return this.businessTypeIDField;
            }
            set
            {
                this.businessTypeIDField = value;
            }
        }

        /// <remarks/>
        public string AddressLine1
        {
            get
            {
                return this.addressLine1Field;
            }
            set
            {
                this.addressLine1Field = value;
            }
        }

        /// <remarks/>
        public string AddressLine2
        {
            get
            {
                return this.addressLine2Field;
            }
            set
            {
                this.addressLine2Field = value;
            }
        }

        /// <remarks/>
        public string AddressLine3
        {
            get
            {
                return this.addressLine3Field;
            }
            set
            {
                this.addressLine3Field = value;
            }
        }

        /// <remarks/>
        public string AddressLine4
        {
            get
            {
                return this.addressLine4Field;
            }
            set
            {
                this.addressLine4Field = value;
            }
        }

        /// <remarks/>
        public string PostCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        public string RatingValue
        {
            get
            {
                return this.ratingValueField;
            }
            set
            {
                this.ratingValueField = value;
            }
        }

        /// <remarks/>
        public string RatingKey
        {
            get
            {
                return this.ratingKeyField;
            }
            set
            {
                this.ratingKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string RatingDate
        {
            get
            {
                return this.ratingDateField;
            }
            set
            {
                this.ratingDateField = value;
            }
        }

        /// <remarks/>
        public ushort LocalAuthorityCode
        {
            get
            {
                return this.localAuthorityCodeField;
            }
            set
            {
                this.localAuthorityCodeField = value;
            }
        }

        /// <remarks/>
        public string LocalAuthorityName
        {
            get
            {
                return this.localAuthorityNameField;
            }
            set
            {
                this.localAuthorityNameField = value;
            }
        }

        /// <remarks/>
        public string LocalAuthorityWebSite
        {
            get
            {
                return this.localAuthorityWebSiteField;
            }
            set
            {
                this.localAuthorityWebSiteField = value;
            }
        }

        /// <remarks/>
        public string LocalAuthorityEmailAddress
        {
            get
            {
                return this.localAuthorityEmailAddressField;
            }
            set
            {
                this.localAuthorityEmailAddressField = value;
            }
        }

        /// <remarks/>
        public FHRSEstablishmentEstablishmentDetailScores Scores
        {
            get
            {
                return this.scoresField;
            }
            set
            {
                this.scoresField = value;
            }
        }

        /// <remarks/>
        public string SchemeType
        {
            get
            {
                return this.schemeTypeField;
            }
            set
            {
                this.schemeTypeField = value;
            }
        }

        /// <remarks/>
        public string NewRatingPending
        {
            get
            {
                return this.newRatingPendingField;
            }
            set
            {
                this.newRatingPendingField = value;
            }
        }

        /// <remarks/>
        public FHRSEstablishmentEstablishmentDetailGeocode Geocode
        {
            get
            {
                return this.geocodeField;
            }
            set
            {
                this.geocodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FHRSEstablishmentEstablishmentDetailScores
    {

        private byte hygieneField;

        private byte structuralField;

        private byte confidenceInManagementField;

        /// <remarks/>
        public byte Hygiene
        {
            get
            {
                return this.hygieneField;
            }
            set
            {
                this.hygieneField = value;
            }
        }

        /// <remarks/>
        public byte Structural
        {
            get
            {
                return this.structuralField;
            }
            set
            {
                this.structuralField = value;
            }
        }

        /// <remarks/>
        public byte ConfidenceInManagement
        {
            get
            {
                return this.confidenceInManagementField;
            }
            set
            {
                this.confidenceInManagementField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FHRSEstablishmentEstablishmentDetailGeocode
    {

        private decimal longitudeField;

        private decimal latitudeField;

        /// <remarks/>
        public decimal Longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }

        /// <remarks/>
        public decimal Latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }
    }




}

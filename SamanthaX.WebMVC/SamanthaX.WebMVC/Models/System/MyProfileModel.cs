using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Linq;

namespace SamanthaX.WebMVC.Models.System {
    
    public class MyProfileModel {

        #region properties

        public int CompanyId { get; set; }
        public string Tradename { get; set; }
        public string BusinessName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FTR { get; set; }
        public string PRK { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string CrossingStreets { get; set; }
        public string Colony { get; set; }
        public string City { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string CellPhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public string Token { get; set; }
        //
        //  Samantha Voice
        public int SamanthaVoiceId { get; set; }
        public int UserId {get; set;}

        [Required(ErrorMessage = "*Artificial Intellingence name*, it can't be left empty")]
        [MinLength(5, ErrorMessage = "The name of the Artificial Intelligence must not be less than 5 characters")]
        public string AIName { get; set; }

        [Required(ErrorMessage = "*Order You*, it can't be left empty")]
        public string OrderYou { get; set; }

        [Required(ErrorMessage = "*Voice Processing Default*, it can't be left empty")]
        public string VoiceProcessingDefault { get; set; }

        [Required(ErrorMessage = "*Voice Solution Default*, it can't be left empty")]
        public string VoiceSolutionDefault { get; set; }

        [Required(ErrorMessage = "*Voice Cancel Default*, it can't be left empty")]
        public string VoiceCancelDefault { get; set; }

        [Required(ErrorMessage = "*Voice Fail Default*, it can't be left empty")]
        public string VoiceFailDefault { get; set; }

        [Required(ErrorMessage = "*An Exception Ocurred*, it can't be left empty")]
        public string AnExceptionOcurred { get; set; }
        
        public DateTime LastUpdate { get; set; }

        #endregion

        #region Constructor

        public MyProfileModel() {
            CompanyId = 0;
            Tradename = String.Empty;
            BusinessName = String.Empty;
            Name = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            FTR = String.Empty;
            PRK = String.Empty;
            Street = String.Empty;
            StreetNumber = String.Empty;
            CrossingStreets = String.Empty;
            Colony = String.Empty;
            City = String.Empty;
            Municipality = String.Empty;
            State = String.Empty;
            Country = String.Empty;
            PostalCode = String.Empty;
            CellPhoneNumber = String.Empty;
            PhoneNumber = String.Empty;
            EMail = String.Empty;
            Token = string.Empty;
            //
            //  Samantha Voice
            SamanthaVoiceId = 0;
            UserId = 0;
            AIName = String.Empty;
            OrderYou = String.Empty;
            VoiceProcessingDefault = String.Empty;
            VoiceSolutionDefault = String.Empty;
            VoiceCancelDefault = String.Empty;
            VoiceFailDefault = String.Empty;
            AnExceptionOcurred = String.Empty;
            LastUpdate = DateTime.MinValue;
        }

        #endregion
    }
}

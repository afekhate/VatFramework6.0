using System;
using System.Collections.Generic;
using System.Text;

namespace VatFramework.Services.Handler.EmailMessaging
{
   public static class EmailTemplateCode
    {
        public const string ACCOUNT_CREATION = "ACCOUNT_CREATION";
        public const string ACCOUNT_VERIFICATION = "ACCOUNT_VERIFICATION";
        public const string PASSWORD_UPDATE = "PASSWORD_UPDATE";
        public const string FORGOT_PASSWORD = "FORGOT_PASSWORD";
        public const string USER_CHANGE_PASSWORD = "USER_CHANGE_PASSWORD";
        public const string EMAIL_NOFITICATION = "EMAIL_NOFITICATION";
        public const string RESPONDER_EMAIL_NOTIFICATION = "RESPONDER_EMAIL_NOTIFICATION";
        public const string REPORTERS_INCIDENT_UPDATE_NOFITICATION = "REPORTERS_INCIDENT_UPDATE_NOFITICATION";
        public const string NEW_MAIL_REPORTING_NOTIFICATION = "NEW_MAIL_REPORTING_NOTIFICATION";
        public const string RESPONDER_REPORT_NOTIFICATION = "RESPONDER_REPORT_NOTIFICATION";
        public const string MAIL_REPORTING_UPDATE = "MAIL_REPORTING_UPDATE";
        public const string RESPONDER_ACCEPT_REQUEST = "RESPONDER_ACCEPT_REQUEST";
        public const string RESPONDER_REJECT_REQUEST = "RESPONDER_REJECT_REQUEST";
        public const string RESOLUTION_EMAIL_NOTIFICATION = "RESOLUTION_EMAIL_NOTIFICATION";
        public const string ESCALATE_PENDING_INCIDENT_TO_EXECUTIVE_AND_SITURATION_MANAGER = "ESCALATE_PENDING_INCIDENT_TO_EXECUTIVE_AND_SITURATION_MANAGER";
        public const string ESCALATE_PENDING_UNACKNOWLEDGED_INCIDENT_TO_EXECUTIVE_AND_SITURATION_MANAGER = "ESCALATE_PENDING_UNACKNOWLEDGED_INCIDENT_TO_EXECUTIVE_AND_SITURATION_MANAGER";
        public const string CLOSED_EMAIL_NOTIFICATION = "CLOSED_EMAIL_NOTIFICATION";
        public const string AUTHORIZER_EMAIL_NOTIFICATION = "AUTHORIZER_EMAIL_NOTIFICATION";
        public const string REPORTER_NOTIFICATION = "REPORTER_NOTIFICATION";
        public const string EXECUTIVE_COMMENT_EMAIL_NOTIFICATION = "EXECUTIVE_COMMENT_EMAIL_NOTIFICATION";
        public const string EMAIL_BROADCAST_ALERT = "EMAIL_BROADCAST_ALERT";
        public const string NEW_SUBSCRIBER_EMAIL_NOTIFICATION = "NEW_SUBSCRIBER_EMAIL_NOTIFICATION";
        public const string PLANT_REQUEST_EMAIL_NOTIFICATION = "PLANT_REQUEST_EMAIL_NOTIFICATION";
        public const string BLANKING_REQUEST_EMAIL_NOTIFICATION = "BLANKING_REQUEST_EMAIL_NOTIFICATION";
        public const string PRINTING_REQUEST_EMAIL_NOTIFICATION = "PRINTING_REQUEST_EMAIL_NOTIFICATION";
        public const string EMBOSSING_REQUEST_EMAIL_NOTIFICATION = "EMBOSSING_REQUEST_EMAIL_NOTIFICATION";
        public const string PACKAGING_REQUEST_EMAIL_NOTIFICATION = "PACKAGING_REQUEST_EMAIL_NOTIFICATION";
        public const string MINI_STORE_REQUEST = "MINI_STORE_REQUEST";
        public const string MINIPLANT_REQUEST_EMAIL_NOTIFICATION_RESULT = "MINIPLANT_REQUEST_EMAIL_NOTIFICATION_RESULT";



        public const string PLANT_REQUEST_EMAIL_NOTIFICATION_RESULT = "PLANT_REQUEST_EMAIL_NOTIFICATION_RESULT";

    }

    public class EmailTokenViewModel
    {
        public string TokenName { get; set; }
        public string Token { get; set; }
        public string TokenValue { get; set; }

    }

    public class EmailTokenConstants
    {

        /// <summary>
        /// User details constants
        /// </summary>
        public const string USERNAME = "#Username";

        public const string EMAIL = "#Email";
        public const string SUBMISSION_DATE = "#SubmittedDate";
        public const string PASSWORD = "#Password";
        public const string PORTALNAME = "#PortalName";
        public const string FULLNAME = "#Name";
        public const string DURATION = "#Duration";
        public const string LogoURL = "#LogoUrl";
        public const string ACCOUNT_VERIFICATION_LINK = "#Link";
        public const string CASE_NUMBER = "#CaseNumber";
        public const string LGA = "#LGA";
        public const string LOCATION = "#LOCATION";
        public const string INCIDENTTYPE = "#IncidentType";
        public const string RESPONDERNAME = "#Responder";
        public const string PICKUPDATE = "#PickupDate";
        public const string AlertExecutive = "#AlertExecutive";
        public const string Now = "#Now";
        public const string COMMENT = "#Comment";
        public const string INCIDENTSTATUS = "#IncidentStatus";
        public const string MESSAGE = "#Message";
        public const string ApplicationStatus = "#ApplicationStatus";
        // Forgot Password
        public const string URL = "[[URL]]";


        /// <summary>
        /// Ticket details constants
        /// </summary>
        public const string REQUESTID = "[[REQUESTID]]";
        public const string DEPARTMENT = "[[DEPARTMENT]]";
        public const string DATECREATED = "[[DATECREATED]]";
        public const string REQUESTERNAME = "[[REQUESTERNAME]]";
        public const string DATERESOLVED = "[[DATERESOLVED]]";
        public const string DATEPUTONHOLD = "[[DATEPUTONHOLD]]";
        public const string RESPONSE = "[[RESPONSE]]";
        public const string FEEDBACK = "[[FEEDBACK]]";


        #region FRSC
        public const string Plant = "#Plant";
        public const string Quantity = "#Quantity";
        public const string MaterialType = "#MaterialType";
        public const string DateRequested = "#DateRequested";
        public const string Subject = "#Subject";
        public const string QuantityApproved = "#QuantityApproved";
        public const string DateTreated = "#DateTreated";


        public const string MiniPlant = "#MiniPlant";
        public const string CategoryName = "#CategoryName";
        #endregion


    }


}

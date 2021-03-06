﻿using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace CMS_Shared
{
    public static class Commons
    {
        public const string Image100_50 = "http://placehold.it/100x50";
        public const string Image200_100 = "http://placehold.it/200x100";
        public const string Image272_259 = "http://placehold.it/272x259";
        public const string Image870_500 = "http://placehold.it/870x500";

        public static int WidthProduct = Convert.ToInt16(ConfigurationManager.AppSettings["WidthProduct"]);
        public static int HeightProduct = Convert.ToInt16(ConfigurationManager.AppSettings["HeightProduct"]);
        public static int WidthCate = Convert.ToInt16(ConfigurationManager.AppSettings["WidthCate"]);
        public static int HeightCate = Convert.ToInt16(ConfigurationManager.AppSettings["HeightCate"]);

        public static int WidthImageNews = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageNews"]);
        public static int HeightImageNews = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageNews"]);
        public static int WidthImageSilder = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageSilder"]);
        public static int HeightImageSilder = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageSilder"]);
        public static string Phone1 = ConfigurationManager.AppSettings["Phone1"];
        public static string Phone2 = ConfigurationManager.AppSettings["Phone2"];
        public static string Email1 = ConfigurationManager.AppSettings["Email1"];
        public static string Email2 = ConfigurationManager.AppSettings["Email2"];
        public static string AddressCompany = ConfigurationManager.AppSettings["AddressCompnay"];
        public static string CompanyTitle = ConfigurationManager.AppSettings["CompanyTitle"];
        public static string HostImage = ConfigurationManager.AppSettings["HostImage"];
        public static string _PublicImages = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PublicImages"]) ? "" : ConfigurationManager.AppSettings["PublicImages"];
        public static string Email = "support@sms4api.com";
        public static string Password = "yTYXRmbR29n439P";
        public static string centriURL = ConfigurationManager.AppSettings["CentriUrl"];
        public static string centriApiKey = ConfigurationManager.AppSettings["CentriApiKey"];
        public static string centriSecretKey = ConfigurationManager.AppSettings["CentriSecretKey"];
        public static string centriSMSChannel = ConfigurationManager.AppSettings["CentriSMSChannel"];
        public enum CustomerStatus
        {
            Watting = 0,
            Open = 1,
            Locked = 2
        }

        public enum ConfigType
        {
            USD = 0,
            PMUSD = 1,
            SMSOTP = 2,
            SMS = 3,
            SMSMarketing = 4,
            CreditDefault = 5,
            WaitingTime = 6,
            SiteHTML = 7,
        }

        public enum ExchangeType
        {
            None = 0,
            Binance = 1,
            Bittrex = 2,
        }

        public enum APIType
        {
            APIPerfectMonney = 0,
            APISMS = 1,
            APISim = 2,
        }

        public enum SMSType
        {
            OTP = 0,
            Marketing = 1,
            Internal = 2,
        }
        public enum DepositStatus
        {
            [Description("Waiting Pay")]
            WaitingPay = 0,
            Completed = 1,
            [Description("Waiting Customer")]
            WaitingCustomer = 2,
            [Description("Confirmed Pay")]
            ConfirmedPay = 3,
            Cancel = 4
        }
        public enum SMSStatus
        {
            [Description("Waiting Send")]
            WaitingSend = 0,
            Sent = 1,
            Success = 2,
            Fail = 3
        }
        public enum SimStatus
        {
            [Description("Waiting Connect")]
            WaitingConnect = 0,
            Connected = 1,
            [Description("Connect Fail")]
            ConnectFail = 3
        }
        private static readonly Dictionary<string, string> dicConvert = new Dictionary<string, string>
        {
            {"á", "as"},{"à", "af"},{"ạ", "aj"},{"ả", "ar"},{"ã", "ax"},{"â", "aa"},{"ấ", "aas"},{"ầ", "aaf"},{"ậ", "aaj"},
            {"ẩ", "aar"},{"ẫ", "aax"},{"ă", "aw"},{"ắ", "aws"},{"ằ", "awf"},{"ặ", "awj"},{"ẳ", "awr"},{"ẵ", "awx"},
            {"é", "es"},{"è", "ef"},{"ẹ", "ej"},{"ẻ", "er"},{"ẽ", "ex"},{"ê", "ee"},{"ế", "ees"},{"ề", "eef"},{"ệ", "eej"},
            {"ể", "eer"},{"ễ", "eex"},
            {"ó", "os"},{"ò", "of"},{"ọ", "oj"},{"ỏ", "or"},{"õ", "ox"},{"ô", "oo"},{"ố", "oos"},{"ồ", "oof"},{"ộ", "ooj"},
            {"ổ", "oor"},{"ỗ", "oox"},{"ơ", "ow"},{"ớ", "ows"},{"ờ", "owf"},{"ợ", "owj"},{"ở", "owr"},{"ỡ", "owx"},
            {"ú", "us"},{"ù", "uf"},{"ụ", "uj"},{"ủ", "ur"},{"ũ", "ux"},{"ư", "uw"},{"ứ", "uws"},{"ừ", "uwf"},{"ự", "uwj"},
            {"ử", "uwr"},{"ữ", "uwx"},
            {"í", "is"},{"ì", "if"},{"ị", "ij"},{"ỉ", "ir"},{"ĩ", "ix"},
            {"đ", "dd"},
            {"ý", "ys"},{"ỳ", "yf"},{"ỵ", "yj"},{"ỷ", "yr"},{"ỹ", "yx"}
        };
        public static string ConvertUnicodeToWithoutAccent(string str)
        {
            string strConvert = str.ToLower();
            foreach (KeyValuePair<string, string> item in dicConvert)
            {
                if (strConvert.Contains(item.Key))
                {
                    strConvert = strConvert.Replace(item.Key, item.Value);
                }
            }
            return strConvert;
        }
        public static string GenerateToken(Dictionary<string, object> payload)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, centriSecretKey);
        }
        
    }
}

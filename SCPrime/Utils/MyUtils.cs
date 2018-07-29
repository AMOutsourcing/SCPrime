﻿using SCPrime.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCPrime.Utils
{
    public class MyUtils
    {
        public static string getFTSearchSQL(string searchString, string strFTView)
        {
            string strSql = "";
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            searchString = regex.Replace(searchString, @" ");
            string[] words = searchString.Split(new char[] { ' ' });

            if (words.Length > 0)
                strSql = "select v._OID from " + strFTView + " v where ";
            for (int i = 0; i < words.Length; i++)
            {
                if (i > 0) { strSql += " and "; }
                strSql += " v.FTSEARCHKEYS like '%" + words[i] + "%'";
            }
            return strSql;
        }


        public static int GetMaxResult()
        {
            int max = 0;

            var strMax = ConfigurationManager.AppSettings["MaxResult"];
            var tmp = Int32.TryParse(strMax, out max);
            if (tmp)
                return max;
            else
                return 0;
        }

        public static string getDateFormat()
        {
            string datefm = ConfigurationManager.AppSettings["DateFormat"];
            return datefm;
        }
        public static string getDateTimeFormat()
        {
            string datefm = ConfigurationManager.AppSettings["DateTimeFormat"];
            return datefm;
        }
        public static DateTime strToDate(string value, string myCultureInfo)
        {
            DateTime dt = DateTime.Now;

            DateTimeFormatInfo ukDtfi = new CultureInfo(myCultureInfo, false).DateTimeFormat;
            dt = Convert.ToDateTime(value, ukDtfi);

            return dt;
        }
    }
}

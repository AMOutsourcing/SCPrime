using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPrime.Utils
{
    class Constant
    {
        public const string Empty = "Empty";
        public const string FirstInvoice = "First Invoice";
        public const string LastInvoice = "Last Invoice";

        //Common

        public const string isMarkDeleted = "isMarkDeleted";
        public const string isAvailable = "isAvailable";

        //Category Table
        public const string OID = "OID";
        public const string Name = "Name";
        public const string InvoiceFlag = "InvoiceFlag";
        public const string ItemNo = "ItemNo";
        public const string ItemSuplNo = "ItemSuplNo"; 
        public const string PurcharPrice = "BuyPr";
        public const string SalePrice = "SelPr";
        public const string LabourCode = "WrksId";
        public const string LabourName = "WrksName";

        public const string CategoryName = "Category";


        //Category column name
        public const string colOID = "OID";
        public const string colName = "Name";
        public const string colInvoiceFlag = "InvoiceFlag";


        //option colum name
        public const string OptionPurcharPrice = "OptionBuyPr";
        public const string OptionSalePrice = "OptionSelPr";
        public const string OptionName = "OptionName";
        public const string OptionisMarkDeleted = "OptionisMarkDeleted";

        //detail column name
        public const string DetailPurcharPrice ="DetailBuyPr";
        public const string DetailSalePrice ="DetailSelPr";
        public const string DetailName = "DetailName";
        public const string DetailisMarkDeleted = "DetailisMarkDeleted";
    }
}

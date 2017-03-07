using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPAS2Model
{
  public static class TypeSelectLists
  {
    public static List<dynamic> CostElementAccountTypeList = new List<dynamic>() {
      new {id=(int)EnumCostElementAccountType.Capex, name="Capex/资本类项目"},
      new {id=(int)EnumCostElementAccountType.Opex, name="Opex/费用类项目"},
      new {id=(int)EnumCostElementAccountType.Other, name="Other/其他类项目"},
    };

    public static List<dynamic> PRItemTypeList = new List<dynamic>() {
      new {id=(int)EnumPRItemType.Goods, name="Goods/货物"},
      new {id=(int)EnumPRItemType.Service, name="Service/服务"},
      new {id=(int)EnumPRItemType.Fee, name="Fee/费用"},
      new {id=(int)EnumPRItemType.Other, name="Other/其他"},
    };

    public static List<dynamic> UnitMeasureTypeList = new List<dynamic>() {
      new {id="ea/每个", name="ea/每个"},
      new {id="ton/吨", name="ton/吨"},
    };

    public static List<dynamic> POTypeList = new List<dynamic>() {
      new {id=(int)EnumPOType.PO, name="PO/采购订单"},
      new {id=(int)EnumPOType.Contract, name="Contract/合同"},
    };

    public static List<dynamic> PaymentAreaTypeList = new List<dynamic>() {
      new {id=(int)EnumPaymentAreaType.domestic, name="Domestic/国内"},
      new {id=(int)EnumPaymentAreaType.oversea, name="Oversea/海外"},
    };

    public static List<dynamic> PaymentCurrencyTypeList = new List<dynamic>() {
      new {id=(int)EnumPaymentCurrencyType.homeCurrency, name="Home Currency/本币"},
      new {id=(int)EnumPaymentCurrencyType.foreignCurrency, name="Foreign Currency/外币"},
    };

    public static List<dynamic> PaymentMethodTypeList = new List<dynamic>() {
      new {id=(int)EnumPaymentMethodType.bankTransfer, name="Bank Transfer/银行转账"},
      new {id=(int)EnumPaymentMethodType.check, name="Check/支票"},
      new {id=(int)EnumPaymentMethodType.letterOfCredit, name="LOC/信用证"},
      new {id=(int)EnumPaymentMethodType.draft, name="Draft/汇票"},
    };



  }
}

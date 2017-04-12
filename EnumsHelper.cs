using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPAS2Model
{
  public static class OPAS2EnumsHelper
  {
    public static string getPRItemTypeName(EnumPRItemType item)
    {
      return new Dictionary<EnumPRItemType, string>()
      {
        { EnumPRItemType.Goods, "Goods/货物"},
        { EnumPRItemType.Service, "Service/服务"},
        { EnumPRItemType.Fee, "Fee/费用" },
        { EnumPRItemType.Other, "Other/其他"}
      }[item];
    }

    public static string getPOTypeName(EnumPOType item)
    {
      return new Dictionary<EnumPOType, string>()
      {
        { EnumPOType.PO, "Purchase Order/采购订单"},
        { EnumPOType.Contract, "Contract/合同"},
      }[item];
    }

  }
}

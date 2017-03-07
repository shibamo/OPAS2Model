using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OPAS2Model
{
  public static class BizSettings
  {
    public static List<Tuple<EnumBizDocumentType, string>>
    BizDocumentTypeList = new List<Tuple<EnumBizDocumentType, string>>()
    {
      new Tuple<EnumBizDocumentType, string>(EnumBizDocumentType.PR, "PR"),
      new Tuple<EnumBizDocumentType, string>(EnumBizDocumentType.PO, "PO"),
      new Tuple<EnumBizDocumentType, string>(EnumBizDocumentType.GR, "GR"),
      new Tuple<EnumBizDocumentType, string>(EnumBizDocumentType.PM, "PM"),
      new Tuple<EnumBizDocumentType, string>(EnumBizDocumentType.BA, "BA"),
    };

    public static string getBizDocumentTypeCode(
      EnumBizDocumentType bizDocumentType)
    {
      return BizDocumentTypeList.Where(
        item => item.Item1 == bizDocumentType).
        FirstOrDefault().Item2;
    }

    public static List<Tuple<EnumPRItemType, string>>
    PRItemType = new List<Tuple<EnumPRItemType, string>>()
    {
      new Tuple<EnumPRItemType, string>(EnumPRItemType.Goods, "Goods/货物"),
      new Tuple<EnumPRItemType, string>(EnumPRItemType.Service, "Service/服务"),
      new Tuple<EnumPRItemType, string>(EnumPRItemType.Fee, "Fee/费用"),
      new Tuple<EnumPRItemType, string>(EnumPRItemType.Other, "Other/其他"),
    };

    public static string getPRItemTypeName(
      EnumPRItemType pRItemType)
    {
      return PRItemType.Where(
        item => item.Item1 == pRItemType).
        FirstOrDefault().Item2;
    }
  }
}
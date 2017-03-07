using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OPAS2Model
{
  public enum EnumCostElementAccountType // 成本要素会计类型
  {
    Capex = 1,
    Opex,
    Other
  }


  public enum EnumBizDocumentType // 业务单据类型
  {
    PR = 1,
    PO,
    GR,
    PM,
    BA,  //Budget Adjustment 
  }

  public enum EnumBizDocumentUrgentType // 业务单据紧急程度,预留
  {
    Normal = 1,
    Urgent,
    ASAP,
    Unknow
  }

  public enum EnumPRType // PR业务单据类型, 预留扩充
  {
    Normal = 1,   // 普通PR
    WithVendor,   // 预先指定供应商PR
  }

  public enum EnumBizState // 单据业务状态类型,用于判断是否可以产生后续表单
  {
    Initial = 1,    // 初始阶段,一般处于流程审批阶段,需等待流程完成,不能直接发生后续表单
    Open,           // 可以直接发生PO,GR,PM等后续表单
    Closed          // 关闭状态,不能直接发生后续表单
  }

  public enum EnumPRItemType // PR业务单据项类型, 预留扩充
  {
    Goods = 1,    // 货物
    Service,      // 服务
    Fee,          // 费用
    Other,        // 其他
  }

  public enum EnumPOType // PO业务单据类型, 预留扩充
  {
    PO = 1,     // Purchase Order 采购订单
    Contract,   // Contract 合同
  }

  public enum EnumPOBizState // PO业务状态类型,用于判断是否可以产生GR/PM等后续表单
  {
    Initial = 1,    // 初始阶段,一般处于流程审批阶段,需等待流程完成,不能直接发生PO等后续表单
    Open,           // 可以直接发生GR/PM等后续表单
    Closed          // 关闭状态,不能直接发生GR/PM等后续表单
  }

  public enum EnumUserMessageType
  {
    Information = 1,
    Reply,
  }

  public enum EnumBizDocumentFlowState
  {
    Initial = 1,      // 尚未建立对应的建立流程实例请求
    Submitted,        // 已提交单据,尚未建立对应的建立流程实例请求
    FirstRequest,     // 已建立对应的建立流程实例请求,流程实例尚未建立
    InProcess,        // 建立了对应的流程实例,正在运行中
    StoppedValid,     // 已正常结束(完成了审批流程), 单据将生效
    StoppedInvalid,   // 已非正常结束, 单据将不生效(可能被管理员或用户Terminate)
    Frozen,           // 出于管理或技术原因被人为冻结,如不能继续发生PO等后续业务
    Error,            // 流程处于出错状态无法继续进行,等待管理员纠错
  }

  public enum EnumLocationType
  {
    domestic = 1,
    oversea,
    unknown,
  }

  public enum EnumVendorClass
  {
    firstClass = 1,
    secondClass,
    thirdClass,
  }

  public enum EnumVendorType
  {
    normal = 1,
    onetime,
    government,
  }

  public enum EnumVendorRelation
  {
    normal = 1,         // 普通
    belongToOneFamily,  // 同一集团
    partnership,        // 合作伙伴
    haveIssue,          // 存在争执
    haveLawsuit         // 存在诉讼
  }

  public enum EnumPaymentAreaType
  {
    domestic = 1,   // 国内付款
    oversea,        // 海外付款
  }

  public enum EnumPaymentCurrencyType
  {
    homeCurrency = 1, //本币
    foreignCurrency,  //外币
  }

  public enum EnumPaymentMethodType
  {
    bankTransfer = 1,   //银行汇款
    check,              //支票
    letterOfCredit,     //信用证
    draft               //汇票
  }
}
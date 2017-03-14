using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Dynamic;

namespace OPAS2Model
{
  public class OPAS2DbContext : DbContext
  {
    #region Some tedious configuration
    public OPAS2DbContext()
      : base("name=OPASDatabase") // DB connection name
    {
    }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      Precision.ConfigureModelBuilder(modelBuilder);

      //去掉系统自带的级联删除
      modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
      modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
    }
    #endregion

    public DbSet<CostCenter> costCenters { get; set; }
    public DbSet<CostCenterControllerRelation> costCenterControllerRelations { get; set; }
    public DbSet<CostCenterUserRelation> costCenterUserRelations { get; set; }
    public DbSet<CurrencyType> currencyTypes { get; set; }
    public DbSet<CurrencyHistoryRecord> currencyHistoryRecords { get; set; }
    public DbSet<CostElementType> costElementTypes { get; set; }
    public DbSet<CostElement> costElements { get; set; }
    public DbSet<BizDocumentSerialNoGenerator> bizDocumentSerialNoGenerators { get; set; }
    public DbSet<UserMessage> userMessages { get; set; }
    public DbSet<PurchaseReq> purchaseReqs { get; set; }
    public DbSet<PurchaseReqDetail> purchaseReqDetails { get; set; }
    public DbSet<PurchaseOrder> purchaseOrders { get; set; }
    public DbSet<PurchaseOrderDetail> purchaseOrderDetails { get; set; }
    public DbSet<GoodsReceiving> goodsReceivings { get; set; }
    public DbSet<GoodsReceivingDetail> goodsReceivingDetails { get; set; }
    public DbSet<Payment> payments { get; set; }
    public DbSet<PaymentDetail> paymentDetails { get; set; }
    public DbSet<Vendor> vendors { get; set; }
    public DbSet<VendorBank> vendorBanks { get; set; }
    public DbSet<AttachFile> attachFiles { get; set; }
    public DbSet<FunctionPermission> functionPermissions { get; set; }
    public DbSet<UserFunctionPermissionRelation> userFunctionPermissionRelations { get; set; }
    public DbSet<RoleFunctionPermissionRelation> roleFunctionPermissionRelations { get; set; }
  }

  [Table("Enou_CostCenter")]
  public class CostCenter
  {
    [Key]
    public int costCenterId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string costCenterNo { get; set; }
    [Required]
    public string chineseName { get; set; }
    public string englishName { get; set; }
    public int parentCostCenterId { get; set; }
    public string description { get; set; }
    public bool isVisible { get; set; } = true;
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_CostCenterControllerRelation")]
  public class CostCenterControllerRelation
  {
    [Key]
    public int costCenterControllerRelationId { get; set; }
    [ForeignKey("CostCenter")]
    public int costCenterId { get; set; }
    public virtual CostCenter CostCenter { get; set; }
    public int userId { get; set; } // userId of the controller
    public string userGuid { get; set; }
    public bool isVisible { get; set; } = true;
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
  }

  [Table("Enou_CostCenterUserRelation")]
  public class CostCenterUserRelation
  {
    [Key]
    public int costCenterUserRelationId { get; set; }
    [ForeignKey("CostCenter")]
    public int costCenterId { get; set; }
    public virtual CostCenter CostCenter { get; set; }
    public int userId { get; set; } // userId
    public string userGuid { get; set; }
    public bool isCostCenterHead { get; set; } = false;
    public bool isVisible { get; set; } = true;
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
  }

  [Table("Enou_CurrencyType")]
  public class CurrencyType
  {
    [Key]
    public int currencyTypeId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    public string name { get; set; }
    [Required]
    public string shortName { get; set; }
    [Required]
    public string symbol { get; set; }
    public bool isVisible { get; set; } = true;
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }

    public virtual List<CurrencyHistoryRecord> currencyHistoryRecords
    { get; set; }

    [NotMapped]
    public decimal? latestRate
    {
      get
      {
        return this.currencyHistoryRecords?.
          LastOrDefault()?.effectiveRate;
      }
    }
  }

  [Table("Enou_CurrencyHistoryRecord")]
  public class CurrencyHistoryRecord
  {
    [Key]
    public int currencyHistoryRecordId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("CurrencyType")]
    public int currencyTypeId { get; set; }
    [Required]
    public virtual CurrencyType CurrencyType { get; set; }
    [Precision(18, 6)]
    [Required]
    public decimal effectiveRate { get; set; }
    public DateTime effectiveDateFrom { get; set; }
    public bool isVisible { get; set; } = true;
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
  }

  [Table("Enou_CostElementType")]
  public class CostElementType
  {
    [Key]
    public int costElementTypeId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string name { get; set; }
    public string englishName { get; set; }
    public bool isVisible { get; set; } = true;
    public string description { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }

    public virtual List<CostElement> costElements { get; set; }
  }

  [Table("Enou_CostElement")]
  public class CostElement
  {
    [Key]
    public int costElementId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string name { get; set; }
    public string englishName { get; set; }
    [ForeignKey("CostElementType")]
    public int costElementTypeId { get; set; }
    public virtual CostElementType CostElementType { get; set; }
    public EnumCostElementAccountType costElementAccountType { get; set; }
    public bool isVisible { get; set; } = true;
    public string description { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_BizDocumentSerialNoGenerator")]
  public class BizDocumentSerialNoGenerator
  {
    [Key]
    public int bizDocumentSerialNoCounterId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string name { get; set; }
    public EnumBizDocumentType bizDocumentType { get; set; }
    public string organizationCode { get; set; }
    public string locationCode { get; set; }
    public string yearCode { get; set; }
    public string departmentCode { get; set; }
    public int startNo { get; set; } = 1;
    public int currentNo { get; set; }
    public int numberPartLength { get; set; } = 4;
  }

  [Table("Enou_UserMessage")]
  public class UserMessage
  {
    [Key]
    public int userMessageId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    public int parentUserMessageId { get; set; } // for the reply UserMessageType
    public EnumUserMessageType userMessageType { get; set; }
    public string subject { get; set; }
    public string content { get; set; }
    public int senderUserId { get; set; }
    public string senderUserGuid { get; set; }
    public string senderUserName { get; set; } //如果是管理员或者系统发的消息则没有上面两个字段
    public int receiverUserId { get; set; }
    public string receiverUserGuid { get; set; }
    public bool isVisible { get; set; } = true;
    public DateTime createTime { get; set; } = DateTime.Now;
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_PurchaseReq")]
  public class PurchaseReq
  {
    [Key]
    public int purchaseReqId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string documentNo { get; set; }
    public int flowActionRequestId { get; set; }
    public string flowActionRequestGuid { get; set; }
    public int? flowInstanceId { get; set; } // 回填时机?
    public string flowInstanceGuid { get; set; } // 回填时机?
    public EnumBizDocumentFlowState bizDocumentFlowState { get; set; }
      = EnumBizDocumentFlowState.Initial;
    public EnumBizState bizState { get; set; } = EnumBizState.Initial;
    public EnumCostElementAccountType costElementAccountType
    { get; set; } //似乎应该放在pr-detailitem上
    public string WBSNo { get; set; }
    public string contactOfficePhone { get; set; }
    public string contactMobile { get; set; }
    public string contactOtherMedia { get; set; }
    public int departmentId { get; set; }
    public string departmentGuid { get; set; }
    public int departmentIdBelongTo { get; set; } // 例如IT实质是帮业务部门提交的项目采购申请
    public string departmentGuidBelongTo { get; set; }
    [ForeignKey("CostCenter")]
    public int costCenterId { get; set; }
    public virtual CostCenter CostCenter { get; set; }
    public DateTime? expectReceiveBeginTime { get; set; }
    public DateTime? expectReceiveEndTime { get; set; }
    public bool isBidingRequired { get; set; } = true;
    public string noBiddingReason { get; set; }
    public string reason { get; set; }
    public string description { get; set; }
    public decimal? estimatedCost { get; set; }
    [ForeignKey("CurrencyType")]
    public int? currencyTypeId { get; set; }
    public virtual CurrencyType CurrencyType { get; set; }
    [Precision(18, 6)]
    public decimal mainCurrencyRate { get; set; }
    public decimal? estimatedCostInRMB { get; set; }
    public decimal? averageBenchmark { get; set; }
    public string benchmarkDescription { get; set; }
    public bool isFirstBuy { get; set; } = true;
    public DateTime? firstBuyDate { get; set; }
    public decimal? firstCostAmount { get; set; }
    public string firstBuyDescription { get; set; }
    public string remarkOfAprrovers { get; set; }
    public EnumPRType PRType { get; set; } = EnumPRType.Normal;
    public virtual List<PurchaseReqDetail> details { get; set; }
    public Vendor designatedVendor { get; set; } // 被指定的供应商,用于预先指定供应商PR
    public virtual List<Vendor> potentialVendors { get; set; } // 候选供应商列表
    public string otherVendorsNotInList { get; set; } // 其他尚无记录的候选供应商名称列表
    public bool isVisible { get; set; } = true;
    public string setInvisibleReason { get; set; }
    public DateTime? submitTime { get; set; }
    public string submitor { get; set; }
    public int submitorUserId { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_PurchaseReqDetail")]
  public class PurchaseReqDetail
  {
    [Key]
    public int purchaseReqDetailId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("PurchaseReq")]
    public int purchaseReqId { get; set; }
    [Required]
    public virtual PurchaseReq PurchaseReq { get; set; }
    public decimal? estimatedCost { get; set; }
    public int lineNo { get; set; }
    [Required]
    public string itemName { get; set; }
    public EnumPRItemType itemType { get; set; } = EnumPRItemType.Goods;
    public string description { get; set; }
    public string WBSNo { get; set; }
    [ForeignKey("CostElement")]
    public int? costElementId { get; set; }
    public virtual CostElement CostElement { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_PurchaseOrder")]
  public class PurchaseOrder
  {
    [Key]
    public int purchaseOrderId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string documentNo { get; set; }
    [ForeignKey("PurchaseReq")]
    public int? purchaseReqId { get; set; } // 可以为空, 业务上允许无对应PR的PO/Contract Detail(即只进行总量控制或者无PR的PO不进行控制)
    public virtual PurchaseReq PurchaseReq { get; set; } // 对应的PR, 可以为空
    public int flowActionRequestId { get; set; }
    public string flowActionRequestGuid { get; set; }
    public int? flowInstanceId { get; set; } // 回填时机?
    public string flowInstanceGuid { get; set; } // 回填时机?
    public EnumBizDocumentFlowState bizDocumentFlowState { get; set; }
      = EnumBizDocumentFlowState.Initial;
    public EnumBizState bizState { get; set; } = EnumBizState.Initial;
    public string WBSNo { get; set; }
    public string contactOfficePhone { get; set; }
    public string contactMobile { get; set; }
    public int departmentId { get; set; }
    public string departmentGuid { get; set; }
    public int departmentIdBelongTo { get; set; } // 例如IT实质是帮业务部门提交的项目采购申请
    public string departmentGuidBelongTo { get; set; }
    [ForeignKey("CostCenter")]
    public int costCenterId { get; set; }
    public virtual CostCenter CostCenter { get; set; }
    public DateTime? orderDate { get; set; }
    public DateTime? effectiveDate { get; set; }
    public string reason { get; set; }
    public string description { get; set; }
    public decimal? totalAmount { get; set; }
    [ForeignKey("CurrencyType")]
    public int? currencyTypeId { get; set; }
    public virtual CurrencyType CurrencyType { get; set; }
    [Precision(18, 6)]
    public decimal mainCurrencyRate { get; set; }
    public decimal? totalAmountInRMB { get; set; }
    public decimal? paidAmount { get; set; }
    public string remarkOfAprrovers { get; set; }
    public EnumPOType POType { get; set; } = EnumPOType.PO;
    public virtual List<PurchaseOrderDetail> details { get; set; }
    [ForeignKey("Vendor")]
    public int? vendorId { get; set; }
    public virtual Vendor Vendor { get; set; } // 被指定的供应商
    public string vendorTel { get; set; }
    public string vendorContactPerson { get; set; }
    public string receiverTel { get; set; } //收货人
    public string receiverContactPerson { get; set; }
    public string invoiceToTel { get; set; } //发票接受人
    public string invoiceToContactPerson { get; set; }
    public string transportTerm { get; set; }
    public string paymentTerm { get; set; }
    public bool isVisible { get; set; } = true;
    public string setInvisibleReason { get; set; }
    public DateTime? submitTime { get; set; }
    public string submitor { get; set; }
    public int submitorUserId { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_PurchaseOrderDetail")]
  public class PurchaseOrderDetail
  {
    [Key]
    public int purchaseOrderDetailId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("PurchaseOrder")]
    public int purchaseOrderId { get; set; }
    [Required]
    public virtual PurchaseOrder PurchaseOrder { get; set; }
    [ForeignKey("PurchaseReqDetail")]
    public int? purchaseReqDetailId { get; set; } // 可以为空, 业务上允许无对应PR Detail的PO/Contract Detail(即只进行总量控制或者无PR的PO不进行控制)
    public virtual PurchaseReqDetail PurchaseReqDetail { get; set; }
    [ForeignKey("PurchaseReq")]
    public int? purchaseReqId { get; set; } // 可以为空, 业务上允许无对应PR的PO/Contract Detail(即只进行总量控制或者无PR的PO不进行控制)
    public virtual PurchaseReq PurchaseReq { get; set; }
    public int lineNo { get; set; }
    [Required]
    public string itemName { get; set; }
    public string unitMeasure { get; set; }
    public decimal price { get; set; }
    public decimal quantity { get; set; }
    public decimal amount { get; set; } // should be quantity*price
    public decimal amountInRMB { get; set; } // should be quantity*price*mainCurrencyRate
    public decimal receivedQuantity { get; set; }
    public decimal paidAmount { get; set; }
    public string description { get; set; }
    public string WBSNo { get; set; }
    [ForeignKey("CostElement")]
    public int? costElementId { get; set; }
    public virtual CostElement CostElement { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_GoodsReceiving")]
  public class GoodsReceiving
  {
    [Key]
    public int goodsReceivingId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string documentNo { get; set; }
    [ForeignKey("PurchaseOrder")]
    public int? purchaseOrderId { get; set; } // 可以为空, 业务上可能发生针对PR的收货(但一般情况下GR都应该对应PO)
    public virtual PurchaseOrder PurchaseOrder { get; set; } // 对应的PO, 可以为空
    [ForeignKey("PurchaseReq")]
    public int? purchaseReqId { get; set; } // 可以为空
    public virtual PurchaseReq PurchaseReq { get; set; } // 对应的PR, 可以为空
    public int flowActionRequestId { get; set; }
    public string flowActionRequestGuid { get; set; }
    public int? flowInstanceId { get; set; } // 回填时机?
    public string flowInstanceGuid { get; set; } // 回填时机?
    public EnumBizDocumentFlowState bizDocumentFlowState { get; set; }
      = EnumBizDocumentFlowState.Initial;
    public EnumBizState bizState { get; set; } = EnumBizState.Initial;
    public virtual List<GoodsReceivingDetail> details { get; set; }
    public int departmentId { get; set; }
    public string departmentGuid { get; set; }
    public string receiver { get; set; } // 收货人
    public string checker { get; set; } // 验货人
    public DateTime? deliveryDate { get; set; } // 交货日期
    public string deliveryLocation { get; set; } // 交货地点
    public string shippingInfo { get; set; } // 运输信息
    public string trackingNo { get; set; } // 运单号
    public string weight { get; set; } // 重量
    public string packingSlipNo { get; set; } // 包装单号
    public string description { get; set; }
    public string remarkOfAprrovers { get; set; }
    [ForeignKey("Vendor")]
    public int? vendorId { get; set; }
    public virtual Vendor Vendor { get; set; } // 供应商
    public decimal? totalAmountInRMB { get; set; }
    public decimal? totalAmount { get; set; } //将由子表计算得出
    public bool isVisible { get; set; } = true;
    public string setInvisibleReason { get; set; }
    public DateTime? submitTime { get; set; }
    public string submitor { get; set; }
    public int submitorUserId { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
  }

  [Table("Enou_GoodsReceivingDetail")]
  public class GoodsReceivingDetail
  {
    [Key]
    public int goodsReceivingDetailId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("GoodsReceiving")]
    public int goodsReceivingId { get; set; }
    [Required]
    public virtual GoodsReceiving GoodsReceiving { get; set; }
    [ForeignKey("PurchaseOrderDetail")]
    public int? purchaseOrderDetailId { get; set; } // 可以为空, 业务上可能发生针对PR的收货(但一般情况下GR都应该对应PO)
    public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }
    [ForeignKey("PurchaseReqDetail")]
    public int? purchaseReqDetailId { get; set; } // 可以为空, 业务上可能发生针对PR的收货(但一般情况下GR都应该对应PO)
    public virtual PurchaseReqDetail PurchaseReqDetail { get; set; }
    public int lineNo { get; set; }
    [Required]
    public string itemName { get; set; }
    public string unitMeasure { get; set; }
    public decimal price { get; set; }
    public decimal quantity { get; set; } // 收货数量
    public decimal amount { get; set; } // should be quantity*price
    public decimal amountInRMB { get; set; } // should be quantity*price*mainCurrencyRate
    public string storageLocation { get; set; } // 存放位置
    public bool? isFixedAsset { get; set; }
    public string keeper { get; set; } // 保管者
    public string WBSNo { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_Payment")]
  public class Payment
  {
    [Key]
    public int paymentId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string documentNo { get; set; }
    [ForeignKey("PurchaseOrder")]
    public int? purchaseOrderId { get; set; } // 可以为空, 业务上可能发生针对PR的收货(但一般情况下GR都应该对应PO)
    public virtual PurchaseOrder PurchaseOrder { get; set; } // 对应的PO, 可以为空
    [ForeignKey("PurchaseReq")]
    public int? purchaseReqId { get; set; } // 可以为空
    public virtual PurchaseReq PurchaseReq { get; set; } // 对应的PR, 可以为空
    public int flowActionRequestId { get; set; }
    public string flowActionRequestGuid { get; set; }
    public int? flowInstanceId { get; set; } // 回填时机?
    public string flowInstanceGuid { get; set; } // 回填时机?
    public EnumBizDocumentFlowState bizDocumentFlowState { get; set; }
      = EnumBizDocumentFlowState.Initial;
    public EnumBizState bizState { get; set; } = EnumBizState.Initial;
    public virtual List<PaymentDetail> details { get; set; }
    public int departmentId { get; set; }
    public string departmentGuid { get; set; }
    public string reason { get; set; }
    public string description { get; set; }
    public string remarkOfAprrovers { get; set; }
    [ForeignKey("Vendor")]
    public int? vendorId { get; set; }
    public virtual Vendor Vendor { get; set; } // 供应商
    public string vendorBankName { get; set; }
    public string vendorBankAccount { get; set; }
    public string SWIFTCode { get; set; }
    public string applicantName { get; set; } // 付款申请人姓名, 可能并不是本人填写
    public string applicantEmail { get; set; }
    public string applicantPhone { get; set; }
    public EnumPaymentAreaType paymentAreaType { get; set; }
      = EnumPaymentAreaType.domestic;
    public EnumPaymentCurrencyType paymentCurrencyType { get; set; }
      = EnumPaymentCurrencyType.homeCurrency;
    public EnumPaymentMethodType paymentMethodType { get; set; }
      = EnumPaymentMethodType.bankTransfer;
    public string invoiceNo { get; set; }
    [ForeignKey("CurrencyType")]
    public int? currencyTypeId { get; set; }
    public virtual CurrencyType CurrencyType { get; set; }
    public decimal? totalAmountInRMB { get; set; }
    public decimal? totalAmount { get; set; } //将由子表计算得出
    [Precision(18, 6)]
    public decimal mainCurrencyRate { get; set; }
    public bool isDownPayment { get; set; } = false; //是否为订金?
    public bool isNormalPayment { get; set; } = true; //是否为正常付款?
    public bool isImmediatePayment { get; set; } = false; //是否为立即付款?
    public bool isAdvancePayment { get; set; } = false; //是否为提前付款?
    public int? payingDaysRequirement { get; set; } // 需在?天内付清
    public DateTime? paidTime { get; set; }
    public int registerPaidUserId { get; set; } // 登记已付的用户id
    public DateTime? registerPaidTime { get; set; } //登记已付的时间
    public bool isVisible { get; set; } = true;
    public string setInvisibleReason { get; set; }
    public DateTime? submitTime { get; set; }
    public string submitor { get; set; }
    public int submitorUserId { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_PaymentDetail")]
  public class PaymentDetail
  {
    [Key]
    public int paymentDetailDetailId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("Payment")]
    public int paymentId { get; set; }
    [Required]
    public virtual Payment Payment { get; set; }
    [ForeignKey("PurchaseOrderDetail")]
    public int? purchaseOrderDetailId { get; set; } // 可以为空, 业务上可能发生针对PR的付款(但一般情况下PM都应该对应PO)
    public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }
    [ForeignKey("PurchaseReqDetail")]
    public int? purchaseReqDetailId { get; set; } // 可以为空, 业务上可能发生针对PR的付款(但一般情况下PM都应该对应PO)
    public virtual PurchaseReqDetail PurchaseReqDetail { get; set; }
    public int lineNo { get; set; }
    [Required]
    public string itemName { get; set; }
    public string unitMeasure { get; set; }
    public decimal price { get; set; }
    public decimal quantity { get; set; } // 对应的付款数量
    public decimal amount { get; set; } // should be quantity*price
    public decimal amountInRMB { get; set; } // should be quantity*price*mainCurrencyRate
    public string WBSNo { get; set; }
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_Vendor")]
  public class Vendor
  {
    [Key]
    public int vendorId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    public string vendorOrgNo { get; set; }
    public string vendorNo { get; set; }
    public string chineseName { get; set; }
    public string englishName { get; set; }
    public EnumLocationType locationType { get; set; } = EnumLocationType.domestic;
    public EnumVendorClass vendorClass { get; set; } = EnumVendorClass.firstClass;
    public EnumVendorType vendorType { get; set; } = EnumVendorType.normal;
    public EnumVendorRelation vendorRelation { get; set; } = EnumVendorRelation.normal;
    public string chineseAddress { get; set; }
    public string englishAddress { get; set; }
    public string telphone { get; set; }
    public string email { get; set; }
    public string fax { get; set; }
    public string contactPerson { get; set; }
    public string contactPersonTel { get; set; }
    public string contactPersonEmail { get; set; }
    public string contactPersonTitle { get; set; }
    public string duns { get; set; }
    public string remark { get; set; }
    public bool isVisible { get; set; } = true;
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    public virtual List<VendorBank> banks { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_VendorBank")]
  public class VendorBank
  {
    [Key]
    public int vendorBankId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [ForeignKey("Vendor")]
    public int vendorId { get; set; }
    public virtual Vendor Vendor { get; set; }
    public string bankName { get; set; }
    public string bankAccount { get; set; }
    public string branchName { get; set; }
    public string SWIFTCode { get; set; }
    public string remark { get; set; }
    public bool isDefaultBank { get; set; } = true;
    public bool isVisible { get; set; } = true;
    public string creator { get; set; }
    public int creatorUserId { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public string updateUser { get; set; }
    public int updateUserId { get; set; }
    public DateTime? updateTime { get; set; }
    #region 自定义字段列表
    public int? intField_1 { get; set; }
    public int? intField_2 { get; set; }
    public int? intField_3 { get; set; }
    public int? intField_4 { get; set; }
    public int? intField_5 { get; set; }
    public int? intField_6 { get; set; }
    public int? intField_7 { get; set; }
    public int? intField_8 { get; set; }
    public int? intField_9 { get; set; }
    public int? intField_10 { get; set; }
    public string stringField_1 { get; set; }
    public string stringField_2 { get; set; }
    public string stringField_3 { get; set; }
    public string stringField_4 { get; set; }
    public string stringField_5 { get; set; }
    public string stringField_6 { get; set; }
    public string stringField_7 { get; set; }
    public string stringField_8 { get; set; }
    public string stringField_9 { get; set; }
    public string stringField_10 { get; set; }
    public decimal? decimalField_1 { get; set; }
    public decimal? decimalField_2 { get; set; }
    public decimal? decimalField_3 { get; set; }
    public decimal? decimalField_4 { get; set; }
    public decimal? decimalField_5 { get; set; }
    public decimal? decimalField_6 { get; set; }
    public decimal? decimalField_7 { get; set; }
    public decimal? decimalField_8 { get; set; }
    public decimal? decimalField_9 { get; set; }
    public decimal? decimalField_10 { get; set; }
    public DateTime? dateTimeField_1 { get; set; }
    public DateTime? dateTimeField_2 { get; set; }
    public DateTime? dateTimeField_3 { get; set; }
    public DateTime? dateTimeField_4 { get; set; }
    public DateTime? dateTimeField_5 { get; set; }
    #endregion
  }

  [Table("Enou_AttachFile")]
  public class AttachFile
  {
    [Key]
    public int attachFileId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    public EnumBizDocumentType bizDocumentType { get; set; }
    public int bizDocumentId { get; set; }
    public string bizDocumentGuid { get; set; }
    public string originalName { get; set; }
    public string serverName { get; set; }
    public string serverPath { get; set; }
    public int uploadUserId { get; set; }
    public string uploadUserGuid { get; set; }
    public DateTime uploadTime { get; set; } = DateTime.Now;
    public bool isVisible { get; set; } = true;
  }

  [Table("Enou_FunctionPermission")]
  public class FunctionPermission
  {
    [Key]
    public int functionPermissionId { get; set; }
    public string guid { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string code { get; set; }
    public string memo { get; set; }
    public bool availableToEveryOne { get; set; } = true;
    public bool isVisible { get; set; } = true;
    public virtual List<UserFunctionPermissionRelation>
      userFunctionPermissionRelations
    { get; set; }
    public virtual List<RoleFunctionPermissionRelation>
      roleFunctionPermissionRelations
    { get; set; }

    //该权限所属的所有用户列表
    public List<int> getUserIdsBelongTo(OPAS2DbContext db)
    {
      var allRelations = userFunctionPermissionRelations.Where(
        obj => obj.isValid == true).ToList();
      if (allRelations.Count() > 0)
      {
        return allRelations.Select(obj => obj.userId).ToList();
      }

      return new List<int>();
    }

    //该权限所属的所有角色列表
    public List<int> getRoleIdsBelongTo(OPAS2DbContext db)
    {
      var allRelations = roleFunctionPermissionRelations.Where(
        obj => obj.isValid == true).ToList();
      if (allRelations.Count() > 0)
      {
        return allRelations.Select(obj => obj.roleId).ToList();
      }

      return new List<int>();
    }
  }

  [Table("Enou_UserFunctionPermissionRelation")]
  public class UserFunctionPermissionRelation
  {
    [Key]
    public int userFunctionPermissionRelationId { get; set; }
    [Required]
    public int userId { get; set; }
    public string userGuid { get; set; }
    [ForeignKey("FunctionPermission")]
    public int functionPermissionId { get; set; }
    public virtual FunctionPermission FunctionPermission { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public bool isValid { get; set; } = true;
  }

  [Table("Enou_RoleFunctionPermissionRelation")]
  public class RoleFunctionPermissionRelation
  {
    [Key]
    public int roleFunctionPermissionRelationId { get; set; }
    [Required]
    public int roleId { get; set; }
    public string roleGuid { get; set; }
    [ForeignKey("FunctionPermission")]
    public int functionPermissionId { get; set; }
    public virtual FunctionPermission FunctionPermission { get; set; }
    public DateTime createTime { get; set; } = DateTime.Now;
    public bool isValid { get; set; } = true;
  }
}

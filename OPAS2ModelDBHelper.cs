using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OPAS2Model
{
  public class OPAS2ModelDBHelper
  {
    #region 表单号生成器
    public string generateDocumentSerialNo(
      EnumBizDocumentType bizDocumentType, string organizationCode,
      string locationCode, string yearCode, string departmentCode
      )
    {
      using (var db = new OPAS2DbContext())
      {
        var currentRecord = db.bizDocumentSerialNoGenerators.Where(
          record => record.bizDocumentType == bizDocumentType &&
            record.organizationCode == organizationCode &&
            record.locationCode == locationCode &&
            record.yearCode == yearCode &&
            record.departmentCode == departmentCode).FirstOrDefault();

        if (currentRecord == null) // No existing generator, need init
        {
          currentRecord = db.bizDocumentSerialNoGenerators.Create();

          currentRecord.name = BizSettings.getBizDocumentTypeCode(bizDocumentType) + organizationCode + locationCode + yearCode + departmentCode;
          currentRecord.bizDocumentType = bizDocumentType;
          currentRecord.organizationCode = organizationCode;
          currentRecord.locationCode = locationCode;
          currentRecord.yearCode = yearCode;
          currentRecord.departmentCode = departmentCode;

          db.bizDocumentSerialNoGenerators.Add(currentRecord);
          db.SaveChanges();

          currentRecord.currentNo = currentRecord.startNo;
        }
        else // generator exists
        {
          currentRecord.currentNo = currentRecord.currentNo + 1;
        }

        db.SaveChanges();

        return currentRecord.name +
          currentRecord.currentNo.ToString(
            "D" + currentRecord.numberPartLength);
      }
    }
    #endregion

    #region 用户功能权限相关
    public static void setFunctionPermissionUser(
      int id, int userId, string userGuid, OPAS2DbContext db)
    {
      var functionPermission = db.functionPermissions.Find(id);

      #region check whether relation already setted, then re-set to valid
      if (functionPermission.getUserIdsBelongTo(db).Contains(userId))
      {
        var r = functionPermission.userFunctionPermissionRelations.
          Where(x => x.userId == userId).
          ToList().FirstOrDefault();
        r.isValid = true;
        db.SaveChanges();
        return;
      }
      #endregion

      saveCreatedFunctionPermissionUserRelation(functionPermission,
        userId, userGuid, db);
    }

    public static void saveCreatedFunctionPermissionUserRelation(
      FunctionPermission functionPermission, int userId, string userGuid,
      OPAS2DbContext db)
    {
      //不能重复建立权限-用户隶属关系(但不限制用户可以同时属于多个权限)
      if (functionPermission.getUserIdsBelongTo(db).Contains(userId))
      {
        return;
      }

      var relation = db.userFunctionPermissionRelations.Create();
      functionPermission.userFunctionPermissionRelations.Add(relation);
      relation.FunctionPermission = functionPermission;
      relation.userId = userId;
      relation.userGuid = userGuid;

      db.userFunctionPermissionRelations.Add(relation);
      db.SaveChanges();
    }

    public static void unsetFunctionPermissionUser(int id, int userId,
      OPAS2DbContext db)
    {
      var functionPermission = db.functionPermissions.Find(id);

      if (functionPermission.getUserIdsBelongTo(db).Contains(userId))
      {
        var r = functionPermission.userFunctionPermissionRelations.
          Where(x => x.userId == userId).
          ToList().FirstOrDefault();
        r.isValid = false;
        db.SaveChanges();
        return;
      }
    }

    public static List<FunctionPermission> getDirectFunctionPermissionsOfUser(
      int userId, OPAS2DbContext db)
    {
      return db.functionPermissions.Where(obj =>
        obj.userFunctionPermissionRelations.Where(
          r => r.userId == userId).Count() > 0).ToList();
    }
    #endregion

    #region 角色功能权限设置
    public static void setFunctionPermissionRole(
      int id, int roleId, string roleGuid, OPAS2DbContext db)
    {
      var functionPermission = db.functionPermissions.Find(id);

      #region check whether relation already setted, then re-set to valid
      if (functionPermission.getRoleIdsBelongTo(db).Contains(roleId))
      {
        var r = functionPermission.roleFunctionPermissionRelations.
          Where(x => x.roleId == roleId).
          ToList().FirstOrDefault();
        r.isValid = true;
        db.SaveChanges();
        return;
      }
      #endregion

      saveCreatedFunctionPermissionRoleRelation(functionPermission,
        roleId, roleGuid, db);
    }

    public static void saveCreatedFunctionPermissionRoleRelation(
      FunctionPermission functionPermission, int roleId, string roleGuid,
      OPAS2DbContext db)
    {
      //不能重复建立权限-用户隶属关系(但不限制用户可以同时属于多个权限)
      if (functionPermission.getUserIdsBelongTo(db).Contains(roleId))
      {
        return;
      }

      var relation = db.roleFunctionPermissionRelations.Create();
      functionPermission.roleFunctionPermissionRelations.Add(relation);
      relation.FunctionPermission = functionPermission;
      relation.roleId = roleId;
      relation.roleGuid = roleGuid;

      db.roleFunctionPermissionRelations.Add(relation);
      db.SaveChanges();
    }

    public static void unsetFunctionPermissionRole(int id, int roleId,
      OPAS2DbContext db)
    {
      var functionPermission = db.functionPermissions.Find(id);

      if (functionPermission.getRoleIdsBelongTo(db).Contains(roleId))
      {
        var r = functionPermission.roleFunctionPermissionRelations.
          Where(x => x.roleId == roleId).
          ToList().FirstOrDefault();
        r.isValid = false;
        db.SaveChanges();
        return;
      }
    }

    public static List<FunctionPermission> getDirectFunctionPermissionsOfRole(
      int roleId, OPAS2DbContext db)
    {
      return db.functionPermissions.Where(obj =>
        obj.roleFunctionPermissionRelations.Where(
          r => r.roleId == roleId).Count() > 0).ToList();
    }
    #endregion

    #region 用户成本中心设置
    public static void setUserCostCenter(
      int costCenterId, int userId, string userGuid, OPAS2DbContext db)
    {
      var relation = db.costCenterUserRelations.Where(
        obj => obj.userId == userId && obj.costCenterId == costCenterId).FirstOrDefault();
      if (relation != null) // 指定的绑定关系已存在,可直接返回
      {
        relation.isVisible = true;
        db.SaveChanges();
        return;
      }

      relation = db.costCenterUserRelations.Where(
        obj => obj.userId == userId && obj.costCenterId != costCenterId).FirstOrDefault();
      if (relation != null) // 如果存在其他绑定关系,则需要先删除
      {
        db.costCenterUserRelations.Remove(relation);
        db.SaveChanges();
      }

      relation = db.costCenterUserRelations.Create();
      relation.CostCenter = db.costCenters.Find(costCenterId) ;
      relation.userId = userId;
      relation.userGuid = userGuid;
      db.costCenterUserRelations.Add(relation);

      db.SaveChanges();
    }

    public static void unsetUserCostCenter(int userId, int costCenterId,
      OPAS2DbContext db) //直接删除关系,一个用户只属于一个成本中心
    {
      var relation = db.costCenterUserRelations.Where(
        obj => obj.userId == userId && 
              obj.costCenterId == costCenterId).FirstOrDefault();
      if (relation != null)
      {
        db.costCenterUserRelations.Remove(relation);
        db.SaveChanges();
      }
    }

    public static CostCenter getUserCostCenter(
      int userId, OPAS2DbContext db)
    {
      CostCenter result = null;
      var relation = db.costCenterUserRelations.Where(
        obj => obj.userId == userId && 
                obj.isVisible==true).FirstOrDefault();
      if (relation!=null)
      {
        result = relation.CostCenter;
      }
      return result;
    }
    #endregion

    #region 获取单据对象相关
    public static PurchaseReq getPR(string guid, OPAS2DbContext db)
    {
      return db.purchaseReqs.Where(
        pr => pr.guid == guid).FirstOrDefault();
    }

    public static PurchaseReq getPR(int id, OPAS2DbContext db)
    {
      return db.purchaseReqs.Find(id);
    }

    public static PurchaseOrder getPO(string guid, OPAS2DbContext db)
    {
      return db.purchaseOrders.Where(
        po => po.guid == guid).FirstOrDefault();
    }

    public static PurchaseOrder getPO(int id, OPAS2DbContext db)
    {
      return db.purchaseOrders.Find(id);
    }

    public static GoodsReceiving getGR(string guid, OPAS2DbContext db)
    {
      return db.goodsReceivings.Where(
        gr => gr.guid == guid).FirstOrDefault();
    }

    public static GoodsReceiving getGR(int id, OPAS2DbContext db)
    {
      return db.goodsReceivings.Find(id);
    }

    public static Payment getPM(string guid, OPAS2DbContext db)
    {
      return db.payments.Where(
        gr => gr.guid == guid).FirstOrDefault();
    }

    public static Payment getPM(int id, OPAS2DbContext db)
    {
      return db.payments.Find(id);
    }

    #endregion
  }
}

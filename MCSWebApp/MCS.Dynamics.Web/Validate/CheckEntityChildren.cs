using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;

namespace MCS.Dynamics.Web.Validate
{
    public class CheckEntityChildren
    {
        public static string CheckSelectEntities(params string[] ids)
        {
            string result = string.Empty;

            //List<string> ids = ids.ToList()();//.Split(',').ToList<string>();
            List<string> errors = new List<string>();
            foreach (var item in ids)
            {
                string error = string.Empty;
                //根据id获取实体
                var entity = DESchemaObjectAdapter.Instance.Load(item) as DynamicEntity;
                var childs = entity.Fields.Where(p => p.FieldType == Library.SOA.DataObjects.Dynamics.Enums.FieldTypeEnum.Collection);
                foreach (var child in childs)
                {
                    var childID = child.ReferenceEntity.ID;
                    if (!ids.Contains(childID))
                    {
                        //记录下来没有带子表的记录
                        error += string.Format("    {0}\r\n", child.ReferenceEntity.CodeName);
                    }
                }
                if (!string.IsNullOrEmpty(error))
                {
                    error = string.Format("实体 {0}的行项目\r\n{1}\r\n没有勾选\r", entity.CodeName, error);
                    errors.Add(error);
                }


            }
            foreach (var item in errors)
            {
                result += "\r\n" + item;
            }
            result.TrimStart('\r').TrimStart('\n');
            return result;
        }

        public static string CheckSelectMoveEntities(params string[] ids)
        {
            string result = string.Empty;
            List<string> errors = new List<string>();
            foreach (var item in ids)
            {
                string error = string.Empty;
                //根据id获取实体
                var entity = DESchemaObjectAdapter.Instance.Load(item) as DynamicEntity;

                #region 判断是否包含了子表
                var childs = entity.Fields.Where(p => p.FieldType == Library.SOA.DataObjects.Dynamics.Enums.FieldTypeEnum.Collection);
                foreach (var child in childs)
                {
                    var childID = child.ReferenceEntity.ID;
                    if (!ids.Contains(childID))
                    {
                        //记录下来没有带子表的记录
                        error += string.Format("    {0}\r\n", child.ReferenceEntity.CodeName);
                    }
                }
                if (!string.IsNullOrEmpty(error))
                {
                    error = string.Format("实体 {0}的行项目\r\n{1}\r\n没有勾选\r", entity.CodeName, error);
                }
                #endregion

                if (string.IsNullOrEmpty(error))
                {
                    //判断是否包含主表
                    List<DynamicEntityField> fields = DEDynamicEntityFieldSnapShotAdapter.Instance.LoadByRefferanceCodeName(entity.CodeName);
                    if (fields.Count != 0)
                    {
                        var exist = fields.Where(p => !ids.Contains(p.Entity.ID));
                        if (exist.Any())
                        {
                            //记录下来没有带主表的子表记录
                            foreach (var f in exist)
                            {
                                error += string.Format("    {0}\r\n", f.Entity.CodeName);
                            }

                        }
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        error = string.Format("实体 {0}的主表\r\n{1}\r\n没有勾选\r", entity.CodeName, error);
                    }
                }
                
                if (!string.IsNullOrEmpty(error))
                {
                    errors.Add(error);
                }

            }
            foreach (var item in errors)
            {
                result += "\r\n" + item;
            }
            result.TrimStart('\r').TrimStart('\n');
            return result;
        }

        public static List<string> CheckCopyEntities(string[] param)
        {
            List<string> ListIDs = new List<string>();

            List<string> ids = param.ToList<string>();
            List<string> errorIDs = new List<string>();
            foreach (var item in ids)
            {
                string error = string.Empty;
                //根据id获取实体
                var entity = DESchemaObjectAdapter.Instance.Load(item) as DynamicEntity;
                var childs = entity.Fields.Where(p => p.FieldType == Library.SOA.DataObjects.Dynamics.Enums.FieldTypeEnum.Collection);
                foreach (var child in childs)
                {
                    var childID = child.ReferenceEntity.ID;
                    if (!ids.Contains(childID))
                    {
                        errorIDs.Add(entity.ID);
                        break;
                    }
                }

            }

            List<string> resultList = new List<string>();
            resultList.AddRange(ListIDs);

            foreach (var item in ListIDs)
            {
                var entity = DESchemaObjectAdapter.Instance.Load(item) as DynamicEntity;
                var childs = entity.Fields.Where(p => p.FieldType == Library.SOA.DataObjects.Dynamics.Enums.FieldTypeEnum.Collection);
                foreach (var child in childs)
                {
                    resultList.Add(child.ReferenceEntity.ID);
                }
            }

            return ListIDs;
        }
    }
}
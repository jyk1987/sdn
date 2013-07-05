using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDN2.VO;

namespace SDN2.DB.Utility
{
    /// <summary>
    /// 数据对象持久化工具类
    /// </summary>
    internal class GObjectPersistent
    {
        /// <summary>
        /// 保存数据对象
        /// </summary>
        /// <param name="obj">通用数据对象</param>
        /// <returns></returns>
        internal static GObject Save(GObject obj)
        {
            return obj;
        }

        /// <summary>
        /// 删除数据对象（需要删除的数据对象必须包含主键）
        /// </summary>
        /// <param name="obj">数据对象</param>
        /// <returns></returns>
        internal static bool Del(GObject obj)
        {
            return false;
        }

        /// <summary>
        /// 删除数据对象
        /// </summary>
        /// <param name="entity">实体名</param>
        /// <param name="pk">主键</param>
        /// <returns></returns>
        internal static bool Del(string entity, string pk)
        {
            return false;
        }

        /// <summary>
        /// 修改数据对象（需要修改的数据对象必须包含主键）
        /// </summary>
        /// <param name="obj">数据对象</param>
        /// <returns></returns>
        internal static GObject Update(GObject obj)
        {
            return obj;
        }

        /// <summary>
        /// 暂不实现
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static GObject Select(GObject obj)
        {
            return obj;
        }
    }
}

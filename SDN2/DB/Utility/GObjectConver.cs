using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDN2.VO;
using System.Data.Common;

namespace SDN2.DB.Utility
{
    /// <summary>
    /// Gobject转换器
    /// jyk
    /// 2011/4/8
    /// </summary>
    internal class GObjectConver
    {
        /// <summary>
        /// Readers to G object.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public static GObject ReaderToGObject(DbDataReader reader)
        {
            GObject result = null;
            if (!reader.HasRows)
            {

            }

            try
            {
                reader.Close();
                reader = null;
            }
            catch (Exception)
            {
                reader = null;
            }
            return result;
        }
    }
}

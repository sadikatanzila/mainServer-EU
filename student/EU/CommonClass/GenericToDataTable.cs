/**
 * File name: GenericToDataTable.cs
 * Author: Anjan Kumar Paul
 * Date: May 22, 2010
 * 
 * Modification history:
 * Name                                 Date                              Desc
 * Anjan Kumar Paul					May 22, 2010				        Created
 * Version: 1.0
  */

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace EUPortalWeb.CommonClass
{
    public class GenericToDataTable : IDisposable
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        } // end Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return; // we're being collected, so let the GC take care of this object
        } 

        public GenericToDataTable()
        { 

        }
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name=”T”>Custome Class </typeparam>
        /// <param name=”lst”>List Of The Custome Class</param>
        /// <returns> Return the class datatbl </returns>
        public DataTable ConvertTo<T>(IList<T> lst)
        {
            //create DataTable Structure
            DataTable tbl = CreateTable<T>();
            Type entType = typeof(T);

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            //get the list item and add into the list
            foreach (T item in lst)
            {
                DataRow row = tbl.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                tbl.Rows.Add(row);
            }

            return tbl;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name=”T”>Custome Class</typeparam>
        /// <returns></returns>
        public DataTable CreateTable<T>()
        {
            //T –> ClassName
            Type entType = typeof(T);
            //set the datatable name as class name
            DataTable tbl = new DataTable(entType.Name);
            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            foreach (PropertyDescriptor prop in properties)
            {
                //add property as column
                tbl.Columns.Add(prop.Name, prop.PropertyType);
            }
            return tbl;
        }
    }
}
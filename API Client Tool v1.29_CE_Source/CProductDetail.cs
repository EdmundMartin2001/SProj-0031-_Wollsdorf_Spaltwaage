using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MTTS.IND890.CE
{
    public class CProductDetail 
    {
        //Private variable.
        private int m_ProductNumber;
        private string m_ProductName;
        private string m_ProductWeight;
        private string m_ActualProductWeight;
        private byte m_Scale;

        #region properties
        /// <summary>
        /// 
        /// </summary>
        public int ProductNumber
        {
            get
            {
                return m_ProductNumber;
            }
            set
            {
                m_ProductNumber = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            get
            {
                return m_ProductName;
            }
            set
            {
                m_ProductName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductWeight
        {
            set
            {
                m_ProductWeight = value;
            }
            get
            {
                return m_ProductWeight;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ActualProductWeight
        {
            set
            {
                m_ActualProductWeight = value;
            }
            get
            {
                return m_ActualProductWeight;
            }
        }
        /// <summary>
        ///  Get and set scale value.
        /// </summary>
        public byte Scale
        {
            set
            {
                m_Scale = value;
            }
            get
            {
                return m_Scale;
            }
        }
        #endregion
    }
}

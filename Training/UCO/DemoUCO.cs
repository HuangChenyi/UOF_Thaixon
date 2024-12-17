using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Training.PO;
using Training.Data;
using Ede.Uof.EIP.Organization.Util;

namespace Training.UCO
{
    public  class DemoUCO
    {
        DemoPO m_DemoPO = new DemoPO();


        public OrderDataSet GetOrderListByOrderID(string OrderID)
        {
            return m_DemoPO.GetOrderListByOrderID(OrderID);
        }

        public OrderDataSet GetOrderList(string customerID)
        {
           return  m_DemoPO.GetOrderList(customerID);
        }
        public DataTable GetUserData(string groupId)
        {
            return m_DemoPO.GetUserData(groupId);
        }

        public void InsertTaskData(DataRow dr)
        {
            m_DemoPO.InsertTaskData(dr);
        }

      

        public DataTable GetUserData()
        {
            return m_DemoPO.GetUserData();
        }

        public void InsertWsEndFormData(DataRow dr)
        {
            m_DemoPO.InsertWsEndFormData(dr);
        }


        public void InsertDDLStartFormData(DemoDataSet.TB_DEMO_DLL_FORMRow dr)
        {
            m_DemoPO.InsertDDLStartFormData(dr);
        }

        public void UpdateFormStatus(string docNbr, string signStatus)
        {
            m_DemoPO.UpdateFormStatus(docNbr, signStatus);
        }

        public void UpdateFormResult(string docNbr, string formResult)
        {
            m_DemoPO.UpdateFormResult(docNbr, formResult);
        }

        public SapB1DataSet GetSapB1Form()
        {
            return m_DemoPO.GetSapB1Form();
        }

        internal string GetFormVersionId(string formName)
        {
            return m_DemoPO.GetFormVersionId(formName);
        }

        internal void UpdateSapB1FormStatus(int docEntry, string status)
        {
            m_DemoPO.UpdateSapB1FormStatus(docEntry, status);
            UserUCO userUCO = new UserUCO();
            var uid= userUCO.GetGUIDByEmpNo("3815");
            var ebUser = userUCO.GetEBUser(uid);
            var account = ebUser.Account;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data;

namespace Training.PO
{
    internal class DemoPO :Ede.Uof.Utility.Data.BasePersistentObject
    {

        internal DataTable GetGoLiveData(string id)
        {

            string GTConnectStr= System.Configuration.
                ConfigurationManager.ConnectionStrings["GT_GOLIVE"].ConnectionString;
            m_db=new Ede.Uof.Utility.Data.DatabaseHelper(GTConnectStr);
            string cmdTxt = @"SELECT * FROM TB_DEMO_GO_LIVE
                            WNERE ID=@ID";

            this.m_db.AddParameter("@ID", id);

            DataTable dt = new DataTable();
            dt.Load(this.m_db.ExecuteReader(cmdTxt));

            return dt;
        }
   
        internal DataTable GetUserData(string groupId)
        {
           

            string cmdTxt = @"SELECT ACCOUNT ,
                                     NAME 
                            FROM
                                TB_EB_USER 
                            INNER JOIN TB_EB_EMPL_DEP
                                ON TB_EB_USER.USER_GUID = TB_EB_EMPL_DEP.USER_GUID
                            WHERE GROUP_ID = @GROUP_ID
                                ";

            this.m_db.AddParameter("@GROUP_ID", groupId);

            DataTable dt = new DataTable();

            dt.Load(this.m_db.ExecuteReader(cmdTxt));

            return dt;

        }

      
        internal void InsertTaskData(DataRow dr)
        {
            string cmdTxt = @"  INSERT INTO [dbo].[TB_DEMO_TASK]  
(	 [FILE_NAME] , 
	 [TYPE_NAME] , 
	 [EXECUTE_TIME] , 
	 [PARAMS]  
) 
 VALUES 
 (	 @FILE_NAME , 
	 @TYPE_NAME , 
	 @EXECUTE_TIME , 
	 @PARAMS  
)";

            this.m_db.AddParameter("@FILE_NAME", dr["FILE_NAME"]);
            this.m_db.AddParameter("@TYPE_NAME", dr["TYPE_NAME"]);
            this.m_db.AddParameter("@EXECUTE_TIME", Convert.ToDateTime( dr["EXECUTE_TIME"]));
            this.m_db.AddParameter("@PARAMS", dr["PARAMS"]);

            this.m_db.ExecuteNonQuery(cmdTxt);

        }

        internal DataTable GetUserData()
        {
            string cmdTxt = @"SELECT ACCOUNT ,
                                     NAME ,TB_EB_USER.USER_GUID
                            FROM
                                TB_EB_USER 
                            INNER JOIN TB_EB_EMPL_DEP
                                ON TB_EB_USER.USER_GUID = TB_EB_EMPL_DEP.USER_GUID
                
                                ";



            DataTable dt = new DataTable();

            dt.Load(this.m_db.ExecuteReader(cmdTxt));

            return dt;
        }

        internal void InsertWsEndFormData(System.Data.DataRow dr)
        {
            string cmdTxt = @"  INSERT INTO [dbo].[TB_DEMO_WS_FORM]  
                                (	 [ID] , 
	                                 [ITEM_QTY] , 
	                                 [ITEM_PRICE] , 
	                                 [ITEM] , 
	                                 [DOC_NBR] , 
	                                 [FORM_RESULT]  
                                ) 
                                 VALUES 
                                 (	 @ID , 
	                                 @ITEM_QTY , 
	                                 @ITEM_PRICE , 
	                                 @ITEM , 
	                                 @DOC_NBR , 
	                                 @FORM_RESULT  
                                )";

            this.m_db.AddParameter("@ID", dr["ID"]);
            this.m_db.AddParameter("@ITEM_QTY", dr["ITEM_QTY"]);
            this.m_db.AddParameter("@ITEM_PRICE", dr["ITEM_PRICE"]);
            this.m_db.AddParameter("@ITEM", dr["ITEM"]);
            this.m_db.AddParameter("@DOC_NBR", dr["DOC_NBR"]);
            this.m_db.AddParameter("@FORM_RESULT", dr["FORM_RESULT"]);

            this.m_db.ExecuteNonQuery(cmdTxt);

        }

        internal void InsertDDLStartFormData(DemoDataSet.TB_DEMO_DLL_FORMRow dr)
        {
            string cmdTxt = @"  INSERT INTO [dbo].[TB_DEMO_DLL_FORM]  
                                        (	 [ID] , 
	                                         [ITEM_QTY] , 
	                                         [ITEM_PRICE] , 
	                                         [ITEM] , 
	                                         [DOC_NBR] , 
	                                         [SIGN_STATIS] , 
	                                         [REMARK]  
                                        ) 
                                         VALUES 
                                         (	 @ID , 
	                                         @ITEM_QTY , 
	                                         @ITEM_PRICE , 
	                                         @ITEM , 
	                                         @DOC_NBR , 
	                                         @SIGN_STATIS , 
	                                         @REMARK  
                                        )";
           
            this.m_db.AddParameter("@ID", dr.ID);
            this.m_db.AddParameter("@ITEM_QTY", dr.ITEM_QTY);
            this.m_db.AddParameter("@ITEM_PRICE", dr.ITEM_PRICE);
            this.m_db.AddParameter("@ITEM", dr.ITEM);
            this.m_db.AddParameter("@DOC_NBR", dr.DOC_NBR);
            this.m_db.AddParameter("@SIGN_STATIS", dr.SIGN_STATIS);
            this.m_db.AddParameter("@REMARK", dr.REMARK);

            this.m_db.ExecuteNonQuery(cmdTxt);
        }

        internal void UpdateFormStatus(string docNbr, string signStatus)
        {
            string cmdTxt = @"  UPDATE [dbo].[TB_DEMO_DLL_FORM]  
                             SET 
	                             [SIGN_STATIS] = @SIGN_STATIS  

                            WHERE 
	                            [DOC_NBR] = @DOC_NBR";

            this.m_db.AddParameter("@SIGN_STATIS", signStatus);
            this.m_db.AddParameter("@DOC_NBR", docNbr);

            this.m_db.ExecuteNonQuery(cmdTxt);
        }

        internal void UpdateFormResult(string docNbr, string formResult)
        {
            string cmdTxt = @"  UPDATE [dbo].[TB_DEMO_DLL_FORM]  
                             SET 
	                             [FORM_RESULT] = @FORM_RESULT  

                            WHERE 
	                            [DOC_NBR] = @DOC_NBR";

            this.m_db.AddParameter("@FORM_RESULT", formResult);
            this.m_db.AddParameter("@DOC_NBR", docNbr);

            this.m_db.ExecuteNonQuery(cmdTxt);
        }

        internal OrderDataSet GetOrderList(string customerID)
        {
            
            string connStr= System.Configuration.ConfigurationManager.ConnectionStrings["connTOERP"].ConnectionString;
            m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connStr);

            string cmdTxt = @"SELECT  [OrderID]
      ,[CustomerID]
      ,[EmployeeID]
      ,[OrderDate]
      ,[RequiredDate]
      ,[ShippedDate]
      ,[ShipVia]
      ,[Freight]
      ,[ShipName]
      ,[ShipAddress]
      ,[ShipCity]
      ,[ShipRegion]
      ,[ShipPostalCode]
      ,[ShipCountry]
  FROM [dbo].[Orders]
WHERE CustomerID LIKE @CustomerID";


        
            m_db.AddParameter("CustomerID", $"%{customerID}%");
            OrderDataSet ds = new OrderDataSet();
            ds.Load(m_db.ExecuteReader(cmdTxt), LoadOption.OverwriteChanges, ds.Orders);


            m_db = new Ede.Uof.Utility.Data.DatabaseHelper();
            return ds;
        }

        internal OrderDataSet GetOrderListByOrderID(string OrderID)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connTOERP"].ConnectionString;
            m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connStr);

            string cmdTxt = @"SELECT  [OrderID]
      ,[CustomerID]
      ,[EmployeeID]
      ,[OrderDate]
      ,[RequiredDate]
      ,[ShippedDate]
      ,[ShipVia]
      ,[Freight]
      ,[ShipName]
      ,[ShipAddress]
      ,[ShipCity]
      ,[ShipRegion]
      ,[ShipPostalCode]
      ,[ShipCountry]
  FROM [dbo].[Orders]
WHERE OrderID = @OrderID";



            m_db.AddParameter("OrderID", OrderID);
            OrderDataSet ds = new OrderDataSet();
            ds.Load(m_db.ExecuteReader(cmdTxt), LoadOption.OverwriteChanges, ds.Orders);


            m_db = new Ede.Uof.Utility.Data.DatabaseHelper();
            return ds;
        }

        internal SapB1DataSet GetSapB1Form()
        {
            string cmdTxt = @"SELECT [DocEntry]
      ,[DocNum]
      ,[DocDate]
      ,[DocDueDate]
      ,[CardCode]
      ,[CardName]
      ,[NumAtCard]
      ,[DocCur]
      ,[DocTotal]
      ,[DocTotalFC]
      ,[SlpCode]
      ,[DeptCode]
      ,[STATUS]
  FROM [dbo].[SAPB1_OINV_APPROVALS_PENDING]
  WHERE STATUS=2
";

            SapB1DataSet ds = new SapB1DataSet();
            ds.Load(this.m_db.ExecuteReader(cmdTxt), LoadOption.OverwriteChanges,
                ds.SAPB1_OINV_APPROVALS_PENDING);

            return ds;
        }

        internal string GetFormVersionId(string formName)
        {
            string cmdTxt = @"SELECT USING_VERSION_ID FROM TB_WKF_FORM
WHERE FORM_NAME=@FORM_NAME";

            this.m_db.AddParameter("@FORM_NAME", formName);

            return this.m_db.ExecuteScalar(cmdTxt).ToString();
        }

        internal void UpdateSapB1FormStatus(int docEntry, string status)
        {
            string cmdTxt = @"UPDATE [dbo].[SAPB1_OINV_APPROVALS_PENDING]
    SET [STATUS] = @STATUS
    WHERE DocEntry=@DocEntry";

            this.m_db.AddParameter("@STATUS", status);
            this.m_db.AddParameter("@DocEntry", docEntry);

            this.m_db.ExecuteNonQuery(cmdTxt);
        }
    }
}

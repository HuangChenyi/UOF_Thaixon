using Ede.Uof.Utility.Log;
using Ede.Uof.Utility.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Training.Data;
using Training.UCO;


namespace Training.SapTask
{
    public class SapB1FormTask : BaseTask
    {
        public override void Run(params string[] args)
        {
            DemoUCO demoUCO = new DemoUCO();
            //ReadTable
            SapB1DataSet ds = demoUCO.GetSapB1Form();
            //CrateXml
            foreach(var dr in ds.SAPB1_OINV_APPROVALS_PENDING)
            {
                //CreateXml
                string formInfo = GetFormInfo(dr);
                //SendForm

                Ede.Uof.WKF.Utility.TaskUtilityUCO taskUco = new Ede.Uof.WKF.Utility.TaskUtilityUCO();
                string result = taskUco.WebService_CreateTask(formInfo);

                Logger.Write("SAPB1FormTask", result);

                XElement resultXe = XElement.Parse(result);

                if(resultXe.Element("Status").Value=="1")
                {
                    string formNumber = resultXe.Element("FormNumber").Value;
                    string taskId = resultXe.Element("TaskId").Value;
                    //Save FormNumber and TaskId

                    //Update Status
                    demoUCO.UpdateSapB1FormStatus(dr.DocEntry, resultXe.Element("Status").Value);
                }
                else
                {
                    string errorMsg = resultXe.Element("Exception").Element("Message").Value;
                    //Save ErrorMsg

                    //Update Status
                    demoUCO.UpdateSapB1FormStatus(dr.DocEntry, resultXe.Element("Status").Value);

                }


            }
      

        }

        private string GetFormInfo(SapB1DataSet.SAPB1_OINV_APPROVALS_PENDINGRow dr)
        {
            //<Form formVersionId="5c3ceef1-c855-43b4-bb1d-4f84aff4135c" urgentLevel="0">
            // <Applicant account="tony" groupId="" jobTitleId="">
            //  <Comment></Comment>
            // </Applicant>
            //  <FormFieldValue>
            //    <FieldItem fieldId="DocNum" fieldValue="" realValue="" enableSearch="True"  IsNeedAutoNbr="false" />
            //    <FieldItem fieldId="DocDate" fieldValue="2024/12/17" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="DocDueDate" fieldValue="2024/12/17" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="CardCode" fieldValue="CE002" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="CardName" fieldValue="CE002" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="NumAtCard" fieldValue="CE002" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="DocCur" fieldValue="JPY" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="DocTotal" fieldValue="100" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="DocTotalFC" fieldValue="200" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="SlpCode" fieldValue="10248" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //    <FieldItem fieldId="DeptCode" fieldValue="10248" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //  </FormFieldValue>
            //</Form>

            DemoUCO demoUCO = new DemoUCO();
            string formVersionId = demoUCO.GetFormVersionId("SAP1");
            XElement formXe = new XElement("Form"
                , new XAttribute("formVersionId", formVersionId)
                , new XAttribute("urgentLevel", "2"));

            //Applicant Get UOFAccount (dr.SlpCode)
            // This Exam is Tony
            XElement applicantXe = new XElement("Applicant",
                new XAttribute("account", "tony"),
                new XAttribute("groupId", ""),
                new XAttribute("jobTitleId", ""));

            XElement commentXe = new XElement("Comment", "");

            XElement formFieldValueXe = new XElement("FormFieldValue");

            XElement DocNumXE = GetFieldItem(dr, "DocNum");
            DocNumXE.Add(new XAttribute("IsNeedAutoNbr", "true"));
            XElement DocDateXE = GetFieldItem(dr, "DocDate");
            XElement DocDueDateXE = GetFieldItem(dr, "DocDueDate");
            XElement CardCodeXE = GetFieldItem(dr, "CardCode");
            XElement CardNameXE = GetFieldItem(dr, "CardName");
            XElement NumAtCardXE = GetFieldItem(dr, "NumAtCard");
            XElement DocCurXE = GetFieldItem(dr, "DocCur");
            XElement DocTotalXE = GetFieldItem(dr, "DocTotal");
            XElement DocTotalFCXE = GetFieldItem(dr, "DocTotalFC");
            XElement SlpCodeXE = GetFieldItem(dr, "SlpCode");
            XElement DepCodeXE = GetFieldItem(dr, "DeptCode");

            formXe.Add(applicantXe);
            formXe.Add(formFieldValueXe);

            applicantXe.Add(commentXe);

            formFieldValueXe.Add(DocNumXE);
            formFieldValueXe.Add(DocDateXE);
            formFieldValueXe.Add(DocDueDateXE);
            formFieldValueXe.Add(CardCodeXE);
            formFieldValueXe.Add(CardNameXE);
            formFieldValueXe.Add(NumAtCardXE);
            formFieldValueXe.Add(DocCurXE);
            formFieldValueXe.Add(DocTotalXE);
            formFieldValueXe.Add(DocTotalFCXE);
            formFieldValueXe.Add(SlpCodeXE);
            formFieldValueXe.Add(DepCodeXE);
                 

            return formXe.ToString();
        }

        private XElement GetFieldItem(SapB1DataSet.SAPB1_OINV_APPROVALS_PENDINGRow dr,
            string fieldId)
        {
            XElement fieldItemXe = new XElement("FieldItem"
                , new XAttribute("fieldId", fieldId),
                new XAttribute("fieldValue", dr[fieldId].ToString()),
                new XAttribute("realValue", ""),
                new XAttribute("enableSearch", "true"));
         

            return  fieldItemXe;
        }
    }
}

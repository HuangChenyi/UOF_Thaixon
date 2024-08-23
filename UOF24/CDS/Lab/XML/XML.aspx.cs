using Ede.Uof.Utility.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class CDS_Lab_XML_XML : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument xmldoc=new XmlDocument();

        XmlElement formElement = xmldoc.CreateElement("Form");
        formElement.SetAttribute("formVersionId","0418");
        formElement.SetAttribute("urgentLevel","2");

        XmlElement applicantElement = xmldoc.CreateElement("Applicant");
        applicantElement.SetAttribute("account","Tony");
        applicantElement.SetAttribute("groupId","");
        applicantElement.SetAttribute("jobTitleId","");

        XmlElement commentElement = xmldoc.CreateElement("Comment");
        commentElement.InnerText = "私事處理";

        XmlElement formFieldValueElement = xmldoc.CreateElement("FormFieldValue");

        XmlElement noElement = xmldoc.CreateElement("FieldItem");
        noElement.SetAttribute("fieldId","NO");
        noElement.SetAttribute("fieldValue","A001");

        XmlElement a01Element = xmldoc.CreateElement("FieldItem");
        a01Element.SetAttribute("fieldId", "A01");
        a01Element.SetAttribute("fieldValue", "事假");

        XmlElement a02Element = xmldoc.CreateElement("FieldItem");
        a02Element.SetAttribute("fieldId", "A02");
        a02Element.SetAttribute("fieldValue", "2018/04/18");

        formElement.AppendChild(applicantElement);
        formElement.AppendChild(formFieldValueElement);

        applicantElement.AppendChild(commentElement);

        formFieldValueElement.AppendChild(noElement);
        formFieldValueElement.AppendChild(a01Element);
        formFieldValueElement.AppendChild(a02Element);

        xmldoc.AppendChild(formElement);
        txtXML.Text = xmldoc.OuterXml;

        ////<ContactList/>
        //XmlElement contactListElement = xmldoc.CreateElement("ContactList");

        ////<Contact name='Emma' Phone='0912345678'/>
        //XmlElement contact01Element = xmldoc.CreateElement("Contact");
        //contact01Element.SetAttribute("name","Emma");
        //contact01Element.SetAttribute("Phone", "0912345678");

        ////<Contact name='Tommy' Phone='095656565'/>
        //XmlElement contact02Element = xmldoc.CreateElement("Contact");
        //contact02Element.SetAttribute("name", "Tommy");
        //contact02Element.SetAttribute("Phone", "095656565");

        ////<Contact name='David' Phone='0910101010'/>
        //XmlElement contact03Element = xmldoc.CreateElement("Contact");
        //contact03Element.SetAttribute("name", "David");
        //contact03Element.SetAttribute("Phone", "0910101010");

        //contactListElement.AppendChild(contact01Element);
        //contactListElement.AppendChild(contact02Element);
        //contactListElement.AppendChild(contact03Element);

        //xmldoc.AppendChild(contactListElement);
        //txtXML.Text=xmldoc.OuterXml;

    }

    protected void btnGetValue_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc =new XmlDocument();
        xmlDoc.LoadXml(txtXML.Text);

        var node = xmlDoc.SelectSingleNode($"./Form/FormFieldValue/FieldItem[@fieldId='{txtID.Text}']");

        txtValue.Text = node.Attributes["fieldValue"].Value;

        //var node = xmlDoc.SelectSingleNode($"./ContactList/Contact[@name='{txtID.Text}']");
        //txtValue.Text = node.Attributes["Phone"].Value; 
    }
}
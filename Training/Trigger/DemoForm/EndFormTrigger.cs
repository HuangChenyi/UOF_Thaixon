using Ede.Uof.Utility.Configuration;
using Ede.Uof.Utility.FileCenter.V3;
using Ede.Uof.WKF.ExternalUtility;
using System;
using System.Security.Cryptography;
using System.Text;
using Training.UCO;

namespace Training.Trigger.DemoForm
{
    public class EndFormTrigger : ICallbackTriggerPlugin
    {
        public void Finally()
        {
            //  throw new NotImplementedException();
        }

        public string GetFormResult(ApplyTask applyTask)
        {
            // throw new NotImplementedException();

            //<Form formVersionId="30d33f52-802f-49b3-933e-f93a9c5d61cb">
            //  <FormFieldValue>
            //    <FieldItem fieldId="NO" fieldValue="" realValue="" />
            //    <FieldItem fieldId="A01" fieldValue="xxx" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A02" fieldValue="3" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A03" fieldValue="4" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A04" fieldValue="222" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //  </FormFieldValue>
            //</Form>

            DemoUCO uco = new DemoUCO();
            string docNbr = applyTask.FormNumber;
            string signStatus = applyTask.FormResult.ToString();

            uco.UpdateFormResult(docNbr, signStatus);

            //同意的時候才能新增文件
            if (applyTask.FormResult == Ede.Uof.WKF.Engine.ApplyResult.Adopt)
            {
                string publicKey = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPjJoSFdMNURxQmpabnROd2pCUW14ZDB6WnJ3ZldzeXl5K1dhR0VPV3cxL0JiZXRhc3BmaENKSVFnQTFQU2Jvd0NlZG10V2FsOUw1eHg2UFcyRU9hNWJZU3RUQ1NWU29FZTA1eFVQeS9yaGczamJ1Tm5OeWg3d3YwVFUxdWVSV0xWWjRwSGtjMWlJNHJ0aHpzMXBVZ1h6RUtSU3FaMHlJZXdXSktrdWM4MWtiRT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";
                Setting setting = new Setting();
                Auth.Authentication auth = new Auth.Authentication();
                auth.Url = $"{setting["SiteUrl"]}/PublicAPI/System/Authentication.asmx";
            
                //Use Common Account To Create Document
                //This Example is Admin
                string token = auth.GetToken("API",
                    RSAEncrypt(publicKey,"admin"),
                    RSAEncrypt(publicKey,"123456"));

                DMS.Dms dms = new DMS.Dms();
                dms.Url = $"{setting["SiteUrl"]}/PublicAPI/DMS/Dms.asmx";

                string folderId = "06f7624d-cee3-4763-b67c-a567390f8f93";
                string fileGroupId = applyTask.Task.AttachId;

                FileGroup fg = FileCenter.GetFileGroup(fileGroupId);

                //有附件才新增
                if(fg.Count >0)
                {
                    //複製檔案使其脫勾
                   var newFileGroupId= FileCenter.Clone(fileGroupId,
                         Module.DMS, "DMS_SOURCE");
                    FileGroup newfg= FileCenter.GetFileGroup(newFileGroupId);

                    //新增文件

                    dms.AddNewDoc(
                        token,
                        folderId,
                        newfg[0].Name,
                        newfg[0].Name,
                        true,
                        newFileGroupId
                        );

                }

            }

            return "";
        }

         /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="crTexturlparam>
        /// <returns></returns>
        private static string RSAEncrypt(string publicKey, string crText)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            byte[] base64PublicKey = Convert.FromBase64String(publicKey);
            rsa.FromXmlString(System.Text.Encoding.UTF8.GetString(base64PublicKey));

            byte[] ctTextArray = Encoding.UTF8.GetBytes(crText);
            byte[] decodeBs = rsa.Encrypt(ctTextArray, false);

            return Convert.ToBase64String(decodeBs);
        }

        public void OnError(Exception errorException)
        {
            //  throw new NotImplementedException();
        }
    }
}

using System;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace Gate4AzureStorageBlob.Controllers
{
    public class Gate4BlobController : Controller
    {
        public ActionResult Content(string account, string container, string name)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var key = appSettings["AzureBlobAccountKey." + account];
            if (key == null)
                throw new Exception("Unknown account.");

            var credential = new StorageCredentials(
                accountName: account,
                keyValue: key);
            var storageAcount = new CloudStorageAccount(credential, useHttps: true);

            var blobClient = storageAcount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(container);
            var blobRef = blobContainer.GetBlockBlobReference(name);
            using (var sr = blobRef.OpenRead())
            {
                var buff = new byte[sr.Length];
                sr.Read(buff, 0, buff.Length);
                return File(buff, blobRef.Properties.ContentType);
            }
        }
    }
}

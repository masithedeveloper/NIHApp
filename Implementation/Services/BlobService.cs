using System;
using System.Threading.Tasks;
using System.Web;
using NIHApp.Implementation.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using static System.String;

namespace NIHApp.Implementation.Services
{
	public class BlobService : IBlobService
	{
		private readonly IApplicationConfiguration _applicationConfiguration;
		private const string BlobCacheControl = "mage-age=3600, must-revalidate";

		public BlobService(IApplicationConfiguration applicationConfiguration)
		{
			_applicationConfiguration = applicationConfiguration;
		}


		public string UploadFile(string filename, byte[] fileData, string folder, string containerDirectory)
		{
			var destinationBlobName = $"{filename}";
			var connectionString = _applicationConfiguration.GetSetting("storage_connection_string");
			var retryStrategy = new FixedInterval(3, TimeSpan.FromSeconds(1));
			var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);
			try
			{
				retryPolicy.ExecuteAction(
					 () =>
					 {
						 var storageAccount = CloudStorageAccount.Parse(connectionString);
						 var cloudBlobClient = storageAccount.CreateCloudBlobClient();
						 var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerDirectory);
						 cloudBlobContainer.CreateIfNotExists();
						 cloudBlobContainer.SetPermissions(new BlobContainerPermissions
						 {
							 PublicAccess = BlobContainerPublicAccessType.Off
						 });
						 var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(destinationBlobName);
						 cloudBlockBlob.Properties.ContentType = MimeMapping.GetMimeMapping(filename);
						 cloudBlockBlob.Properties.CacheControl = BlobCacheControl;
						 cloudBlockBlob.UploadFromByteArray(fileData, 0, fileData.Length);
						 return filename;
					 });
			}
			catch (Exception exception)
			{
				//_logger.Error(string.Format("Signature Blob storage exception {0}", e.Message), e);
				var e = exception.Message;
				throw;
			}

			return filename;
		}

		public bool DeleteFile(string filename, string personFolder, string containerDirectory)
		{
			//var targetFilename = Path.Combine(@"C:\Development\MediClinic\BlobStorage", personFolder, filename);
			//File.Delete(targetFilename);
			//return true;

			var destinationBlobName = IsNullOrEmpty(personFolder) ? filename : Format("{0}/{1}", personFolder, filename);
			var connectionString = _applicationConfiguration.GetSetting("storage_connection_string");
			var retryStrategy = new FixedInterval(3, TimeSpan.FromSeconds(1));
			var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);
			try
			{
				retryPolicy.ExecuteAction(
				  () =>
				  {
					  var account = CloudStorageAccount.Parse(connectionString);
					  var client = account.CreateCloudBlobClient();
					  var container = client.GetContainerReference(containerDirectory);
					  var blob = container.GetBlockBlobReference(destinationBlobName);
					  blob.Delete();
					  return true;
				  });
			}
			catch (Exception exception)
			{
				//We will catch deletes as it just means it doesnt exist
				var e = exception.Message;
				//_logger.Error(string.Format("Blob storage exception {0}", e.Message), e);
			}
			return false;
		}


        public string GetContainerSasUri(string containerDirectory)
        {
            var connectionString = _applicationConfiguration.GetSetting("storage_connection_string");
            var retryStrategy = new FixedInterval(3, TimeSpan.FromSeconds(1));
            var retryPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(retryStrategy);
            var sasContainerToken = Empty;
            try
            {
                retryPolicy.ExecuteAction(
                     () =>
                     {
                         var storageAccount = CloudStorageAccount.Parse(connectionString);
                         var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                         var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerDirectory);
                         cloudBlobContainer.CreateIfNotExists();
                         cloudBlobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });
                         var adHocPolicy = new SharedAccessBlobPolicy()
                         {
                               
                            SharedAccessExpiryTime = DateTime.UtcNow.AddHours(3),
                            Permissions = SharedAccessBlobPermissions.Read
                         };
                         sasContainerToken = cloudBlobContainer.GetSharedAccessSignature(adHocPolicy, null);
                     });
            }
            catch (Exception exception)
            {
                var e = exception.Message;
                return sasContainerToken;
            }
            return sasContainerToken;
        }
    }
}

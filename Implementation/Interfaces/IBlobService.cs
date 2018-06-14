namespace NIHApp.Implementation.Interfaces
{
	public interface IBlobService
	{
		string UploadFile(string filename, byte[] fileData, string folder, string containerDirectory);
		bool DeleteFile(string filename, string personFolder, string containerDirectory);
	    string GetContainerSasUri(string containerDirectory);
	}
}

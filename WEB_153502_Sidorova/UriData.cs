namespace WEB_153502_Sidorova
{
    public class UriData
    {
        public static string ApiUri { get; set; } = string.Empty;
        public static string ISUri { get; set; } = string.Empty;

        public UriData(string? apiUri, string? iSUri) 
        {
            ApiUri = apiUri;
            ISUri = iSUri;
        }
    }

}

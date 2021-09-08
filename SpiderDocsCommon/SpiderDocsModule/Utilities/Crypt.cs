using System;
using Spider.Security;

namespace SpiderDocsModule
{
	public class Crypt : Spider.Security.Crypt
	{
		static private System.Security.Cryptography.SHA256 Sha256 = System.Security.Cryptography.SHA256.Create();

		public Crypt() : base("!SpiderDocs!")
		{


		}

		public static string GetHashMD5(string filename)
		{
            if ( false == System.IO.File.Exists(filename)) return BytesToString(new byte[] { });

            try
            {

                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    //using (var stream = System.IO.File.OpenRead(filename))
                    //{
                    //    var hash = md5.ComputeHash(stream);

                    //    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                    //}

                    using (System.IO.FileStream stream = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
                    {
                        var hash = md5.ComputeHash(stream);

                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }

                }
            }
            catch
            {
                return BytesToString(new byte[] { });
            }
            //using (System.IO.FileStream stream = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            //{
            //    return BytesToString(Sha256.ComputeHash(stream));
            //}

            //         using (System.IO.FileStream stream = System.IO.File.OpenRead(filename))
            //{
            //	return BytesToString(Sha256.ComputeHash(stream));
            //}
        }

		public static string BytesToString(byte[] bytes)
		{
			string result = "";
			foreach (byte b in bytes) result += b.ToString("x2");
			return result;
		}
	}
}

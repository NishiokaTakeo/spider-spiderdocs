using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Spider.Common;
using SpiderDocsModule;

namespace WebSpiderDocs.Models
{
	// This "Requests" inheritance is just only workaround for reports which cannot access nested objects.
	//[Serializable]
	public class UserViewModel
	{
		//[Required]
		//[Display(Name = "First Name")]
		//public string FirstName { get; set; }

		//[Required]
		//[Display(Name = "Last Name")]
		//public string LastName { get; set; }

		////Required attribute implements validation on Model item that this fields is mandatory for user
		//[Required]
		////We are also implementing Regular expression to check if email is valid like a1@test.com
		//[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
		//public string Email { get; set; }

		[Required]
		[Display(Name = "Login")]
		public string Login { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string PasswordMD5 {
			get
			{
				System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create();
				
				// Convert the input string to a byte array and compute the hash.
				byte[] data = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();

					// Loop through each byte of the hashed data 
					// and format each one as a hexadecimal string.
					for (int i = 0; i < data.Length; i++)
					{
						sBuilder.Append(data[i].ToString("x2"));
					}

					// Return the hexadecimal string.
					return sBuilder.ToString();
			}
		}	
	}
}

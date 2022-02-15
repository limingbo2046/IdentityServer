using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.ObjectExtending;

namespace Lcn.IdentityServer
{
	public class ProfileDto : ExtensibleObject
	{
		/// <summary>
		/// 提供用户ID输出
		/// </summary>
		public Guid Id { get; set; }
		public string UserName { get; set; }

		public string Email { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string PhoneNumber { get; set; }

		public bool PhoneNumberConfirmed { get; set; }
		/// <summary>
		/// 员工编码
		/// </summary>
		public string EmployeNo { get; set; }
	}

}

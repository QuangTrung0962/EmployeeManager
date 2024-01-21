﻿using System.ComponentModel.DataAnnotations;

namespace DTO
{
	public class JobDto
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Tên công việc không được để trống")]
		public string JobName { get; set; }
	}
}

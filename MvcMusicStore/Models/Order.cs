using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcMusicStore.Models.CustomValidationAttribute;

namespace MvcMusicStore.Models
{
	public class Order : IValidatableObject
	{
		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }

		[ScaffoldColumn(false)]
		public string Username { get; set; }

		[Required]
		[StringLength(160)]
		[Display(Name = "First Name", Order = 11000)]
		public string FirstName { get; set; }

		[Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "LastNameRequired")]
		[StringLength(160, ErrorMessage = "Your last name is too long")]
		[MinLength(3, ErrorMessage = "Your {0} is too short")]
		public string LastName { get; set; }

		[Display(Name = "Address 2", Order = 11001)]
		public string Address { get; set; }

		[MaxWordsAttribute(2, ErrorMessage = "Bad")]
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }

		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "HHHHH!!!")]
		public string Email { get; set; }

		//[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c4}")]
		[ReadOnly(true)]
		public decimal Total { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (LastName != null && LastName.Split(' ').Length > 3)
			{
				yield return new ValidationResult($"The {nameof(LastName)} has too many words!", new []{"LastName"});
			}
		}
	}
}
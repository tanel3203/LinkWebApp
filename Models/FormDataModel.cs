
using Microsoft.AspNetCore.Mvc;


namespace Link.Models {
	public class FormDataModel
	{

		public string inputFirstName { get; set; }
        public string inputLastName { get; set; }
        public string inputBirthDate { get; set; }
        public string inputTitle { get; set; }
        public string inputUrl { get; set; }
        public string inputDescription { get; set; }
        public string inputOwnerName { get; set; }
        public string inputCategory { get; set; }
        public string inputPoints { get; set; }


    public FormDataModel (string inputFirstName,
	                string inputLastName,
	                string inputBirthDate,
	                string inputTitle,
	                string inputUrl,
	                string inputDescription,
	                string inputOwnerName,
	                string inputCategory,
	                string inputPoints)
    {
        this.inputFirstName = inputFirstName;
        this.inputLastName = inputLastName;
        this.inputBirthDate = inputBirthDate;
        this.inputTitle = inputTitle;
        this.inputUrl = inputUrl;
        this.inputDescription = inputDescription;
        this.inputOwnerName = inputOwnerName;
        this.inputCategory = inputCategory;
        this.inputPoints = inputPoints;
    }

	}
}



using System;
namespace task1
{
	public class User
	{
		public string fname{set;get;}
		public string lname { set; get; }
		public DateOnly DateOfBirth { set; get; }
		public string email { get; set; }
		public int id { get; set; }
		public User()
		{
		}
        public User( int id,string fname , string lname , DateOnly DateOfBirth, string email)
        {
			this.id = id;
			this.fname = fname;
			this.lname = lname;
			this.DateOfBirth = DateOfBirth;
			this.email = email;
        }
    }
}


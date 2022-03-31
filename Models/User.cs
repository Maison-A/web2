using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace web2.Models
{

	public class User
	{
		public long UID = 0;
		public string FirstName = string.Empty;
		public string LastName = string.Empty;
		public string UserID = string.Empty;
		public string Password = string.Empty;
		public string Email = string.Empty;
		// instantiating the actiontype datatype with no type so it swill allocate the mem
		public ActionTypes ActionType = ActionTypes.NoType;
		public Image UserImage;
		//this is closely related to an array - it's a list of objects
		public List<Image> Images;
		public List<Event> Events = new List<Event>();

		// Determines if user is logged in - a "read only property procedure"
		// ----------------------------------------------------------- //
		// Name:
		// Desc:
		// ----------------------------------------------------------- //
		public bool IsAuthenticated
		{
			// get is what makes read only - this property is critical to determining if logged in
			get
			{
				// once we log in the uid changes from 0
				if (UID > 0) return true;
				return false;
			}
		}



		// ----------------------------------------------------------- //
		// Name: GetEvents
		// Desc:returns list of event objects
		// ----------------------------------------------------------- //
		public List<Event> GetEvents(long ID=0)
        {
            try
            {
				Database db = new Database();
				return db.GetEvents(ID, this.UID);
            }
			catch(Exception ex) { throw new Exception(ex.Message); }
        }

		// enter new chunk of code 
		// ----------------------------------------------------------- //
		// Name:
		// Desc:
		// ----------------------------------------------------------- //
		public sbyte AddGalleryImage(HttpPostedFileBase f)
		{
			try
			{
				this.UserImage = new Image();
				this.UserImage.Primary = false;
				this.UserImage.FileName = Path.GetFileName(f.FileName);

				if (this.UserImage.IsImageFile())
				{
					this.UserImage.Size = f.ContentLength;
					Stream stream = f.InputStream;
					BinaryReader binaryReader = new BinaryReader(stream);
					this.UserImage.ImageData = binaryReader.ReadBytes((int)stream.Length);
					this.UpdatePrimaryImage();
				}
				return 0;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		
		
		
		// ----------------------------------------------------------- //
		// Name:
		// Desc:
		// ----------------------------------------------------------- //
		public sbyte UpdatePrimaryImage()
		{
			try
			{
				Models.Database db = new Database();
				long NewUID;
				if (this.UserImage.ImageID == 0)
				{
					NewUID = db.InsertUserImage(this);
					if (NewUID > 0) UserImage.ImageID = NewUID;
				}
				else
				{
					db.UpdateUserImage(this);
				}
				return 0;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}


		// ----------------------------------------------------------- //
		// Name: 
		// Desc:
		// ----------------------------------------------------------- //
		// a method to return user object  
		public User Login()
		{
			try
			{
				Database db = new Database();
				return db.Login(this);
			}
			// error trap
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		// will return action type
		// ----------------------------------------------------------- //
		// Name:
		// Desc:
		// ----------------------------------------------------------- //
		public User.ActionTypes Save()
		{
			try
			{
				Database db = new Database();
				if (UID == 0)
				{ //insert new user
					this.ActionType = db.InsertUser(this);
				}
				else
				{
					this.ActionType = db.UpdateUser(this);
				}
				return this.ActionType;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		// ----------------------------------------------------------- //
		// Name:
		// Desc:
		// ----------------------------------------------------------- //
		public bool RemoveUserSession()
		{
			try
			{
				HttpContext.Current.Session["CurrentUser"] = null;
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		// ----------------------------------------------------------- //
		// Name:
		// Desc:
		// ----------------------------------------------------------- //
		public User GetUserSession()
		{
			try
			{
				User u = new User();
				if (HttpContext.Current.Session["CurrentUser"] == null)
				{
					return u;
				}
				u = (User)HttpContext.Current.Session["CurrentUser"];
				return u;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		// ----------------------------------------------------------- //
		// Name:
		// Desc:
		// ----------------------------------------------------------- //
		public bool SaveUserSession()
		{
			try
			{
				HttpContext.Current.Session["CurrentUser"] = this;
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		// enum gives a numerical value an English name so we can understand what it means
		// ----------------------------------------------------------- //
		// Name:
		// Desc:
		// ----------------------------------------------------------- //
		public enum ActionTypes
		{
			NoType = 0,
			InsertSuccessful = 1,
			UpdateSuccessful = 2,
			DuplicateEmail = 3,
			DuplicateUserID = 4,
			Unknown = 5,
			RequiredFieldsMissing = 6,
			LoginFailed = 7
		}
	}
}

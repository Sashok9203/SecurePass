using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class ContactVM :  SecureObjectVM
    {
        private string title;
        private string name;
        private string surname;
        private string gender;
        private DateTime birthday;
        private string workPLace;
        private string company;
        private string position;

        public ContactVM() : base(0, -1, 0, "", "", false)
        {
            name = string.Empty;
            surname = string.Empty;
            gender = string.Empty;
            workPLace = string.Empty;
            company = string.Empty;
            position = string.Empty;
        }

        public ContactVM(Contact contact) : base(contact.Id,contact.ImageId,contact.CategoryId,contact.Title,contact.Name, contact.IsFavorit)
        {
            title = contact.Title;
            name = contact.Name;
            surname = contact.Surname;
            gender = contact.Gender;
            birthday = contact.Birthday;
            workPLace = contact.WorkPlace;
            company = contact.Company;
            position = contact.Position;
        }

        public string Title 
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }

        public string BirthdayStr => Birthday.ToShortDateString();

        public DateTime Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BirthdayStr));
            }
        }

        public string WorkPLace
        {
            get => workPLace;
            set
            {
                workPLace = value;
                OnPropertyChanged();
            }
        }

        public string Company
        {
            get => company;
            set
            {
                company = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        public override void CopyToEntity(BaseEntity entity)
        {
            base.CopyToEntity(entity);
            var temp = (Contact)entity;
            temp.Name = name;
            temp.Surname = surname;
            temp.Gender = gender;
            temp.Birthday = birthday;
            temp.WorkPlace = workPLace;
            temp.Company = company;
            temp.Position = position;
        }
    }
}

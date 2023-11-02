using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class CreditCardVM :SecureObjectVM
    {
        private string ownerName;
        private string type;
        private string number;
        private string verificationCode;
        private DateTime validity;
        private DateTime startDate;

        public CreditCardVM() : base(0, -1, 0, "", "", false,21)
        {
            ownerName = string.Empty;
            type = string.Empty;
            number = string.Empty;
            verificationCode = string.Empty;
        }

        public CreditCardVM(CreditCard creditCard) : base(creditCard.Id,creditCard.ImageId,creditCard.CategoryId,creditCard.Title,creditCard.OwnerName, creditCard.IsFavorit,21)
        {
            ownerName = creditCard.OwnerName;
            type = creditCard.Type;
            number = creditCard.Number;
            verificationCode = creditCard.VerificationCode;
            validity = creditCard.Validity;
            startDate = creditCard.StartDate;
        }

        public string OwnerName
        {
            get => ownerName;
            set
            {
                ownerName = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public string Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

        public string VerificationCode
        {
            get => verificationCode;
            set
            {
                verificationCode = value;
                OnPropertyChanged();
            }
        }

        public string ValidityStr => Validity.ToShortDateString();

        public string StartDateStr => StartDate.ToShortDateString();

        public DateTime Validity
        {
            get => validity;
            set
            {
                validity= value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValidityStr));
            }
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StartDateStr));
            }
        }

        public override void CopyToEntity(BaseEntity entity)
        {
            base.CopyToEntity(entity);
            var temp = (CreditCard)entity;
            temp.OwnerName = ownerName;
            temp.Type = type;
            temp.Number = number;
            temp.VerificationCode = verificationCode;
            temp.Validity = validity;
            temp.StartDate = startDate;
        }
    }
}

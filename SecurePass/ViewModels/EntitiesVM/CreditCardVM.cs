using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public CreditCardVM(CreditCard creditCard) : base(creditCard.Id,creditCard.ImageId,creditCard.CategoryId,creditCard.UserId,creditCard.Title,creditCard.OwnerName)
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

        public DateTime Validity
        {
            get => validity;
            set
            {
                validity= value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged();
            }
        }
    }
}

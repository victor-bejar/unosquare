using System;

using App.Model.Interface;

// TODO: Consider to move the min/max validations to an utils class
// in case there is another class may use it
namespace App.Model.Class
{

    public class Product : IProduct
    {

        private const int NAME_MIN_LEN = 1;
        private const int NAME_MAX_LEN = 50;
        private string _name = string.Empty;

        private const int DESCRIPTION_MIN_LEN = 0;
        private const int DESCRIPTION_MAX_LEN = 100;
        private string _description = string.Empty;

        private const int AGE_MIN_VAL = 0;
        private const int AGE_MAX_VAL = 100;
        private int? _ageRestriction = AGE_MAX_VAL;

        private const int COMPANY_MIN_LEN = 1;
        private const int COMPANY_MAX_LEN = 50;
        private string _company = string.Empty;

        private const decimal PRICE_MIN_VAL = 1;
        private const decimal PRICE_MAX_VAL = 1000;
        private decimal _price = AGE_MAX_VAL;


        public int ProductId { get; set; }

        public string Name
        {

            get
            {
                return this._name;
            }

            set
            {

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Name));
                }
                else if (value.Length < NAME_MIN_LEN)
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Name));
                }
                else if (value.Length > NAME_MAX_LEN)
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Name));
                }

                this._name = value;

            }

        }

        public string Description
        {

            get
            {
                return this._description;
            }

            set
            {

                if (value.Length < DESCRIPTION_MIN_LEN)
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Description));
                }
                else if (value.Length > DESCRIPTION_MAX_LEN)
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Description));
                }

                this._description = value;

            }

        }

        public int? AgeRestriction
        {

            get
            {
                return this._ageRestriction;
            }

            set
            {

                if (value != null)
                {

                    if (value < AGE_MIN_VAL)
                    {
                        throw new ArgumentException($"Invalid Value: { value }", nameof(AgeRestriction));
                    }
                    else if (value > AGE_MAX_VAL)
                    {
                        throw new ArgumentException($"Invalid Value: { value }", nameof(AgeRestriction));
                    }

                }

                this._ageRestriction = value;

            }

        }

        public string Company
        {

            get
            {
                return this._company;
            }

            set
            {

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Company));
                }
                else if (value.Length < COMPANY_MIN_LEN)
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Company));
                }
                else if (value.Length > COMPANY_MAX_LEN)
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Company));
                }

                this._company = value;

            }

        }

        public decimal Price
        {

            get
            {
                return this._price;
            }

            set
            {

                if (value < PRICE_MIN_VAL)
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Price));
                }
                else if (value > PRICE_MAX_VAL)
                {
                    throw new ArgumentException($"Invalid Value: { value }", nameof(Price));
                }

                this._price = value;

            }

        }

    }

}

using Common.Attributes;
using Common.Other;
using DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        private IBaseDL<T> _baseDL;
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        public object Get()
        {
            return _baseDL.Get();
        }
        public object GetById(Guid id)
        {
            return _baseDL.GetById(id);
        }
        public object DeleteById(Guid id)
        {
            return _baseDL.DeleteById(id);
        }

        public object Post(T record)
        {
            Validate(record);
            return _baseDL.Post(record);
        }

        public object Put(T record, Guid id)
        {
            Validate(record);
            return _baseDL.Put(record, id);
        }

        /// <summary>
        /// Hàm validate dữ liệu truyền vào
        /// </summary>
        /// <param name="record"></param>
        /// <exception cref="ValidateException"></exception>
        public void Validate(T record)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {

                var propValue = property.GetValue(record);
                var displayNameNotEmpty = String.Empty;
                var displayNameMaxLength = String.Empty;

                // Not Empty
                var propertiesNotEmpty = property.GetCustomAttributes(typeof(NotEmpty), true);
                if (propertiesNotEmpty.Length > 0)
                {
                    displayNameNotEmpty = (propertiesNotEmpty[0] as NotEmpty).Name;
                }

                // Max Length
                var propertiesMaxLength = property.GetCustomAttributes(typeof(MaxLength), true);
                var length = 0;
                if (propertiesMaxLength.Length > 0)
                {
                    displayNameMaxLength = (propertiesMaxLength[0] as MaxLength).Name;
                    length = (propertiesMaxLength[0] as MaxLength).Length;
                }

                // Kiểm tra
                var notEmptyAttribute = (NotEmpty)property.GetCustomAttributes(typeof(NotEmpty), true).FirstOrDefault();
                var maxLengthAttribute = (MaxLength)property.GetCustomAttributes(typeof(MaxLength), true).FirstOrDefault();

                // property phải có attribute NotEmpty thì mới kiểm tra
                if (notEmptyAttribute != null && string.IsNullOrEmpty(propValue.ToString().Trim()))
                {
                    throw new ValidateException(false, $"{displayNameNotEmpty} không để trống");

                }

                // property phải có attribute MaxLength thì mới kiểm tra
                if (propValue != null)
                {
                    if (maxLengthAttribute != null && propValue.ToString().Length >= length)
                    {
                        throw new ValidateException(false, $"{displayNameMaxLength} dài quá {length} kí tự");
                    }
                }
            }
        }
    }
}

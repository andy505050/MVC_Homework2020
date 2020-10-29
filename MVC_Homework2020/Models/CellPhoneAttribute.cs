using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Homework2020.Models
{
    public class CellPhoneAttribute : DataTypeAttribute, IClientValidatable
    {
        public CellPhoneAttribute() : base(DataType.Text)
        {
            ErrorMessage = "手機格式錯誤";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string data = Convert.ToString(value);

            return System.Text.RegularExpressions.Regex.IsMatch(data, @"^\d{4}-\d{6}$");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "cellphoneformaterror"
            };

            yield return rule;
        }
    }
}